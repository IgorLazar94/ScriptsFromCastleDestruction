using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [HideInInspector] public Transform[] dotsList;
    private int dotsNumber;
    private float dotSpacing;
    private float dotMinScale;
    private float dotMaxScale;
    private Vector2 pos;
    private float timeStamp;

    private void Awake()
    {
        GetMainSettings();
    }

    private void Start()
    {
        HideTrajectory();
        PrepareDots();
    }

    public void ShowTrajectory()
    {
        dotsParent.SetActive(true);
    }

    public void HideTrajectory()
    {
        dotsParent.SetActive(false);
    }

    private void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber; 
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots (Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y+ forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp) / 2f;
            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    private void GetMainSettings()
    {
        dotsNumber = GameSettings.Instance.GetDotsNumber();
        dotSpacing = GameSettings.Instance.GetDotSpacing();
        dotMinScale = GameSettings.Instance.GetDotMinScale();
        dotMaxScale = GameSettings.Instance.GetDotMaxScale();
    }
}
