using UnityEngine;
using System.Collections;
using System;

public interface ICommandListener {
    void ActOnCommand(Command c, PlayerControler player);
}

[Serializable]
public struct Command
{
    public Byte Value;
    public Button Button1
    {
        get { return (Button)(Value & 0x03); }
        set { Value = (Byte)((Value & 0xfc) | (Byte)value); }
    }
    public Button Button2
    {
        get { return (Button)((Value & 0x0C) >> 2); }
        set { Value = (Byte)((Value & 0xf3) | ((Byte)value << 2)); }
    }
    public Button Button3
    {
        get { return (Button)((Value & 0x03) >> 4); }
        set { Value = (Byte)((Value & 0xcf) | ((Byte)value << 4)); }
    }
    public uint Buttons
    {
        get { return (uint)(Value >> 6); }
        set { Value = (Byte)((Value & 0x3f) | ((Byte)(value & 0x03) << 6)); }
    }
    //True if the command is complete
    public bool AddButton(Button b)
    {
        switch (Buttons)
        {
            case 0:
                Button1 = b;
                break;
            case 1:
                Button2 = b;
                break;
            case 2:
                Button3 = b;
                Value += 0x40;
                return true;
            default:
                return true;
        }
        Value += 0x40;
        return false;
    }
    public static Command Empty = new Command { Value = 0 };
}

public enum Button : byte
{
    R = 0,
    Y = 1,
    G = 2,
    B = 3
}
