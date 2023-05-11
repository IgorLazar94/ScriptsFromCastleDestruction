using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBorderBox : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera;
    private float cameraDistance;
    private BoxCollider boxCollider;
    private CinemachineComponentBase cinemachineComponent;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        cinemachineComponent = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    public void CalculateBorders()
    {
        cameraDistance = (cinemachineComponent as CinemachineFramingTransposer).m_CameraDistance;

        boxCollider.center = new Vector3(-5f, (((cameraDistance - 30) / 100) * 20f), 0f);
        float boxCollSize_X = (cameraDistance - 30) * (-14 / 5) + 170;
        float boxCollSize_Y = (cameraDistance - 30) * (-15 / 100) + 20;

        boxCollider.size = new Vector3(boxCollSize_X + 40f, boxCollSize_Y, boxCollider.size.z);
    }
}
