using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script qui servira pour controller l'inventaire du joueur
public class PlayerInventory : MonoBehaviour
{
    //On prend les textes de l'UI: le nombre des coins, des cristaux et le niveau
    public Text NumberOfCoinsText;
    public Text NumberOfCrystalsText;
    public Text TextLevelNumber;
    //On prend aussi l'image de vie
    public Image HurtImage;
    public int Coins = 0;
    public int Crystals = 0;
    public int LevelNumber = 0;
    public int Life = 10;
    public float damocles_position;
    void Start()
    {
        //On transforme au debut le coins, cristaux et niveau en texte
        NumberOfCoinsText.text = Coins.ToString();
        NumberOfCrystalsText.text = Crystals.ToString();
        TextLevelNumber.text = LevelNumber.ToString();
    }
    private void Update()
    {
        //On regarde la position du joueur, s'il arrive à la fin, il gagne
        damocles_position = transform.position.x;
        if(damocles_position >= 159)
        {
            GetComponent<PlayerUI>().EndGame();
        }
    }
    //Fonction pour ajouter des coins quand le joueur entre en contact avec des coins
    public void AddCoins()
    {
        Coins++;
        NumberOfCoinsText.text = Coins.ToString();
    }
    //Fonction pour ajouter des cristaux quand le joueur entre en contact avec des cristaux
    public void AddCrystals()
    {
        Crystals++;
        NumberOfCrystalsText.text = Crystals.ToString();
        //On ajoute aussi le niveau, car niveau = cristaux
        AddLevelNumber();
    }
    //Fonction pour ajouter des niveaux quand le joueur entre en contact avec des cristaux
    public void AddLevelNumber()
    {
        LevelNumber = Crystals;
        TextLevelNumber.text = LevelNumber.ToString();
    }
    //Fonction pour enlever de vie quand le joueur prends des degats
    public void LessLife()
    {
        //Il perd sa vie et on l'affiche sur l'ecran (la barre de vie)
        Life--;
        HurtImage.fillAmount -= 0.1f;
        //Si sa vie est égale à 0, le joueur perd
        if (Life == 0)
        {
            gameObject.GetComponent<PlayerUI>().MakeDead();
            Destroy(gameObject);
        }
    }
}
