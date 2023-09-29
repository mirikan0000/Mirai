using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // 最大HP
    private int currentHP;  // 現在のHP

    [Header("次シーン遷移時_読み込みシーンリスト")]
    public List<string> loadSceneNameList;
    private bool isDestroyScene;
    private IEnumerable<string> unLoadSceneNameList;
    private string loadSceneName;

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
        SceneManager.Instance.LoadScene("EndScene");

        if (isDestroyScene)
        {

            // シーン削除
            foreach (string unLoadSceneName in unLoadSceneNameList)
            {
                if (unLoadSceneName != loadSceneName)
                {
                    foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                    {
                        if (scene.name == unLoadSceneName)
                        {
                            SceneManager.Instance.DestroyScene(unLoadSceneName);
                        }
                    }
                }
            }
            isDestroyScene = false;
        }
    }
}
