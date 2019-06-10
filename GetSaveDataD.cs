using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSaveDataD : MonoBehaviour {

    /* GameObject qui contient ce script */
    public GameObject Parent;
    /* GameObject qui contient le script dans lequel il y a les données */
    public GameObject GODonnees;
    /* Entier qui contient la valeur du déplacement */
    private int DeplD; 
    

    void  Start()
    {
        
        DeplD = PlayerPrefs.GetInt("DeplacementD");

        Parent.transform.GetChild(0).gameObject.SetActive(false);
        Parent.transform.GetChild(1).gameObject.SetActive(false);
        Parent.transform.GetChild(2).gameObject.SetActive(false);
        Parent.transform.GetChild(3).gameObject.SetActive(false);
        Parent.transform.GetChild(4).gameObject.SetActive(false);
        Parent.transform.GetChild(5).gameObject.SetActive(false);
        Parent.transform.GetChild(6).gameObject.SetActive(false);
/*
        GODonnees.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Toggle>().isOn = false;
        GODonnees.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Toggle>().isOn = false;
        GODonnees.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Toggle>().isOn = false;
        GODonnees.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Toggle>().isOn = false;
*/
        switch (DeplD)
        {
            case 1:
                Parent.transform.GetChild(0).gameObject.SetActive(true);
               // GODonnees.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Toggle>().isOn = true;
                break;
            case 2:
                Parent.transform.GetChild(2).gameObject.SetActive(true);
               // GODonnees.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Toggle>().isOn = true;
                break;
            case 3:
                Parent.transform.GetChild(4).gameObject.SetActive(true);
               // GODonnees.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Toggle>().isOn = true;
                break;
            case 4:
               // GODonnees.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Toggle>().isOn = true;
                break;
        }      
    } 
}
