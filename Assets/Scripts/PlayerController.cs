using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// This is the player controller class
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, IDamageable {
    public int PlayerID;
    //public float RunSpeed;
    //public float RunAccel;
    //public float JumpSpeed;
    public int MaxJumps;
    public int SpeedLevel;
    public int JumpLevel;

    public static float JumpSpeed;
    public static float RunSpeed;
    private static float RunAccel;
    public static int SJumpLevel;
    public static int SSpeedLevel;
    public static int STotal;

    private float raycastLength = 1.1f;
    private float raycastWidth = 0.5f;
    private bool isGrounded;
    private int jumpCount;
    private CommandManager manager;
    private Command command;
    private InputManager input;
    private float baseMove;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        SJumpLevel += JumpLevel;
        SSpeedLevel += SpeedLevel;
        STotal += 2;
        manager = GameObject.Find("CommandManager").GetComponent<CommandManager>();
        input = new InputManager(PlayerID);
        input.OnJump += JumpPressed;
        input.OnCommandEnter += CommandPressed;
        manager.Effects.Update();
	}

    void OnDestroy() {
        SJumpLevel -= JumpLevel;
        SSpeedLevel -= SpeedLevel;
        STotal -= 2;
        manager.Effects.Update();
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
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        anim.SetFloat("yVelocity", velocity.y);
        anim.SetBool("isRunning", Mathf.Abs(velocity.x - baseMove) > 0.2f);
    }

    //Called once per physics frame
    void FixedUpdate()
    {
        float lastBaseMove = baseMove;
        RaycastHit2D hit1 = Physics2D.Raycast((Vector2)transform.position + new Vector2(raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1 << 8));
        RaycastHit2D hit2 = Physics2D.Raycast((Vector2)transform.position + new Vector2(-raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1 << 8));
        if (isGrounded = (hit1 || hit2))
        {
            PlatformBehavior pform = hit1.collider ? hit1.collider.GetComponent<PlatformBehavior>() : hit2.collider.GetComponent<PlatformBehavior>();
            if (pform)
                baseMove = pform.horizontalSpeed * (pform.movingRight ? 1 : -1);
            else
                baseMove = 0;
        }
        anim.SetBool("IsGrounded", isGrounded);
        Vector2 v = this.GetComponent<Rigidbody2D>().velocity;
        if ((lastBaseMove > 0) ^ (baseMove > 0))
            v.x = baseMove;
        v.x = Mathf.MoveTowards(v.x, baseMove + RunSpeed * input.HorizontalVal, RunAccel * Time.fixedDeltaTime);
        if (v.x > 0 ^ transform.localScale.x > 0)
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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void CommandPressed(Button b)
    {
        if (command.AddButton(b))
        {
            Debug.Log(command);
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

    [Serializable]
    public class PlayerEffectListener : ICommandListener
    {
        public Command SpeedUp;
        public Command SpeedDown;
        public Command JumpUp;
        public Command JumpDown;
        public float SpeedLow;
        public float SpeedHigh;
        public float JumpLow;
        public float JumpHigh;
        public float AccelLow;
        public float AccelHigh;

        public void ActOnCommand(Command command, PlayerController player)
        {
            if (command == SpeedUp)
            {
                if (player.SpeedLevel != 2)
                {
                    player.SpeedLevel++;
                    SSpeedLevel++;
                }
            }
            else if (command == SpeedDown)
            {
                if (player.SpeedLevel != 0)
                {
                    player.SpeedLevel--;
                    SSpeedLevel--;
                }
            }
            else if (command == JumpDown)
            {
                if (player.JumpLevel != 0)
                {
                    player.JumpLevel--;
                    SJumpLevel--;
                }
            }
            else if (command == JumpUp)
            {
                if (player.JumpLevel != 2)
                {
                    player.JumpLevel++;
                    SJumpLevel++;
                }
            }
            Update();
        }

        public void Update()
        {
            if (STotal == 0)
                return;
            RunSpeed = Mathf.Lerp(SpeedLow, SpeedHigh, (float)SSpeedLevel / STotal);
            RunAccel = Mathf.Lerp(AccelLow, AccelHigh, (float)SSpeedLevel / STotal);
            JumpSpeed = Mathf.Lerp(JumpLow, JumpHigh, (float)SJumpLevel / STotal);
        }
    }
}
