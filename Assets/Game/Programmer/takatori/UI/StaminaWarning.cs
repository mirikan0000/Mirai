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
        // �R���̗ʂɉ�����rotation���v�Z���A0����90�ɃN�����v
        float fuelRatio = Mathf.Clamp01(player.GetcurrentFuel() / player.GetMaxFuel()); // �R���̊����i0����1�j
        image.fillAmount = Mathf.Lerp(1, 0, fuelRatio); // 0����90�͈̔͂ŔR���̊����ɉ����ĉ�]
        
    }
}
