using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody projRB;
    [HideInInspector] public Collider projColl;
    [HideInInspector] public Vector3 projPos { get { return transform.position; } }

    [HideInInspector] public bool isPlayerProjectile;
    [HideInInspector] public bool moveAlongTrajectory = true;
    private bool isFirstHit = false;

    protected int damage;

    public System.Action isFlying;

    [HideInInspector] public bool isStartFly = false;

    public void EnableMoveAlongTrajectory ()
    {
        moveAlongTrajectory = true;
    }

    public void DisableMoveAlongTrajectory(Projectile projectile)
    {
        projectile.gameObject.GetComponent<Rigidbody>().useGravity = true;
        projectile.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Force); // Vector 3 up for fix push ragdoll in ground
        moveAlongTrajectory = false;
    }

    private void OnEnable()
    {
        isFlying += UseSkills;
    }

    private void OnDisable()
    {
        isFlying -= UseSkills;
    }

  
    void Awake()
    {
        projRB = GetComponent<Rigidbody>();
        projColl = GetComponent<SphereCollider>();
    }

    public void Push(Vector2 force)
    {
        projRB.AddForce(force, ForceMode.Impulse);
    }

    public void ActivateRb()
    {
        projRB.isKinematic = false;
    }

    public void ActivateCollider()
    {
        projColl.enabled = true;
    }

    public void DeactivateRb()
    {
        projRB.velocity = Vector3.zero;
        projRB.angularVelocity = Vector3.zero;
        projRB.isKinematic = true;
    }

    public int getDamage()
    {
        return damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.ProjectilePart))
        {
            if (isPlayerProjectile && !isFirstHit)
            {
                isFirstHit = true;
            }
        }
    }

    protected virtual void UseSkills()
    {
    }

}
