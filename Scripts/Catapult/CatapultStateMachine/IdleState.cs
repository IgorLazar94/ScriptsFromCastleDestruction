using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleState : State
{
    private bool checker;
    public static Action onIdleState;
    public static bool isIdleStateActive;
    
    public IdleState(ISwitchState _ISW) : base(_ISW)
    {

    }

    public override void Enter()
    {
        isIdleStateActive = true;
        checker = false;
        InputControllerBase.onPlayerTap += SwitchState;
        Debug.Log("����� � ����� �����");
    }

    public override void Exit()
    {
        isIdleStateActive = false;
        InputControllerBase.onPlayerTap -= SwitchState;
        Debug.Log("����� �� ������ �����");
    }
    
    public override void DoWork()
    {
        if (checker)
        {
            _switchState.SwitchState<AimState>();
        } 
    }

    private void SwitchState()
    {
        checker = true;
    }
}
