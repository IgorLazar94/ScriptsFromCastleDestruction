using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShotDestroyed : MonoBehaviour
{
    [SerializeField] private ParticleSystem littleDust;


    private float timeToDestroyObjectFromCastle = 0.5f;
    private bool isCollision = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.ProjectilePart) && !isCollision)
        {
            isCollision = true;
           EnableLittleDustParticles();
        }

        if ((collision.gameObject.CompareTag(TagList.Ground) && !isCollision) || (collision.gameObject.CompareTag(TagList.Wall) && !isCollision))
        {
            gameObject.GetComponent<Projectile>().DisableMoveAlongTrajectory(this.gameObject.GetComponent<Projectile>());
            gameObject.tag = TagList.Ground;
            isCollision = true;
            EnableLittleDustParticles();

        }

        if (collision.gameObject.tag == TagList.EnemyProjectile && gameObject.tag == TagList.Projectile && !isCollision)
        {
            isCollision = true;
            if (gameObject.GetComponent<Projectile>().isStartFly)
            {
                isCollision = true;
                EnableLittleDustParticles();
            }
            if (collision.gameObject.GetComponent<EnemyProjectile>().isStartFly)
            {
                isCollision |= true;
                Destroy(collision.gameObject);
            }
        }
    }

    private void DestroyObject()
    {
        gameObject.transform.DOScale(Vector3.zero, timeToDestroyObjectFromCastle)
            .OnComplete(() => Destroy(this.gameObject));
    }

    private void EnableLittleDustParticles()
    {
        littleDust.transform.SetParent(null);
        littleDust.Play();
        DestroyObject();
    }
}
