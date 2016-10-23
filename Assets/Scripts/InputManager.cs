using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Not using this right now, maybe later
public class InputManager {
    private static bool paused;

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
    public InputManager(int playerId) {
        horizontalName += playerId;
        verticalName += playerId;
        redName += playerId;
        yellowName += playerId;
        greenName += playerId;
        blueName += playerId;
        startName += playerId;
        paused = false;
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
        if (Input.GetButtonDown(startName))
        {
            paused = !paused;
            GameObject pauseMenu = GameObject.FindGameObjectWithTag("pauseScreen");
            if (paused)
            {
                Time.timeScale = 0;
                pauseMenu.GetComponentInChildren<Text>().color = Color.black;
                pauseMenu.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.45f);
            }
            else if (!paused)
            {
                Time.timeScale = 1;
                pauseMenu.GetComponentInChildren<Text>().color = Color.clear;
                pauseMenu.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    public delegate void CommandEventHandler(Button b);
    public event CommandEventHandler OnCommandEnter;

    public delegate void TapEventHandler();
    public event TapEventHandler OnJump;

    public float HorizontalVal { get { return horizontal; } }

}
