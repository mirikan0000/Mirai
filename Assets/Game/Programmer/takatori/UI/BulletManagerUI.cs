using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletManagerUI : MonoBehaviour
{
    [Header("プレイヤーのWeponクラス")] [SerializeField] private Weapon weapon;
    private int bulletMaxUI;
    private int bulletsRemainingUI;

    [Header("UIの左側の数字")] [SerializeField]private TextMeshProUGUI BulletsumLeftImage;
    [Header("UIの右側の数字")] [SerializeField]private TextMeshProUGUI BulletMaxRightImage;
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
