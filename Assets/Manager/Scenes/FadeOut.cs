using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeOut : MonoBehaviour
{

    public GameObject Panelfade;   //フェードパネルの取得

    Image fadealpha;               //フェードパネルのイメージ取得変数

    private float alpha;           //パネルのalpha値取得変数
    [SerializeField, Range(0.0f, 0.01f)]
    public float alphaSpeed;
     public bool fadeout;          //フェードアウトのフラグ変数
     public bool fadein;

    // Use this for initialization
    void Start()
    {
        
    }
    private void Update()
    {
        if (fadein)
        {
            FadeIn();
        }
    }

    public void Fadeout()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //パネルのイメージ取得
        alpha = fadealpha.color.a;                 //パネルのalpha値を取得
        fadeout = true;                             //シーン読み込み時にフェードインさせる
        alpha += alphaSpeed;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            fadeout = false;
        }
    }
    public void FadeIn()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //パネルのイメージ取得
        alpha = fadealpha.color.a;                 //パネルのalpha値を取得
        fadeout = true;                             //シーン読み込み時にフェードインさせる
   
            alpha -= alphaSpeed;
            fadealpha.color = new Color(0, 0, 0, alpha);
        
       
        if (alpha <= 0)
        {
            fadein = false;
        }
    }

}
