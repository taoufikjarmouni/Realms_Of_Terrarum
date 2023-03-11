using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller les obstacles
public class ObstacleController : MonoBehaviour
{
    //Le son des degats
    public GameObject damage;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si l'obstacle entre en collision avec le joueur, ce dernier prendra des degats, il sera repousse, et on entendra le son des degats
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInventory>().LessLife();
            Instantiate(damage, transform.position, Quaternion.identity);
            print("Vous avez perdu 1 vie");
            var magnitude = 250;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
        }
        //Si c'est l'ennemi qui entre en contact avec l'obstacle, ce dernier sera detruit
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
