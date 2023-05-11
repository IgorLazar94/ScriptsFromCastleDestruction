using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcher : MonoBehaviour
{
    public float detectionRadius = 10f;

    private EnemyWarrior nearestEnemy;

    public void Start()
    {
        InvokeRepeating("UpdateNearestEnemy", 0f, 1f);
    }

    public void UpdateNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            EnemyWarrior enemy = collider.GetComponent<EnemyWarrior>();
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    nearestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
    }

    public EnemyWarrior GetNearestEnemy()
    {
        return nearestEnemy;
    }
}
