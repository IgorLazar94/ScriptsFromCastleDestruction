using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    [SerializeField] private ParticleSystem flameTrail;
    [SerializeField] private ParticleSystem explosiveFX;
    private bool isCollision = false;


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
        damage = GameSettings.Instance.GetFireBallDamage();
    }

    public void FireBallBehaviour()
    {
    }

    protected override void UseSkills()
    {
        base.UseSkills();
        flameTrail.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.ProjectilePart) && !isCollision)
        {
            explosiveFX.transform.SetParent(null);
            explosiveFX.Play();
        }
    }
}
