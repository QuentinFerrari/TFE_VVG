using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class gsm_disparaitre : MonoBehaviour {


    private GameObject gsm;
    private GameEventsManager gameEventsManager;
    private GameObject controllerDroit;
    private GameObject controllerGauche;
    public GameObject text01;
    public GameObject text02;

	// Use this for initialization
	void Start () {
        gsm = this.gameObject;
        gameEventsManager = FindObjectOfType<GameEventsManager>();
        controllerDroit = gameEventsManager.controllerDroit;
        controllerGauche = gameEventsManager.controllerGauche;
	}
	
	// Update is called once per frame
	void Update () {
        if (gsm.GetComponent<Interactable_Object_Extension>().ValueIsGrabbed() == true)
        {
            Debug.Log("appuyer sur grip pour ranger le telephone dans votre poche");
            StartCoroutine(TextePourXSecondes(5, text01));
            if (controllerDroit.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress) == true || controllerGauche.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress) == true)
            {
                gsm.GetComponentInChildren<MeshRenderer>().enabled = false;
                
                Debug.Log("le gsm a été rangé dans votre poche");
                StartCoroutine(TextePourYSecondes(5,text02));
            }
        }
        else if (gsm.GetComponent<Interactable_Object_Extension>().ValueIsGrabbed() == false)
        {
            EteindreTexte(text01);
        }
	}

    IEnumerator TextePourXSecondes(int temps, GameObject text)
    {
        while (temps > 0)
        {
            yield return new WaitForSeconds(1);
            AllumerTexte(text);
            temps--;
        }
        EteindreTexte(text02);        
    }
    IEnumerator TextePourYSecondes(int temps, GameObject text)
    {
        while (temps > 0)
        {
            yield return new WaitForSeconds(1);
            AllumerTexte(text);
            temps--;
        }
        EteindreTexte(text02);
        gsm.SetActive(false);
    }
    void EteindreTexte(GameObject text)
    {
        text.SetActive(false);
    }
    void AllumerTexte(GameObject text)
    {
        text.SetActive(true);
    }
}
