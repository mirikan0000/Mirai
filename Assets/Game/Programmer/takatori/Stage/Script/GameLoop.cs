using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // ���̃N���X��ϐ��̒�`�Ȃǂ�����Βǉ�
    [SerializeField] private PlayerHealth Player1;
    [SerializeField] private PlayerHealth Player2;
    private object m_GameWinner;
    private int roundCount = 0; // ���݂̃��E���h��
    private int roundsToWin = 5; // �����ɕK�v�ȃ��E���h��
    private int player1Wins = 0; // Player1�̏�����
    private int player2Wins = 0; // Player2�̏�����

    
    private void Start()
    {
     //  StartCoroutine(GameLoops());
    }
    private IEnumerator GameLoops()
    {
        // �Q�[���J�n
        yield return StartCoroutine(RoundStarting());

        // ���E���h���Ƃ̃��[�v
        while (!IsGameFinished())
        {
            Debug.Log("���E���h���[�v");
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
        }

        // ���҂��m�肵���Ƃ��̏���
        if (m_GameWinner != null)
        {
            // �����ŏ��҉�ʂȂǂ̏��������s
            //image�摜��\��1P.ver,2P.ver��\��

            // ��莞�Ԓ�~���Ă���Q�[�������Z�b�g
            yield return new WaitForSeconds(3f);
            // NextScene��GameLoop�̎Q�Ƃ�n��
            NextScene nextSceneScript = FindObjectOfType<NextScene>();
            if (nextSceneScript != null)
            {
                nextSceneScript.SetGameLoopReference(this);
            }   
        }
        else
        {
            // �Q�[�����I�����Ă��Ȃ��ꍇ�A���̃��E���h��
            StartCoroutine(GameLoops());
        }
    }
    //���E���h�J�n���̏������������s���R���[�`��
    private IEnumerator RoundStarting()
    {
        // ���E���h�J�n���̏����������Ȃǂ������ɒǉ�
        Debug.Log("Round Starting...");
        //�V�[���̏�����
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        // ���E���h�\��


       yield return null;
    }
    //�Q�[���v���C���̏������s���R���[�`��
    private IEnumerator RoundPlaying()
    {
        // �ǂ��炩�̐�Ԃ����ł���܂őҋ@
        while (!IsRoundOver())
        {
            yield return null;
        }
    }
    //���E���h�I�����̏������s���R���[�`��
    private IEnumerator RoundEnding()
    {
        // ���E���h�I�����̏����������ɒǉ�
        Debug.Log("Round Ending...");

        // ��F���҂̕\���Ȃ�

        // ���҂��Ƃɏ������𑝂₷
        if (Player1.GetCurrentHP() <= 0)
        {
            player2Wins++;
        }
        else if (Player2.GetCurrentHP() <= 0)
        {
            player1Wins++;
        }

        // ��莞�Ԓ�~���Ă��玟�̃��E���h��
        yield return new WaitForSeconds(3f);
    }

    // ���҂��m�肵�����ǂ����̔���
    public bool IsGameFinished()
    {
        // ��F5���E���h���ŏ��҂��m�肷��ꍇ
        return player1Wins >= roundsToWin || player2Wins >= roundsToWin;
    }

    // ���E���h�I�������̔���
    private bool IsRoundOver()
    {
        // ��F�ǂ��炩�̐�Ԃ�HP��0�ɂȂ����ꍇ
        return Player1.GetCurrentHP() <= 0 || Player2.GetCurrentHP() <= 0;
    }
}
