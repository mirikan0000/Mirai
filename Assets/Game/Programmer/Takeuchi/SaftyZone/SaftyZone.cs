using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class SaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("安置拡縮用")]
    public bool reducationFlag;             //安置を縮小させるか
    public bool magnificationFlag;          //安置を拡大させるか
    public bool destroyFlag;                //拡大終了後削除するためのフラグ
    public bool delayFlag;                  //待ち時間用フラグ
    public float delayTime;                 //待ち時間
    public float destroyTime;               //破壊までの時間
    private float timer;                    //待ち時間計測用

    [Header("各縮小段階ごとの待ち時間(縮小完了後に待つ時間)")]
    public float redu1DelayTime;            //一段階目の時の待ち時間
    public float redu2DelayTime;            //二段階目の時の待ち時間
    public float redu3DelayTime;            //三段階目の時の待ち時間
    public float redu4DelayTime;            //四段階目の時の待ち時間
    public float redu5DelayTime;            //五段階目の時の待ち時間
    public float redu6DelayTime;            //六段階目の時の待ち時間

    [Header("各拡大段階ごとの待ち時間")]
    public float mag1DelayTime;             //一段階目の待ち時間
    public float mag2DelayTime;             //二段階目の待ち時間
    public float mag3DelayTime;             //三段階目の待ち時間
    public float mag4DelayTime;             //四段階目の待ち時間
    public float mag5DelayTime;             //五段階目の待ち時間
    public float mag6DelayTime;             //六段階目の待ち時間

    [Header("各子オブジェクト移動完了用")]
    //redu = 縮小完了
    //mag  = 拡大完了
    public bool zone1ReduEnd = false;
    public bool zone1MagEnd = false;
    public bool zone2ReduEnd = false;
    public bool zone2MagEnd = false;
    public bool zone3ReduEnd = false;
    public bool zone3MagEnd = false;
    public bool zone4ReduEnd = false;
    public bool zone4MagEnd = false;

    [Header("各子オブジェクト縮小段階判定用")]
    public bool zone1reduStage = false;
    public bool zone2reduStage = false;
    public bool zone3reduStage = false;
    public bool zone4reduStage = false;

    [Header("各子オブジェクト拡大段階判定用")]
    public bool zone1magStage = false;
    public bool zone2magStage = false;
    public bool zone3magStage = false;
    public bool zone4magStage = false;

    [Header("バグチェック用")]
    public bool bug = false;                //移動終了時にバグったらTrueにする
    public float bugTimer;                  //バグった時に一定時間で安置を破壊する

    [Header("オブジェクト取得用")]
    GameObject saftyZoneSpawner;            //親オブジェクト
    CreateSaftyZone saftyZoneSpwnerScript;  //親オブジェクトのスクリプト
    GameObject zone1Obj;                    //
    Zone1 zone1Script;                      //
    GameObject zone2Obj;                    //
    Zone2 zone2Script;                      //
    GameObject zone3Obj;                    //
    Zone3 zone3Script;                      //
    GameObject zone4Obj;                    //
    Zone4 zone4Script;                      //

    void Start()
    {
        //各種数値初期化
        bugTimer = 0.0f;
        timer = 0.0f;

        //各種フラグ初期化
        FlagInitialize();

        //各オブジェクトとスクリプトを取得
        GetObjectAndScript();

        Debug.Log("生まれたよ");
    }

    void Update()
    {
        if (bug == false)
        {
            //安置の縮小段階判定
            CheckReducationStage();

            //安置の拡大段階判定
            CheckMagnificationStage();
            
            //縮小完了時(完全に縮小仕切った時）
            if (zone1ReduEnd == true && zone2ReduEnd == true && zone3ReduEnd == true && zone4ReduEnd == true)
            {
                if (delayFlag == true)
                {
                    ReducationMoveEnd();
                }
                else
                {
                    reducationFlag = false;
                    magnificationFlag = true;
                }
            }

            //拡大完了時(完全に拡大しきった時）
            if (zone1MagEnd == true && zone2MagEnd == true && zone3MagEnd == true && zone4MagEnd == true)
            {
                destroyFlag = true;

                magnificationFlag = false;
            }
        }
        else
        {
            OccurredBug();
        }

        //安置オブジェクト破壊
        if (destroyFlag == true)
        {
            ObjDestroy();
        }
    }

    //各種フラグ初期化
    private void FlagInitialize()
    {
        reducationFlag = true;
        magnificationFlag = false;
        destroyFlag = false;
        delayFlag = true;
    }

    //オブジェクト取得
    private void GetObjectAndScript()
    {
        //安置生成用スクリプト取得(親オブジェクト)
        saftyZoneSpawner = transform.parent.gameObject;
        saftyZoneSpwnerScript = saftyZoneSpawner.GetComponent<CreateSaftyZone>();

        //各安置オブジェクトのスクリプト取得(子オブジェクト)
        zone1Obj = transform.Find("ChildSaftyZone1").gameObject;
        zone1Script = zone1Obj.GetComponent<Zone1>();

        zone2Obj = transform.Find("ChildSaftyZone2").gameObject;
        zone2Script=zone2Obj.GetComponent<Zone2>();

        zone3Obj = transform.Find("ChildSaftyZone3").gameObject;
        zone3Script=zone3Obj.GetComponent<Zone3>();

        zone4Script=transform.Find("ChildSaftyZone4").gameObject.GetComponent<Zone4>();
        Debug.Log(zone4Script.preZone4pos);
    }

    //待ち時間用
    private void ReducationMoveEnd()
    {
        timer += Time.deltaTime;

        if (timer > delayTime)
        {
            reducationFlag = false;
            magnificationFlag = true;

            timer = 0.0f;
        }
    }

    //オブジェクト破壊
    private void ObjDestroy()
    {
        timer += Time.deltaTime;

        if (timer > destroyTime)
        {
            saftyZoneSpwnerScript.spawnCheck = true;
            timer = 0.0f;

            Destroy(this.gameObject);
        }
    }

    //安置の縮小段階判定
    private void CheckReducationStage()
    {
        if (zone1reduStage == true && zone2reduStage == true && zone3reduStage == true && zone4reduStage == true)
        {
            //待ち時間
            switch (saftyZoneSpwnerScript.reduCount)
            {
                case 1.0f:  //一段階目
                    Debug.Log("一段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu1DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 2.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
                case 2.0f:  //二段階目
                    Debug.Log("二段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu2DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 3.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
                case 3.0f:  //三段階目
                    Debug.Log("三段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu3DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 4.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
                case 4.0f:  //四段階目
                    Debug.Log("四段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu4DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 5.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
                case 5.0f:  //五段階目
                    Debug.Log("五段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu5DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 6.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
                case 6.0f:  //六段階目
                    Debug.Log("六段階目縮小完了");
                    timer += Time.deltaTime;

                    if (timer > redu6DelayTime)
                    {
                        //各子オブジェクトの縮小段階完了フラグをリセット
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //安置を次の段階にする
                        saftyZoneSpwnerScript.reduCount = 0.0f;
                        saftyZoneSpwnerScript.magCount = 1.0f;

                        //待ち時間計測用変数を初期化
                        timer = 0.0f;
                    }
                    break;
            }
        }
    }

    //安置の拡大段階判定
    private void CheckMagnificationStage()
    {
        if(zone1magStage==true&&zone2magStage==true&&zone3magStage==true&& zone4magStage == true)
        {
            //拡大段階に応じて処理
            if (saftyZoneSpwnerScript.magStageCount <= 1)  //一段階以下の時
            {
                timer += Time.deltaTime;

                if (timer > delayTime)
                {
                    //親オブジェクトの拡大段階カウント用変数を初期化

                }
            }
            else if (saftyZoneSpwnerScript.magStageCount > 1)  //二段階以上の時
            {
                switch (saftyZoneSpwnerScript.magCount)
                {
                    case 1.0f:  //一段階目
                        Debug.Log("一段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag1DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 2.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                    case 2.0f:  //二段階目
                        Debug.Log("二段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag2DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 3.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                    case 3.0f:  //三段階目
                        Debug.Log("三段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag3DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 4.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                    case 4.0f:  //四段階目
                        Debug.Log("四段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag4DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 5.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                    case 5.0f:  //五段階目
                        Debug.Log("五段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag5DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 6.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                    case 6.0f:  //六段階目
                        Debug.Log("六段階目拡大完了");
                        timer += Time.deltaTime;

                        if (timer > mag6DelayTime)
                        {
                            //各子オブジェクトの縮小段階完了フラグをリセット
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //安置を次の段階にする
                            saftyZoneSpwnerScript.magCount = 0.0f;

                            //待ち時間計測用変数を初期化
                            timer = 0.0f;
                        }
                        break;
                }
            }
            
        }
    }

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
