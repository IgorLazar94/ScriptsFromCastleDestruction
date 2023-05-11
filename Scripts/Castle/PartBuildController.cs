using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBuildController : MonoBehaviour
{
    [HideInInspector] public int partHealthPoint;
    [SerializeField] public MaterialsType materialsType;
    private Rigidbody buildBody;
    private BuildController buildController;
    private bool isNotKinematic = false;

    private void Awake()
    {
        buildBody = GetComponent<Rigidbody>();
        gameObject.GetComponent<MeshRenderer>().material =  SetMaterialSingleton.Instance.getMaterial(materialsType);
        partHealthPoint = SetMaterialSingleton.Instance.getHP(materialsType);
        buildController = gameObject.transform.parent.gameObject.GetComponent<BuildController>();
        
        CallParent();
    }

    private void CallParent()
    {
        buildController.addHPBuild(partHealthPoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isNotKinematic)
        {
            if (collision.gameObject.CompareTag(TagList.Projectile) || collision.gameObject.CompareTag(TagList.EnemyProjectile))
            {
                int damage = collision.gameObject.GetComponent<Projectile>().getDamage();
                collision.gameObject.tag = TagList.Ground;
                CallParent(damage, this);
            }
        }
    }

    public void deactivatePart()
    {
        StartDestroyPart();
    }

    private void CallParent(int damage, PartBuildController partBuildController)
    {
       buildController.TakeDamage(damage, partBuildController);
    }


    public IEnumerator DestroyPartBuild()
    {
        DisableKinematic();
        yield return new WaitForSeconds(0.3f);
        buildController.RemoveListOfParts(this);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void StartDestroyPart()
    {
        StartCoroutine(DestroyPartBuild());
    }


    private void DisableKinematic()
    {
        isNotKinematic = true;
        buildBody.isKinematic = false;
    }

}