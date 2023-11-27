using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Cube�̈ړ����x
    public float rotationSpeed = 90.0f; // ��]���x

    private void Start()
    {

    }

    void Update()
    {
        // ���͂��󂯎��ACube���ړ�������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        // Q�L�[�������ƍ��ɉ�]
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        // E�L�[�������ƉE�ɉ�]
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
