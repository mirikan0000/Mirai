using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f; // �}�E�X���x
    public Transform player; // �v���C���[�L�����N�^�[��Transform

    private float rotationX = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�������b�N
    }

    private void Update()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // �v���C���[�𐅕������ɉ�]
        player.Rotate(Vector3.up * mouseX);

        // �J�����𐂒������ɉ�]�i����E������ݒ�j
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
