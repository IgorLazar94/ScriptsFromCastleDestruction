using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    private void OnEnable()
    {
        isFlying += UseSkills;
    }

    private void OnDisable()
    {
        isFlying -= UseSkills;
    }

    private void Start()
    {
        damage = GameSettings.Instance.GetBallDamage();
    }

    protected override void UseSkills()
    {
        base.UseSkills();
    }
}
