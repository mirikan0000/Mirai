using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private Vector3[] initialPositions; // オブジェクトの初期座標を記録する変数

    private int buttonPushCount;

    void Start()
    {
        buttonPushCount = 0;
        RecordInitialPositions(); // 初期座標を記録
     
        UpdateObjects();
    }

    // ボタンで順番を入れ替える関数
    public void ChangeRightItem()
    {
        buttonPushCount++;
        UpdateObjects();
    }

    // ボタンで順番を入れ替える関数
    public void ChangeLeftItem()
    {
        buttonPushCount--;
        UpdateObjects();
    }

    // オブジェクトの初期座標を記録する関数
    private void RecordInitialPositions()
    {
        if (objects == null || objects.Length == 0)
        {
            return;
        }

        initialPositions = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            initialPositions[i] = objects[i].transform.position;
        }
    }

    // オブジェクトの位置を更新する関数
    private void UpdateObjects()
    {
        if (objects == null || objects.Length == 0)
        {
            return;
        }

        int itemCount = objects.Length;

        // ボタンが押された回数を使って順番を決定
        // ボタンが押された回数を使って順番を決定
        int index1 = ModuloWithNegative(buttonPushCount, itemCount);
        int index2 = ModuloWithNegative(buttonPushCount + 1, itemCount);
        int index3 = ModuloWithNegative(buttonPushCount + 2, itemCount);

        // 各オブジェクトの位置をずらした
        UpdateObjectPosition(objects[0], index1);
        UpdateObjectPosition(objects[1], index2);
        UpdateObjectPosition(objects[2], index3);
    }

    // オブジェクトの位置を更新する関数
    private void UpdateObjectPosition(GameObject obj, int index)
    {
        if (index >= 0 && index < initialPositions.Length)
        {
            obj.transform.position = initialPositions[index];
        }
    }

    public int GetCurrentSlotIndex()
    {
        if (objects == null || objects.Length == 0)
        {
            return -1;
        }

        int itemCount = objects.Length;
        int selectedIndex = Mathf.Abs(buttonPushCount) % itemCount;
        return selectedIndex;
    }
    private int ModuloWithNegative(int a, int b)
    {
        return (a % b + b) % b;
    }
}
