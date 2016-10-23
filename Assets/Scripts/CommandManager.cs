using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour {
    public PlayerController.PlayerEffectListener Effects;
    private ICommandListener[] listeners = new ICommandListener[64];

    void Start()
    {
        RegisterCommand(Effects.SpeedUp, Effects);
        RegisterCommand(Effects.SpeedDown, Effects);
        RegisterCommand(Effects.JumpUp, Effects);
        RegisterCommand(Effects.JumpDown, Effects);
    }

    public void SendComand(Command command, PlayerController player)
    {
        ICommandListener l = listeners[command.Value & 0x3f];
        if (l != null)
            l.ActOnCommand(command, player);
    }

    public void RegisterCommand(Command command, ICommandListener listener)
    {
        ICommandListener l = listeners[command.Value & 0x3f];
        if (l == null)
            listeners[command.Value & 0x3f] = listener;
        else if (l is CommandDispatcher)
            ((CommandDispatcher)l).AddListener(listener);
        else
            listeners[command.Value & 0x3f] = (new CommandDispatcher()).AddListener(l).AddListener(listener);
    }
}

class CommandDispatcher : ICommandListener {
    List<ICommandListener> listeners = new List<ICommandListener>();
    public void ActOnCommand(Command command, PlayerController player)
    {
        foreach (ICommandListener l in listeners)
            l.ActOnCommand(command, player);
    }
    public CommandDispatcher AddListener(ICommandListener l)
    {
        listeners.Add(l);
        return this;
    }
}
