/*
     DESCRIPTION :

     Sert à envoyer l'information à un autre script (en général le GameEventsManager) qu'un objet/Player (gameObject avec le bon Tag) est entré/ sorti d'une boîte de collision déterminée

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionBox : MonoBehaviour {

    public bool hasAlreadyEnteredPlayer = false;
    public bool hasAlreadyEnteredObject = false;

    public bool hasLeftPlayer = false;
    public bool hasLeftObject = false;

    public bool hasEnteredPlayer = false;
    public bool hasEnteredObject = false;

    public bool hasEnteredChaiseRoulante = false;
    public bool hasEnteredLitHopital = false;

    public bool hasEnteredNPC = false;

    public string NPCTag;


    //s'enclenche lorsqu'un collider est entré/sorti
    private void OnTriggerEnter(Collider col)
    {
        if (hasAlreadyEnteredPlayer == false && col.gameObject.tag == "Player") 
        {
            hasAlreadyEnteredPlayer = true;
            //Debug.Log("Le joueur est bien entré dans le collider" +this.gameObject);
        }
        if (hasAlreadyEnteredObject == false && col.gameObject.tag == "ObjetADeplacer") 
        {
            hasAlreadyEnteredObject = true;
            //Debug.Log("L'objet vient d' entrer dans le collider" +this.gameObject);
        }
        if (hasEnteredPlayer == false && col.gameObject.tag == "Player")
        {
            hasEnteredPlayer = true;
            //Debug.Log("Le joueur vietn d'entrer dans le collider" + this.gameObject);
        }
        if (hasEnteredObject == false && col.gameObject.tag == "ObjetADeplacer")
        {
            hasEnteredObject = true;
            //Debug.Log("L'objet est bien entré dans le collider" + this.gameObject);
        }
        if (hasEnteredChaiseRoulante == false && col.gameObject.tag == "ChaiseRoulante")
        {
            hasEnteredChaiseRoulante = true;
            //Debug.Log("La Chaise Roulante est bien entrée dans le collider" + this.gameObject);
        }
        if (hasEnteredLitHopital == false && col.gameObject.tag == "LitHopital")
        {
            hasEnteredLitHopital = true;
            //Debug.Log("Le lit d'hopital est bien entré dans le collider" + this.gameObject);
        }
        if (hasEnteredNPC == false && col.gameObject.tag == NPCTag)
        {
            hasEnteredNPC = true;
            //Debug.Log("Le NPC est bien entré dans le collider" +this.gameObject);
        }

    }   //entré
    private void OnTriggerExit(Collider col)
    {
        if (hasLeftPlayer == false && col.gameObject.tag == "Player")
        {
            hasLeftPlayer = true;
            //Debug.Log("Le joueur est bien sorti du collider " +this.gameObject);
        }
        if (hasLeftObject == false && col.gameObject.tag == "ObjetADeplacer")
        {
            hasLeftObject = true;
            //Debug.Log("L'objet est bien sorti du collider" +this.gameObject);
        }

        if (hasEnteredPlayer == true && col.gameObject.tag == "Player")
        {
            hasEnteredPlayer = false;
            //Debug.Log("Le joueur est bien entré dans le collider" + this.gameObject);
        }
        if (hasEnteredObject == true && col.gameObject.tag == "ObjetADeplacer")
        {
            hasEnteredObject = false;
            //Debug.Log("L'objet est bien entré dans le collider" + this.gameObject);
        }
    }   //sorti





    //pour renvoyer la valeur vers un autre script
    public bool ValueHasAlreadyEnteredPlayer()
    {
        return hasAlreadyEnteredPlayer;
    }   
    public bool ValueHasAlreadyEnteredObject()
    {
        return hasAlreadyEnteredObject;
    }
    public bool ValueHasLeftPlayer()
    {
        return hasLeftPlayer;
    }
    public bool ValueHasLeftObject()
    {
        return hasLeftObject;
    }
    public bool ValueHasEnteredPlayer()
    {
        return hasEnteredPlayer;
    }
    public bool ValueHasEnteredObject()
    {
        return hasEnteredObject;
    }
    public bool ValueHasEnteredChaiseRoulante()
    {
        return hasEnteredChaiseRoulante;
    }
    public bool ValueHasEnteredLitHopital()
    {
        return hasEnteredLitHopital;
    }
    public bool ValueHasEnteredNPC()
    {
        return hasEnteredNPC;
    }



    //s'il est nécessaire de reset l'état d'un bool en cours de partie
    private void Reset(string reset)
    {
        switch (reset)
        {
            case "hasAlreadyEnteredPlayer" :
                hasAlreadyEnteredPlayer = false;
                break;
            case "hasAlreadyEnteredObject":
                hasAlreadyEnteredObject = false;
                break;
            case "hasLeftPlayer":
                hasLeftPlayer = false;
                break;
            case "hasLeftObject":
                hasLeftObject = false;
                break;

        }
    }
}
