using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    public Transform player1; // プレイヤー1のTransform
    public Transform player2; // プレイヤー2のTransform
    public Text playerPositionText; // プレイヤーの座標を表示するText UI 要素

    void Update()
    {
        // プレイヤー1とプレイヤー2の座標を改行して表示
        string positionText = "1P: " + player1.position.ToString("F1") + "\n" + "2P: " + player2.position.ToString("F1");

        // Text UI 要素に表示
        playerPositionText.text = positionText;
    }
}
