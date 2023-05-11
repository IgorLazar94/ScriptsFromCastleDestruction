using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainBuildController : MonoBehaviour
{
    public List<BuildController> listOfBuilds = new List<BuildController>();
    private bool isMainSafe = true;
    public int mainBuildHealthPoint;

    private ParentCastle parentCastle;
    private Tower tower;

    private void Awake()
    {
        FillList();
        if (gameObject.transform.parent.gameObject.GetComponent<ParentCastle>())
        {
            parentCastle = gameObject.transform.parent.gameObject.GetComponent<ParentCastle>();
        }
        else if (gameObject.transform.parent.gameObject.GetComponent<Tower>())
        {
            tower = gameObject.transform.parent.gameObject.GetComponent<Tower>();
        }
    }

    private void FillList()
    {
        BuildController[] buildsArray = GetComponentsInChildren<BuildController>();
        for (int i = 0; i < buildsArray.Length; i++)
        {
            listOfBuilds.Add(buildsArray[i]);
        }
    }

    public void addHPBuild(int hp)
    {
        mainBuildHealthPoint += hp;
    }

    public int CalculateSafeBuilds()
    {
        List<BuildController> safeBuilds = new List<BuildController>();
        for (int i = 0; i < listOfBuilds.Count; i++)
        {
            safeBuilds.Add(listOfBuilds[i]);
        }
        return safeBuilds.Count;
    }

    public void CheckBuild(BuildController destroyedBuild)
    {
        for (int i = 0; i < listOfBuilds.Count; i++)
        {
            if (listOfBuilds[i] == destroyedBuild && listOfBuilds[i].GetSafeBuild())
            {
                listOfBuilds[i].SetSafeBuildFalse();
                if (parentCastle != null)
                {
                    parentCastle.RemoveHP(listOfBuilds[i].buildHealthPoint);
                }
                CrumbleUpperBlocks(i);
                //RemoveElementList(destroyedBuild); // Add to fixed bug with level up tower
            }
        }
        CheckThisMainController();

        if (parentCastle != null /*&& isMainSafe*/)
        {
            parentCastle.checkMainController?.Invoke();
        }


    }

    private void CrumbleUpperBlocks(int numberCrumbleObject)
    {
        for (int i = 0; i < listOfBuilds.Count; i++)
        {
            if (i < numberCrumbleObject /*&& listOfBuilds[i].GetSafeBuild()*/)
            {
                listOfBuilds[i].SetSafeBuildFalse();
                listOfBuilds[i].DestroyAllParts();
                //RemoveElementList(listOfBuilds[i]);  // Add to fixed bug with level up tower

                if (parentCastle != null)
                {
                    parentCastle.RemoveHP(listOfBuilds[i].buildHealthPoint);
                }
            }
        }

    }

    public void RemoveElementList(BuildController buildController)
    {
        int index = -1;
        for (int i = 0; i < listOfBuilds.Count; i++)
        {
            if (listOfBuilds[i] == buildController)
            {
                index = i;
                break;
            }
        }

        if (index >= 0)
        {
            listOfBuilds.RemoveAt(index);
        }

        if (tower != null && listOfBuilds.Count <= 0)
        {
            tower.DestroyedTowerInvoke();
        }
    }

    private void CheckThisMainController()
    {
        if (listOfBuilds.Count < 1)
        {
            SetMainSafe();
        }

    }

    private void SetMainSafe()
    {
        isMainSafe = false;
    }

    public bool GetMainSafe()
    {
        return isMainSafe;
    }

    public void StartDestroyMainController()
    {
        StartCoroutine(DestroyThisMainController());
    }

    private IEnumerator DestroyThisMainController()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    public void DestroyBuildFromUnit(int damage)
    {
        if (listOfBuilds != null && listOfBuilds.Count > 0)
        {
            listOfBuilds[0].TakeDamageFromUnit(damage);
        } else
        {
            SetMainSafe();
        }
    }
}
