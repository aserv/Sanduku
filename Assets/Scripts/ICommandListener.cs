﻿using UnityEngine;
using System.Collections;
using System;

public interface ICommandListener {
    void ActOnCommand(Command c, PlayerController player);
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
        get { return (Button)((Value & 0x30) >> 4); }
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

    public override bool Equals(object other)
    {
        if (other is Command)
        {
            Command c = (Command)other;
            return this == c;
        }
        return false;
    }
    public static bool operator==(Command c1, Command c2)
    {
        return ((c1.Value ^ c2.Value) & 0x3f) == 0;
    }
    public static bool operator !=(Command c1, Command c2)
    {
        return ((c1.Value ^ c2.Value) & 0x3f) != 0;
    }
    //Stop yelling at me VS
    public override int GetHashCode()
    {
        return Value;
    }
    public override string ToString()
    {
        switch(Buttons)
        {
            case 1:
                return String.Format("{0}__", Button1);
            case 2:
                return String.Format("{0}{1}_", Button1, Button2);
            case 3:
                return String.Format("{0}{1}{2}", Button1, Button2, Button3);
        }
        return "___";
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

public interface IDamageable
{
    void Damage();
}
