using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Not using this right now, maybe later
public class InputManager {
    private string horizontalName = "Horizontal";
    private string verticalName = "Vertical";
    private string redName = "Red";
    private string yellowName = "Yellow";
    private string greenName = "Green";
    private string blueName = "Blue";
    private string startName = "Start";
    private float tapTheshold = 0.9f;
    private float lastVert;
    private float horizontal;
    private int controllerID;
    [HideInInspector]
    public PlayerController Player;
    public InputManager(int controllerID) {
        horizontalName += controllerID;
        verticalName += controllerID;
        redName += controllerID;
        yellowName += controllerID;
        greenName += controllerID;
        blueName += controllerID;
        startName += controllerID;
        this.controllerID = controllerID;
    }

    public void Update()
    {
        horizontal = Input.GetAxis(horizontalName);
        if (Input.GetButtonDown(redName) && OnCommandEnter != null)
            OnCommandEnter(Button.R);
        if (Input.GetButtonDown(yellowName) && OnCommandEnter != null)
            OnCommandEnter(Button.Y);
        if (Input.GetButtonDown(greenName) && OnCommandEnter != null)
            OnCommandEnter(Button.G);
        if (Input.GetButtonDown(blueName) && OnCommandEnter != null)
            OnCommandEnter(Button.B);
        if ((lastVert < tapTheshold & ((lastVert = Input.GetAxis(verticalName)) >= tapTheshold)) && OnJump != null) //Single & is intentional
            OnJump();
        if (Input.GetButtonDown(startName) && OnStart != null)
        {
            OnStart(this);
        }
            
    }

    public delegate void CommandEventHandler(Button b);
    public event CommandEventHandler OnCommandEnter;

    public delegate void TapEventHandler();
    public event TapEventHandler OnJump;

    public delegate void StartEventHandler(InputManager sender);
    public event StartEventHandler OnStart;

    public float HorizontalVal { get { return horizontal; } }

}
