using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private UITutorialTextBehaviour tutorialText;
    [SerializeField] private ParticleSystem confettiParticle;

    [SerializeField] private PlayerKingController PlayerKingController;
    [SerializeField] private EnemyKingController enemyKingController;
    [SerializeField] private int curLevel;
    private void Start()
    {
        Time.timeScale = 1;
        curLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void SetWinUI()
    {
        PlayerKingController.StartWinAnimation();
        if (tutorialText != null)
        {
            tutorialText.HideTutorText();
        }

        confettiParticle.Play();
        Invoke("SetWinPanel", 3f);
    }

    private void SetWinPanel()
    {
        winPanel.SetActive(true);
        GameAnalitica.instance.OnLevelComplete(curLevel);
        FBManager.Instance.OnLevelEnded(curLevel);
        SFXSourcesController.instance.MusicOnOff();
        SFXSourcesController.instance.SoundPlay(SoundType.Win);
        Time.timeScale = 0;
    }

    public void SetLoseUI()
    {
        enemyKingController.StartWinAnimation();
        if (tutorialText != null)
        {
            tutorialText.HideTutorText();
        }

        Invoke("SetLosePanel", 2f);
    }

    private void SetLosePanel()
    {
        losePanel.SetActive(true);
        GameAnalitica.instance.OnLevelComplete(curLevel);
        FBManager.Instance.OnLevelEnded(curLevel);
        SFXSourcesController.instance.MusicOnOff();
        SFXSourcesController.instance.SoundPlay(SoundType.Lose);
        Time.timeScale = 0;
    }

}
