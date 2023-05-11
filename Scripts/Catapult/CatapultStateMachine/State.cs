using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State
{

    protected ISwitchState _switchState;

    protected State (ISwitchState _ISW)
    {
        _switchState = _ISW;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void DoWork();
}

