using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour
{
    protected MainBuildController mainBuildController;

    private void Start()
    {
        mainBuildController = GetComponentInChildren<MainBuildController>();
    }

    public void DestroyTowerFromUnit(int damage)
    {
        mainBuildController.DestroyBuildFromUnit(damage);
    }

    public virtual void DestroyedTowerInvoke()
    {
    }

    protected virtual void DestroyThisTower()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
