using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class ExtinguisherExtension : MonoBehaviour {

    private GameObject extincteur;
    public GameObject extincteurSnapDropZone;
    private bool whenIsGrabbed = false;

        private void Start()
    {
        extincteur = this.gameObject;
        extincteurSnapDropZone.GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);
    }

    private void Update()
    {
        whenIsGrabbed = extincteur.GetComponent<Interactable_Object_Extension>().ValueIsGrabbed();

        if (whenIsGrabbed == true)
        {
            extincteurSnapDropZone.GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive = true;
        }
       
    }

    private void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        extincteurSnapDropZone.GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive = false;    
    }
}
