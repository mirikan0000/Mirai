using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCount2 : MonoBehaviour
{
    [SerializeField] private Image image;

    // ゲームカウントの参照
    private GameCount gameCount;

    void Start()
    {
        // GameCountオブジェクトを検索して、そのコンポーネントを取得
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
        // GameCountが見つかっていればFillAmountを更新
        if (gameCount != null)
        {
            // ゲーム勝利数に応じてFillAmountを更新
            float fillAmount = (float)gameCount.player2Wins / gameCount.roundsToWin;
            image.fillAmount = fillAmount;
        }
    }
}
