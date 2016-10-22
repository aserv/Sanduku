using UnityEngine;
using System.Collections;

public class AxeBehavior : MonoBehaviour, ICommandListener {
    public float Angle;
    public float FallDuration;
    public float RiseDuration;
    public Command Command;

    private float duration;
    private Quaternion startAngle;
    private Quaternion endAngle;
    private int state = 0;

    // Use this for initialization
    void Start()
    {
        startAngle = transform.rotation;
        endAngle = transform.rotation * Quaternion.AngleAxis(Angle, Vector3.forward);
        GameObject.Find("CommandManager").GetComponent<CommandManager>().RegisterCommand(Command, this);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                return;
            case 1:
                duration += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(startAngle, endAngle, duration / FallDuration);
                if (duration < FallDuration)
                    return;
                state = 2;
                duration = 0;
                break;
            case 2:
                duration += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(endAngle, startAngle, duration / RiseDuration);
                if (duration < RiseDuration)
                    return;
                state = 0;
                duration = 0;
                break;
        }

    }

    public void ActOnCommand(Command c, PlayerController player)
    {
        if (state == 0)
        {
            state = 1;
        }
    }
}
