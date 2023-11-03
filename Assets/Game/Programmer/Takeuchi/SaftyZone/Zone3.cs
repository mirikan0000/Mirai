using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone3 : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト取得用")]
    public GameObject childSaftyZone3Obj;  //自身のゲームオブジェクト
    public string childSaftyZone3ObjName;  //自身の名前
    GameObject parentSaftyZoneObj;         //親オブジェクト
    SaftyZone parentSaftyZoneScript;       //親オブジェクトのスクリプト
    [Header("移動用")]
    public Vector3 preZone3pos;            //自身の初期位置
    public Vector3 postZone3Pos;           //自身の移動後の目標位置
    public Vector3 nowZone3Pos;            //自身の現在位置
    public int zone3MoveSpeed;           //移動速度

    public Vector3 pre3Pos;                       //初期位置用(小数点以下切り捨て
    float pre3Posx, pre3Posy, pre3Posz;    

    public Vector3 now3Pos;               　　　　//現在位置用(小数点以下切り捨て
    float now3Posx, now3Posy, now3Posz;

    private float dis;                     //誤差が出た時用

    [Header("縮小用")]
    public bool setReduPosFlag = true;     //縮小後の位置をセットするためのフラグ
    public bool endReduFlag = false;       //縮小完了したか
    [Header("拡大用")]
    public bool setMagPosFlag = false;     //拡大後の位置をセットするためのフラグ
    public bool endMagFlag = false;        //拡大完了したか

    void Start()
    {
        //各種フラグの初期化
        Zone3FlagInitialize();

        //安置の名前と初期位置を取得
        childSaftyZone3Obj = this.gameObject;
        childSaftyZone3ObjName = childSaftyZone3Obj.name;
        preZone3pos = childSaftyZone3Obj.transform.position;

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
                SetReducationZone2Pos();
            }

            //縮小
            ReducationZone2();

            //縮小完了チェック
            Zone2ReducationCheck();
        }

        //拡大
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagPosFlag == true)
            {
                //拡大後の位置を設定
                SetMagnificationZone2Pos();
            }

            //拡大
            MagnificationZone2();

            //拡大完了チェック
            Zone2MagnificationCheck();
        }
    }

    //各種フラグの初期化
    private void Zone3FlagInitialize()
    {
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //縮小後の目標位置を設定
    private void SetReducationZone2Pos()
    {
        ////初期位置を小数点以下切り捨て
        pre3Posx = Mathf.Floor(preZone3pos.x);
        pre3Posy = Mathf.Floor(preZone3pos.y);
        pre3Posz = Mathf.Floor(preZone3pos.z);
        pre3Pos = new Vector3(pre3Posx, pre3Posy, pre3Posz);

        //目標位置設定
        postZone3Pos = new Vector3(pre3Posx, pre3Posy, pre3Posz + 425.0f);

        //設定した後縮小位置設定用フラグをFalseにする
        setReduPosFlag = false;
    }

    //縮小
    private void ReducationZone2()
    {
        //縮小目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone3Pos, zone3MoveSpeed);
    }

    //縮小完了チェック
    private void Zone2ReducationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now3Pos, postZone3Pos);

        //縮小完了しているか
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //完了していたら親オブジェクトの変数を加算
                parentSaftyZoneScript.zone3redu = true;
                endReduFlag = true;

                //完了していたら拡大後位置設定用フラグをTrueにする
                setMagPosFlag = true;
            }
        }
    }

    //拡大後の位置を設定
    private void SetMagnificationZone2Pos()
    {
        //目標位置に初期位置を設定
        postZone3Pos = pre3Pos;

        //設定した後拡大後位置設定用フラグをFalseにする
        setMagPosFlag = false;
    }

    //拡大
    private void MagnificationZone2()
    {
        //拡大目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone3Pos, zone3MoveSpeed);
    }

    //拡大完了チェック
    private void Zone2MagnificationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now3Pos, postZone3Pos);

        //拡大完了しているか
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //親オブジェクトの変数を加算
                parentSaftyZoneScript.zone3mag = true;

                endMagFlag = true;
            }
        }
    }

    //現在地取得用
    private void GetNowPos()
    {
        //現在位置を取得
        nowZone3Pos = this.gameObject.transform.position;

        //取得した座標を小数点以下切り捨て
        now3Posx = Mathf.Floor(nowZone3Pos.x);
        now3Posy = Mathf.Floor(nowZone3Pos.y);
        now3Posz = Mathf.Floor(nowZone3Pos.z);

        //Vector3型にする
        now3Pos = new Vector3(now3Posx, now3Posy, now3Posz);
    }
}
