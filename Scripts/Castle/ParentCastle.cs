using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentCastle : MonoBehaviour
{
    [SerializeField] protected Image healthBarImage;
    protected List<MainBuildController> listOfMainBuild = new List<MainBuildController>();
    [SerializeField] protected WinLoseBehaviour winLoseBehaviour;
    public Action checkMainController;

    private float safeBuildPercent;
    protected int castleHealthPoints;
    private int defaultHP;

    private void Start()
    {
        AddMainBuildsList();
        CalculateAllSafeBuilds();
        defaultHP = castleHealthPoints;
    }

    private void OnEnable()
    {
        checkMainController += CheckMainBlocks;
        checkMainController += CalculateRemainBuilds;
        checkMainController += UpdateHPCastle;
        
    }

    private void OnDisable()
    {
        checkMainController -= CheckMainBlocks;
        checkMainController -= CalculateRemainBuilds;
        checkMainController -= UpdateHPCastle;
    }

    private void AddMainBuildsList()
    {
        var mainBuilds = gameObject.GetComponentsInChildren<MainBuildController>();

        for (int i = 0; i < mainBuilds.Length; i++)
        {
            listOfMainBuild.Add(mainBuilds[i]);
            CalculateCastleHealthPoints(mainBuilds[i]);
        }
    }

    private void CalculateCastleHealthPoints(MainBuildController mainBuildController)
    {
        castleHealthPoints += mainBuildController.mainBuildHealthPoint;
    }

    private void CalculateAllSafeBuilds()
    {
        safeBuildPercent = 1f / castleHealthPoints;
    }

    private void CalculateRemainBuilds()
    {
        int safeBuilds = 0;
        for (int i = 0; i < listOfMainBuild.Count; i++)
        {
            safeBuilds += listOfMainBuild[i].CalculateSafeBuilds();
        }
    }

    public void RemoveHP(int hp)
    {
        castleHealthPoints -= hp;
        UpdateHPCastle();
    }

    protected virtual void UpdateHPCastle()
    {
        safeBuildPercent = (float)castleHealthPoints / defaultHP;
        healthBarImage.fillAmount = safeBuildPercent;
        if (healthBarImage.fillAmount <= 0f)
        {
            Destroy(healthBarImage.transform.parent.gameObject, 1.0f);
        }
    }

    private void CheckMainBlocks()
    {
        foreach (MainBuildController mainBlock in listOfMainBuild)
        {
            if (!mainBlock.GetMainSafe() && mainBlock != null)
            {
                mainBlock.StartDestroyMainController();
            }
        }
    }
}
