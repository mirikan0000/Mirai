using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_HPColor : MonoBehaviour
{
    [SerializeField]
    Image image1;
    Image image2;

    bool mouseButton = false;

    private void Start()
    {
        image2 = GetComponent<Image>();
    }

    private void Update()
    {
        //Fill Amountによってゲージの色を変える
        if(image2.fillAmount > 0.5f)
        {
            image1.color = Color.red;
        }
        else if(image2.fillAmount > 0.2f)
        {
            image1.color = new Color(1f, 0.67f, 0f);
        }
        else
        {
            image1.color = Color.green;
        }

        //マウスを使ってゲージを増減させる
        if (Input.GetMouseButtonDown(0))
        {
            mouseButton = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseButton = false;
        }

        if (mouseButton)
        {
            image2.fillAmount += Time.deltaTime;

        }
        else if (image2.fillAmount > 0f)
        {
            image2.fillAmount -= Time.deltaTime;
        }
    }
}
