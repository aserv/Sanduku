using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class SawBehavior : MonoBehaviour, ICommandListener {
    public float MoveSpeed;
    public float RotationalSpeed;
    public Command UpCommand;
    public Command DownCommand;
    public Command LeftCommand;
    public Command RightCommand;
    private Vector2 velocity;
    private float radius;

	// Use this for initialization
	void Start () {
        CommandManager manager = GameObject.Find("CommandManager").GetComponent<CommandManager>();
        radius = GetComponent<CircleCollider2D>().radius;
        manager.RegisterCommand(UpCommand, this);
        manager.RegisterCommand(DownCommand, this);
        manager.RegisterCommand(LeftCommand, this);
        manager.RegisterCommand(RightCommand, this);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, RotationalSpeed * Time.deltaTime);
        if (Physics2D.Raycast(transform.position, velocity, radius, 1))
            velocity *= -1;
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    public void ActOnCommand(Command command, PlayerController player)
    {
        if (command == UpCommand) {
            velocity = Vector2.up * MoveSpeed;
        } else if (command == DownCommand) {
            velocity = Vector2.down * MoveSpeed;
        } else if (command == LeftCommand) {
            velocity = Vector2.left * MoveSpeed;
        } else if (command == RightCommand) {
            velocity = Vector2.right * MoveSpeed;
        }
    }
}
