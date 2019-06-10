using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(VRTK_InteractableObject))]
[RequireComponent(typeof(VRTK_FixedJointGrabAttach))]
[RequireComponent(typeof(VRTK_SwapControllerGrabAction))]
[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
[RequireComponent(typeof(VRTK_InteractObjectAppearance))]



public class Interactable_Object_Script : MonoBehaviour {
    private void Start()
    {
        this.gameObject.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
        this.gameObject.GetComponent<VRTK_InteractableObject>().isUsable = true;
        this.gameObject.GetComponent<VRTK_InteractableObject>().grabAttachMechanicScript = this.gameObject.GetComponent<VRTK_FixedJointGrabAttach>();
        this.gameObject.GetComponent<VRTK_InteractableObject>().secondaryGrabActionScript = this.gameObject.GetComponent<VRTK_SwapControllerGrabAction>();
        this.gameObject.GetComponent<VRTK_FixedJointGrabAttach>().precisionGrab = true;
        this.gameObject.GetComponent<VRTK_InteractObjectHighlighter>().touchHighlight = Color.green;
        this.gameObject.GetComponent<VRTK_InteractObjectHighlighter>().grabHighlight = Color.green;
        this.gameObject.GetComponent<VRTK_InteractObjectHighlighter>().objectToHighlight = this.gameObject;

        if(this.gameObject.GetComponent<MeshCollider>() != null)
        {
            this.gameObject.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
        
        
        

    }
}
