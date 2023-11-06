using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrefab : MonoBehaviour
{
    private RectTransform rectTransform;
    public float speed;

    public Image parentImage;  // 親オブジェクトのImageコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        float textHeight = rectTransform.sizeDelta.y;

        // 最初の位置を親オブジェクトのImageの右端にする
        float parentImageWidth = parentImage.rectTransform.sizeDelta.x;
        float textWidth = rectTransform.sizeDelta.x;
        float startPosX = parentImageWidth / 2 + textWidth / 2;
        float startPosY = Random.Range(-parentImage.rectTransform.sizeDelta.y / 2 + textHeight / 2, parentImage.rectTransform.sizeDelta.y / 2 - textHeight / 2);

        rectTransform.localPosition = new Vector2(startPosX, startPosY);
    }

    // Update is called once per frame
    void Update()
    {
        // speedに応じて右から左へ移動
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        // 画面外に出た場合は自身を削除する
        if (rectTransform.localPosition.x + rectTransform.sizeDelta.x / 2 < -parentImage.rectTransform.sizeDelta.x / 2)
        {
            Destroy(this.gameObject);
        }
    }

    // テキストを設定する
    public void SetText(string text)
    {
        // UnityのUI Textコンポーネントを取得し、テキストを設定する
        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}
