using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Cube�̈ړ����x

    void Update()
    {
        // ���͂��󂯎��ACube���ړ�������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }
}
