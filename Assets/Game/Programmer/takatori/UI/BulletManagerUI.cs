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

    [Header("UIの右側の数字リスト")] [SerializeField] private List<Image> imagesRight;
    [Header("UIの右側の数字リスト")] [SerializeField] private List<Image> imagesLeft;
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
