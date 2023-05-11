using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForEnemy : MonoBehaviour
{
    [SerializeField] GameObject targetBuild;
    private List<Transform> listOfParts = new List<Transform>();
    private float timeToCheckPosition;
    private Transform targetTransform;

    private void Start()
    {
        timeToCheckPosition = GameSettings.Instance.GetEnemyReloadTime();
        targetTransform = gameObject.transform;
        //targetTransform.position = Vector3.zero;
        CalculateAllParts();
        StartSetTargetPosition();
    }

    private void CalculateAllParts()
    {
        PartBuildController[] partsOfBuild;
        partsOfBuild = targetBuild.GetComponentsInChildren<PartBuildController>();

        for (int i = 0; i < partsOfBuild.Length; i++)
        {
            listOfParts.Add(partsOfBuild[i].gameObject.transform);
        }
    }

    private void StartSetTargetPosition()
    {
        StartCoroutine(SetTargetPosition());
    }

    private IEnumerator SetTargetPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToCheckPosition);
            ChooseTargetPosition();
        }
    }

    private void ChooseTargetPosition()
    {
        targetTransform.position = Vector3.zero;

        List<Transform> tempListPos = new List<Transform>();
        for (int i = 0; i < listOfParts.Count; i++)
        {
            if (listOfParts[i] != null)
            {
                tempListPos.Add(listOfParts[i].gameObject.transform);
            }
        }

        for (int i = 0; i < tempListPos.Count; i++)
        {
            targetTransform.position += tempListPos[i].transform.position;
        }
        if (tempListPos.Count != 0)
        {
            targetTransform.position /= tempListPos.Count;
        }
    }








}
