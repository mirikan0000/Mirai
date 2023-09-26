using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // 最大HP
    private int currentHP;  // 現在のHP

    private void Start()
    {
        currentHP = maxHP; // 初期HPを設定
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ダメージを受ける
        if (currentHP <= 0)
        {
            Die(); // HPが0以下になったら死亡処理を実行
        }
    }

    void Die()
    {
        // 死亡時の処理をここに記述（例：ゲームオーバー画面の表示など）
        // この例ではプレイヤーオブジェクトを無効にします。
        gameObject.SetActive(false);
    }
}
