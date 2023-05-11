using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTower : Tower
{
    [HideInInspector] public bool isTowerDestroy = false;

    public override void DestroyedTowerInvoke()
    {
        CheckMainDestroy();
    }
    private void CheckMainDestroy()
    {
        isTowerDestroy = true;
        DestroyThisTower();
    }
}
