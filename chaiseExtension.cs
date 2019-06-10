using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class chaiseExtension : MonoBehaviour
{

    private GameObject chaise;
    public GameObject[] chaiseSnapDropZones;
    private bool whenIsGrabbedChaise = false;

    private void Start()
    {
        chaise = this.gameObject;

        for (int i = 0; i < chaiseSnapDropZones.Length; i++)
        {
            chaiseSnapDropZones[i].GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);
        }
    }
        

    private void Update()
    {
        whenIsGrabbedChaise = chaise.GetComponent<Interactable_Object_Extension>().ValueIsGrabbed();

        if (whenIsGrabbedChaise == true)
        {
            for (int i = 0; i < chaiseSnapDropZones.Length; i++)
            {
                chaiseSnapDropZones[i].GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive = true;
            }
        }

    }

    private void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {

        for (int i = 0; i < chaiseSnapDropZones.Length; i++)
        {
            chaiseSnapDropZones[i].GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive =false;
        }
    }
}
