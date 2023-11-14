using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class SaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("安置拡縮用")]
    public bool reducationFlag = false;     //安置を縮小させるか
    public bool magnificationFlag = false;  //安置を拡大させるか
    public bool destroyFlag = false;        //拡大終了後削除するためのフラグ
    public bool delayFlag = true;           //待ち時間用フラグ
    public float delayTime;                 //待ち時間
    public float destroyTime;               //破壊までの時間

    [Header("縮小段階ごとの変数")]
    public float redu1DelayTime;            //一段階目の時の待ち時間
    public float redu2DelayTime;
    public float redu3DelayTime;
    public float redu4DelayTime;

    [Header("各子オブジェクト移動完了用")]
    public bool zone1redu = false;
    public bool zone1mag = false;
    public bool zone2redu = false;
    public bool zone2mag = false;
    public bool zone3redu = false;
    public bool zone3mag = false;
    public bool zone4redu = false;
    public bool zone4mag = false;

    [Header("各子オブジェクト縮小段階判定用")]
    public int reduStageNum = 1;                 //何段階目か
    public bool zone1reduStage = false;
    public bool zone2reduStage = false;
    public bool zone3reduStage = false;
    public bool zone4reduStage = false;

    [Header("各子オブジェクト拡大段階判定用")]
    public int magStageNum = 0;                  //何段階目か
    public bool zone1magStage = false;
    public bool zone2magStage = false;
    public bool zone3magStage = false;
    public bool zone4magStage = false;

    [Header("バグチェック用")]
    public bool bug = false;                //移動終了時にバグったらTrueにする
    public float bugTimer;                  //バグった時に一定時間で安置を破壊する

    [Header("安置生成管理オブジェクト用")]
    GameObject saftyZoneSpawner;
    CreateSaftyZone saftyZoneSpwnerScript;

    void Start()
    {
        //各種数値初期化
        bugTimer = 0.0f;

        //安置生成用スクリプト取得
        saftyZoneSpawner = GameObject.Find("SaftyZoneSpawner");
        saftyZoneSpwnerScript = saftyZoneSpawner.GetComponent<CreateSaftyZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bug == false)
        {
            //安置の縮小段階判定
            CheckReducationStage();

            //安置の拡大段階判定

            //縮小完了時(各段階まで移動したとき）
            switch (reduStageNum)
            {
                case 0:  //初期状態(何もしない）
                    break;
                case 1:  //第一安置
                    redu1DelayTime += Time.deltaTime;
                    break;

            }
            //縮小完了時(完全に縮小仕切った時）
            if (zone1redu == true && zone2redu == true && zone3redu == true && zone4redu == true)
            {
                if (delayFlag == true)
                {
                    Invoke("DelayRedu", delayTime);
                }
                else
                {
                    reducationFlag = false;
                    magnificationFlag = true;
                }
            }

            //拡大完了時(完全に拡大しきった時）
            if (zone1mag == true && zone2mag == true && zone3mag == true && zone4mag == true)
            {
                destroyFlag = true;

                magnificationFlag = false;
            }
        }
        else
        {
            OccurredBug();
        }

        if (destroyFlag == true)
        {
            Invoke("ObjDestroy", destroyTime);
        }
    }

    //待ち時間用
    private void DelayRedu()
    {
        reducationFlag = false;
        magnificationFlag = true;

        Debug.Log("待ち時間発生");
    }

    //オブジェクト破壊
    private void ObjDestroy()
    {
        saftyZoneSpwnerScript.spawnCheck = true;
        Destroy(this.gameObject);
    }

    //安置の縮小段階判定
    private void CheckReducationStage()
    {
        if (zone1reduStage == true && zone2reduStage == true && zone3reduStage == true && zone4reduStage == true)
        {
            //各子オブジェクトが１段階分の縮小完了してたら段階を進める
            reduStageNum++;

            //各子オブジェクトの縮小段階フラグをFalseにする
            zone1reduStage = false;
            zone2reduStage = false;
            zone3reduStage = false;
            zone4reduStage = false;
        }
    }

    //安置の拡大段階判定


    //バグった時用
    private void OccurredBug()
    {
        bugTimer += Time.deltaTime;

        if (bugTimer >= 3.0f)
        {
            destroyFlag = true;
        }
        else
        {
            Debug.Log(bugTimer);
        }
    }
}
