using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script qui sert pour controller l'UI une fois qu'on a gagné ou perdu
public class PlayerUI : MonoBehaviour
{
    //On ajoute le canvas qui apparaitra à la fin, avec le text de fin (gagner ou perdre)
    public CanvasGroup endGC;
    public Text endGameUIText;
    public string endText = "Vous avez gagné!";
    public string endTextLoose = "Vous avez perdu!";
    //Le son à la fin
    public GameObject end;
    public void EndGame()
    {
        //Texte: gagné
        endGameUIText.text = endText;
        //On affiche le canvas
        endGC.alpha = 1;
        //On arrete la musique si on est encore au debut
        GameObject.FindGameObjectWithTag("MainTheme").GetComponent<AudioSource>().Stop();
        //Si on a perdu à la fin, on arrete la musique finale du boss
        if(GameObject.FindGameObjectWithTag("MainBossTheme"))
        {
            GameObject.FindGameObjectWithTag("MainBossTheme").GetComponent<AudioSource>().Stop();
        }
    }
    public void MakeDead()
    {
        EndGame();
        //Texte: perdu
        endGameUIText.text = endTextLoose;
        //On leve la cemra du parent au cas ou pour ne pas afficher des problemes liees a la camera
        Camera.main.transform.parent = null;
        Destroy(gameObject);
        //On employe le son
        Instantiate(end, transform.position, Quaternion.identity);
    }
}
