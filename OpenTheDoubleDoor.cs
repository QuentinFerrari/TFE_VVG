using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables.PhysicsBased;

public class OpenTheDoubleDoor : MonoBehaviour
{

    public GameObject porteDroite;
    public GameObject porteGauche;
    public int ouvertState =0;  //fermé 

    public List<string> listTags;

    // Use this for initialization
   
    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < listTags.Count; i++)
        {
            if (col.gameObject.tag == listTags[i] && ouvertState == 0)
            {
                ouvertState = 1; //ouverture
                porteDroite.GetComponent<VRTK_PhysicsRotator>().angleTarget = -110;
                porteGauche.GetComponent<VRTK_PhysicsRotator>().angleTarget = -80; 
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < listTags.Count; i++)
        {
            if (col.gameObject.tag == listTags[i] && ouvertState == 1)
            {
                ouvertState = 0;
                porteGauche.GetComponent<VRTK_PhysicsRotator>().angleTarget = -180;
                porteDroite.GetComponent<VRTK_PhysicsRotator>().angleTarget = 0;
            }
        }
    }
}

