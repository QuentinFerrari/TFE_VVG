using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;


public class LifePointsViewer : MonoBehaviour
{


    [Tooltip("Toggles whether the  text is visible.")]
    public bool display01 = true;
    [Tooltip("The frames per second deemed acceptable that is used as the benchmark to change the FPS text colour.")]
    public int target01 = 100;
    [Tooltip("The size of the font the FPS is displayed in.")]
    public int fontSize = 80;
    [Tooltip("The position of the FPS text within the headset view.")]
    public Vector3 position = new Vector3(0, 0, -0.015f);
    [Tooltip("The colour of the FPS text when the frames per second are within reasonable limits of the Target FPS.")]
    public Color goodColor = Color.green;
    [Tooltip("The colour of the FPS text when the frames per second are falling short of reasonable limits of the Target FPS.")]
    public Color warnColor = Color.yellow;
    [Tooltip("The colour of the FPS text when the frames per second are at an unreasonable level of the Target FPS.")]
    public Color badColor = Color.red;

    
    protected Canvas canvas;
    protected Text text;
    protected GameEventsManager gameEventsManager;
    protected int lifePoints;

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

        lifePoints = gameEventsManager.lifePoints;
    }

    protected virtual void Update()
    {
        lifePoints = gameEventsManager.lifePoints;
        text.text = string.Format("{0:F2} PV", lifePoints);

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

        if (text != null)
        {
            text.fontSize = fontSize;
            text.transform.localPosition = position;
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