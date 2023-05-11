using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class BuildController : MonoBehaviour
{
    [SerializeField] private List<PartBuildController> partsOfBuild = new List<PartBuildController>();
    public int buildHealthPoint;
    private int currentBuildHP;
    [SerializeField] private MainBuildController mainBuildController;
    [HideInInspector] public bool isSafeBuild;
    private bool isDestroyAllParts = false;
    private DecorationBehaviour decorationBehaviour;
    private bool hasDecorBlock = false;

    private void Awake()
    {
        isSafeBuild = true;
        FillList();
        CheckIfHasDecoration();
        mainBuildController = gameObject.transform.parent.gameObject.GetComponent<MainBuildController>();
    }

    private void CheckIfHasDecoration()
    {
        if (gameObject.GetComponentInChildren<DecorationBehaviour>() != null)
        {
            hasDecorBlock = true;
            decorationBehaviour = gameObject.GetComponentInChildren<DecorationBehaviour>();
        }
    }
    private void FillList()
    {
        PartBuildController[] partsArray = GetComponentsInChildren<PartBuildController>();

        for (int i = 0; i < partsArray.Length; i++)
        {
            partsOfBuild.Add(partsArray[i]);
        }
    }

    public void addHPBuild(int partHP)
    {
        buildHealthPoint += partHP;
        currentBuildHP = buildHealthPoint;

        mainBuildController.addHPBuild(partHP);
    }
    public void DestroyAllParts()
    {
        for (int i = 0; i < partsOfBuild.Count; i++)
        {
            partsOfBuild[i].deactivatePart();
        }
        ActivateDecorationPhysics();
        RemoveBlock();
    }

    public bool GetSafeBuild()
    {
        return isSafeBuild;
    }

    public void SetSafeBuildFalse()
    {
        isSafeBuild = false;
    }

    private void ActivateDecorationPhysics()
    {
        if (hasDecorBlock)
        {
            decorationBehaviour.ActivateBodies();
        }
    }
  
    public void TakeDamage(int damage, PartBuildController partBuildController)
    {

        if (currentBuildHP == buildHealthPoint)
        {
            if (damage >= currentBuildHP)
            {
                DestroyAllParts();
                RemoveDecorationObjects();
                currentBuildHP -= buildHealthPoint;
                CallParent();
            }
            else
            {
                destroyOnePartCastle(partBuildController);
                ActivateDecorationPhysics();
                currentBuildHP -= damage;
            }
        }
        else
        {
            DestroyAllParts();
            RemoveDecorationObjects();
            currentBuildHP -= buildHealthPoint;
            CallParent();
        }

    }

    public void TakeDamageFromUnit(int damage)
    {
        if (partsOfBuild.Count != 0)
        {
            if (partsOfBuild[0].partHealthPoint < damage)
            {
                if (partsOfBuild.Count > 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        destroyOnePartCastle(partsOfBuild[0]);
                        ActivateDecorationPhysics();
                    }
                    currentBuildHP -= damage;
                }
                else if (!isDestroyAllParts)
                {
                    isDestroyAllParts = true;
                    DestroyAllParts();
                    RemoveDecorationObjects();
                    CallParent();
                    currentBuildHP -= damage;
                }
            }
            else
            {
                destroyOnePartCastle(partsOfBuild[0]);
                ActivateDecorationPhysics();
                currentBuildHP -= damage;
            }
        }
        else
        {
            DestroyAllParts();
            RemoveDecorationObjects();
            CallParent();
        }
    }

    public void destroyOnePartCastle(PartBuildController _partBuildController)
    {
        _partBuildController.StartDestroyPart();
    }
    private void RemoveDecorationObjects()
    {
        if (hasDecorBlock)
        {
            decorationBehaviour.RemoveDecorations();
        }
    }

    public void RemoveListOfParts(PartBuildController partBuildController)
    {
        int index = -1;
        for (int i = 0; i < partsOfBuild.Count; i++)
        {
            if (partsOfBuild[i] == partBuildController)
            {
                index = i;
                break;
            }
        }

        if (index >= 0)
        {
            partsOfBuild.RemoveAt(index);
        }
    }

    public void CallParent()
    {
        mainBuildController.CheckBuild(this);
    }

    private void RemoveBlock()
    {
        StartCoroutine(waitBeforeRemove());
    }
    IEnumerator waitBeforeRemove()
    {
        yield return new WaitForSeconds(2f);
        mainBuildController.RemoveElementList(this);
        DestroyThisBuildController();
    }

    public void DestroyThisBuildController()
    {
        Destroy(this.gameObject);
    }
}
