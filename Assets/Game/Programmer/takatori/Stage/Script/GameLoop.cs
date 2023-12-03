using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    private PlayerHealth Player1;
    private PlayerHealth Player2;
    private object m_GameWinner;
    private int roundCount = 0;
    private int roundsToWin = 5;
    [SerializeField]  private int player1Wins = 0;
    [SerializeField] private int player2Wins = 0;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Image image;
    [SerializeField] private Animation anim;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
   
    }

    private void Update()
    {
        if (player1==null||player2==null)
        {
            InitializePlayers();
        }
        if (player1 != null || player2 != null)
        {
            GameLoops();
        }
    
    }
    public void GameLoops()
    {
        if (IsRoundOver())
        {
            RoundEnding();
        }

        if (IsGameFinished())
        {
            // ここで勝者画面などの処理を実行
            // image画像を表示1P.ver,2P.verを表示
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }



    public void RoundEnding()
    {
        // 勝者ごとに勝利数を増やす
        if (Player1.GetCurrentHP() <= 0)
        {
            player2Wins++;
        }
        else if (Player2.GetCurrentHP() <= 0)
        {
            player1Wins++;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public bool IsGameFinished()
    {
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

    public bool IsRoundOver()
    {
        return Player1.GetCurrentHP() <= 0 || Player2.GetCurrentHP() <= 0;
    }
    private void InitializePlayers()
    {

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player1 != null)
        {
            Player1 = player1.GetComponent<PlayerHealth>();
        }
        if (player2 != null)
        {
            Player2 = player2.GetComponent<PlayerHealth>();
        }
        // イメージのアニメーションを再生
        //anim.Play("YourAnimationName");
        //// アニメーションの長さを取得して終了時に呼ぶメソッドをセット
        //float animationLength = anim.GetClip("YourAnimationName").length;
        //Invoke("OnAnimationFinished", animationLength);
        // 時間を停止
        Time.timeScale = 0.0f;
    }
    private void OnAnimationFinished()
    {
        // アニメーションが終わったら時間を元に戻す
        Time.timeScale = 1.0f;
    }
}
