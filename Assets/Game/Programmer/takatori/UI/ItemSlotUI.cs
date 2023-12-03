using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;

    public Sprite[] itemSprites; // アイテムに対応するスプライトの配列

    private int buttonPushCount; // ボタンが押された回数

    void Start()
    {
        buttonPushCount = 0;
        UpdateImages();
    }

    // ボタンで順番を入れ替える関数
    public void ChangeRightItem()
    {
        buttonPushCount++;
        UpdateImages();
    }

    // ボタンで順番を入れ替える関数
    public void ChangeLeftItem()
    {
        buttonPushCount--;
        UpdateImages();
    }

    // イメージの表示を更新する関数
    private void UpdateImages()
    {
        if (itemSprites == null || itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites is not initialized or empty.");
            return;
        }

        int itemCount = itemSprites.Length;

        // ボタンが押された回数を使って順番を決定
        int index1 = Mathf.Abs(buttonPushCount) % itemCount;
        int index2 = Mathf.Abs(buttonPushCount + 1) % itemCount;
        int index3 = Mathf.Abs(buttonPushCount + 2) % itemCount;

        // 各イメージの表示を切り替え
        image1.sprite = itemSprites[index1];
        image2.sprite = itemSprites[index2];
        image3.sprite = itemSprites[index3];
    }
    public int GetCurrentSlotIndex()
    {
        if (itemSprites == null || itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites is not initialized or empty.");
            return -1; // エラーの場合は適切なエラー値を返す
        }

        int itemCount = itemSprites.Length;

        // ボタンが押された回数を使って順番を決定
        int selectedIndex = Mathf.Abs(buttonPushCount) % itemCount;

        // selectedIndex を返す
        return selectedIndex;
    }
}
