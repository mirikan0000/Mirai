using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform target; // �v���C���[��Transform

    public float fireRate = 2.0f; // �e�̔��˃��[�g�i1�b�Ԃɉ��񔭎˂��邩�j
    private float nextFireTime = 0.0f; // ���̔��ˎ���

    private void Update()
    {
        // �v���C���[�̕���������
        transform.LookAt(target);

        // E�L�[�������Ĕ���
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1.0f / fireRate; // ���̔��ˎ�����ݒ�
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
