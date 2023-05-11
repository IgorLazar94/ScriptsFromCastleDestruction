using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomCinemachineCamera : MonoBehaviour
{
    [SerializeField] public CameraBorderBox cameraBorderBox;
    [SerializeField] float sensitivityPC = 50f;
    [SerializeField] float sensitivityAndroid = 50f;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineComponentBase componentBase;
    private float cameraDistance;

  
    private float currentZoom;

    private float mod_cameraDistance;

    float fieldOfView;

    float aspectRatio;
    float halfHeight;
    float halfWidth;

    private bool isCompleteTutorial = false;

    [SerializeField] private float zoomMax = 130f;
    [SerializeField] private float zoomMin = 30f;
    private void Start()
    {
        mod_cameraDistance = zoomMax - (zoomMax/5);

        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = mod_cameraDistance;
        }
    }

   


    private void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            ZoomPC();
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            ZoomAndroid();
        }

    }

    private void ZoomPC()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivityPC;
            mod_cameraDistance -= cameraDistance;
            TutorialPhase();
            CheckZoomBorders();
            cameraBorderBox.CalculateBorders();
        }
    }

    private void ZoomAndroid()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distanceTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistanceTouch = (touchZero.position - touchOne.position).magnitude;


            float difference = currentDistanceTouch - distanceTouch;

            cameraDistance = Mathf.Clamp(cameraDistance - (difference * sensitivityAndroid), zoomMin, zoomMax);
            mod_cameraDistance = cameraDistance;
            TutorialPhase();
            CheckZoomBorders();
            cameraBorderBox.CalculateBorders();
        }
    }

    private void CheckZoomBorders()
    {
        if (mod_cameraDistance < zoomMin)
        {
            mod_cameraDistance = zoomMin;
        }
        else if (mod_cameraDistance > zoomMax)
        {
            mod_cameraDistance = zoomMax;
        }
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = mod_cameraDistance;
    }

    private void TutorialPhase()
    {
        if (UITutorialTextBehaviour.CheckPlayerAction != null)
        {
            if (!isCompleteTutorial && UITutorialTextBehaviour.tutorLevelUI == 1)
            {
                UITutorialTextBehaviour.CheckPlayerAction.Invoke();
                isCompleteTutorial = true;
            }
        }
    }
}
