using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script qui sert pour activer les autres gameobjects liés au countdown at le boss
public class EnemySpawner : MonoBehaviour
{
    //On prend comme parametres l'ennemi boss, la musique, le son des degats, l'animator et le countdown
    float damocles_position;
    public GameObject commodus;
    public GameObject time;
    public GameObject maintheme;
    public GameObject damage;
    public Animator enemyAnim;
    // Update is called once per frame
    void Update()
    {
        //A chaque frame on donne a damocles_position la position du joueur
        damocles_position = GameObject.Find("Damocles").transform.position.x;
        if (damocles_position >= 69f)
        {
            //Si la position du joueur depasse 70.1 (la meilleure valeur possible pour spawner l'ennemi dans l'ecran du joueur)
            //On desactive la musique du jeu et on active l'autre musique pour le final boss battle, on active aussi le boss et le countdown
            GameObject.FindGameObjectWithTag("MainTheme").GetComponent<AudioSource>().Stop();
            maintheme.SetActive(true);
            commodus.SetActive(true);
            time.SetActive(true);
            //L'ennemi se deplacera avec la cemra, meme vitesse, il se trouvera sur le bord gauche
            commodus.transform.Translate(Vector3.right * 1.4f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Si l'ennemi entre en collision avec le joueur, ce dernier prendra des degats, on entendra le son, et le joueur sera repousse
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
        //Si il est en train de se deplacer vers droite, il aura l'animation de course
        float move = 1;
        if (move > 0)
        {
            if(enemyAnim)
            {
                enemyAnim.SetBool("isRunning", true);
            }
        }
    }
}
