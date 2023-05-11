using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectilesDestroyed : MonoBehaviour
{

    [SerializeField] private ParticleSystem littleDust;
    [SerializeField] private ParticleSystem bigDust;


    private float timeToDestroyObjectFromCastle = 0.5f;
    private bool isCollision = false;

    public bool GetIsCollision()
    {
        return isCollision;
    }

    public void SetIsCollision(bool value)
    {
        isCollision = value;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.ProjectilePart) && !isCollision)
        {
            isCollision = true;
            bigDust.transform.SetParent(null);
            bigDust.Play();
            DestroyObject();
        }

        if ((collision.gameObject.CompareTag(TagList.Ground) && !isCollision) || (collision.gameObject.CompareTag(TagList.Wall) && !isCollision))
        {
            gameObject.GetComponent<Projectile>().DisableMoveAlongTrajectory(this.gameObject.GetComponent<Projectile>());
            isCollision = true;
            gameObject.tag = TagList.Ground;
            Invoke("EnableLittleDustParticles", 1f);
        }

        if (collision.gameObject.tag == TagList.EnemyProjectile && gameObject.tag == TagList.Projectile && !isCollision)
        {
            if (gameObject.GetComponent<Projectile>().isStartFly)
            {
                isCollision = true;
                EnableBigDustParticles();
            }
            if (collision.gameObject.GetComponent<EnemyProjectile>().isStartFly)
            {
                isCollision = true;
                Destroy(collision.gameObject);
            }
        }

        if ((collision.gameObject.CompareTag(TagList.EnemyWarrior) && !isCollision) || (collision.gameObject.CompareTag(TagList.PlayerWarrior) && !isCollision) || (collision.gameObject.CompareTag(TagList.Wall) && !isCollision))
        {
            gameObject.GetComponent<Projectile>().DisableMoveAlongTrajectory(this.gameObject.GetComponent<Projectile>());
            isCollision = true;
            //gameObject.tag = TagList.Ground;
            Invoke("EnableLittleDustParticles", 1f);
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

    private void EnableBigDustParticles ()
    {
        bigDust.transform.SetParent(null);
        bigDust.Play();
        DestroyObject();
    }
}
