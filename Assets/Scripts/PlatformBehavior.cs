using UnityEngine;
using System.Collections;

public class PlatformBehavior : MonoBehaviour, ICommandListener {

    public float rotationSpeed;
    public float horizontalSpeed;
    public Command command;
    
    public bool rotating;

    public bool movingRight;

    // Use this for initialization
    void Start () {
        rotating = false;
        GameObject.Find("CommandManager").GetComponent<CommandManager>().RegisterCommand(command, this);
    }
	
	// Update is called once per frame
	void Update () {
	    if(rotating)
        {
            float newRotation = transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime;
            if(newRotation >= 180)
            {
                newRotation = 0;
                rotating = false;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newRotation));
        }
        Vector3 movementVector = new Vector3(-horizontalSpeed*Time.deltaTime, 0, 0);
        if(movingRight)
        {
            movementVector *= -1;
        }
        transform.position += movementVector;
        if (Physics2D.Raycast(transform.position, movementVector, 0.5f * transform.localScale.x, 1))
            movingRight = !movingRight;
    }

    public void ActOnCommand(Command c, PlayerController player)
    {
        if(!rotating)
        {
            rotating = true;
        }
    }
}
