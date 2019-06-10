using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables.PhysicsBased;

public class HasRoomBeenChecked : MonoBehaviour {

    public GameObject triggerInterieur;
    public GameObject triggerExterieur;
    public GameObject door;
    public GameObject symbol;
    public bool hasCheckedInside;
    public bool hasSteppedOutside;
    public bool doorIsLocked;
    public bool symbolOnTheDoor;
    public bool check01;
    public bool check02;
    public GameEventsManager gameEventsManager;
    // Update is called once per frame
    private void Start()
    {
        gameEventsManager = FindObjectOfType<GameEventsManager>();

    }
    void Update()
    {

        hasCheckedInside = triggerInterieur.GetComponent<CollisionBox>().hasAlreadyEnteredPlayer;
        hasSteppedOutside = triggerExterieur.GetComponent<CollisionBox>().hasEnteredPlayer;


        if (hasCheckedInside == true && check01 == false)
        {
            if (hasSteppedOutside == true)
            {
                doorIsLocked = true;
                symbolOnTheDoor = true;
                check01 = true;
            }
        }
        if (symbolOnTheDoor == true && check02 == false)
        {
            symbol.GetComponent<MeshRenderer>().enabled = true;
            gameEventsManager.nombreChambresVerifiées++;
            //door.GetComponent<VRTK_PhysicsRotator>().isLocked = true;
            check02 = true;
        }
    }
}
