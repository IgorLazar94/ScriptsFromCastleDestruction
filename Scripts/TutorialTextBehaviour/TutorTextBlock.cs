using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TutorTextBlock : MonoBehaviour
{

    private TextMeshProUGUI tutorText;
    private Image arrowIcon;

    public static int tutorLevelFromObjects = 0;
    public static Action OnTutorObjectsActivate;

    private void Start()
    {
        tutorLevelFromObjects = 0;
        tutorText = GetComponentInChildren<TextMeshProUGUI>();
        arrowIcon = GetComponentInChildren<Image>();
    }

    public void ShowTutorText()
    {
        if (arrowIcon != null)
        {
            //arrowIcon.DOColor(new Color(1, 1, 1, 1), 0.5f);
            arrowIcon.DOFade(1.0f, 0.5f);
            arrowIcon.rectTransform.DOMove(new Vector3(
                                                       arrowIcon.rectTransform.position.x,
                                                       arrowIcon.rectTransform.position.y + 1.5f,
                                                       arrowIcon.rectTransform.position.z), 1f)
                                                       .SetLoops(-1).
            OnComplete(() => arrowIcon.rectTransform.DOMove(new Vector3(
                                                       arrowIcon.rectTransform.position.x,
                                                       arrowIcon.rectTransform.position.y - 1.5f,
                                                       arrowIcon.rectTransform.position.z), 1f)
                                                       .SetLoops(-1));
        }

        if (tutorText != null)
        {
            var rectTransform = tutorText.gameObject.GetComponent<RectTransform>();
            rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
                .OnComplete(() => rectTransform.DOScale(Vector3.one, 0.25f));
            tutorText.DOColor(new Color(1, 1, 1, 1), 0.5f);
        }
    }

    public void HideTutorText()
    {
        if (tutorText != null)
        {
            var rectTransform = tutorText.gameObject.GetComponent<RectTransform>();
            tutorLevelFromObjects++;
            OnTutorObjectsActivate.Invoke();
            if (arrowIcon != null)
            {
                arrowIcon.DOFade(0.0f, 0.25f);
            }

            tutorText.DOColor(new Color(1, 1, 1, 0), 0.25f);
            rectTransform.DOScale(Vector3.zero, 0.25f);
            Invoke("DestroyTutorText", 1f);
        }
    }

    private void DestroyTutorText()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }

}
