using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCameraBase FPSCamera;
    [SerializeField] CinemachineVirtualCameraBase TPSCamera;

    private void Start()
    {
        FPSCamera.gameObject.SetActive(true);
        TPSCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TPSCamera.gameObject.SetActive(FPSCamera.gameObject.activeSelf);
            FPSCamera.gameObject.SetActive(!TPSCamera.gameObject.activeSelf);
        }
    }
}
