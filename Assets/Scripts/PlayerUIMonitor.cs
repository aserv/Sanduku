using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIMonitor : MonoBehaviour {
    public Sprite Red;
    public Sprite Yellow;
    public Sprite Green;
    public Sprite Blue;
    private Image m_button1;
    private Image m_button2;
    private Image m_button3;
    private Image button1 { get { return m_button1 ?? (m_button1 = transform.Find("Button1").GetComponent<Image>()); } }
    private Image button2 { get { return m_button2 ?? (m_button2 = transform.Find("Button2").GetComponent<Image>()); } }
    private Image button3 { get { return m_button3 ?? (m_button3 = transform.Find("Button3").GetComponent<Image>()); } }
	
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
