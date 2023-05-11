using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UITutorialTextBehaviour : MonoBehaviour
{

    private List<TextMeshProUGUI> tutorList = new List<TextMeshProUGUI>();
    [HideInInspector] public static int tutorLevelUI;
    [SerializeField] PushController pushController;

    public static Action CheckPlayerAction;
    private float delayForHideTutor = 5.0f;

    private void OnEnable()
    {
        CheckPlayerAction += RightPlayerAction;
    }

    private void OnDisable()
    {
        CheckPlayerAction -= RightPlayerAction;
    }

    private void Start()
    {
        tutorLevelUI = 0;
        FillTutorList();
        //tutorList[0].enabled = true;
        TextEnableBehaviour(tutorList[0]);
    }

    private void UpdateTutorText(TextMeshProUGUI activeText)
    {
        var nextTextIndex = tutorList.IndexOf(activeText) + 1;


        //activeText.enabled = false;
        TextDisableBehaviour(activeText);
        TextEnableBehaviour(tutorList[nextTextIndex]);
        //tutorList[nextTextIndex].enabled = true;
        tutorLevelUI++;

        if (tutorLevelUI == tutorList.Count - 1)
        {
            pushController.ShowPushTutorial();
            Invoke("HideTutorText", delayForHideTutor);
        }
        //}
    }

    private void RightPlayerAction()
    {
        UpdateTutorText(tutorList[tutorLevelUI]);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //CheckPlayerAction();
    //    }
    //}

    private void FillTutorList()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tutorList.Add(gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
        }
    }

    public void HideTutorText()
    {
        gameObject.SetActive(false);
    }

    private void TextEnableBehaviour(TextMeshProUGUI Text)
    {
        if (Text.GetComponentInChildren<Image>() != null)
        {
            var image = Text.GetComponentInChildren<Image>();
            image.DOFade(1f, 1f);
        }

        var rectTransform = Text.gameObject.GetComponent<RectTransform>();
        rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
            .OnComplete(() => rectTransform.DOScale(Vector3.one, 0.25f));
        Text.DOColor(new Color(1, 1, 1, 1), 0.5f);
    }

    private void TextDisableBehaviour(TextMeshProUGUI Text)
    {
        if (Text.GetComponentInChildren<Image>() != null)
        {
            var image = Text.GetComponentInChildren<Image>();
            image.DOFade(0f, 0.25f);
        }
        var rectTransform = Text.gameObject.GetComponent<RectTransform>();

        Text.DOColor(new Color(1, 1, 1, 0), 0.25f);
        rectTransform.DOScale(Vector3.zero, 0.25f);
    }

















}
