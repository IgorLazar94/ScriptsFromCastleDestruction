using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGate : MonoBehaviour
{
    [SerializeField] EnemyCastle enemyCastle;
    private GateFX gateFX;

    private void Start()
    {
        gateFX = GetComponentInChildren<GateFX>();
    }

    public void DamageEnemyCastle(int damage)
    {
        enemyCastle.DestroyEnemyMainFromUnit(damage);
        gateFX.StartPlayParticles();
    }

}
