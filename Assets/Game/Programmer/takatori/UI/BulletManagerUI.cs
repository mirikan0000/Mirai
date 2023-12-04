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

    [Header("UI�̉E���̐������X�g")] [SerializeField] private List<Image> imagesRight;
    [Header("UI�̉E���̐������X�g")] [SerializeField] private List<Image> imagesLeft;
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

    }
}
