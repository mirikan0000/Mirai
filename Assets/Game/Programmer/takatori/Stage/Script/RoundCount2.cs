using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCount2 : MonoBehaviour
{
    [SerializeField] private Image image;

    // �Q�[���J�E���g�̎Q��
    private GameCount gameCount;

    void Start()
    {
        // GameCount�I�u�W�F�N�g���������āA���̃R���|�[�l���g���擾
        GameObject gameCountObject = GameObject.FindWithTag("GameCount");
        if (gameCountObject != null)
        {
            gameCount = gameCountObject.GetComponent<GameCount>();
        }
        else
        {
            Debug.LogWarning("GameCount not found!");
        }
    }

    void Update()
    {
        // GameCount���������Ă����FillAmount���X�V
        if (gameCount != null)
        {
            // �Q�[���������ɉ�����FillAmount���X�V
            float fillAmount = (float)gameCount.player2Wins / gameCount.roundsToWin;
            image.fillAmount = fillAmount;
        }
    }
}
