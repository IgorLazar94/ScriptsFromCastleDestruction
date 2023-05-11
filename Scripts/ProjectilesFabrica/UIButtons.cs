using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private BallFactory _ballFactory;
    [SerializeField] private FireBallFactory _fireBallFactory;
    [SerializeField] private SplitShotFactory _splitShotFactory;

    public void BallButton()
    {
        if (IdleState.isIdleStateActive)
        {
            var prefabBall = _ballFactory.GetNewInstance();
        }
    }

    public void FireBallButton()
    {
        if (IdleState.isIdleStateActive)
        {
            var prefabFireBall = _fireBallFactory.GetNewInstance();
        }
    }

    public void SplitShotButton()
    {
        if (IdleState.isIdleStateActive)
        {
            var prefabSplitShot = _splitShotFactory.GetNewInstance();
        }
    }
}
