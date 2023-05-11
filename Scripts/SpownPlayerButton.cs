using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpownPlayerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCost;
    [SerializeField] Transform spownPlayerPoint;
    [SerializeField] Transform playerWariorParent;

    

    [SerializeField] WarriorType warriorType;
    [SerializeField, HideInInspector] EnergyManager energyManager;
    private GameObject warriorPrefab;
    private int costWarrior;

    private bool isCoolDown = false;

    private void Start()
    {
        
        energyManager = EnergyManager.instance;

        if (energyManager.energyPlayerDataDict.ContainsKey(warriorType))
        {
            if (energyManager.energyPlayerDataDict.TryGetValue(warriorType, out DataUnits cost))
            {
                costWarrior = cost.cost;
            }
                
            _textCost.text = costWarrior.ToString();
            warriorPrefab = cost.warriorPrefab;
            warriorType = cost.warriorType;
        }  
    }

    public void SpownButton()
    {
        if (!isCoolDown)
        {
            StartCoroutine(CoolDown());
            if (energyManager.CanSpownWarrior(costWarrior))
            {
                Instantiate(warriorPrefab, spownPlayerPoint.position, Quaternion.Euler(0, 90, 0), playerWariorParent);
                EnergyManager.OnDecreaseEnergy.Invoke(warriorType);
            }

        }
        
    }

    private IEnumerator CoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(1);
        isCoolDown = false;
    }


}
