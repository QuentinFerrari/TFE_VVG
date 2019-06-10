    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LitHopitalUsed : MonoBehaviour {

    bool isUsed = false;
    public GameObject objetATourner;
    public GameObject controllerDroit;
    public GameObject controllerGauche;
    private Interactable_Object_Extension script;

    Vector3 vector3; 
    public void Rotate90Degree()
    {
        //Vector3 point = objetATourner.gameObject.transform.GetChild(0).GetChild(0).transform.position;

        //Vector3 axe = new Vector3(0, 1, 0);

        objetATourner.transform.gameObject.SetActive(false);

        objetATourner.transform.RotateAround(vector3, 0.15f);

        objetATourner.transform.gameObject.SetActive(true);



    }

    void Start()

    {
        vector3.Set(0,transform.localPosition.y,0);

        //make sure the object has the VRTK script attached... 
        if (GetComponent<VRTK_InteractableObject>() == null)

        {

            Debug.LogError("Interactable_Object_Extension is required to be attached to an Object that has the VRTK_InteractableObject script attached to it");

            return;

        }
                      
        //subscribe to the event.  NOTE: the "ObectGrabbed"  this is the procedure to invoke if this objectis grabbed.. 
        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(ObjectUsed);

        script = gameObject.GetComponent<Interactable_Object_Extension>();

    }

    private void Update()
    {
        if (controllerDroit.GetComponent<VRTK_ControllerEvents>().triggerPressed == true && script.ValueIsGrabbed() == true)
        {
            Rotate90Degree();
        }
    }



    private void ObjectUsed(object sender, InteractableObjectEventArgs e)

    {
        Rotate90Degree();
        Debug.Log(this + " vient d'être Used");
        isUsed = true;

    }

   
    public bool ValueIsUsed()
    {
        return isUsed;
    }
}


