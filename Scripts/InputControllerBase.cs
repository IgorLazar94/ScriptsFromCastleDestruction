using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class InputControllerBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Action onPlayerTap;
    public static Action onPlayerDrag;
    public static Action onPlayerUp;

    public static Action onTriggerTap;

    private bool isTrigerTap = false;
    public static bool trueDirection = false;
    private bool _isReady = false;


    private void OnEnable()
    {
        EventBus.onCreateNewProjectile += enableInput;
        onTriggerTap += SetTrueTrigerTap;
    }

    private void OnDisable()
    {
        EventBus.onCreateNewProjectile -= enableInput;
        onTriggerTap -= SetTrueTrigerTap;
        
    }

    private void SetTrueTrigerTap()
    {
        isTrigerTap = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isReady && isTrigerTap)
        {
            onPlayerTap.Invoke();
        } else
        {
            if (CameraTargetControl.onPlayerTapCamera != null)
            {
                CameraTargetControl.onPlayerTapCamera.Invoke();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isReady && isTrigerTap)
        {
            onPlayerDrag.Invoke();
        } 
        else
        {
            CameraTargetControl.onPlayerDragCamera.Invoke();
        }
    }

  
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isReady && isTrigerTap)
        {
            isTrigerTap = false;
            onPlayerUp.Invoke(); 
            disableInput();
        }
    }

    private void enableInput(GameObject newObject)
    {
        _isReady = true;
    }

    private void disableInput()
    {
        _isReady = false;
    }

    IEnumerator deleyBeforeEnabled()
    {
        yield return new WaitForSeconds(0.3f);
        _isReady = true;
    }

}
