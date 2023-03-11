using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller les coins
public class CoinController : MonoBehaviour
{
    //On initialise le gameobject reward qui employera le son une fois que la collision a eu lieu
    public GameObject reward;
    //Si le joueur entre en collision avec le coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Alors on détruira le coin, on employera le son et on appellera la fonction AddCoins pour ajouter un coin au joueur
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerInventory>().AddCoins();
            Instantiate(reward, transform.position, Quaternion.identity);
        }
    }
}
