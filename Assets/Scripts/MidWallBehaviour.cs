using UnityEngine;
using System.Collections;

public class MidWallBehaviour : MonoBehaviour, ICommandListener {

    public enum MidWallState
    {
        atTop,
        atBottom,
        movingUp,
        movingDown
    }

    public float speed;
    public float bottomY;
    public float topY;
    public Command command;

    public MidWallState state;

	// Use this for initialization
	void Start () {
        state = MidWallState.atTop;
        GameObject.Find("CommandManager").GetComponent<CommandManager>().RegisterCommand(command, this);
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case MidWallState.movingDown:
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                if(transform.position.y <= bottomY)
                {
                    state = MidWallState.atBottom;
                    transform.position = new Vector3(transform.position.x, bottomY, transform.position.z);
                }
                break;
            case MidWallState.movingUp:
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                if (transform.position.y >= topY)
                {
                    state = MidWallState.atTop;
                    transform.position = new Vector3(transform.position.x, topY, transform.position.z);
                }
                break;
        }

	}

    public void ActOnCommand(Command c, PlayerController player)
    {
        if(state==MidWallState.atTop)
        {
            state = MidWallState.movingDown;
        }
        if(state==MidWallState.atBottom)
        {
            state = MidWallState.movingUp;
        }
    }
}
