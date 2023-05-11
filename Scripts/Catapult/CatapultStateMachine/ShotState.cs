using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShotState : State
{
    public static Action onProjectileDestroyed;
    private bool checker;
   
    public ShotState(ISwitchState _ISW) : base(_ISW)
    {
    }

    public override void Enter()
    {
        onProjectileDestroyed += SwitchState;
        checker = false;
        Debug.Log("вошёл в режим стрельбы");
    }

    public override void Exit()
    {
        onProjectileDestroyed -= SwitchState;
        Debug.Log("вышел из режима стрельбы");
    }

    public override void DoWork()
    {
        if (checker)
        {
            _switchState.SwitchState<IdleState>();
        }
    }

    private void SwitchState()
    {
        checker = true;
    }

}
