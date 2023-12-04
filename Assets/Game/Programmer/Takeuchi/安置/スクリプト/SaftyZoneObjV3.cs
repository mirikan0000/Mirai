using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaftyZoneObjV3 : MonoBehaviour
{
    [SerializeField]
    [Header("安置拡縮用")]
    public bool reducationFlag;     //安置縮小用フラグ
    public bool magnificationFlag;  //安置拡大用フラグ
    public bool destroyFlag;        //安置破壊用フラグ

    private float timer;            //待ち時間計測用

    [Header("安置の段階")]
    public float maxReduStage;      //最大縮小回数(インスペクターで設定）
    public float maxMagStage;       //最大拡大回数(インスペクターで設定)
    public float nowReduStage;      //現在の縮小回数
    public float nowMagStage;       //現在の拡大回数
    public float nextReduStage;     //次の安置の縮小段階
    public float nextMagStage;      //次の安置の拡大段階

    [Header("各縮小段階ごとの待ち時間")]
    public float redu1DelayTime;  //一段階目縮小前の待ち時間
    public float redu2DelayTime;  //二段階目縮小前の待ち時間
    public float redu3DelayTime;  //三段階目縮小前の待ち時間
    public float redu4DelayTime;  //四段階目縮小前の待ち時間
    public float redu5DelayTime;  //五段階目縮小前の待ち時間
    public float redu6DelayTime;  //六段階目縮小前の待ち時間

    [Header("各拡大段階ごとの待ち時間")]
    public float mag1DelayTime;  //一段階目拡大前の待ち時間
    public float mag2DelayTime;  //二段階目拡大前の待ち時間
    public float mag3DelayTime;  //三段階目拡大前の待ち時間
    public float mag4DelayTime;  //四段階目拡大前の待ち時間
    public float mag5DelayTime;  //五段階目拡大前の待ち時間
    public float mag6DelayTime;  //六段階目拡大前の待ち時間

    [Header("各子オブジェクト移動完了用")]  //移動完了したら＋１する
    //縮小の時用
    public float zone1NowReduStage;
    public float zone2NowReduStage;
    public float zone3NowReduStage;
    public float zone4NowReduStage;
    //縮小が完全に終わった時用 True　で移動完了
    public bool zone1ReduEndFlag;
    public bool zone2ReduEndFlag;
    public bool zone3ReduEndFlag;
    public bool zone4ReduEndFlag;
    //拡大の時用
    public float zone1NowMagStage;
    public float zone2NowMagStage;
    public float zone3NowMagStage;
    public float zone4NowMagStage;
    //拡大が完全に終わった時用 True　で移動完了
    public bool zone1MagEndFlag;
    public bool zone2MagEndFlag;
    public bool zone3MagEndFlag;
    public bool zone4MagEndFlag;

    [Header("オブジェクト取得用")]
    GameObject spawnerObj;      //親オブジェクト
    Spawner spawnerScript;      //親オブジェクトのスクリプト

    void Start()
    {
        //変数初期化
        VariableInitialize();

        //フラグ初期化
        FlagInitialize();
    }

    void Update()
    {
        //安置の縮小段階確認
        CheckReducationStage();

        //安置の拡大段階確認
        CheckMagnificationStage();

        //安置の破壊処理
        if (destroyFlag == true)
        {
            spawnerScript.spawnFlag = true;

            Destroy(this.gameObject);
        }
    }

    //変数初期化
    private void VariableInitialize()
    {
        timer = 0.0f;
        nowReduStage = 0.0f;
        nowMagStage = 0.0f;
        zone1NowReduStage = 0.0f;
        zone2NowReduStage = 0.0f;
        zone3NowReduStage = 0.0f;
        zone4NowReduStage = 0.0f;
        zone1NowMagStage = 0.0f;
        zone2NowMagStage = 0.0f;
        zone3NowMagStage = 0.0f;
        zone4NowMagStage = 0.0f;

        nextReduStage = 1.0f;
        nextMagStage = 0.0f;

        //親オブジェクトとスクリプト取得
        spawnerObj = transform.parent.gameObject;
        spawnerScript = spawnerObj.GetComponent<Spawner>();
    }

    //フラグの初期化
    private void FlagInitialize()
    {
        reducationFlag = false;
        reducationFlag = true;
        magnificationFlag = false;
        destroyFlag = false;

        zone1ReduEndFlag = false;
        zone2ReduEndFlag = false;
        zone3ReduEndFlag = false;
        zone4ReduEndFlag = false;

        zone1MagEndFlag = false;
        zone2MagEndFlag = false;
        zone3MagEndFlag = false;
        zone4MagEndFlag = false;
    }

    //安置の縮小段階確認
    private void CheckReducationStage()
    {
        //各子オブジェクトの現在縮小段階に応じて縮小回数を加算
        if(zone1NowReduStage == 0.0f && zone2NowReduStage == 0.0f &&
            zone3NowReduStage == 0.0f && zone4NowReduStage == 0.0f && nowReduStage == 0.0f)
        {
            //第一安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu1DelayTime)
            {
                nowReduStage = 1.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==1.0f&&zone2NowReduStage==1.0f&&
            zone3NowReduStage == 1.0f && zone4NowReduStage == 1.0f)
        {
            //第二安置の予報
            nextReduStage = 2.0f;

            //第二安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu2DelayTime)
            {
                nowReduStage = 2.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==2.0f&&zone2NowReduStage==2.0f&&
            zone3NowReduStage == 2.0f && zone4NowReduStage == 2.0f)
        {
            //第三安置の予報
            nextReduStage = 3.0f;

            //第三安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu3DelayTime)
            {
                nowReduStage = 3.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==3.0f&&zone2NowReduStage==3.0f&&
            zone3NowReduStage == 3.0f && zone4NowReduStage == 3.0f)
        {
            //第四安置の予報
            nextReduStage = 4.0f;

            //第四安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu4DelayTime)
            {
                nowReduStage = 4.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowReduStage == 4.0f && zone2NowReduStage == 4.0f &&
            zone3NowReduStage == 4.0f && zone4NowReduStage == 4.0f)
        {
            //第五安置の予報
            nextReduStage = 5.0f;

            //第五安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu5DelayTime)
            {
                nowReduStage = 5.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowReduStage == 5.0f && zone2NowReduStage == 5.0f &&
            zone3NowReduStage == 5.0f && zone4NowReduStage == 5.0f)
        {
            //第六安置の予報
            nextReduStage = 6.0f;

            //第六安置の待ち時間
            timer += Time.deltaTime;

            if (timer > redu6DelayTime)
            {
                nowReduStage = 6.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage == 6.0f && zone2NowReduStage == 6.0f &&
            zone3NowReduStage == 6.0f && zone3NowReduStage == 6.0f)
        {
            nowReduStage = 7.0f;
        }

        //縮小完了処理
        if (nowReduStage > maxReduStage)
        {
            //子オブジェクトの縮小完了フラグ管理
            zone1ReduEndFlag = true;
            zone2ReduEndFlag = true;
            zone3ReduEndFlag = true;
            zone4ReduEndFlag = true;

            EndReducation();
        }
    }

    //安置の拡大段階確認
    private void CheckMagnificationStage()
    {
        //各子オブジェクトの現在縮小段階に応じて縮小回数を加算
        if(zone1NowMagStage == 0.0f && zone2NowMagStage == 0.0f &&
            zone3NowMagStage == 0.0f && zone4NowMagStage == 0.0f && nowMagStage == 0.0f&& reducationFlag == false)
        {
            //安置第一拡大の待ち時間
            timer += Time.deltaTime;

            if (timer > mag1DelayTime)
            {
                nowMagStage = 1.0f;
                magnificationFlag = true;

                timer = 0.0f;
            }
        }
        else if(zone1NowMagStage == 1.0f && zone2NowMagStage == 1.0f &&
            zone3NowMagStage == 1.0f && zone4NowMagStage == 1.0f)
        {
            //安置第二拡大の待ち時間
            timer += Time.deltaTime;
            
            if (timer > mag2DelayTime)
            {
                nowMagStage = 2.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowMagStage==2.0f&&zone2NowMagStage==2.0f&&
            zone3NowMagStage == 2.0f && zone4NowMagStage == 2.0f)
        {
            //安置第三拡大の待ち時間
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > mag3DelayTime)
            {
                nowMagStage = 3.0f;
                Debug.Log(nowMagStage);
                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 3.0f && zone2NowMagStage == 3.0f &&
            zone3NowMagStage == 3.0f && zone4NowMagStage == 3.0f)
        {
            //安置第四拡大の待ち時間
            timer += Time.deltaTime;

            if (timer > mag4DelayTime)
            {
                nowMagStage = 4.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 4.0f && zone2NowMagStage == 4.0f &&
            zone3NowMagStage == 4.0f && zone4NowMagStage == 4.0f)
        {
            //安置第五拡大の待ち時間
            timer += Time.deltaTime;

            if (timer > mag5DelayTime)
            {
                nowMagStage = 5.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 5.0f && zone2NowMagStage == 5.0f &&
            zone3NowMagStage == 5.0f && zone4NowMagStage == 5.0f)
        {
            //安置第六拡大の待ち時間
            timer += Time.deltaTime;

            if (timer > mag6DelayTime)
            {
                nowMagStage = 6.0f;

                timer = 0.0f;
            }
        }

        //拡大完了処理
        if (nowMagStage > maxMagStage)
        {
            //各子オブジェクトの拡大完了フラグ管理
            zone1MagEndFlag = true;
            zone2MagEndFlag = true;
            zone3MagEndFlag = true;
            zone4MagEndFlag = true;

            EndMagnification();
        }
    }

    //縮小完了処理
    private void EndReducation()
    {
        if (reducationFlag == true)
        {
            //子オブジェクト用の縮小完了フラグが全てTrueなら
            if (zone1ReduEndFlag == true && zone2ReduEndFlag == true &&
                zone3ReduEndFlag == true && zone4ReduEndFlag == true)
            {
                //安置縮小用フラグをFalseに
                reducationFlag = false;

                //現在の縮小回数を０に
                nowReduStage = 0.0f;

                //現在の拡大回数を１に
                //nowMagStage = 1.0f;
            }
        }
    }

    //拡大完了処理
    private void EndMagnification()
    {
        if (magnificationFlag == true)
        {
            //子オブジェクト用の拡大完了フラグが全てTrueなら
            if (zone1MagEndFlag == true &&
                zone2MagEndFlag == true &&
                zone3MagEndFlag == true &&
                zone4MagEndFlag == true)
            {
                //安置拡大用フラグをFalseに
                magnificationFlag = false;

                //現在の拡大回数を０に
                nowMagStage = 0.0f;

                //安置破壊用フラグをTrueに
                destroyFlag = true;
            }
        }
    }
}
