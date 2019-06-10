using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsBehaviourManager : MonoBehaviour {


    //Fire State: off or on
    public enum FireState
    {
        fireIsOff,     
        fireIsOn
    }
    public FireState fireState = FireState.fireIsOff;

    //non-interactable npcs
    public GameObject[] nINPCs;

    //behaviour to display if the fire is off
    BasicAI.StateAfter stateAfter;

    //behaviour to display if the fire is on
    BasicAI.StateBefore stateBefore;

    private void Awake()
    {
        //Init();
    }

    //Initialize variables

    private void Init()
    {
        //search all the active non-interactable NPCs
        nINPCs = GameObject.FindGameObjectsWithTag("NINPC");

        //Assign a random state for non-interactable NPCs
        //that they will perform before the fire event started
        foreach (GameObject NPC in nINPCs)
        {
            stateBefore = (BasicAI.StateBefore)Random.Range(0, 14);
            NPC.GetComponent<BasicAI>().stateBefore = stateBefore;
        }

        //Assign a random state for non-interactable NPCs
        //that they will perform after the fire event started
        foreach (GameObject NPC in nINPCs)
        {
            stateAfter = (BasicAI.StateAfter)Random.Range(0, 26);

            NPC.GetComponent<BasicAI>().stateAfter = stateAfter;
        }

    }
}
