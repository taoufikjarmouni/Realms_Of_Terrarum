using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script qui sert pour controller le timer et executer des fonctions par rapport à ce dernier
public class Countdown : MonoBehaviour
{
    //On cree une variable de type Text liée au texte dans l'arborescence UI du jeu
    public Text counterText;
    //On part avec un timer de 67 secondes (parce que la musique choisie dure 1 minute et 6 secondes)
    float timeLeft = 67.0f;
    //On ajoute aussi le gameobject count qui au debut est invisible, ce dernier est lié au texte "COUNTDOWN"
    public GameObject count;
    void Start()
    {
        //Au debut on defini countertexte
        counterText = GetComponent<Text>() as Text;
    }
    void Update()
    {
        //A chaque frame, on lève un second reel à timeleft
        timeLeft -= Time.deltaTime;
        //On trasnforme ce temps en string pour l'afficher dans l'UI
        counterText.text = timeLeft.ToString("00");
        //On affiche le texte COUNTDOWN pour 2 secondes, après on l'efface
        if(timeLeft <= 65.0f)
        {
            count.SetActive(false);
        }
        //Si le timer est arrivé à 0 secondes, le joueur perds, du coup on appelle la fonction MakeDead()
        if(timeLeft < 1.0f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().MakeDead();
        }
    }
}
