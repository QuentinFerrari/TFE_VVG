using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjetsEncombrants : MonoBehaviour
{

    public GameObject[] objetsADeplacer;

    //[HideInInspector]
    public List<bool> isInColliderObject;
    //[HideInInspector]
    public List<bool> hasLeftObject;
    //[HideInInspector]
    public List<bool> hasEnteredObject;

    //public List<bool> hasBeenSnapDropped;
    public GameObject[] snapDropZones;


    private void Start()
    {

        for (int i = 0; i < objetsADeplacer.Length; i++)
        {
            hasLeftObject.Add(false);
            isInColliderObject.Add(false);
            hasEnteredObject.Add(false);
        }
    }



    //s'enclenche lorsqu'un collider est entré/sorti
    private void OnTriggerEnter(Collider col)
    {

        for (int i = 0; i < objetsADeplacer.Length; i++)
        {
            if (isInColliderObject[i] == false && col.gameObject == objetsADeplacer[i])
            {
                isInColliderObject[i] = true;
                //Debug.Log("L'objet vient d' entrer dans le collider" + this.gameObject);
            }

            if (hasEnteredObject[i] == false && col.gameObject == objetsADeplacer[i])
            {
                hasEnteredObject[i] = true;
                hasLeftObject[i] = false;
                //Debug.Log("L'objet est déjà entré une fois dans le collider" + this.gameObject);
            }
            
        }



    }   //entré
    private void OnTriggerExit(Collider col)
    {

        for (int i = 0; i < objetsADeplacer.Length; i++)
        {
            if (hasLeftObject[i] == false && col.gameObject == objetsADeplacer[i])
            {
                hasLeftObject[i] = true;
                hasEnteredObject[i] = false;
                //Debug.Log("L'objet est bien sorti du collider" + this.gameObject);
            }

        }



    }
}
    


    


      


