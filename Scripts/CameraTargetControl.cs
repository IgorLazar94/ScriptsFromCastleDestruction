using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Cinemachine;

public class CameraTargetControl : MonoBehaviour
{
    public static Action onPlayerTapCamera;
    public static Action onPlayerDragCamera;

    [SerializeField] private BoxCollider cameraBordersCollider;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform backgroundTransform;
    [SerializeField] private float camSpeedPC = 5.0f;
    [SerializeField] private float camSpeedAndroid = 20.0f;
    //[SerializeField] private float zoomMax = 130f;
    //[SerializeField] private float zoomMin = 30f;
    private Vector3 touch;
    private Bounds cameraBounds;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private bool isCompleteTutorial = false;


    private void OnEnable()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            onPlayerDragCamera += DragPC;
            onPlayerDragCamera += CheckBounds;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            onPlayerDragCamera += DragAndroid;
            onPlayerDragCamera += CheckBounds;

        }
    }

    private void OnDisable()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            onPlayerDragCamera -= DragPC;
            onPlayerDragCamera -= CheckBounds;

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            onPlayerDragCamera -= DragAndroid;
            onPlayerDragCamera -= CheckBounds;
        }
    }

    //private void Start()
    //{
    //    currentZoom = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
    //}
    //private void Touch()
    //{
    //    touch = mainCamera.ScreenToViewportPoint(Input.mousePosition);
    //}


    private void DragPC()
    {
        horizontalMovement = Input.GetAxis("Mouse X");
        verticalMovement = Input.GetAxis("Mouse Y");

        Vector3 movement = new Vector3(-horizontalMovement, -verticalMovement, 0) * camSpeedPC * Time.deltaTime;
        transform.Translate(movement);
        TutorialPhase();
    }

    private void DragAndroid()
    {
        horizontalMovement = Input.GetAxis("Mouse X");
        verticalMovement = Input.GetAxis("Mouse Y");

        Vector3 movement = new Vector3(-horizontalMovement, -verticalMovement, 0) * camSpeedAndroid * Time.deltaTime;
        transform.Translate(movement);
        TutorialPhase();
    }

    private void TutorialPhase()
    {
        if (UITutorialTextBehaviour.CheckPlayerAction != null)
        {
            if (!isCompleteTutorial && UITutorialTextBehaviour.tutorLevelUI == 0)
            {
                UITutorialTextBehaviour.CheckPlayerAction.Invoke();
                isCompleteTutorial = true;
            }
        }
    }

    private void CheckBounds()
    {
        cameraBounds = cameraBordersCollider.bounds;
        float clampedX = Mathf.Clamp(transform.position.x, cameraBounds.min.x, cameraBounds.max.x);
        float clampedY = Mathf.Clamp(transform.position.y, cameraBounds.min.y, cameraBounds.max.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    //private void CheckTargetBorders()
    //{
    //    Vector3 position = gameObject.transform.position;
    //    position.x = Mathf.Clamp(position.x, minX, maxX);
    //    position.y = Mathf.Clamp(position.y, minY, maxY);
    //    transform.position = position;
    //}
}
