using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimer : MonoBehaviour
{
    [SerializeField] private Weapon weapon; // Weaponクラスへの参照
    [SerializeField] private List<Sprite> digitImages; // 0から9までの数字の画像
    [SerializeField] private Image reloadCountRight; // 右側の数字表示用Image


    void Update()
    {
        // リロード中のみ表示
        if (weapon.isReloading)
        {
            float remainingTime = weapon.elapsedTime;
            UpdateReloadCount(remainingTime);
        }
        else
        {
            // リロード中でない場合、表示を非アクティブにする
     
            reloadCountRight.gameObject.SetActive(false);
      
        }
    }

    void UpdateReloadCount(float remainingTime)
    {
        // リロード時間から秒数を取得（小数点以下も考慮）
        int seconds = Mathf.CeilToInt(remainingTime);
         // 右側の数字表示
        int rightDigit = seconds % 10;

        // リロード時間がゼロ以下の場合は右側の数字をゼロにする
        if (remainingTime <= 0)
        {
            rightDigit = 0;
        }

        reloadCountRight.sprite = digitImages[rightDigit];

        // 表示をアクティブにする
        reloadCountRight.gameObject.SetActive(true);
    }
}
