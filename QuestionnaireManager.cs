using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class QuestionnaireManager : MonoBehaviour {

    // Use this for initialization

    public GameEventsManager gameEventsManager;
    public GameManager gameManager;
    public int score;
    public int erreurs;
    public GameObject controleD;
    public GameObject controleG;
    public GameObject controllerDroit;
    public GameObject controllerGauche; // désactiver les controles durant les questions

    
    float timeState = 1;
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update () {
        score = gameEventsManager.score;

        if (timeState == 1)
        {

        }
        else if (timeState == 0)
        {
            controllerDroit.GetComponent<VRTK_UIPointer>().enabled = true;                    //on active les pointers
            controllerDroit.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;
            controllerDroit.GetComponent<VRTK_Pointer>().enabled = true;
            controllerGauche.GetComponent<VRTK_UIPointer>().enabled = true;
            controllerGauche.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;
            controllerGauche.GetComponent<VRTK_Pointer>().enabled = true;
        }
        
    }

    public void BonneReponse()
    {
        score = score + 100;
        GameManager.Instance.Sound1();
    }
   public void MauvaiseReponse()
    {
        score = score - 100;
        GameManager.Instance.Sound2();
        erreurs++;
    }
 
    public void DesactiverControles()
    {
        controllerDroit = GameObject.FindGameObjectWithTag("RightController");
        controllerGauche = GameObject.FindGameObjectWithTag("LeftController");

        timeState = 0;
        
        controleD.SetActive(false);
        controleG.SetActive(false);
    }
    public void ActiverControles()
    {
        timeState = 1;
        controllerDroit.GetComponent<VRTK_UIPointer>().enabled = false;
        controllerDroit.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;
        controllerDroit.GetComponent<VRTK_Pointer>().enabled = false;
        controllerGauche.GetComponent<VRTK_UIPointer>().enabled = false;
        controllerGauche.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;
        controllerGauche.GetComponent<VRTK_Pointer>().enabled = false;

        controleD.SetActive(true);
        controleG.SetActive(true);

    }
}
