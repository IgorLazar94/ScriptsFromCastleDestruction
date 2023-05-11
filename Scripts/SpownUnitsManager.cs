using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpownUnitsManager : MonoBehaviour
{
    private int numScene;

    [Header("Player soldier")]
    [SerializeField] GameObject[] playerWariorPrefab;
    [SerializeField] GameObject regularPlayerWarrior;
    [SerializeField] GameObject fastPlayerWarrior;
    [SerializeField] GameObject bigPlayerWarrior;
    [SerializeField] GameObject bowmanPlayerWarrior;
    [SerializeField] float probability;
    [SerializeField] Transform playerWariorParent;
    [SerializeField] Transform playerCastle;
    [SerializeField] Transform spownPlayerPoint;
    [SerializeField] List<GameObject> playerWariors = new List<GameObject>();
    [SerializeField] int gridRowPlayerSquad = 3;
    [SerializeField] int gridColPlayerSquad = 4;
    [SerializeField] float spacingPlayerSquad = 2f;
    
    [Header("Enemy soldier")]
    [SerializeField] GameObject[] enemyWariorPrefab;
    [SerializeField] float probabilityForEnemy;
    [SerializeField] Transform enemyWariorParent;
    [SerializeField] Transform spownEnemyPoint;
    [SerializeField] List<GameObject> enemyWariors = new List<GameObject>();
    [SerializeField] int gridRowEnemySquad = 3;
    [SerializeField] int gridColEnemySquad = 4;
    [SerializeField] float spacingEnemySquad = 2f;
    

    public void Start()
    {
        numScene = SceneManager.GetActiveScene().buildIndex;
        //SpownPlayerSoldier();
        SpownEnemyWarriors();
        StartCoroutine(SpownEnemyWarrior());
    }

    /*public void SpownSquadPlayerSoldier() //якщо потрібно буде спавнити загін. Змінити префаб, який будемо спавнити.
    {
        for (int i = 0; i < gridRowPlayerSquad; i++)
        {
            for (int j = 0; j < gridColPlayerSquad; j++)
            {
                Vector3 spownPos = new(playerCastle.position.x + i * spacingPlayerSquad, playerCastle.position.y, playerCastle.position.z + j * spacingPlayerSquad);
                GameObject warior = Instantiate(playerWariorPrefab[0], spownPos, Quaternion.Euler(0, 90, 0), playerWariorParent);
                playerWariors.Add(warior);
            }
        }
    }*/

    /*public void SpownSquadEnemySoldier()//якщо потрібно буде спавнити загін. Змінити префаб, який будемо спавнити.
    {
        for (int i = 0; i < gridRowEnemySquad; i++)
        {
            for (int j = 0; j < gridColEnemySquad; j++)
            {
                Vector3 spownPos = new(enemyCastle.position.x + i * spacingEnemySquad, enemyCastle.position.y, enemyCastle.position.z + j * spacingEnemySquad);
                GameObject warior = Instantiate(enemyWariorPrefab, spownPos, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                enemyWariors.Add(warior);
            }
        }
    }*/

    public void SpownPlayerWarrior()
    {
        PlayerEnergyUI.OnDecreaseEnergy(1);
        float randomValue = Random.value;
        switch (numScene)
        {
            case 1:
                Instantiate(playerWariorPrefab[0], spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                break;
            case 2:
                if (randomValue <= probability) //встановлюємо ймовірність вибору воїна наступного рівня
                {
                    Instantiate(playerWariorPrefab[1], spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                }
                else
                {
                    Instantiate(playerWariorPrefab[0], spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                }
                break;
            case 3:
                
                if (randomValue <= probability) //встановлюємо ймовірність вибору воїна наступного рівня
                {
                    Instantiate(playerWariorPrefab[2], spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                }
                else
                {
                    Instantiate(playerWariorPrefab[Random.Range(0, 2)], spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                }
                break;
        }
    }

    public void SpownRegularPWarrior()
    {
        var playerUnit = Instantiate(regularPlayerWarrior, spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
        PlayerEnergyUI.OnDecreaseEnergy(1);
    }
    public void SpownFastPWarrior()
    {
        Instantiate(fastPlayerWarrior, spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
    }
    public void SpownBigPWarrior()
    {
        Instantiate(bigPlayerWarrior, spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
    }
    public void SpownBowmanPWarrior()
    {
        Instantiate(bowmanPlayerWarrior, spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
    }
    public void SpownEnemyWarriors()
    {
        float randomValue = Random.value;
        switch (numScene)
        {
            case 0:
                Instantiate(enemyWariorPrefab[0], spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                break;
            case 1:
                if (randomValue <= probability) //встановлюємо ймовірність вибору воїна наступного рівня
                {
                    Instantiate(enemyWariorPrefab[1], spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                }
                else
                {
                    Instantiate(enemyWariorPrefab[0], spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                }
                break;
            case 2:

                if (randomValue <= probability) //встановлюємо ймовірність вибору воїна наступного рівня
                {
                    Instantiate(enemyWariorPrefab[2], spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                }
                else
                {
                    Instantiate(enemyWariorPrefab[Random.Range(0, 2)], spownEnemyPoint.position, Quaternion.Euler(0, -90, 0), enemyWariorParent);
                }
                break;
        }
    }
    

    IEnumerator SpownEnemyWarrior()
    {
        var time = GameSettings.Instance.TimeEnemyWarriorSpown;

       /* if (level > 0) //Якщо потрібно зменшувати час спавну в залежності від номеру рівня
        {
            time -= 0.02f;
        }*/

        while (true) //Потрібно замінити на булеву змінну, щоб перевіряти чи гра ще триває.
        {
            yield return new WaitForSeconds(time);

            SpownEnemyWarriors();
        }
        
    }
}
