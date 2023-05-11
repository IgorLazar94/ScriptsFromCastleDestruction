using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    private int allScenes;
    

    private void Start()
    {
        allScenes = SceneManager.sceneCountInBuildSettings;

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void LoadNextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) == allScenes)
        {
            LoadFirstLevel();
            //coinControll.OnPlayerLevelUp();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            CoinControll.instance.OnPlayerLevelUp();
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        //return
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
        //return
    }











}
