/*
     DESCRIPTION :

     Sert à savoir et/ou dire à d'autres scripts si un objet a été utilisé / attrapé

*/


using UnityEngine;
using VRTK;
using System.Collections;


public class Interactable_Object_Extension : MonoBehaviour
{

    bool isGrabbed = false;
    bool isUsed = false;
    void Start()

    {
            
        //make sure the object has the VRTK script attached... 
        if (GetComponent<VRTK_InteractableObject>() == null)

        {

            Debug.LogError("Interactable_Object_Extension is required to be attached to an Object that has the VRTK_InteractableObject script attached to it");

            return;

        }               


        //subscribe to the event.  NOTE: the "ObectGrabbed"  this is the procedure to invoke if this objectis grabbed.. 
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);


        //subscribe to the event.  NOTE: the "ObectGrabbed"  this is the procedure to invoke if this objectis grabbed.. 
        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(ObjectUsed);
                          
    }
    
    

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)

    {

        Debug.Log(this+" vient d'être Grabbed");
        isGrabbed = true;

    }

    private void ObjectUsed(object sender, InteractableObjectEventArgs e)

    {

        Debug.Log(this+" vient d'être Used");
        isUsed = true;

    }

    public bool ValueIsGrabbed()
    {
        return isGrabbed;
    }
    public bool ValueIsUsed()
    {
        return isUsed;
    }
}