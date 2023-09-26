using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform target; // プレイヤーのTransform

    public float fireRate = 2.0f; // 弾の発射レート（1秒間に何回発射するか）
    private float nextFireTime = 0.0f; // 次の発射時刻

    private void Update()
    {
        // プレイヤーの方向を向く
        transform.LookAt(target);

        // Eキーを押して発射
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1.0f / fireRate; // 次の発射時刻を設定
        }
    }

    void Shoot()
    {
        // 弾を生成して前方に発射
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10f; // 弾の速度
            Destroy(bullet, 2f); // 2秒後に弾を破棄
        }
    }
}
