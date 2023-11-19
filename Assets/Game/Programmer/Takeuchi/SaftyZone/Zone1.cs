using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1 : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト取得用")]
    GameObject parentSaftyZoneObj;               //親オブジェクト
    SaftyZone parentSaftyZoneScript;             //親オブジェクトのスクリプト
    GameObject grandParentObj;                   //親の親オブジェクト
    CreateSaftyZone grandParentScript;           //親の親オブジェクトのスクリプト

    [Header("移動用")]
    public Vector3 preZone1pos;                  //自身の初期位置
    public Vector3 postZone1Pos;                 //自身の移動後の目標位置
    public Vector3 nowZone1Pos;                  //自身の現在位置
    public float zone1MoveSpeed;                 //移動速度
    Vector3 pre1Pos;                             //初期位置用(小数点以下切り捨て
    float pre1Posx, pre1Posy, pre1Posz;    
    Vector3 now1Pos;                 　　　      //現在位置用(小数点以下切り捨て
    float now1Posx, now1Posy, now1Posz;
    private float dis;                           //誤差が出た時用

    [Header("縮小用")]
    public bool setReduDistanceFlag;             //段階ごとの移動量を設定するためのフラグ
    public bool endReduFlag;                     //縮小完了したか
    //縮小段階判定用
    public float zone1ReduCount;                 //縮小段階カウント用変数

    private float reduMoveDistance;              //安置縮小時の移動量
    private float reduMoveMaxDistance = 600.0f;  //安置縮小時の最大移動量

    [Header("拡大用")]
    public bool setMagDistanceFlag;              //段階ごとの移動量を設定する為のフラグ
    public bool endMagFlag;                      //拡大完了したか
    //拡大段階判定用
    public bool zone1MagStage1;
    public bool zone1MagStage2;
    public bool zone1MagStage3;
    public bool zone1MagStage4;
    public bool zone1MagStage5;
    public bool zone1MagStage6;

    private float magMoveDistance;               //安置拡大時の移動量
    private float magMoveMaxDistance = 600.0f;   //安置拡大時の最大移動量

    void Start()
    {
        //各種数値の初期化
        dis = 0.0f;
        reduMoveDistance = 0.0f;
        magMoveDistance = 0.0f;
        zone1ReduCount = 1.0f;

        //各種フラグの初期化処理
        Zone1FlagInitialize();

        //初期位置を取得
        GetZone1PrePos();

        //親オブジェクトのスクリプト取得
        parentSaftyZoneObj = transform.parent.gameObject;
        parentSaftyZoneScript = parentSaftyZoneObj.GetComponent<SaftyZone>();

        //親の親オブジェクトのスクリプトを取得
        grandParentObj = transform.parent.parent.gameObject;
        grandParentScript = grandParentObj.GetComponent<CreateSaftyZone>();
    }

    void Update()
    {
        //縮小
        if (parentSaftyZoneScript.reducationFlag == true)
        {
            if (setReduDistanceFlag == true)
            {
                //縮小段階の数に応じて移動量を設定
                SetReducationZone1MoveDistance();
            }

            //縮小
            ReducationMoveZone1();

            //縮小完了チェック
            Zone1ReducationCheck();
        }

        //拡大
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagDistanceFlag == true)
            {
                //拡大後の位置を設定
                SetMagnificationZone1Pos();
            }

            //拡大
            MagnificationZone1();

            //拡大完了チェック
            Zone1MagnificationCheck();
        }
    }

    //フラグの初期化処理
    private void Zone1FlagInitialize()
    {
        //縮小用
        setReduDistanceFlag = true;
        endReduFlag = false;
        //拡大用
        setMagDistanceFlag = false;
        endMagFlag = false;

        
    }

    //初期位置を取得
    private void GetZone1PrePos()
    {
        //初期位置を取得
        preZone1pos = this.transform.position;

        //初期位置を小数点以下切り捨て
        pre1Posx = Mathf.Floor(preZone1pos.x);
        pre1Posy = Mathf.Floor(preZone1pos.y);
        pre1Posz = Mathf.Floor(preZone1pos.z);
        pre1Pos = new Vector3(pre1Posx, pre1Posy, pre1Posz);
    }
    //縮小後の目標位置までの移動量を設定
    private void SetReducationZone1MoveDistance()
    {
        //最大縮小段階に応じて移動量計算
        if (grandParentScript.reduStageCount > 1)  //縮小段階が２以上の時
        {
            reduMoveDistance = reduMoveMaxDistance / grandParentScript.reduStageCount;
        }
        else  //縮小段階が1以下の時
        {
            reduMoveDistance = reduMoveMaxDistance;
        }
        //移動量設定用フラグをFalseにする
        setReduDistanceFlag = false;
    }

    //縮小
    private void ReducationMoveZone1()
    {
        //縮小段階数に応じて目標地点を決めて移動
        if (grandParentScript.reduStageCount <= 1)  //一段階以下の時
        {
            //初期位置から最大移動量分移動した座標を目標位置に設定
            postZone1Pos = new Vector3(pre1Pos.x + reduMoveMaxDistance, pre1Pos.y, pre1Pos.z);

            //縮小目標地点まで移動
            transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
        }
        else if (grandParentScript.reduStageCount > 1)  //二段階以上の時
        {
            //現在位置取得
            GetNowPos();

            //現在位置から移動量分移動した座標を目標位置に設定
            postZone1Pos = new Vector3(now1Pos.x + reduMoveDistance, now1Pos.y, now1Pos.z);

            //縮小目標地点まで移動
            transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);

            //縮小段階判定用変数加算
            zone1ReduCount = zone1ReduCount + 1.0f;
        }
    }

    //縮小完了チェック
    private void Zone1ReducationCheck()
    {
        //現在位置を取得
        GetNowPos();

        //現在位置と目標位置の距離を計算
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //縮小完了しているか(誤差１までは許容）
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //縮小段階に応じて処理
                if (grandParentScript.reduStageCount <= 1)  //
                {
                    //完了していたら親オブジェクトのフラグをtrue
                    parentSaftyZoneScript.zone1reduStage = true;
                  
                    endReduFlag = true;

                    //完了していたら拡大後位置設定用フラグをTrueにする
                    setMagDistanceFlag = true;
                }
                else if (grandParentScript.reduStageCount > 1)  //
                {
                    //現在の縮小段階が設定した縮小段階よりも小さいとき
                    if (zone1ReduCount < grandParentScript.reduStageCount)
                    {
                        parentSaftyZoneScript.zone1reduStage = true;
                    }
                    else if (zone1ReduCount >= grandParentScript.reduStageCount)
                    {
                        parentSaftyZoneScript.zone1ReduEnd = true;

                    }
                }
            }
        }
    }

    //拡大後の位置を設定
    private void SetMagnificationZone1Pos()
    {
        //目標位置に初期位置を設定
        postZone1Pos = pre1Pos;

        //設定した後拡大後位置設定用フラグをFalseにする
        setMagDistanceFlag = false;
    }

    //拡大
    private void MagnificationZone1()
    {
        //拡大目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
    }

    //拡大完了チェック
    private void Zone1MagnificationCheck()
    {
        //現在位置を取得
        GetNowPos();

        //現在位置と目標位置の距離を計算
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //拡大完了しているか
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //親オブジェクトの変数を加算
                parentSaftyZoneScript.zone1MagEnd = true;

                endMagFlag = true;
            }
        }
    }

    //現在地取得用
    private void GetNowPos()
    {
        //現在位置を取得
        nowZone1Pos = this.gameObject.transform.position;

        //取得した座標を小数点以下切り捨て
        now1Posx = Mathf.Floor(nowZone1Pos.x);
        now1Posy = Mathf.Floor(nowZone1Pos.y);
        now1Posz = Mathf.Floor(nowZone1Pos.z);

        //Vector3型にする
        now1Pos = new Vector3(now1Posx, now1Posy, now1Posz);
    }
}
