using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShot : Projectile
{
    private float timeToDivision = 1f;
    [SerializeField] private float distanceBetweenProj = 2f;
    public static bool splitFindCastle;
    private bool isClone = false;
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
        damage = GameSettings.Instance.GetSplitShotDamage();
        timeToDivision = GameSettings.Instance.GetSplitShotTimeDivision();
    }

    public void StartDivision(Vector3 trajectoryEndPoint)
    {
        if (!isClone)
        {
            StartCoroutine(DivisionProjectile(trajectoryEndPoint));
        }
    }

    private IEnumerator DivisionProjectile(Vector3 _trajectoryEndPoint)
    {
        splitFindCastle = false;
        yield return new WaitForSeconds(timeToDivision);
        var tempVelocity = gameObject.GetComponent<Rigidbody>().velocity;
        //tempVelocity.y = -tempVelocity.y;
        var splitCloneOne = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + distanceBetweenProj, transform.position.z), Quaternion.identity);
        var splitCloneTwo = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y - distanceBetweenProj, transform.position.z), Quaternion.identity);
        splitCloneOne.GetComponent<SplitShot>().SetClone();
        splitCloneTwo.GetComponent<SplitShot>().SetClone();
        splitCloneOne.GetComponent<Rigidbody>().useGravity = true;
        splitCloneTwo.GetComponent<Rigidbody>().useGravity = true;
        splitCloneOne.GetComponent<Rigidbody>().velocity = tempVelocity;
        splitCloneTwo.GetComponent<Rigidbody>().velocity = tempVelocity;
        //splitCloneOne.GetComponent<Rigidbody>().AddForce(_trajectoryEndPoint.normalized * 40, ForceMode.Impulse);
        //splitCloneTwo.GetComponent<Rigidbody>().AddForce(_trajectoryEndPoint.normalized * 40, ForceMode.Impulse);
    }

    protected override void UseSkills()
    {

    }

    public void SetClone()
    {
        isClone = true;
    }
}
