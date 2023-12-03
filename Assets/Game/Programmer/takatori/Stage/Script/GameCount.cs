using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCount : SingletonMonoBehaviour<GameCount>
{
    public int roundCount = 0;
    private int roundsToWin = 5;
    public int player1Wins = 0;
    public int player2Wins = 0;
    // �ʂ̃N���X����@���g�p���ăf�[�^��ۑ�����
   
    // �V�[����؂�ւ���O�Ƀf�[�^��ۑ�
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
            // �����ŏ��҉�ʂȂǂ̏��������s
            // image�摜��\��1P.ver,2P.ver��\��
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }

    }
    public bool IsGameFinished()
    {
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

}
