using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    private PlayerHealth Player1;
    private PlayerHealth Player2;
    [SerializeField] private GameCount count;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField]
    private Animator animator;
    [SerializeField] private Image roundImage;

    public Sprite[] roundSprites; // ���E���h���Ƃ� Image ���i�[����z��
    private void Start()
    {
        UpdateRoundImage(); // ���������Ƀ��E���h�ɉ����� Image ��\��
    }

    private void Update()
    {
        if (player1==null||player2==null)
        {
            InitializePlayers();
        }
        if (player1 != null || player2 != null)
        {
            animator.SetBool("Bigin",false);
            GameLoops();
        }
    
    }
    public void GameLoops()
    {
        if (IsRoundOver())
        {
            RoundEnding();
        }

    }
    public void RoundEnding()
    {
        // ���҂��Ƃɏ������𑝂₷
        if (Player1.GetCurrentHP() <= 0)
        {
            count.player2Wins++;
        }
        else if (Player2.GetCurrentHP() <= 0)
        {
            count.player1Wins++;
        }
        count.roundCount++;
        UpdateRoundImage(); // ���E���h���I�������� Image ���X�V
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
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

        animator.Play("Bigin");

    }
    // �A�j���[�V�����I�����ɌĂяo����郁�\�b�h
    private void UpdateRoundImage()
    {
        if (count!=null)
        {
            GameCount[] gameCountObjects = GameObject.FindGameObjectsWithTag("GameCount")
                                              .Select(obj => obj.GetComponent<GameCount>())
                                              .Where(gc => gc != null)
                                              .ToArray();
            if (gameCountObjects.Length > 0)
            {
                count = gameCountObjects[0];
            }

        }
        Debug.Log("���E���h�J�E���g�I"+count.roundCount);
        // ���E���h�����z��͈͓̔��Ɏ��܂�悤�ɃN�����v
        roundImage.sprite = roundSprites[count.roundCount];
    }

}
