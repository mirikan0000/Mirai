using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCount : SingletonMonoBehaviour<GameCount>
{
    public int roundCount = 0;
    public int roundsToWin = 5;
    public int player1Wins = 0;
    public int player2Wins = 0;

    // 勝利画像
    public GameObject player1WinImage;
    public GameObject player2WinImage;

    // 敗北画像
    public GameObject player1LoseImage;
    public GameObject player2LoseImage;

    // エフェクト1
    public GameObject winEffect1;
    public GameObject winEffect2;
    public GameObject loseEffect1;
    public GameObject loseEffect2;

    // プレイヤーオブジェクト
    public GameObject player1;
    public GameObject player2;
    private bool gameFinished = false; // ゲーム終了フラグ
    // 別のクラスや方法を使用してデータを保存する

    // シーンを切り替える前にデータを保存
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinished)
        {
            return; // ゲームが終了していれば処理をスキップ
        }
        if (player1==null|| player2==null)
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
        }

        if (IsGameFinished())
        {
            gameFinished = true;
            // 勝者画面などの処理を実行
            // image画像を表示1P.ver,2P.verを表示
            StartCoroutine(TransitionAfterDelay());

            // 勝者に応じて勝利画像とエフェクトをアクティブ化
            if (player1Wins >= roundsToWin)
            {
                player1WinImage.SetActive(true);
                winEffect1.SetActive(true);
                player2LoseImage.SetActive(true);
                loseEffect2.SetActive(true);

                // エフェクトをプレイヤーの頭上に降らせる
                ActivateEffectOnPlayer(winEffect1, player1);
                ActivateEffectOnPlayer(loseEffect2, player2);
            }
            else if (player2Wins >= roundsToWin)
            {
                player2WinImage.SetActive(true);
                winEffect2.SetActive(true);
                player1LoseImage.SetActive(true);
                loseEffect1.SetActive(true);

                // エフェクトをプレイヤーの頭上に降らせる
                ActivateEffectOnPlayer(winEffect2, player2);
                ActivateEffectOnPlayer(loseEffect1, player1);
            }
        }
    }

    public bool IsGameFinished()
    {
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

 
    // プレイヤーの頭上にエフェクトをアクティブ化する関数
    void ActivateEffectOnPlayer(GameObject effect, GameObject player)
    {
        Vector3 playerHeadPosition = player.transform.position + Vector3.up * 2f; // 頭上にするために適切な位置を計算
        Instantiate(effect, playerHeadPosition, Quaternion.identity);
    }
    IEnumerator TransitionAfterDelay()
    {
        yield return new WaitForSeconds(5f); // 5秒待つ
        UnityEngine.SceneManagement.SceneManager.LoadScene(3); // シーン遷移

        // シーン遷移後、全ての画像とエフェクトを非アクティブにする
        player1WinImage.SetActive(false);
        player2WinImage.SetActive(false);
        player1LoseImage.SetActive(false);
        player2LoseImage.SetActive(false);
        winEffect1.SetActive(false);
        winEffect2.SetActive(false);
        loseEffect1.SetActive(false);
        loseEffect2.SetActive(false);
    }
}
