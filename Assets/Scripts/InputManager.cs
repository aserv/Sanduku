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
    private float tapTheshold;
    public InputManager(int playerId, float tapTheshold = 0.8f) {
        horizontalName += playerId;
        verticalName += playerId;
        redName += playerId;
        yellowName += playerId;
        greenName += playerId;
        blueName += playerId;
    }

    public float HorizontalVal { get { return Input.GetAxis(horizontalName); } }
    public float VerticalVal { get { return Input.GetAxis(verticalName); } }

    public bool RedDown { get { return Input.GetButtonDown(redName); } }
    public bool RedUp { get { return Input.GetButtonUp(redName); } }
    public bool YellowDown { get { return Input.GetButtonDown(yellowName); } }
    public bool YellowUp { get { return Input.GetButtonUp(yellowName); } }
    public bool GreenDown { get { return Input.GetButtonDown(greenName); } }
    public bool GreenUp { get { return Input.GetButtonUp(greenName); } }
    public bool BlueDown { get { return Input.GetButtonDown(blueName); } }
    public bool BlueUp { get { return Input.GetButtonUp(blueName); } }
}
