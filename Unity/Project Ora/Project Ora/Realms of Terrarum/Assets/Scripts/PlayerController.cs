using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller tout ce qui comporte le mouvement, l'attaque et les sauts du joueur
public class PlayerController : MonoBehaviour
{
    //Partie qui comprend le deplacement du joueur et le flip vers gauche ou droite par rapport à l'axe
    Rigidbody2D playerRB;
    SpriteRenderer playerRenderer;
    Animator playerAnim;
    bool facingRight = true;
    public float maxSpeed;

    //Partie qui comprend le saut et executer du code si le joueur ne touche plus le sol
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower;
    bool canMove = true;

    //Partie qui comprend l'attaque, les degats et la portee
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 10;
    public float attackRate = 10f;
    float nextAttackTime = 0f;

    //Partie qui comprend les interactions avec l'échelle
    public float distance;
    public LayerMask Ladder;
    private bool isClimbing;

    //Le niveau du joueur qui nous servira pour amplifier les degats
    public int LevelNumber;
    

    void Start()
    {
        //On prend les composants du joueur
        playerRB = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }
    void Update()
    {
        
        //On prend le niveau du joueur de PlayerInventory
        LevelNumber = GetComponent<PlayerInventory>().LevelNumber;
        //Et on augmente les degats proportionnellement
        attackDamage = 10 + LevelNumber * 10;
        //Si le joueur touche le sol et il saut
        if (grounded && Input.GetAxis("Fire2") > 0 && canMove)
        {
            //Alors on aura l'animation du saut, avec sa vitesse definie par velocity et addforce
            playerAnim.SetBool("IsGrounded", false);
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            //Il ne touchera plus le sol
            grounded = false;
        }
        //Si on appuie sur la touche pour attaquer, le joueur attaquera
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        //On defini ce que on considère comme grounded (quand le joueur touche le sol)
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        playerAnim.SetBool("IsGrounded", grounded);
        //Move prend la valeur du deplacement vers gauche ou droite
        float move = Input.GetAxis("Horizontal");
        //Si on peut se deplacer, le joueur se deplacera vers la direction choisie et on definira sa vitesse
        if (canMove)
        {
            playerRB.velocity = new Vector2(move * maxSpeed, playerRB.velocity.y);
            playerAnim.SetFloat("MoveSpeed", Mathf.Abs(move));
            //Du coup s'il se deplace vers droite avec un move < 0, il se tournera vert droite
            if (move < 0 && facingRight == true)
            {
                Flip();
            }
            //Sinon il se tournera vers gauche avec un move > 0
            if (move > 0 && facingRight == false)
            {
                Flip();
            }
        }
    }

    void FixedUpdate()
    {
        //On defini le hitinfo pour informer le programme quand le jouer est en train de se deplacer sur l'echelle
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, Ladder);
        //Si le joueur touche l'echelle et appuie sur la fleche directionelle vers haut, il commencera a se deplacer vers le haut
        if(hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
        }
        //Sinon il arretera de se deplacer vers l'axe verticale
        else
        {
            isClimbing = false;
        }
        //S'il est en train de monter l'echelle, le joueur pourra se deplacer vers le haut ou le bas avec sa animation et la vitesse chosie
        if (isClimbing == true)
        {
            float inputVertical = Input.GetAxisRaw("Vertical");
            playerAnim.SetTrigger("IsClimbing");
            playerRB.velocity = new Vector2(playerRB.velocity.x, inputVertical * maxSpeed);
            playerRB.gravityScale = 0;
        }
        //Sinon il tombera
        else
        {
            playerRB.gravityScale = 1;
        }
    }
    //Si on est en train d'attaquer, on jouera l'animation d'attaque, si le joueur touche un ennemi vers sa gauche ou droite, l'ennemi prendera des degats
    void Attack()
    {
        playerAnim.SetTrigger("IsAttacking");
        Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
        Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemiesLeft)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
        foreach (Collider2D enemy in hitEnemiesRight)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }
    //On dessin un rond au tour des attackpoint pour mieux les agrandir et deplacer selon sa position
    void OnDrawGizmosSelected()
    {
        if (attackPointLeft == null)
            return;
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        if (attackPointRight == null)
            return;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
    }
    //Le joueur tournera vers la face opposée
    void Flip()
    {
        facingRight = !facingRight;
        playerRenderer.flipX = !playerRenderer.flipX;
    }
    //Le joueur peut se deplacer
    public void toggleCanMove()
    {
        canMove = !canMove;
    }
    //Si le joueur entre en collision avec la plateforme, il deviendra le fils de ce dernier et le suivra
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("MovingPlatform"))
            this.transform.parent = col.transform;
    }
    //Si le joueur laisse la plateforme, tout reviendra comme avant
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("MovingPlatform"))
            this.transform.parent = null;
    }
}
