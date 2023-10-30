using UnityEngine;

public class RotateObjectToTarget : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // ��]���x
    public Vector3 targetRotation = new Vector3(0, 0, 180); // �ڕW�̉�]�p�x

    private void Update()
    {
        // �ڕW�̉�]�p�x�Ɍ����ăI�u�W�F�N�g����]������
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
    }
}
