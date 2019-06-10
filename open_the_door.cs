using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables.PhysicsBased;


public class open_the_door : MonoBehaviour {


    public GameObject porte;
    private bool ouvert;
    public int angleVoulu =170;
    public List<string> listTags;

    // Use this for initialization

    private void Start()
    {
        ouvert = false;
    }
    void Ouvrir(int i)
    {
        porte.GetComponent<VRTK_PhysicsRotator>().angleTarget = i;
        
    }
    void Fermer()
    {
        porte.GetComponent<VRTK_PhysicsRotator>().angleTarget = 0;        
    }

    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < listTags.Count; i++)
        {
            if (col.gameObject.tag == listTags[i] && ouvert == false)
            {
                ouvert = true;
                Ouvrir(angleVoulu); 
            }
        }
        
    }
    /*
    private void OnTriggerStay(Collider col)
    {
        ouvert = true;        
    }
    */

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "NINPC" && ouvert == true)
        {
            ouvert = false;
            Fermer();

        }
    }
    // Update is called once per frame
      
}
