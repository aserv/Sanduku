using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIMonitor : MonoBehaviour {
    public Sprite Red;
    public Sprite Yellow;
    public Sprite Green;
    public Sprite Blue;
    private Image button1;
    private Image button2;
    private Image button3;
	// Use this for initialization
	void Start () {
        button1 = transform.Find("Button1").GetComponent<Image>();
        button2 = transform.Find("Button2").GetComponent<Image>();
        button3 = transform.Find("Button3").GetComponent<Image>();
    }
	
	public void SetCommand(Command command)
    {
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;
        switch (command.Buttons)
        {
            case 3:
                button3.enabled = true;
                button3.sprite = FromButton(command.Button3);
                goto case 2;
            case 2:
                button2.enabled = true;
                button2.sprite = FromButton(command.Button2);
                goto case 1;
             case 1:
                button1.enabled = true;
                button1.sprite = FromButton(command.Button1);
                goto case 0;
            case 0:
                break;                
        }
    }

    private Sprite FromButton(Button b)
    {
        switch (b)
        {
            case Button.R:
                return Red;
            case Button.Y:
                return Yellow;
            case Button.G:
                return Green;
            case Button.B:
                return Blue;
        }
        return null;
    }
}
