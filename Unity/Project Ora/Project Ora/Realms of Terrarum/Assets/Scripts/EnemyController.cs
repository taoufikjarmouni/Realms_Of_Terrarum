using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//Script qui sert pour controller l'ennemi
public class EnemyController : MonoBehaviour
{
    //On declare l'animator, qui servira pour animer l'ennemi
    public Animator enemyAnim;
    //On donne 100 HP à l'ennemi
    public int maxHealth = 100;
    //Une variable qui contiendra le vie courante de l'ennemi
    int currentHealth;
    //Variable qui contiendra le son une fois que l'ennemi a touché le joueur
    public GameObject damage;
    //On prend les valeurs des positions de l'ennemi
    public Transform enemy;
    //Cette variable appelle le script AI Path dans le gameobject Enemy
    public AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        //Au debut, la vie courante est égale à la vie MAX
        currentHealth = maxHealth;
    }

    //Cette fonction est appellé par le PlayerController une fois que le joueur a attaqué l'ennemi
    public void TakeDamage(int damage)
    {
        //Il prend des dégats égaux au damage du joueur
        currentHealth -= damage;
        //S'il a 0 ou moins de HP, il meurt
        if(currentHealth<=0)
        {
            Die();
        }
    }
    //Fonction qu'on appelle quand l'ennemi meurt
    public void Die()
    {
        //On employe l'animation de mort
        enemyAnim.SetBool("IsDead", true);
        //On desactive le collider, comme ça le joueur pourra se déplacer sur l'ennemi
        GetComponent<Collider2D>().enabled = false;
        //L'ennemi n'aura plus son parent 'Enemy' qui contient le script pathfinding (ce dernier force l'ennemi a se deplacer même s'il est mort)
        this.transform.parent = null;
        //On le desactive
        this.enabled = false;
    }
    //On appelle cette fonction si le joueur a poussé l'ennemi hors la map
    public void DieFall()
    {
        //Il est détruit
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si le joueur entre en collision avec l'ennemi, le joueur prend des dégâts, on entend le son, et le joueur et repoussé
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInventory>().LessLife();
            Instantiate(damage, transform.position, Quaternion.identity);
            print("Vous avez perdu 1 vie");
            var magnitude = 250;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
        }
    }
    private void FixedUpdate()
    {
        //On regarde à chaque fois si l'ennemi est en train de se deplacer vers droite ou gauche, si oui, il se tournera vers la direction correcte, animé
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            enemy.localScale = new Vector3(-1f, 1f, 1f);
            enemyAnim.SetBool("isRunning", true);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            enemy.localScale = new Vector3(1f, 1f, 1f);
            enemyAnim.SetBool("isRunning", true);
        }
    }
}
