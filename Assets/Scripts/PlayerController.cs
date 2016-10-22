using UnityEngine;
using System.Collections;

/// <summary>
/// This is the player controller class
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable {
    public int PlayerID;
    public float RunSpeed;
    public float RunAccel;
    public float JumpSpeed;
    public int MaxJumps;

    private float raycastLength = 1.1f;
    private float raycastWidth = 0.5f;
    private bool isGrounded;
    private int jumpCount;
    private CommandManager manager;
    private Command command;
    private InputManager input;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("CommandManager").GetComponent<CommandManager>();
        input = new InputManager(PlayerID);
        input.OnJump += JumpPressed;
        input.OnCommandEnter += CommandPressed;
	}
	
	// Update is called once per frame
	void Update () {
        input.Update();
        //QueryCommands();
        //float vert = Input.GetAxis("Vertical1");
        //if (((lastVert < 0.7f) & ((lastVert = vert) > 0.7f)) && jumpCount < MaxJumps && !jump)
        //{
        //    ++jumpCount;
        //    jump = true;
        //}
    }

    //Called once per physics frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + new Vector2(raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1 << 8)) ||
            Physics2D.Raycast((Vector2)transform.position + new Vector2(-raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1 << 8));
        Vector2 v = this.GetComponent<Rigidbody2D>().velocity;
        v.x = Mathf.MoveTowards(v.x, RunSpeed * input.HorizontalVal, RunAccel * Time.fixedDeltaTime);
        if (v.x > 0 ^ transform.localScale.z < 0)
            Flip();
        //float v = Input.GetAxis("Vertical1");

        //if (jump)
        //{
        //    v.y = JumpSpeed;
        //    jump = false;
        //}
        if (v.y <= 0 && isGrounded)
            jumpCount = 0;

        this.GetComponent<Rigidbody2D>().velocity = v;
    }

    public void Damage()
    {
        Destroy(gameObject);
    }

    private void JumpPressed()
    {
        if (jumpCount < MaxJumps)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, JumpSpeed);
            jumpCount++;
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
    }

    private void CommandPressed(Button b)
    {
        if (command.AddButton(b))
        {
            manager.SendComand(command, this);
            command = Command.Empty;
        }
    }

    private void QueryCommands()
    {
        if (Input.GetButtonDown("Red1") && command.AddButton(Button.R))
        {
            manager.SendComand(command, this);
            command = Command.Empty;
        }
        if (Input.GetButtonDown("Yellow1") && command.AddButton(Button.Y))
        {
            manager.SendComand(command, this);
            command = Command.Empty;
        }
        if (Input.GetButtonDown("Green1") && command.AddButton(Button.G))
        {
            manager.SendComand(command, this);
            command = Command.Empty;
        }
        if (Input.GetButtonDown("Blue1") && command.AddButton(Button.B))
        {
            manager.SendComand(command, this);
            command = Command.Empty;
        }
    }
}
