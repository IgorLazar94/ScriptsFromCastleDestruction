using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI readyText;

    public System.Action onStartReloaded;

    private float reloadTime;
    private Image timerImage;
    private float timeLeft = 0f;
    private SpawnPlayerProjectiles spawnPlayerProjectiles;

    //private Sequence seq;

    private void Start()
    {
        spawnPlayerProjectiles = gameObject.GetComponent<SpawnPlayerProjectiles>();
        timerImage = GetComponentInChildren<Image>();
        //seq = DOTween.Sequence();
    }


    private void OnEnable()
    {
        onStartReloaded += StartTimer;
    }

    private void OnDisable()
    {
        onStartReloaded -= StartTimer;
    }

    private IEnumerator StartOn()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(timeLeft / reloadTime, 0.0f, 1.0f);
            timerImage.fillAmount = normalizedValue;
            yield return null;
        }

        if (timeLeft <= 0)
        {
            ShowReadyText();
        }
    }

    private void StartTimer()
    {
        reloadTime = spawnPlayerProjectiles.reloadTime;
        timeLeft = reloadTime;
        StartCoroutine(StartOn());
    }

    private void ShowReadyText()
    {
        //readyText.gameObject.transform.DOScale(Vector3.one, 0.25f); 
        //DOShakePosition(1.5f, 1f, 5, 90, false);

        //seq.Append(readyText.transform.DOScale(Vector3.one, 0.25f));
        //seq.Append(readyText.transform.DOShakePosition(1.5f, 1f, 5, 90, false));
        //seq.Append(readyText.transform.DOScale(Vector3.zero, 0.25f));
        StartCoroutine(HideReadyText());
        readyText.transform.DOScale(Vector3.one, 0.25f).
            OnComplete(() => readyText.transform.DOShakePosition(1.5f, 1f, 10, 180, false));
            //.OnComplete(() => readyText.transform.DOScale(Vector3.zero, 0.25f));
    }

    private IEnumerator HideReadyText()
    {
        yield return new WaitForSeconds(1.75f);
        readyText.transform.DOScale(Vector3.zero, 0.25f);
    }
}
