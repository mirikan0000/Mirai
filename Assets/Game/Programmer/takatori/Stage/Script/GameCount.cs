using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCount : SingletonMonoBehaviour<GameCount>
{
    public int roundCount = 0;
    public int roundsToWin = 5;
    public int player1Wins = 0;
    public int player2Wins = 0;

    // �����摜
    public GameObject player1WinImage;
    public GameObject player2WinImage;

    // �s�k�摜
    public GameObject player1LoseImage;
    public GameObject player2LoseImage;

    // �G�t�F�N�g1
    public GameObject winEffect1;
    public GameObject winEffect2;
    public GameObject loseEffect1;
    public GameObject loseEffect2;

    // �v���C���[�I�u�W�F�N�g
    public GameObject player1;
    public GameObject player2;
    private bool gameFinished = false; // �Q�[���I���t���O
    // �ʂ̃N���X����@���g�p���ăf�[�^��ۑ�����

    // �V�[����؂�ւ���O�Ƀf�[�^��ۑ�
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinished)
        {
            return; // �Q�[�����I�����Ă���Ώ������X�L�b�v
        }
        if (player1==null|| player2==null)
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
        }

        if (IsGameFinished())
        {
            gameFinished = true;
            // ���҉�ʂȂǂ̏��������s
            // image�摜��\��1P.ver,2P.ver��\��
            StartCoroutine(TransitionAfterDelay());

            // ���҂ɉ����ď����摜�ƃG�t�F�N�g���A�N�e�B�u��
            if (player1Wins >= roundsToWin)
            {
                player1WinImage.SetActive(true);
                winEffect1.SetActive(true);
                player2LoseImage.SetActive(true);
                loseEffect2.SetActive(true);

                // �G�t�F�N�g���v���C���[�̓���ɍ~�点��
                ActivateEffectOnPlayer(winEffect1, player1);
                ActivateEffectOnPlayer(loseEffect2, player2);
            }
            else if (player2Wins >= roundsToWin)
            {
                player2WinImage.SetActive(true);
                winEffect2.SetActive(true);
                player1LoseImage.SetActive(true);
                loseEffect1.SetActive(true);

                // �G�t�F�N�g���v���C���[�̓���ɍ~�点��
                ActivateEffectOnPlayer(winEffect2, player2);
                ActivateEffectOnPlayer(loseEffect1, player1);
            }
        }
    }

    public bool IsGameFinished()
    {
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

 
    // �v���C���[�̓���ɃG�t�F�N�g���A�N�e�B�u������֐�
    void ActivateEffectOnPlayer(GameObject effect, GameObject player)
    {
        Vector3 playerHeadPosition = player.transform.position + Vector3.up * 2f; // ����ɂ��邽�߂ɓK�؂Ȉʒu���v�Z
        Instantiate(effect, playerHeadPosition, Quaternion.identity);
    }
    IEnumerator TransitionAfterDelay()
    {
        yield return new WaitForSeconds(5f); // 5�b�҂�
        UnityEngine.SceneManagement.SceneManager.LoadScene(3); // �V�[���J��

        // �V�[���J�ڌ�A�S�Ẳ摜�ƃG�t�F�N�g���A�N�e�B�u�ɂ���
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
