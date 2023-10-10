using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Cube�̈ړ����x
    [SerializeField] CinemachineVirtualCameraBase FPSCamera;
    [SerializeField] CinemachineVirtualCameraBase TPSCamera;

    private void Start()
    {
        FPSCamera.gameObject.SetActive(true);
        TPSCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // ���͂��󂯎��ACube���ړ�������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.M))
        {
            TPSCamera.gameObject.SetActive(FPSCamera.gameObject.activeSelf);
            FPSCamera.gameObject.SetActive(!TPSCamera.gameObject.activeSelf);
        }
    }
}
