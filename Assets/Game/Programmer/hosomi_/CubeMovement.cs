using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Cubeの移動速度
    public float rotationSpeed = 90.0f; // 回転速度

    private void Start()
    {

    }

    void Update()
    {
        // 入力を受け取り、Cubeを移動させる
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        // Qキーを押すと左に回転
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        // Eキーを押すと右に回転
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
