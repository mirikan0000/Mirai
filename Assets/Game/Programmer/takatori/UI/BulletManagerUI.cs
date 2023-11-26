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

    [Header("UI�̍����̐���")] [SerializeField]private TextMeshProUGUI BulletsumLeftImage;
    [Header("UI�̉E���̐���")] [SerializeField]private TextMeshProUGUI BulletMaxRightImage;
    void Start()
    {
        bulletMaxUI = weapon.GetbulletsMax();
        bulletsRemainingUI = weapon.GetbulletsRemaining();
    }
    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        bulletsRemainingUI = weapon.GetbulletsRemaining();
        BulletsumLeftImage.text = bulletsRemainingUI.ToString();
        BulletMaxRightImage.text = bulletMaxUI.ToString();
    }
}
