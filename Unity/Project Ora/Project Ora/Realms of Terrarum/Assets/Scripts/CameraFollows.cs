using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui sert pour définir la position et la vitesse de MainCamera, cela est smooth et suis le joueur au début mais après avoir atteint la position 69 sur l'axe x, il se déplace tout seul vers droite
public class CameraFollows : MonoBehaviour
{
    //On déclare la variable target qui veut dire le gameobject que la camera suivra
    public Transform target;
    //On déclare la variable smoothing qui decidera la qualité du déplacement de MainCamera
    public float smoothing = 5f;
    //On déclare la variable qui contiendra la position de MainCamera
    Vector3 offset;
    //On déclare le booléan qui executera des actions si le joueur a atteint une certaine position
    public bool isGoingRight = false;
    //On prend les bords de la camera
    public Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        //Au debut on défini offset et screenbounds avec leur positions
        offset = transform.position - target.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //A chaque frame on regarde si la camera est en train d'aller vers droite toute seule (ça arrive vers la fin), au début c'est faux
        if(isGoingRight == false)
        {
            //Alors la camera se deplacera avec le joueur
            Vector3 targetCamPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);
        }
        //Si on a atteint la position choisi
        if(target.position.x >= 69)
        {    
            //La camera commencera à aller vers droite
            isGoingRight = true;
        }
        if(isGoingRight == true)
        {
            //Si la camera a atteint la position choisi, elle sortira de son gameObject parent (s'il y a un) et elle se déplacera vers droite toute seule grâce à un vecteur
            this.transform.parent = null;
            transform.Translate(Vector3.right * 1.4f * Time.deltaTime);
        }
    }
}
