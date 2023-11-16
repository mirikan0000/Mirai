using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // 最大HP
    private int currentHP;  // 現在のHP

    [Header("次シーン遷移時_読み込みシーンリスト")]
  //  public List<string> loadSceneNameList;
    public string loadSceneName;
    public bool isEnd;
    public List<string> unLoadSceneNameList;
    public bool hitflog; //コメントシステム用のフラグ

    private void Start()
    {
        currentHP = maxHP; // 初期HPを設定
        isEnd = false;
    }
    private void Update()
    {
        hitflog = false;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ダメージを受ける
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
