using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance;

    [Header ("Energy")]
    [SerializeField] int currentEnergy;
    [SerializeField] int maxEnergy;
    [SerializeField] private TextMeshProUGUI _textScore;


    public static Action<WarriorType> OnAddEnergy;
    public static Action<WarriorType> OnDecreaseEnergy;

    [Header ("Player Warrior")]
    [SerializeField] private List<DataUnits> energyPlayerDataList;
    public Dictionary<WarriorType, DataUnits> energyPlayerDataDict;

    [Header("Enemy Warrior"), Space(10)]
    [SerializeField] private List<DataUnits> energyEnemyDataList;
    public Dictionary<WarriorType, DataUnits> energyEnemyDataDict;

    [Header("Enemy Buildings"), Space(10)]
    [SerializeField] private List<DataEnemyUnits> energyEnemyBuildDataList;
    public Dictionary<WarriorType, DataEnemyUnits> energyEnemyBuildDict;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            energyPlayerDataDict = new Dictionary<WarriorType, DataUnits>();
            energyEnemyDataDict = new Dictionary<WarriorType, DataUnits>();
            energyEnemyBuildDict = new Dictionary<WarriorType, DataEnemyUnits>();

            foreach (DataUnits dataUnits in energyPlayerDataList)
            {
                energyPlayerDataDict.Add(dataUnits.warriorType, dataUnits);
            }

            foreach (DataUnits dataEnemyUnits in energyEnemyDataList)
            {
                energyEnemyDataDict.Add(dataEnemyUnits.warriorType, dataEnemyUnits);
            }
            foreach (DataEnemyUnits dataEnemyBuildUnits in energyEnemyBuildDataList)
            {
                energyEnemyBuildDict.Add(dataEnemyBuildUnits.warriorType, dataEnemyBuildUnits);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("currentEnergy"))
        {
            _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
        }
        else 
        _textScore.text = currentEnergy.ToString();
    }

    private void OnEnable()
    {
        OnAddEnergy += IncreaseEnergy; //підписуємось на екшен, який додає енергії
        OnDecreaseEnergy += DecreaseEnergy; //підписуємось на екшен, який віднімає енергію
    }

    private void OnDisable()
    {
        OnAddEnergy -= IncreaseEnergy; //відписуємось від екшена, який додає енергії
        OnDecreaseEnergy -= DecreaseEnergy; //відписуємось від екшена, який віднімає енергію
    }

    public void DecreaseEnergy(WarriorType warriorType)
    {
        if (energyPlayerDataDict.ContainsKey(warriorType))
        {
            DataUnits dataUnits = energyPlayerDataDict[warriorType];
            if(currentEnergy >= dataUnits.cost)
            {
                currentEnergy -= dataUnits.cost;
                PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
                //Debug.Log("Energy decrease by " + dataUnits.cost + "Current energy = " + currentEnergy);
            }
            else
            {
                //button disable
                //Debug.Log("Not enough energy to spown " + warriorType);
            }
        }
        else
        {
            //Debug.Log("Energy data not found for warriorType " + warriorType);
        }
    }

    public void IncreaseEnergy(WarriorType warriorType)
    {
        if (energyEnemyDataDict.ContainsKey(warriorType))
        {
            DataUnits dataEnemyUnits = energyEnemyDataDict[warriorType];
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += dataEnemyUnits.cost;
                PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
                if (currentEnergy > maxEnergy)
                {
                    currentEnergy = maxEnergy;
                    PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                    _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
                }
                //Debug.Log("Energy increase by " + dataEnemyUnits.cost + "Current energy = " + currentEnergy);
            }
            else
            {
                currentEnergy = maxEnergy;
                PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
            }
        }
        else if (energyEnemyBuildDict.ContainsKey(warriorType))
        {
            DataEnemyUnits dataEnemyBuildUnits = energyEnemyBuildDict[warriorType];
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += dataEnemyBuildUnits.cost;
                PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
                if (currentEnergy > maxEnergy)
                {
                    currentEnergy = maxEnergy;
                    PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                    _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
                }
                //Debug.Log("Energy increase by " + dataEnemyBuildUnits.cost + "Current energy = " + currentEnergy);
            }
            else
            {
                currentEnergy = maxEnergy;
                PlayerPrefs.SetInt("currentEnergy", currentEnergy);
                _textScore.text = PlayerPrefs.GetInt("currentEnergy").ToString();
            }
        }
        else
        {
            Debug.Log("Energy data not found for warriorType " + warriorType);
        }
    }

    public bool CanSpownWarrior(int cost)
    {
        return cost <= currentEnergy;
    }


    public void DeleteKeyPlayerPref()
    {
        PlayerPrefs.DeleteKey("currentEnergy");
        PlayerPrefs.DeleteKey("CoinsAmount");
        PlayerPrefs.DeleteKey("PlayerLevel");
    }

}
