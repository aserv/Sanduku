using UnityEngine;
using System.Collections;

public class TurretBehavior : MonoBehaviour, ICommandListener {
    public float Duration;
    public float Angle;
    public GameObject Bullet;
	public Command Command;

    private float duration;
    private Quaternion startAngle;
    private Quaternion endAngle;

	// Use this for initialization
	void Start () {
        startAngle = transform.rotation;
        endAngle = transform.rotation * Quaternion.AngleAxis(Angle, Vector3.forward);
		GameObject.Find("CommandManager").GetComponent<CommandManager>().RegisterCommand(Command, this);
	}
	
	// Update is called once per frame
	void Update () {
        duration += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(startAngle, endAngle, duration / Duration);
        if (duration >= Duration)
        {
            Quaternion temp = startAngle;
            startAngle = endAngle;
            endAngle = temp;
            duration = 0;
        }
	}

    public void ActOnCommand(Command c, PlayerController player)
    {
        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
