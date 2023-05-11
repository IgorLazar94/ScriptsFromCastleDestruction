using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGate : MonoBehaviour
{
    [SerializeField] PlayerCastle playerCastle;
    private GateFX gateFX;

    private void Start()
    {
        gateFX = GetComponentInChildren<GateFX>();
    }

    public void DamagePlayerCastle(int damage)
    {
        if (playerCastle != null)
        {
            playerCastle.DestroyPlayerMainFromUnit(damage);
        }
        gateFX.StartPlayParticles();
    }

}
