using UnityEngine;
using System.Collections;

/// <summary>
/// This is the player controller class
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour {
    public float RunSpeed;
    public float RunAccel;
    public float JumpSpeed;
    public int MaxJumps;

    private float raycastLength = 1.2f;
    private float raycastWidth = 0.5f;
    private bool isGrounded;
    private int jumpCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + new Vector2(raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1<<8)) ||
            Physics2D.Raycast((Vector2)transform.position + new Vector2(-raycastWidth, 0), Vector2.down, raycastLength, -1 ^ (1 << 8));
        Vector2 v = this.GetComponent<Rigidbody2D>().velocity;
        v.x = RunSpeed * Input.GetAxis("Horizontal1");
        //float v = Input.GetAxis("Vertical1");

        jumpCount = isGrounded ? 0 : jumpCount;
        if (jumpCount < MaxJumps && Input.GetButtonDown("Vertical1")) {
            ++jumpCount;
            v.y = JumpSpeed;
        }

        this.GetComponent<Rigidbody2D>().velocity = v;
	}
}
