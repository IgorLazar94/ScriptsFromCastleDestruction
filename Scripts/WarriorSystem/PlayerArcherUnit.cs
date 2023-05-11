using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherUnit : PlayerWarrior
{
    [SerializeField] ArrowShoot arrowShot;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpownPoint;


    private void Awake()
    {
        

        //movingController.SetDirection(baseDirection);
        //movingController.SetSpeed(baseSpeed);
    }
    public override void AttackEnemy(GameObject target) // Attack unit
    {
        Debug.Log("Archer Attack Enemy Unit ");
        target.GetComponent<Collider>();
        StartCoroutine(Attack(target));
        StartShootArrowAnim(); 
        movingController.SetSpeed(0);
    }
    public override void AttackTowerByBowman(GameObject target, EnemyTower enemyTower) // AttackTower
    {
        Debug.Log("Archer Attack Tower ");
        StartCoroutine(AttackTower(target, enemyTower));
        StartShootArrowAnim();
        movingController.SetSpeed(0);
    }

    IEnumerator AttackTower(GameObject target, EnemyTower tower)
    {
        while (!tower.isTowerDestroy && isHitTower)
        {
            yield return new WaitForSeconds(2);
            //StartShootArrowAnim();
            if (isLive)
            {
                SpownArrow(target);
                tower.DestroyTowerFromUnit(damage);
            }
        }
        isHitTower = false;
        isFighting = false;
        StartRun();
    }

    IEnumerator Attack(GameObject target)
    {
        /*Debug.Log("enemyWarrior.isLive = " + enemyWarrior.isLive + "  isHitTarget" + isHitTarget);
        Debug.Log("enemyWarrior = " + enemyWarrior);*/
        while (enemyWarrior.isLive && isHitTarget)
        {
            yield return new WaitForSeconds(2);
            
            if (isLive)
            {
                SpownArrow(target);
                enemyWarrior.TakeDamage(damage);
            }
        }
        gameObject.layer = LayerMask.NameToLayer(LayerList.PlayerWarrior);
        isHitTarget = false;
        isFighting = false;
        Debug.Log("isHitTarget = " + isHitTarget + " isFighting " + isFighting + " gameObject " + gameObject + " gameObject.layer " + gameObject.layer);
        StartRun();

    }

    public void SpownArrow(GameObject newTarget)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpownPoint.position, Quaternion.Euler(0, 0, -90));
        arrow.GetComponent<ArrowShoot>().SetTarget(newTarget);
    }
}
