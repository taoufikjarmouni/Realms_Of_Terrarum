using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller les plateformes qui tombent une fois entres en contact avec le joueur
public class FallingPlatform : MonoBehaviour
{
    //On prend le rigidbody du terrain
    Rigidbody2D rb;
    //Et le son quand ils tombent
    public GameObject damage;
    // Start is called before the first frame update
    void Start()
    {
        //On lui donne son rigibody
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Si le terrain entre en contact avec le joueur, ils tombent sans aucun delai, on entend le son et ils seront detruits apres 1 seconde
        if (col.gameObject.name.Equals("Damocles"))
        {
            Invoke("DropPlatform", 0f);
            Instantiate(damage, transform.position, Quaternion.identity);
            Destroy(gameObject, 1f);
        }
    }
    void DropPlatform()
    {
        //Le terrain est kinematic pour permettre au joueur d'entrer en contact avec eux avant de tomber, sinon ils tombent du debut du jeu tout seuls
        rb.isKinematic = false;
    }
}
