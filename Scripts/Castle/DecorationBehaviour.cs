using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class DecorationBehaviour : MonoBehaviour
{
    List<Rigidbody> decorBodies = new List<Rigidbody>();
    private float physicForce;

    void Start()
    {
        Rigidbody[] childBodies = GetComponentsInChildren<Rigidbody>();
        FillBodiesList(childBodies);   
    }

    private void FillBodiesList(Rigidbody[] childBodies)
    {
        for (int i = 0; i < childBodies.Length; i++)
        {
            decorBodies.Add(childBodies[i]);
        }
    }

    public void ActivateBodies()
    {
        int randomValue = Random.Range(0, 1);
        physicForce = Random.Range(5f, 15f);
        for (int i = 0; i < decorBodies.Count; i++)
        {
            decorBodies[i].isKinematic = false;
            decorBodies[i].AddForce(new Vector3(randomValue, randomValue, randomValue) * physicForce, ForceMode.Impulse);
        }
    }

    public void RemoveDecorations()
    {
        //ActivateBodies();
        for (int i = 0; i < decorBodies.Count; i++)
        {
            decorBodies[i].gameObject.transform.DOScale(Vector3.zero, 4f).OnComplete(() => Destroy(decorBodies[i].gameObject));
        }
    }
    
}
