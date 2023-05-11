using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerWarrior
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootingDelay = 2f;
    public float shootingForce = 2f;
    public float shootingAngle = 25f;

    private PlayerArcher player;
    private EnemyWarrior target;
    private bool isShooting = false;

    private void Start()
    {
        player = GetComponent<PlayerArcher>();
        movingController.SetSpeed(baseSpeed);
        
    }

    private void Update()
    {
        if (player == null) return;

        if (isShooting) return;

        target = player.GetNearestEnemy();
        if (target != null)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;

        yield return new WaitForSeconds(shootingDelay);

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(0,0,-90));
        ArrowShoot arrowScript = arrow.GetComponent<ArrowShoot>();
        //arrowScript.SetTarget(target.transform.position);
        //arrowScript.SetShootingForce(shootingForce, shootingAngle);

        isShooting = false;
    }
}
