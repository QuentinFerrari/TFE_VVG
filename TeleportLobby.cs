using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLobby : MonoBehaviour {

    public bool checkForTutorial;
    public bool checkForScene01;

    private void OnTriggerEnter(Collider other)
    {
        if(checkForTutorial == true && other.tag == "Player")
        {
            GameManager.Instance.SkipToNextLevel(5);
        }
        else if(checkForScene01 == true && other.tag == "Player")
        {
            GameManager.Instance.SkipToNextLevel(6);
        }
    }
}
