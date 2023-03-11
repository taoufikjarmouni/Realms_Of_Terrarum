using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script qui permet de controller le jeu à la fin mais aussi quand le joueur ou l'ennemi tombent
public class GameCleanerController : MonoBehaviour
{
    //Button restart game qui permet de rejouer
    public void RestartGame()
    {
        SceneManager.LoadScene("BeginScene");
    }
    //Button Quit sui sert pour quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }
    //Si le joueur ou l'ennemi tombent, ils meurent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Pour faire cela on appelle la fonction MakeDead() du joueur
            collision.gameObject.GetComponent<PlayerUI>().MakeDead();
        }
        if (collision.tag == "Enemy")
        {
            //Pour faire cela on appelle la fonction DieFall() de l'ennemi
            collision.gameObject.GetComponent<EnemyController>().DieFall();
        }
    }
}
