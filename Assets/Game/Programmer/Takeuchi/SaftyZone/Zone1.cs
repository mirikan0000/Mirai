using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1 : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト取得用")]
    public GameObject childSaftyZone1Obj;  //自身のゲームオブジェクト
    public string childSaftyZone1ObjName;  //自身の名前
    GameObject parentSaftyZoneObj;         //親オブジェクト
    SaftyZone parentSaftyZoneScript;       //親オブジェクトのスクリプト
    [Header("移動用")]
    public Vector3 preZone1pos;            //自身の初期位置
    public Vector3 postZone1Pos;           //自身の移動後の目標位置
    public Vector3 nowZone1Pos;            //自身の現在位置
    public float zone1MoveSpeed = 0.0001f;             //移動速度

    public Vector3 pre1Pos;                //初期位置用(小数点以下切り捨て
    float pre1Posx, pre1Posy, pre1Posz;    

    public Vector3 now1Pos;          　　　//現在位置用(小数点以下切り捨て
    float now1Posx, now1Posy, now1Posz;

    private float dis;                     //誤差が出た時用

    [Header("縮小用")]
    public bool setReduPosFlag = true;     //縮小後の位置をセットするためのフラグ
    public bool endReduFlag = false;       //縮小完了したか
    [Header("拡大用")]
    public bool setMagPosFlag = false;     //拡大後の位置をセットするためのフラグ
    public bool endMagFlag = false;        //拡大完了したか

    void Start()
    {
        //各種数値の初期化
        dis = 0.0f;

        //各種フラグの初期化処理
        Zone1FlagInitialize();

        //安置の名前と初期位置を取得
        childSaftyZone1Obj = this.gameObject;
        childSaftyZone1ObjName = childSaftyZone1Obj.name;
        preZone1pos = childSaftyZone1Obj.transform.position;
        zone1MoveSpeed = 0.01f;
        //親オブジェクトのスクリプト取得
        parentSaftyZoneObj = transform.parent.gameObject;
        parentSaftyZoneScript = parentSaftyZoneObj.GetComponent<SaftyZone>();
    }

    void Update()
    {
        //縮小
        if (parentSaftyZoneScript.reducationFlag == true)
        {
            if (setReduPosFlag == true)
            {
                //縮小後の位置を設定
                SetReducationZone1Pos();
            }

            //縮小
            ReducationZone1();

            //縮小完了チェック
            Zone1ReducationCheck();
        }

        //拡大
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagPosFlag == true)
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
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //縮小後の目標位置を設定
    private void SetReducationZone1Pos()
    {
        //初期位置を小数点以下切り捨て
        pre1Posx = Mathf.Floor(preZone1pos.x);
        pre1Posy = Mathf.Floor(preZone1pos.y);
        pre1Posz = Mathf.Floor(preZone1pos.z);
        pre1Pos = new Vector3(pre1Posx, pre1Posy, pre1Posz);

        //目標位置設定
        postZone1Pos = new Vector3(pre1Posx + 425.0f, pre1Posy, pre1Posz);

        //設定した後縮小位置設定用フラグをFalseにする
        setReduPosFlag = false;
    }

    //縮小
    private void ReducationZone1()
    {
        //縮小目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
    }

    //縮小完了チェック
    private void Zone1ReducationCheck()
    {
        //現在位置を取得
        GetNowPos();

        //現在位置と目標位置の距離を計算
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //縮小完了しているか
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //完了していたら親オブジェクトの変数を加算
                parentSaftyZoneScript.zone1redu = true;
                endReduFlag = true;

                //完了していたら拡大後位置設定用フラグをTrueにする
                setMagPosFlag = true;
            }
        }
    }

    //拡大後の位置を設定
    private void SetMagnificationZone1Pos()
    {
        //目標位置に初期位置を設定
        postZone1Pos = pre1Pos;

        //設定した後拡大後位置設定用フラグをFalseにする
        setMagPosFlag = false;
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
                parentSaftyZoneScript.zone1mag = true;

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
