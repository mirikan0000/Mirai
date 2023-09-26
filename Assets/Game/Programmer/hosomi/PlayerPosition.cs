using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    public Transform player1; // �v���C���[1��Transform
    public Transform player2; // �v���C���[2��Transform
    public Text playerPositionText; // �v���C���[�̍��W��\������Text UI �v�f

    void Update()
    {
        // �v���C���[1�ƃv���C���[2�̍��W�����s���ĕ\��
        string positionText = "1P: " + player1.position.ToString("F1") + "\n" + "2P: " + player2.position.ToString("F1");

        // Text UI �v�f�ɕ\��
        playerPositionText.text = positionText;
    }
}
