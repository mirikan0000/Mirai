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
            // �����ŏ��҉�ʂȂǂ̏��������s
            // image�摜��\��1P.ver,2P.ver��\��
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }



    public void RoundEnding()
    {
        // ���҂��Ƃɏ������𑝂₷
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
        // �C���[�W�̃A�j���[�V�������Đ�
        //anim.Play("YourAnimationName");
        //// �A�j���[�V�����̒������擾���ďI�����ɌĂԃ��\�b�h���Z�b�g
        //float animationLength = anim.GetClip("YourAnimationName").length;
        //Invoke("OnAnimationFinished", animationLength);
        // ���Ԃ��~
        Time.timeScale = 0.0f;
    }
    private void OnAnimationFinished()
    {
        // �A�j���[�V�������I������玞�Ԃ����ɖ߂�
        Time.timeScale = 1.0f;
    }
}
