using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller les cristaux
public class CrystalController : MonoBehaviour
{
    //On declare la variable qui contiendra le son que le programme employera
    public GameObject reward;
    //Une fois que le cristal est entré en collision avec le joueur, il se détruit, on éntend le son et le joueur gagnera un cristal (Et du coup un niveau)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerInventory>().AddCrystals();
            Instantiate(reward, transform.position, Quaternion.identity);
        }
    }
}
