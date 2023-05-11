using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalitica : MonoBehaviour
{
    public static GameAnalitica instance;

    private void Awake()
    {
        instance = this;
        if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void OnLevelComplete(int _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level");
        Debug.Log("Level: " + _level); 
    }
}
