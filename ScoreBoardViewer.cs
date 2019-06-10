using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class ScoreBoardViewer : MonoBehaviour {

	
    protected Canvas canvas;
    protected Text text;
    protected GameEventsManager gameEventsManager;
    protected GameManager gameManager;
    public string texteADeployer;

    public bool[] numeroText = new bool[8]; 

    //protected VRTK_SDKManager sdkManager;

    protected virtual void OnEnable()
    {
        InitCanvas();
    }

    protected virtual void OnDisable()
    {

    }

    protected void Start()
    {
        gameEventsManager = GameObject.Find("GameEventsManager").GetComponent<GameEventsManager>();
        gameManager = FindObjectOfType<GameManager>();
        texteADeployer = gameEventsManager.texteADeployer;
    }

    protected virtual void Update()
    {
        if (numeroText[0] == true)
        {
            texteADeployer = gameEventsManager.texteADeployer;// partie a modifier
            text.text = string.Format("{0:F2}", texteADeployer);
        } // chises rangées sous la table ou non ? A FAIRE
        if (numeroText[1] == true)
        {
            texteADeployer = gameEventsManager.nombreObjetsRangés.ToString(); 
            text.text = string.Format("{0:F2}", texteADeployer);
        }//nombre d'ojets encombnrants rangés
        if (numeroText[2] == true)
        {
            texteADeployer = gameEventsManager.nombreChambresVerifiées.ToString(); 
            text.text = string.Format("{0:F2}", texteADeployer);
        }// nombre de chambres vérifiées  
        if (numeroText[3] == true)
        {
            if (gameEventsManager.aPrisLeGSM == true)
            {
                texteADeployer = "OUI";
                text.text = string.Format("{0:F2}", texteADeployer);
            }
            else if (gameEventsManager.aPrisLeGSM == false)
            {
                texteADeployer = "NON";
                text.text = string.Format("{0:F2}", texteADeployer);
            }
        }// aPrisGsm oui ou non
        if (numeroText[4] == true)
        {
            if (gameEventsManager.choixPatientAAider01 == true)
            {
                texteADeployer = "Patient Mobile";
                text.text = string.Format("{0:F2}", texteADeployer);
            }
            else if (gameEventsManager.choixPatientAAider02 == true)
            {
                texteADeployer = "Patient dans le lit";
                text.text = string.Format("{0:F2}", texteADeployer);
            }
            else if (gameEventsManager.choixPatientAAider01 == false && gameEventsManager.choixPatientAAider02 == false)
            {
                texteADeployer = "Aucun des 2";
                text.text = string.Format("{0:F2}", texteADeployer);
            }
        }//choixpatient
        if (numeroText[5] == true)
        {
            texteADeployer = (Mathf.FloorToInt(gameManager.totalTime)/60).ToString() +"   min " + (Mathf.FloorToInt(gameManager.totalTime)%60).ToString() +"   sec";
            text.text = string.Format("{0:F2}", texteADeployer);
        }//totaltime
        if (numeroText[6] == true)//timebetweenEvents
        {
            //texteADeployer = ;
            text.text = string.Format("{0:F2}", texteADeployer);
        }
        if (numeroText[7] == true)
        {
            texteADeployer = gameEventsManager.texteADeployer;// partie a modifier
            text.text = string.Format("{0:F2}", texteADeployer);
        }

    }
    protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        SetCanvasCamera();
    }
    protected virtual void InitCanvas()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        text = GetComponent<Text>();

        if (canvas != null)
        {
            canvas.planeDistance = 0.5f;
        }        
        SetCanvasCamera();
    }
    protected virtual void SetCanvasCamera()
    {
        Transform sdkCamera = VRTK_DeviceFinder.HeadsetCamera();
        if (sdkCamera != null)
        {
            canvas.worldCamera = sdkCamera.GetComponent<Camera>();
        }
    }
}
