using UnityEngine;

public class Bulletkari : MonoBehaviour
{
    public int damage = 20; // 弾のダメージ

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーにダメージを与える
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage,true);
            // 弾丸を消す
            Destroy(gameObject);
        }

      
    }
}
