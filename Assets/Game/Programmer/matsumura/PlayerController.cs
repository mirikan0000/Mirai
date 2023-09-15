using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �v���C���[�̈ړ����x
    public GameObject bulletPrefab; // �e�̃v���n�u

    private void Update()
    {
        // WASD�ł̈ړ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // �}�E�X�̍��N���b�N�Œe����
        if (Input.GetButtonDown("Fire1")) // "Fire1"�̓f�t�H���g�Ń}�E�X�̍��N���b�N�Ƀ}�b�s���O����Ă��܂�
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �e�𐶐����đO���ɔ���
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10f; // �e�̑��x
            Destroy(bullet, 2f); // 2�b��ɒe��j��
        }
    }
}
