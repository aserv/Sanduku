using UnityEngine;
using System.Collections;

//Not using this right now, maybe later
public class InputManager {
    private string horizontalName = "Horizontal";
    private string verticalName = "Vertical";
    private string redName = "Red";
    private string yellowName = "Yellow";
    private string greenName = "Green";
    private string blueName = "Blue";
    private float tapTheshold = 0.8f;
    private float lastVert;
    private float horizontal;
    public InputManager(int playerId) {
        horizontalName += playerId;
        verticalName += playerId;
        redName += playerId;
        yellowName += playerId;
        greenName += playerId;
        blueName += playerId;
    }

    public void Update()
    {
        horizontal = Input.GetAxis(horizontalName);
        if (Input.GetButtonDown(redName))
            OnCommandEnter(Button.R);
        if (Input.GetButtonDown(yellowName))
            OnCommandEnter(Button.Y);
        if (Input.GetButtonDown(greenName))
            OnCommandEnter(Button.G);
        if (Input.GetButtonDown(blueName))
            OnCommandEnter(Button.B);
        if (lastVert < tapTheshold & ((lastVert = Input.GetAxis(verticalName)) >= tapTheshold))
            OnJump();
    }

    public delegate void CommandEventHandler(Button b);
    public event CommandEventHandler OnCommandEnter;

    public delegate void TapEventHandler();
    public event TapEventHandler OnJump;

    public float HorizontalVal { get { return horizontal; } }

}
