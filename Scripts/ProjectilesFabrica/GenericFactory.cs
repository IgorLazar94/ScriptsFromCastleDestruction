using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPos;
    private GameObject newObject;

    public GameObject GetNewInstance()
    {
        Vector3 pos = projectileSpawnPos.transform.position;
        newObject = Instantiate(projectilePrefab, pos, Quaternion.identity);
        newObject.GetComponent<Projectile>().isPlayerProjectile = true;
        EventBus.onCreateNewProjectile.Invoke(newObject);
        return newObject;
    }


}
