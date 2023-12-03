using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;

    public Sprite[] itemSprites; // �A�C�e���ɑΉ�����X�v���C�g�̔z��

    private int buttonPushCount; // �{�^���������ꂽ��

    void Start()
    {
        buttonPushCount = 0;
        UpdateImages();
    }

    // �{�^���ŏ��Ԃ����ւ���֐�
    public void ChangeRightItem()
    {
        buttonPushCount++;
        UpdateImages();
    }

    // �{�^���ŏ��Ԃ����ւ���֐�
    public void ChangeLeftItem()
    {
        buttonPushCount--;
        UpdateImages();
    }

    // �C���[�W�̕\�����X�V����֐�
    private void UpdateImages()
    {
        if (itemSprites == null || itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites is not initialized or empty.");
            return;
        }

        int itemCount = itemSprites.Length;

        // �{�^���������ꂽ�񐔂��g���ď��Ԃ�����
        int index1 = Mathf.Abs(buttonPushCount) % itemCount;
        int index2 = Mathf.Abs(buttonPushCount + 1) % itemCount;
        int index3 = Mathf.Abs(buttonPushCount + 2) % itemCount;

        // �e�C���[�W�̕\����؂�ւ�
        image1.sprite = itemSprites[index1];
        image2.sprite = itemSprites[index2];
        image3.sprite = itemSprites[index3];
    }
    public int GetCurrentSlotIndex()
    {
        if (itemSprites == null || itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites is not initialized or empty.");
            return -1; // �G���[�̏ꍇ�͓K�؂ȃG���[�l��Ԃ�
        }

        int itemCount = itemSprites.Length;

        // �{�^���������ꂽ�񐔂��g���ď��Ԃ�����
        int selectedIndex = Mathf.Abs(buttonPushCount) % itemCount;

        // selectedIndex ��Ԃ�
        return selectedIndex;
    }
}
