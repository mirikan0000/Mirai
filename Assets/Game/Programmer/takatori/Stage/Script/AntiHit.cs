using UnityEngine;

public class AntiHit : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] [Header("フィールドダメージ")] private int FieldDamage = 5;
    [SerializeField] private PlayerHealth playerHealth1P;
    [SerializeField] private PlayerHealth playerHealth2P;

    // 前回ダメージを与えた時間
    private float lastDamageTime;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            TryApplyDamage(playerHealth1P);
        }
        else if (other.CompareTag("Player2"))
        {
            TryApplyDamage(playerHealth2P);
        }
    }

    void Start()
    {
        playerHealth1P = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        playerHealth2P = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
        lastDamageTime = Time.time; // ゲーム開始時に初期化
    }

    void TryApplyDamage(PlayerHealth playerHealth)
    {
        // 5秒ごとにダメージを与える
        if (Time.time - lastDamageTime >= 5f)
        {
            // ダメージを与える
            playerHealth.TakeDamage(FieldDamage, false);

            // 前回ダメージを与えた時間を更新
            lastDamageTime = Time.time;
        }
    }
}
