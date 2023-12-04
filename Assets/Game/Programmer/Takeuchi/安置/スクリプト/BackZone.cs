using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackZone : MonoBehaviour
{
    [SerializeField]
    [Header("安置移動用")]
    public float zone3MoveSpeed;  //安置の移動速度
    public Vector3 pre3Pos;       //安置の初期位置
    //安置の目標位置(縮小用)
    public Vector3 zone3PostReduPos1;
    public Vector3 zone3PostReduPos2;
    public Vector3 zone3PostReduPos3;
    public Vector3 zone3PostReduPos4;
    public Vector3 zone3PostReduPos5;
    public Vector3 zone3PostReduPos6;
    //安置の目標位置(拡大用)
    public Vector3 zone3PostMagPos1;
    public Vector3 zone3PostMagPos2;
    public Vector3 zone3PostMagPos3;
    public Vector3 zone3PostMagPos4;
    public Vector3 zone3PostMagPos5;
    public Vector3 zone3PostMagPos6;
    public Vector3 zone3NowPos;        //現在位置

    private bool setPosFlag;             //安置の目標位置設定用フラグ
    private Vector3 preZone3Pos;         //安置の初期位置
    private float prePosx, prePosy, prePosz;
    private Vector3 nowZone3Pos;         //安置の現在位置
    private float nowPosx, nowPosy, nowPosz;
    private float maxDistance = 600.0f;  //最大移動量
    private float reduDistance;          //縮小時移動量
    private float magDistance;           //拡大時移動量
    private float dis;                   //誤差が出た時用

    [Header("オブジェクト＆スクリプト取得用")]
    private GameObject parentObj;        //親オブジェクト
    private SaftyZoneObjV3 parentScript;  //親オブジェクトのスクリプト

    void Start()
    {
        //変数初期化
        VariableInitialize();
    }


    void Update()
    {
        //縮小
        //親スクリプトの縮小用フラグがTrueなら
        if (parentScript.reducationFlag == true)
        {
            //移動
            MoveReducation();

            //移動完了チェック
            ReducationCheck();
        }

        //拡大
        //親スクリプトの拡大用フラグがTrueなら
        if (parentScript.magnificationFlag == true)
        {
            //移動
            MoveMagnification();

            //移動完了チェック
            MagnificationCheck();
        }
    }

    //変数初期化
    private void VariableInitialize()
    {
        //安置の初期位置取得
        preZone3Pos = this.transform.position;
        prePosx = Mathf.Floor(preZone3Pos.x);
        prePosy = Mathf.Floor(preZone3Pos.y);
        prePosz = Mathf.Floor(preZone3Pos.z);
        pre3Pos = new Vector3(prePosx, prePosy, prePosz);

        //目標位置設定用フラグ初期化
        setPosFlag = true;

        //誤差が出た時用変数初期化
        dis = 0.0f;

        //親オブジェクト＆スクリプト取得
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneObjV3>();

        //安置の移動量を設定
        SetMoveDistance();

        //目標位置の設定
        SetPostPos();
    }

    //現在位置取得
    private void GetNowPos()
    {
        //現在位置を取得
        nowZone3Pos = this.gameObject.transform.position;

        //取得した座標を小数点以下切り捨て
        nowPosx = Mathf.Floor(nowZone3Pos.x);
        nowPosy = Mathf.Floor(nowZone3Pos.y);
        nowPosz = Mathf.Floor(nowZone3Pos.z);

        //現在位置設定
        zone3NowPos = new Vector3(nowPosx, nowPosy, nowPosz);
    }

    //移動量を設定
    private void SetMoveDistance()
    {
        //最大移動量を縮小回数で割る
        reduDistance = maxDistance / parentScript.maxReduStage;

        //最大移動量を拡大回数で割る
        magDistance = maxDistance / parentScript.maxMagStage;
    }

    //目標位置の設定
    private void SetPostPos()
    {
        if (setPosFlag == true)
        {
            //設定した縮小回数で目標地点を設定
            switch (parentScript.maxReduStage)
            {
                case 1:  //縮小回数が一回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z + reduDistance);
                    break;

                case 2:  //縮小回数が二回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //二段階目の目標視点を設定
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z + reduDistance);
                    //二段階目の拡大目標地点設定
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    break;

                case 3:  //縮小回数が三回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //二段階目の目標視点を設定
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //三段階目の目標視点を設定
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z + reduDistance);
                    //二段階目の拡大目標地点設定
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //三段階目の拡大目標地点設定
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    break;

                case 4:  //縮小回数が四回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //二段階目の目標視点を設定
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //三段階目の目標視点を設定
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //四段階目の目標視点を設定
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z + reduDistance);
                    //二段階目の拡大目標地点設定
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //三段階目の拡大目標地点設定
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //四段階目の拡大目標地点設定
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    break;

                case 5:  //縮小回数が五回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //二段階目の目標視点を設定
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //三段階目の目標視点を設定
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //四段階目の目標視点を設定
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);
                    //五段階目の目標視点を設定
                    zone3PostReduPos5 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos5.x, zone3PostReduPos5.y, zone3PostReduPos5.z + reduDistance);
                    //二段階目の拡大目標地点設定
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //三段階目の拡大目標地点設定
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //四段階目の拡大目標地点設定
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    //五段階目の拡大目標地点設定
                    zone3PostMagPos5 = new Vector3(zone3PostMagPos4.x, zone3PostMagPos4.y, zone3PostMagPos4.z + reduDistance);
                    break;

                case 6:  //縮小回数が六回の時
                    //一段階目の縮小目標地点設定
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //二段階目の目標視点を設定
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //三段階目の目標視点を設定
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //四段階目の目標視点を設定
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);
                    //五段階目の目標視点を設定
                    zone3PostReduPos5 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z - reduDistance);
                    //六段階目の目標視点を設定
                    zone3PostReduPos6 = new Vector3(zone3PostReduPos5.x, zone3PostReduPos5.y, zone3PostReduPos5.z - reduDistance);

                    //一段階目の拡大目標地点設定
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos6.x, zone3PostReduPos6.y, zone3PostReduPos6.z + reduDistance);
                    //二段階目の拡大目標地点設定
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //三段階目の拡大目標地点設定
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //四段階目の拡大目標地点設定
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    //五段階目の拡大目標地点設定
                    zone3PostMagPos5 = new Vector3(zone3PostMagPos4.x, zone3PostMagPos4.y, zone3PostMagPos4.z + reduDistance);
                    //六段階目の拡大目標地点設定
                    zone3PostMagPos6 = new Vector3(zone3PostMagPos5.x, zone3PostMagPos5.y, zone3PostMagPos5.z + reduDistance);
                    break;
            }

            //目標地点設定用フラグをFalse
            setPosFlag = false;
        }
    }

    //縮小移動
    private void MoveReducation()
    {
        //親スクリプトの現在の安置の段階によって移動
        switch (parentScript.nowReduStage)
        {
            case 1.0f:  //一回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos1, zone3MoveSpeed);
                break;
            case 2.0f:  //二回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos2, zone3MoveSpeed);
                break;
            case 3.0f:  //三回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos3, zone3MoveSpeed);
                break;
            case 4.0f:  //四回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos4, zone3MoveSpeed);
                break;
            case 5.0f:  //五回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos5, zone3MoveSpeed);
                break;
            case 6.0f:  //六回目の縮小
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos6, zone3MoveSpeed);
                break;
        }
    }

    //縮小完了チェック
    private void ReducationCheck()
    {
        //現在位置取得
        GetNowPos();

        //現在の縮小回数によって処理
        switch (parentScript.nowReduStage)
        {
            case 1.0f:  //一回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos1);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //二回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos2);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //三回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos3);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //四回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos4);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //五回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos5);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //六回目の縮小チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos6);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowReduStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }

    //拡大移動
    private void MoveMagnification()
    {
        //親スクリプトの現在の安置の段階によって移動
        switch (parentScript.nowMagStage)
        {
            case 1.0f:  //一回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos1, zone3MoveSpeed);
                break;
            case 2.0f:  //二回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos2, zone3MoveSpeed);
                break;
            case 3.0f:  //三回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos3, zone3MoveSpeed);
                break;
            case 4.0f:  //四回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos4, zone3MoveSpeed);
                break;
            case 5.0f:  //五回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos5, zone3MoveSpeed);
                break;
            case 6.0f:  //六回目の拡大
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos6, zone3MoveSpeed);
                break;
        }
    }

    //拡大完了チェック
    private void MagnificationCheck()
    {
        //現在位置取得
        GetNowPos();

        //現在の拡大回数によって処理
        switch (parentScript.nowMagStage)
        {
            case 1.0f:  //一回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos1);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //二回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos2);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //三回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos3);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //四回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos4);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //五回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos5);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //六回目の拡大チェック
                //現在位置と目標位置の距離を計算
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos6);

                //縮小完了しているか(誤差１まで許容)
                if (dis <= 1.0f)
                {
                    //親スクリプトの変数加算
                    parentScript.zone3NowMagStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }
}
