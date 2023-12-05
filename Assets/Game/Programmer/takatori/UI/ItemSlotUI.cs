using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private Vector3[] initialPositions; // �I�u�W�F�N�g�̏������W���L�^����ϐ�

    private int buttonPushCount;

    void Start()
    {
        buttonPushCount = 0;
        RecordInitialPositions(); // �������W���L�^
        UpdateObjects();
    }

    // �{�^���ŏ��Ԃ����ւ���֐�
    public void ChangeRightItem()
    {
        buttonPushCount++;
        UpdateObjects();
    }

    // �{�^���ŏ��Ԃ����ւ���֐�
    public void ChangeLeftItem()
    {
        buttonPushCount--;
        UpdateObjects();
    }

    // �I�u�W�F�N�g�̏������W���L�^����֐�
    private void RecordInitialPositions()
    {
        if (objects == null || objects.Length == 0)
        {
            Debug.LogError("objects is not initialized or empty.");
            return;
        }

        initialPositions = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            initialPositions[i] = objects[i].transform.position;
        }
    }

    // �I�u�W�F�N�g�̈ʒu���X�V����֐�
    private void UpdateObjects()
    {
        if (objects == null || objects.Length == 0)
        {
            Debug.LogError("objects is not initialized or empty.");
            return;
        }

        int itemCount = objects.Length;

        // �{�^���������ꂽ�񐔂��g���ď��Ԃ�����
        int index1 = Mathf.Abs(buttonPushCount) % itemCount;
        int index2 = Mathf.Abs(buttonPushCount + 1) % itemCount;
        int index3 = Mathf.Abs(buttonPushCount + 2) % itemCount;

        // �e�I�u�W�F�N�g�̈ʒu�����炵��
        UpdateObjectPosition(objects[0], index1);
        UpdateObjectPosition(objects[1], index2);
        UpdateObjectPosition(objects[2], index3);
    }

    // �I�u�W�F�N�g�̈ʒu���X�V����֐�
    private void UpdateObjectPosition(GameObject obj, int index)
    {
        obj.transform.position = initialPositions[index];
    }

    public int GetCurrentSlotIndex()
    {
        if (objects == null || objects.Length == 0)
        {
            Debug.LogError("objects is not initialized or empty.");
            return -1;
        }

        int itemCount = objects.Length;
        int selectedIndex = Mathf.Abs(buttonPushCount) % itemCount;
        return selectedIndex;
    }
}
