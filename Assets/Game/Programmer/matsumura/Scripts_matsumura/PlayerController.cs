using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // プレイヤーの移動速度
    public GameObject bulletPrefab; // 弾のプレハブ

    private void Update()
    {
        // WASDでの移動
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // マウスの左クリックで弾発射
        if (Input.GetButtonDown("Fire1")) // "Fire1"はデフォルトでマウスの左クリックにマッピングされています
        {
            Shoot();
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
