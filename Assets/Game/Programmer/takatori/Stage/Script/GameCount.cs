using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCount : SingletonMonoBehaviour<GameCount>
{
    public int roundCount = 0;
    private int roundsToWin = 5;
    public int player1Wins = 0;
    public int player2Wins = 0;
    // 別のクラスや方法を使用してデータを保存する
   
    // シーンを切り替える前にデータを保存
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);  
    }

    // Update is called once per frame
    void Update()
    {

        if (IsGameFinished())
        {
            // ここで勝者画面などの処理を実行
            // image画像を表示1P.ver,2P.verを表示
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }

    }
    public bool IsGameFinished()
    {
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

}
