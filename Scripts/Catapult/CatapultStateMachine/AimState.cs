using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimState : State
{
    public static Action onAimState;
    private bool checker;

    public AimState(ISwitchState _ISW) : base(_ISW)
    {
       
    }

    public override void Enter()
    {
        checker = false;
        InputControllerBase.onPlayerUp += SwitchState;
        Debug.Log("вошёл в режим прицеливания");

    }

    public override void Exit()
    {
        InputControllerBase.onPlayerUp -= SwitchState;
        Debug.Log("вышел из режима прицеливания");
    }

    public override void DoWork()
    {
        if (checker)
        {
            _switchState.SwitchState<ShotState>();
        } 
    }

    private void SwitchState()
    {
        checker = true;
    }


}
