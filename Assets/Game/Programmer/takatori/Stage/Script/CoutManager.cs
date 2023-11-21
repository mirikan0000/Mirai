using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoutManager : MonoBehaviour
{
    //private object m_GameWinner;
    //private int roundCount = 0;
    //private int roundsToWin = 5;
    //[SerializeField] private int player1Wins = 0;
    //[SerializeField] private int player2Wins = 0;
    //private bool isRoundEnding_Cout = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameLoop.Instance.isRoundEnding && GameLoop.Instance.IsRoundOver())
        {
            // ゲームが終了していない場合、次のラウンドを開始
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    

    }
    

}
