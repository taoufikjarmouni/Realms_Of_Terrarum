using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui servira pour controller les boites qui tombent du ciel
public class FallingDamage : MonoBehaviour
{
    //On lui donnera un timer de 10 secondes, il tombera chaque 10s
    public float Timer = 10;
    //Le joueur
    public GameObject damocles;
    //La position du joueur
    float damocles_position;
    //Le son du crash
    public GameObject damage;
    void Update()
    {
        //A chaque fois on leve 1 du timer et une fois qu'il a atteint 0 ou moins, on trouve la position du joueur et on spawn une boite qui tombera sur lui
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            damocles = GameObject.Find("Damocles");
            damocles_position = damocles.transform.position.x;
            Instantiate(Resources.Load("Block1"), new Vector3(damocles_position, 2, 0), Quaternion.identity);
            //On la detruira apres une seconde pour ne pas polluer la carte avec ces boites
            Destroy(gameObject, 1f);
            //On reinitialise le timer à 5s
            Timer = 5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la boite entre en collision avec le terrain, elle se fermera sur contact, et on entendra le son du crash, on pourra passer à travers de l'objet
        if (collision.tag == "Ground")
        {
            Instantiate(damage, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Collider2D>().enabled = false;
        }
        //Si la boite entre en collision avec le joueur, elle fera des degats à ce dernier, on entendra le son et le joueur sera repoussé. Enfin la boite sera detruite sur contact
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInventory>().LessLife();
            Instantiate(damage, transform.position, Quaternion.identity);
            print("Vous avez perdu 1 vie");
            var magnitude = 400;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            Destroy(gameObject, 1f);
        }
    }
}
