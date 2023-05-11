using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 2f;

    private Vector3 direction;
    private GameObject target;
    private bool isHit = false;
    private Collider collider;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            collider = newTarget.GetComponent<Collider>();
            //Vector3 f = Random.Range(collider.bounds.center, collider.bounds.max);
            direction = collider.bounds.center;
            //direction = collider.bounds.max;
            /*  direction = newTarget.transform.position;
              direction = new Vector3(direction.x, transform.position.y, transform.position.z);*/
            target = newTarget;
        }
        

    }
    private void Update()
    {
        if (!isHit)
        {
            //Debug.Log("IM SHOT ARROW ");
            transform.position = Vector3.MoveTowards(transform.position, direction, speed);
            if (Vector3.Distance(transform.position, direction) < 4f)
            {
                
                isHit = true;
                if (target != null)
                {
                    gameObject.transform.SetParent(target.transform);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyWarrior>(out EnemyWarrior enemy) && collision.gameObject == target)
        {
            
            /*isHit = true;
            //Destroy(gameObject);
            gameObject.transform.SetParent(collision.gameObject.transform);
            Debug.Log("IM SHOT ARROW ");*/
        }
    }
}

