using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletManagerUI : MonoBehaviour
{
    [Header("�v���C���[��Wepon�N���X")] [SerializeField] private Weapon weapon;
    private int bulletMaxUI;
    private int bulletsRemainingUI;

    [Header("UI�̉E���̐������X�g")] [SerializeField] private List<Sprite> imagesRight;
    [Header("UI�̍����̐������X�g")] [SerializeField] private List<Sprite> imagesLeft;
    [SerializeField] private Image NorMalBUllet_rightDigitImage;
    [SerializeField] private Image NormalBUllet_leftDigitImage;


    void Start()
    {
        bulletMaxUI = weapon.GetbulletsMax();
        bulletsRemainingUI = weapon.GetbulletsRemaining();

        UpdateUI();
    }

    void Update()
    {
        // �����ŕK�v�Ȃ�X�V������ǉ�
        UpdateUI();
    }

    private void UpdateUI()
    {
        bulletsRemainingUI = weapon.GetbulletsRemaining();

        // �o���b�g�̎c�e�����E���ƍ����ɕ�����
        int rightDigit = bulletsRemainingUI % 10;
        int leftDigit = bulletsRemainingUI / 10;

        // �摜��\������
        NorMalBUllet_rightDigitImage.sprite = imagesRight[rightDigit];
        NormalBUllet_leftDigitImage.sprite = imagesLeft[leftDigit];
    }
}
