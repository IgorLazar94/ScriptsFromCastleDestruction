using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Threading;

public class FBManager : MonoBehaviour
{
    //public static FBManager instance;
    private static FBManager _instance;

    public static FBManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FBManager>();
            }

            return _instance;
        }
    }

    // Awake function from Unity's MonoBehavior
    void Awake()
    {
      /*  instance = this;
        if (instance != null)
            Destroy(gameObject);*/
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        //DontDestroyOnLoad(this);
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    public void OnLevelEnded(int level)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["Level Number"] = level.ToString();

        FB.LogAppEvent("Level Passed", parameters: tutParams);
        //Debug.LogError("Level Passed " + tutParams);
    }

}
