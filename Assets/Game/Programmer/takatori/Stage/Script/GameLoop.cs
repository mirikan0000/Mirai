using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // 他のクラスや変数の定義などがあれば追加
    [SerializeField] private PlayerHealth Player1;
    [SerializeField] private PlayerHealth Player2;
    private object m_GameWinner;
    private int roundCount = 0; // 現在のラウンド数
    private int roundsToWin = 5; // 勝利に必要なラウンド数
    private int player1Wins = 0; // Player1の勝利数
    private int player2Wins = 0; // Player2の勝利数

    
    private void Start()
    {
     //  StartCoroutine(GameLoops());
    }
    private IEnumerator GameLoops()
    {
        // ゲーム開始
        yield return StartCoroutine(RoundStarting());

        // ラウンドごとのループ
        while (!IsGameFinished())
        {
            Debug.Log("ラウンドループ");
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
        }

        // 勝者が確定したときの処理
        if (m_GameWinner != null)
        {
            // ここで勝者画面などの処理を実行
            //image画像を表示1P.ver,2P.verを表示

            // 一定時間停止してからゲームをリセット
            yield return new WaitForSeconds(3f);
            // NextSceneにGameLoopの参照を渡す
            NextScene nextSceneScript = FindObjectOfType<NextScene>();
            if (nextSceneScript != null)
            {
                nextSceneScript.SetGameLoopReference(this);
            }   
        }
        else
        {
            // ゲームが終了していない場合、次のラウンドへ
            StartCoroutine(GameLoops());
        }
    }
    //ラウンド開始時の初期化処理を行うコルーチン
    private IEnumerator RoundStarting()
    {
        // ラウンド開始時の初期化処理などをここに追加
        Debug.Log("Round Starting...");
        //シーンの初期化
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        // ラウンド表示


       yield return null;
    }
    //ゲームプレイ中の処理を行うコルーチン
    private IEnumerator RoundPlaying()
    {
        // どちらかの戦車が消滅するまで待機
        while (!IsRoundOver())
        {
            yield return null;
        }
    }
    //ラウンド終了時の処理を行うコルーチン
    private IEnumerator RoundEnding()
    {
        // ラウンド終了時の処理をここに追加
        Debug.Log("Round Ending...");

        // 例：勝者の表示など

        // 勝者ごとに勝利数を増やす
        if (Player1.GetCurrentHP() <= 0)
        {
            player2Wins++;
        }
        else if (Player2.GetCurrentHP() <= 0)
        {
            player1Wins++;
        }

        // 一定時間停止してから次のラウンドへ
        yield return new WaitForSeconds(3f);
    }

    // 勝者が確定したかどうかの判定
    public bool IsGameFinished()
    {
        // 例：5ラウンド先取で勝者が確定する場合
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

    // ラウンド終了条件の判定
    private bool IsRoundOver()
    {
        // 例：どちらかの戦車のHPが0になった場合
        return Player1.GetCurrentHP() <= 0 || Player2.GetCurrentHP() <= 0;
    }
}
