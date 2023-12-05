using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingEye : MonoBehaviour
{
    public Sprite[] images; // 切り替える画像の配列

    private Image imageComponent;
    private bool isBlinking = false;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        // 初回の瞬きを開始
        StartBlink();
    }

    private void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            // 画像を切り替える
            imageComponent.sprite = images[0];
            yield return new WaitForSeconds(0.3f);

            imageComponent.sprite = images[1];
            yield return new WaitForSeconds(0.3f);

            // ランダムな間隔で次の瞬きまで待機
            float randomInterval = Random.Range(0.5f, 4f);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
