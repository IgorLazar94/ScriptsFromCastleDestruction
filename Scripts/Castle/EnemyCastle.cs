using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCastle : ParentCastle
{
    [SerializeField] EnemyKingController kingController;
    [SerializeField] private TutorTextBlock tutorTextBlock;
    private bool isActiveStageTutor = false;
    private bool isFinishTutorial = false;



    private void OnEnable()
    {
        if (tutorTextBlock != null)
        {
            TutorTextBlock.OnTutorObjectsActivate += ShowTutorialTextBlock;
        }
    }

    private void OnDisable()
    {
        if (tutorTextBlock != null)
        {
            TutorTextBlock.OnTutorObjectsActivate -= ShowTutorialTextBlock;
        }
    }

    public void DestroyEnemyMainFromUnit(int damage)
    {
        for (int i = 0; i < listOfMainBuild.Count; i++)
        {
            if (listOfMainBuild[i] != null && listOfMainBuild[i].GetMainSafe())
            {
                listOfMainBuild[i].DestroyBuildFromUnit(damage);
                return;
            }
        }
    }

    protected override void UpdateHPCastle()
    {
        base.UpdateHPCastle();
        Invoke("CheckEnemyLose", 1.0f);
    }

    private void CheckEnemyLose()
    {
        if (castleHealthPoints <= 0)
        {
            HideTutorTextBlock();
            kingController.StartDisableDefenseParticle();
            DestroyCastle();
        }
    }

    private void HideTutorTextBlock()
    {
        if (isActiveStageTutor && !isFinishTutorial)
        {
            tutorTextBlock.HideTutorText();
            isActiveStageTutor = false;
            isFinishTutorial = true;
        }
    }

    private void ShowTutorialTextBlock()
    {
        if (TutorTextBlock.tutorLevelFromObjects == 1 && tutorTextBlock != null)
        {
            tutorTextBlock.ShowTutorText();
            isActiveStageTutor = true;
        }
    }

    private void DestroyCastle()
    {
        Destroy(this.gameObject);
    }
}
