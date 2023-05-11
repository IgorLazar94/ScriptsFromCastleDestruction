using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyPush : MonoBehaviour
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform projectilesSpawnPoint;
    [SerializeField] private GetEnemyPath trajectoryEnemyPath;
    [Space]
    private float _speedProjectile;
    private float shotTimer;
    private GameObject Activeprojectile;
    private Animator enemyPushAnimation;

    private void Start()
    {
        enemyPushAnimation = GetComponentInChildren<Animator>();
        _speedProjectile = GameSettings.Instance.GetEnemyProjectileSpeed();
        shotTimer = GameSettings.Instance.GetEnemyReloadTime();
        StartEnemyShot();
    }

    private void StartEnemyShot()
    {
        StartCoroutine(EnemyShot());
    }

    private void ChooseProjectile()
    {
        Activeprojectile = Instantiate(projectiles[Random.Range(0, projectiles.Length)], projectilesSpawnPoint.position, Quaternion.identity);
    }

    private void PushProjectile()
    {
        enemyPushAnimation.SetTrigger("ReadyToPush");
        Rigidbody projBody = Activeprojectile.GetComponent<Rigidbody>();
        //Activeprojectile.GetComponent<Projectile>().isFlying.Invoke();
        projBody.isKinematic = false;
        projBody.useGravity = false;
        EnemyKingController.onPushEnemyProjectile.Invoke();
        Activeprojectile.GetComponent<EnemyProjectile>().isStartFly = true;
        //StartCoroutine(moveProjectile(projBody));
        Activeprojectile.GetComponent<EnemyProjectile>().StartMoveProjectile(trajectoryEnemyPath, _speedProjectile);
    }

    private IEnumerator EnemyShot()
    {
        while (true)
        {
            ChooseProjectile();
            yield return new WaitForSeconds(shotTimer / 2);
            PushProjectile();
            yield return new WaitForSeconds(shotTimer / 2);
        }
    }

}
