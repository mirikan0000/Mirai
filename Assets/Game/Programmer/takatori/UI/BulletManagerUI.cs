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

    [Header("UIの右側の数字リスト")] [SerializeField] private List<Sprite> imagesRight;
    [Header("UIの左側の数字リスト")] [SerializeField] private List<Sprite> imagesLeft;
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
        // ここで必要なら更新処理を追加
        UpdateUI();
    }

    private void UpdateUI()
    {
        bulletsRemainingUI = weapon.GetbulletsRemaining();

        // バレットの残弾数を右側と左側に分ける
        int rightDigit = bulletsRemainingUI % 10;
        int leftDigit = bulletsRemainingUI / 10;

        // 画像を表示する
        NorMalBUllet_rightDigitImage.sprite = imagesRight[rightDigit];
        NormalBUllet_leftDigitImage.sprite = imagesLeft[leftDigit];
    }
}
