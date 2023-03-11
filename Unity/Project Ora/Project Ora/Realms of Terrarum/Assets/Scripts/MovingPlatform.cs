using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour controller la plateforme qui se deplace à gauche et droite
public class MovingPlatform : MonoBehaviour
{
    //On declare la variable qui contiendra la vitesse de deplacement
    float  moveSpeed = 1f;
    //Boolean qui informe le programme au cas ou la plateforme se deplace vers gauche ou droite
    bool moveRight = true;

    // Update is called once per frame
    void Update()
    {
        //Si la plateforme a atteint une position superieure a 15.84f sur l'axe x, alors elle s'arretera et commencera a se deplacer vers gauche
        if(transform.position.x > 15.84f)
        {
            moveRight = false;
        }
        //Sinon elle se deplacera vers droite
        if (transform.position.x < 15f)
        {
            moveRight = true;
        }
        //Si la plateforme doit se deplacer vers droite, elle se deplacera avec une certaine vitesse
        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        //Sinon vers gauche
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

    }
}
