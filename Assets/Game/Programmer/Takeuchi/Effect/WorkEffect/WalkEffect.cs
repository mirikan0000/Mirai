using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WalkEffect : MonoBehaviour
{
    [SerializeField]
    VisualEffect effect;     //エフェクト
    public float stopTimer;  //生成からの時間計測
    public float timerLimit; //制限時間

    void Start()
    {
        //各種変数初期化
        stopTimer = 0.0f;
    }

    void Update()
    {
        //時間計測
        stopTimer += Time.deltaTime;

        //生成停止と破壊
        if (stopTimer > timerLimit)
        {
            //エフェクト停止
            effect.SendEvent("OnStop");

            //タイマー初期化
            stopTimer = 0.0f;

            //オブジェクト破壊
            Destroy(this.gameObject);
        }
    }
}
