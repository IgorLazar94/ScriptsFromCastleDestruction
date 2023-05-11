using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector] public bool isStartFly = false;

    public void StartMoveProjectile(GetEnemyPath trajectoryEnemyPath, float _speedProjectile)
    {
        StartCoroutine(moveProjectile(trajectoryEnemyPath, _speedProjectile));
    }

    private IEnumerator moveProjectile(GetEnemyPath trajectoryEnemyPath, float _speedProjectile)
    {
        Rigidbody projectileRB = GetComponent<Rigidbody>();
        Vector3[] points = trajectoryEnemyPath.getTrajectory();
        int index = 0;
        Vector3 direction;
        //bool isUseSkills = false;
        bool SplitShotIsNotDivision = false;
        if (gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            //isUseSkills = true;
            projectile.EnableMoveAlongTrajectory();
            projectile.GetComponent<SphereCollider>().enabled = true;
            SplitShotIsNotDivision = true;
            projectile.isFlying.Invoke();
        }
        while (Vector3.Distance(projectileRB.transform.position, points[points.Length - 2]) > 1f && projectileRB != null && projectile.moveAlongTrajectory)
        {
            if (Vector3.Distance(projectileRB.transform.position, points[index]) < 0.9f)
            {
                index++;
            }

            yield return new WaitForFixedUpdate();


            direction = points[index] - projectileRB.transform.position;
            direction = direction.normalized * _speedProjectile;
            projectileRB.velocity = direction;

            if (projectileRB.velocity.magnitude < 0.3f)
            {
                break;
            }

            if (projectile is SplitShot && SplitShotIsNotDivision && index > points.Length / 6f)
            {
                projectile.GetComponent<SplitShot>().StartDivision(points[points.Length - 1]);
                SplitShotIsNotDivision = false;
            }

            //if (isUseSkills && index > points.Length / 2f)
            //{
            //    isUseSkills = false;
            //}

            

        }
        direction = points[points.Length - 1] - projectileRB.transform.position;
        projectileRB.velocity = direction.normalized * _speedProjectile;
    }

}
