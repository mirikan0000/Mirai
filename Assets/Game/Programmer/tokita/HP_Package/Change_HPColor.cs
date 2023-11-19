using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_HPColor : MonoBehaviour
{
    [SerializeField]private Image image1;//フロント
    [SerializeField]private PlayerHealth playerHealth;

    private void Start()
    {
    }

    private void Update()
    {
        // PlayerHealthから現在のHPを取得
        float currentHP = playerHealth.GetCurrentHP();

        // HPの最大値を取得
        float maxHP = playerHealth.GetMaxHP();

        // HPが最大値より大きい場合は最大値に設定
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        // FillAmountを設定
        float fillAmount = currentHP / maxHP;
        image1.fillAmount = fillAmount;
    }
}
