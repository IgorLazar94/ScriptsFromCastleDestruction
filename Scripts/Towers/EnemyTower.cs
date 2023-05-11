using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnemyTower : Tower
{
    [HideInInspector] public bool isTowerDestroy = false;
    [SerializeField] WarriorType warriorType;
    [SerializeField] private TutorTextBlock tutorTowerTextBlock;
    private bool isActiveStageTutor = false;
    private bool isFinishTutorial = false;

    private void OnEnable()
    {
        TutorTextBlock.OnTutorObjectsActivate += ShowTutorialTextBlock;
    }

    private void OnDisable()
    {
        TutorTextBlock.OnTutorObjectsActivate -= ShowTutorialTextBlock;
    }

    public override void DestroyedTowerInvoke()
    {
        CheckMainDestroy();
    }
    private void CheckMainDestroy()
    {
        isTowerDestroy = true;
        EnergyManager.OnAddEnergy.Invoke(warriorType);
        GameSettings.Instance.TimeEnemyWarriorSpown -= 1;
        SpawnPlayerProjectiles.levelUpProjectile.Invoke();
        CheckTutorText();
        DestroyThisTower();
    }

    private void ShowTutorialTextBlock()
    {
        if (TutorTextBlock.tutorLevelFromObjects == 1)
        {
            tutorTowerTextBlock.ShowTutorText();
            isActiveStageTutor = true;
        }
    }

    private void CheckTutorText()
    {
        if (isActiveStageTutor && !isFinishTutorial)
        {
            tutorTowerTextBlock.HideTutorText();
            isActiveStageTutor = false;
            isFinishTutorial = true;
        }
    }
    
}
