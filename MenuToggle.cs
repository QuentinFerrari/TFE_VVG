using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class MenuToggle : MonoBehaviour {

    public VRTK_ControllerEvents controllerEvents;
    public GameObject menu;

    bool menuState = false;
    float timeState = 1;

    private void OnEnable()
    {
        controllerEvents.ButtonTwoPressed += ControllerEvents_ButtonTwoPressed;
        controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;

    }
    private void OnDisable()
    {
        controllerEvents.ButtonTwoPressed -= ControllerEvents_ButtonTwoPressed;
        controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;

    }
    private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        menuState = !menuState;
        menu.SetActive(menuState);
        if(timeState == 1)
        {
            timeState = 0;
        }
        else if (timeState == 0)
        {
            timeState = 1;
        };
        Time.timeScale = timeState;
    }

    private void ControllerEvents_ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
       
    }
}
