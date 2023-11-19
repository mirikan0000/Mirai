using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("体力ゲージ")]
    public int maxHP = 100; // 最大HP
    private int currentHP;  // 現在のHP
    [Header("アーマーゲージ")]
    public int maxArmor = 50; // 最大アーマー
    private int currentArmor; // 現在のアーマー
    public bool armorflog;    
    [Header("次シーン遷移時_読み込みシーンリスト")]
    public string loadSceneName;
    public bool isEnd;
    public List<string> unLoadSceneNameList;
    public bool hitflog; //コメントシステム用のフラグ

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        currentHP = maxHP; // 初期HPを設定
        currentArmor = maxArmor; // 初期アーマーを設定
        isEnd = false;
    }

    private void Update()
    {
        hitflog = false;
    }

    public void TakeDamage(int damage, bool useArmor)
    {
        if (useArmor && currentArmor > 0)
        {
            // アーマーを使う場合かつアーマーがある場合はアーマーを削る
            int remainingArmor = Mathf.Max(0, currentArmor - damage);
            int damageToHealth = Mathf.Max(0, damage - currentArmor);

            currentArmor = remainingArmor;
            currentHP -= damageToHealth;
        }
        else
        {
            // アーマーを使わない場合またはアーマーがない場合は直接HPを削る
            currentHP -= damage;
        }

        hitflog = true;

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
        isEnd = true;
    }
}
