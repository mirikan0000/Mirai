using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    [Header("フェードアウト関係")]
    public Image fadePanel;
    public float fadeDeration = 1.0f;
    public bool fadein;

    [Header("Manager関係")]
    private GameObject managerObj;
    private Tittle_Manager managerScript;

    void Start()
    {
        //フェード管理フラグ初期化
        fadein = false;

        //Managerのスクリプト取得
        managerObj = GameObject.Find("Manager");
        managerScript = managerObj.GetComponent<Tittle_Manager>();
    }

    void Update()
    {
        if (fadein == true)
        {
            CallCoroutine();
        }
    }

    //画面をフェードアウト
    public void CallCoroutine()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        //パネルを有効化
        fadePanel.enabled = true;

        //経過時間初期化
        float elapsedTime = 0.0f;

        //フェードパネルの開始色を取得
        Color startColor = fadePanel.color;

        //フェードパネルの最終色を設定
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        //フェードアウトアニメーションを実行
        while (elapsedTime < fadeDeration)
        {
            //経過時間計測
            elapsedTime += Time.deltaTime;

            //フェードの進行度を計算
            float t = Mathf.Clamp01(elapsedTime / fadeDeration);

            //パネルの色を変更してフェードアウト
            fadePanel.color = Color.Lerp(startColor, endColor, t);

            //１フレーム待機
            yield return null;
        }

        //フェードが完了したら最終色に設定
        fadePanel.color = endColor;

        //フェードアウトフラグ管理
        fadein = false;
        managerScript.fadeEnd = true;
    }
}
