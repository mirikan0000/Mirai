using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimer : MonoBehaviour
{
    [SerializeField] private Weapon weapon; // Weapon�N���X�ւ̎Q��
    [SerializeField] private List<Sprite> digitImages; // 0����9�܂ł̐����̉摜
    [SerializeField] private Image reloadCountRight; // �E���̐����\���pImage


    void Update()
    {
        // �����[�h���̂ݕ\��
        if (weapon.isReloading)
        {
            float remainingTime = weapon.elapsedTime;
            UpdateReloadCount(remainingTime);
        }
        else
        {
            // �����[�h���łȂ��ꍇ�A�\�����A�N�e�B�u�ɂ���
     
            reloadCountRight.gameObject.SetActive(false);
      
        }
    }

    void UpdateReloadCount(float remainingTime)
    {
        // �����[�h���Ԃ���b�����擾�i�����_�ȉ����l���j
        int seconds = Mathf.CeilToInt(remainingTime);
         // �E���̐����\��
        int rightDigit = seconds % 10;

        // �����[�h���Ԃ��[���ȉ��̏ꍇ�͉E���̐������[���ɂ���
        if (remainingTime <= 0)
        {
            rightDigit = 0;
        }

        reloadCountRight.sprite = digitImages[rightDigit];

        // �\�����A�N�e�B�u�ɂ���
        reloadCountRight.gameObject.SetActive(true);
    }
}
