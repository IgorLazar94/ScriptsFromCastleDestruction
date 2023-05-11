using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemyPath : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private int countPointTrajectory = 10;
    [SerializeField] GameObject kingObject;
    private float _multiplyHeight;
    private List<Transform> listOfTargets;
    private Transform targetPosition;
    private float _heightTrajectory = 50f;
    private Vector3[] _trajectory;
    private float randomXPositionCoefficient;
    private float xPositionTarget;
    private float _randomXPosition;
    private float _randomHeight;
    private PlayerKingController kingController;
    private void Start()
    {
        kingController = kingObject.GetComponent<PlayerKingController>();
        GetGameSettings();
        ChooseTarget();
        xPositionTarget = targetPosition.transform.position.x;
        _trajectory = new Vector3[countPointTrajectory];
        _randomXPosition = xPositionTarget;
        _randomHeight = _heightTrajectory;
    }

    private void ChooseTarget()
    {
        CheckDestroyedTarget();
        if (listOfTargets != null && kingController.isEnablePlayerDefense)
        {
            targetPosition = listOfTargets[Random.Range(0, listOfTargets.Count)];
            xPositionTarget = targetPosition.transform.position.x;
        } else
        {
            targetPosition = kingObject.transform;
        }
    }

    public Transform GetTarget()
    {
        return targetPosition;
    }

    private void CheckDestroyedTarget()
    {
        if (listOfTargets.Count > 0)
        {
            for (int i = 0; i < listOfTargets.Count; i++)
            {
                if (listOfTargets[i] == null)
                {
                    listOfTargets.RemoveAt(i);
                }
            }
        }
        else
        {
            listOfTargets = null;
        }
    }

    public Vector3[] getTrajectory()
    {
        ChooseTarget();
        randomHeightTrajectory();
        SetRandomXPosition();
        reload();
        return _trajectory;
    }

    private void randomHeightTrajectory()
    {
        _randomHeight = Random.Range(_heightTrajectory - _multiplyHeight, _heightTrajectory + _multiplyHeight) -
                        (Vector3.Distance(new Vector3(targetPosition.transform.position.x,
                                         targetPosition.transform.position.y,
                                         targetPosition.transform.position.z),
                                         new Vector3(spawnPosition.transform.position.x,
                                         spawnPosition.transform.position.y,
                                         spawnPosition.transform.position.z)) * (1f / 3f));
    }

    private void SetRandomXPosition()
    {
        _randomXPosition = Random.Range(xPositionTarget - randomXPositionCoefficient,
                                        xPositionTarget + randomXPositionCoefficient);
    }
    private void reload()
    {
        Vector3[] points = generatePoints(spawnPosition.position, new Vector3(_randomXPosition,
                                                                               targetPosition.position.y,
                                                                               targetPosition.position.z), countPointTrajectory);

        //foreach (var item in points)  // Debug
        //{
        //    Instantiate("Simple3DObject".gameObject, item, Quaternion.identity);
        //}

        _trajectory = points;
    }
    private Vector3[] generatePoints(Vector3 start, Vector3 end, int numPoints)
    {
        Vector3 direction = (end - start).normalized;

        Vector3[] points = new Vector3[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
            Vector3 point = start + t * (end - start) + (t * (1 - t) * Vector3.up * _randomHeight);
            points[i] = point;
        }
        return points;
    }

    private void GetGameSettings()
    {
        listOfTargets = GameSettings.Instance.GetListOfTargets();
        randomXPositionCoefficient = GameSettings.Instance.GetEnemyMissTarget_X();
        _heightTrajectory = GameSettings.Instance.GetHeightTrajectory();
        _multiplyHeight = GameSettings.Instance.GetRandomHeight();
    }

}
