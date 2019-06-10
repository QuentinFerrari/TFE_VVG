using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class IsTheDoorHot : MonoBehaviour
{

    public GameObject boxColliderUp;
    public GameObject boxColliderDown;
    public bool isBoxColliderUpChecked = false;
    public bool isBoxColliderDownChecked = false;
    public bool laPorteAEteVerifiee = false;
    public GameObject snapDropZone;
    public bool porteBloquee;

    private void Start()
    {
        snapDropZone.GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);
        snapDropZone.GetComponent<VRTK_SnapDropZone>().enabled = false;

    }

  

    private void Update()
    {
        if (boxColliderUp.GetComponent<CollisionBox>().hasEnteredNPC == true && isBoxColliderUpChecked == false)
        {
            isBoxColliderUpChecked = true;

            MeshRenderer meshRend = boxColliderUp.GetComponent<MeshRenderer>();
            meshRend.material.color = Color.green;
            Debug.Log("Le joueur a bien vérifié si la porte était chaude en haut");

        }
        if (boxColliderDown.GetComponent<CollisionBox>().hasEnteredNPC == true && isBoxColliderDownChecked == false)
        {
            isBoxColliderDownChecked = true;
            MeshRenderer meshRend = boxColliderDown.GetComponent<MeshRenderer>();
            meshRend.material.color = Color.green;
            Debug.Log("Le joueur a bien vérifié si la porte était chaude en haut");
        }



        if (isBoxColliderDownChecked == true && isBoxColliderUpChecked == true && laPorteAEteVerifiee == false)
        {
            laPorteAEteVerifiee = true;

            
            List<Material> liste = new List<Material>();
            liste.AddRange(this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().materials);
            liste[0].color = Color.red;
            liste[1].color = Color.red;
            
                        

            snapDropZone.GetComponent<VRTK_SnapDropZone>().enabled = true;

            Debug.Log("Le joueur a bien vérifié si la porte était chaude ");
        }
               
    }
    private void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {

        if (e.snappedObject.GetComponent<ComptagePoints>().aEteCompte == false)
        {
            Debug.Log("vous avez bloqué la chambre en feu, félicitations");
            GameManager.Instance.Sound1();
            e.snappedObject.GetComponentInChildren<ComptagePoints>().aEteCompte = true;
            porteBloquee = true;

        }
        else if (e.snappedObject.GetComponent<ComptagePoints>().aEteCompte == true)
        {
            Debug.Log("<b><color=#0000FF> Vous avez déjà posé cet objet à son bon emplacement !!  </color></b>");
            GameManager.Instance.Sound2();

        }
    }

    public bool PorteVerifiee()
    {
        return laPorteAEteVerifiee;
    }
    public bool PorteBloquee()
    {
        return porteBloquee;
    }
}
