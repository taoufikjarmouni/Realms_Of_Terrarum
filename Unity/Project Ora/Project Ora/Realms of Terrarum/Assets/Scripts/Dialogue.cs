using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script qui sert pour controller le dialogue mais aussi le debut du jeu
public class Dialogue : MonoBehaviour
{
    //On prend comme parametres le texte qui contiendra les phrases et sa vitesse d'affichage
    public Text textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    //On prend aussi le bouton, la boite de dialogue et tous les autres gameobjects qui ne s'afficheront pas au debut du jeu
    public GameObject continueButton;

    public GameObject DialogueBox;

    public GameObject Allgameobjects;

    //Cette couroutine affichera lettre par lettre les mots composant les phrases
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //On commence la couroutine au debut
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        //Au debut de chaque phrase on ne verra pas le bouton
        continueButton.SetActive(false);
        //Si on a encore une phrase a afficher, on commencera avec une phrase vide qui sera completee au fur et a mesure
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        //Sinon c'est la fin, on leve la boite de dialogue, avec le bouton et on commence le jeu
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            Allgameobjects.SetActive(true);
            DialogueBox.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //A chaque frame on regarde si on a affiche completement la phrase, si oui on met un bouton qui permet d'avancer a la phrase prochaine
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
}
