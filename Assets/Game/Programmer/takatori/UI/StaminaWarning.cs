using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWarning : MonoBehaviour
{
    public PlayerManager_ player;
    public Image image;

    private void Update()
    {
        // 燃料の量に応じてrotationを計算し、0から90にクランプ
        float fuelRatio = Mathf.Clamp01(player.GetcurrentFuel() / player.GetMaxFuel()); // 燃料の割合（0から1）
        image.fillAmount = Mathf.Lerp(1, 0, fuelRatio); // 0から90の範囲で燃料の割合に応じて回転
        
    }
}
