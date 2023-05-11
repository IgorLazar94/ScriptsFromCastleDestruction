using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastle : ParentCastle
{
    [SerializeField] PlayerKingController kingController;

    public void DestroyPlayerMainFromUnit(int damage)
    {
        for (int i = 0; i < listOfMainBuild.Count; i++)
        {
            if (listOfMainBuild[i] != null && listOfMainBuild[i].GetMainSafe())
            {
                listOfMainBuild[i].DestroyBuildFromUnit(damage);
                return;
            }
        }
    }

    protected override void UpdateHPCastle()
    {
        base.UpdateHPCastle();
        Invoke("CheckPlayerLose", 1.0f);
    }

    private void CheckPlayerLose()
    {
        if (castleHealthPoints <= 0)
        {
            kingController.StartDisableDefenseParticle();
        }
    }
}
