using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpownEnemyWarrior : MonoBehaviour
{
    [SerializeField] Transform spownEnemyPoint;
    [SerializeField] Transform enemyWarriorParent;

    [SerializeField] WarriorType warriorType;
    [SerializeField, HideInInspector] EnergyManager energyManager;
    private GameObject warriorPrefab;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject regularPrefab;
    [SerializeField] GameObject fastPrefab;
    [SerializeField] GameObject bigPrefab;


    [Header("For Game Manager")]
/*    public int level;
    public int koeficient;
    public int enemyEnergy;*/
    public int kof;
    public float time;

    private void Start()
    {
        /*energyManager = EnergyManager.instance;

        if (energyManager.energyPlayerDataDict.ContainsKey(warriorType))
        {
            if (energyManager.energyPlayerDataDict.TryGetValue(warriorType, out DataUnits cost))
            {
                //costWarrior = cost.cost;
            }

            warriorPrefab = cost.warriorPrefab;
            warriorType = cost.warriorType;
        }*/
        Instantiate(regularPrefab, spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWarriorParent);
        StartCoroutine(SpownEnemy());
    }

    public IEnumerator SpownEnemy()
    {
        
        kof = (GameSettings.Instance.Level * GameSettings.Instance.Koeficient + GameSettings.Instance.EnemyEnergy) / GameSettings.Instance.Level;
        time = GameSettings.Instance.TimeEnemyWarriorSpown - (GameSettings.Instance.Level / 10) * 2;
        while (Time.timeScale > 0)
        {
            yield return new WaitForSeconds(time);
            if (GameSettings.Instance.Level <= 1)
            {
                Instantiate(regularPrefab, spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWarriorParent);
            }
            else
            {
                if (kof > 0)
                {
                    Instantiate(regularPrefab, spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWarriorParent);
                    yield return new WaitForSeconds(time/2);
                }
                if (kof > 40)
                {
                    Instantiate(fastPrefab, spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWarriorParent);
                    yield return new WaitForSeconds(time / 2);
                }
                if (kof > 100)
                {
                    Instantiate(bigPrefab, spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWarriorParent);
                    yield return new WaitForSeconds(time / 2);
                }
            }
        }
    }

   /* public void SpownEnemyUnits()
    {
        //if (energyManager.CanSpownWarrior(costWarrior))
            Instantiate(warriorPrefab, spownEnemyPoint.position, Quaternion.Euler(0, 90, 0), enemyWarriorParent);

    }*/

    IEnumerator SpownEnemies()
    {
        var time = GameSettings.Instance.TimeEnemyWarriorSpown;
        while (true) //Потрібно замінити на булеву змінну, щоб перевіряти чи гра ще триває.
        {
            yield return new WaitForSeconds(time);

            //SpownEnemyUnits();
        }

    }
}
