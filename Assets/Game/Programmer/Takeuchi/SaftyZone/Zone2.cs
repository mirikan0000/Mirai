using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2 : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト取得用")]
    public GameObject childSaftyZone2Obj;  //自身のゲームオブジェクト
    public string childSaftyZone2ObjName;  //自身の名前
    GameObject parentSaftyZoneObj;         //親オブジェクト
    SaftyZone parentSaftyZoneScript;       //親オブジェクトのスクリプト
    [Header("移動用")]
    public Vector3 preZone2pos;            //自身の初期位置
    public Vector3 postZone2Pos;           //自身の移動後の目標位置
    public Vector3 nowZone2Pos;            //自身の現在位置
    public int zone2MoveSpeed;           //移動速度

    public Vector3 pre2Pos;                       //初期位置用(小数点以下切り捨て
    float pre2Posx, pre2Posy, pre2Posz;    

    public Vector3 now2Pos;                       //現在位置用(小数点以下切り捨て
    float now2Posx, now2Posy, now2Posz;

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
        Zone2FlagInitialize();

        //安置の名前と初期位置を取得
        childSaftyZone2Obj = this.gameObject;
        childSaftyZone2ObjName = childSaftyZone2Obj.name;
        preZone2pos = childSaftyZone2Obj.transform.position;

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
    private void Zone2FlagInitialize()
    {
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //縮小後の目標位置を設定
    private void SetReducationZone2Pos()
    {
        //初期位置を小数点以下切り捨て
        pre2Posx = Mathf.Floor(preZone2pos.x);
        pre2Posy = Mathf.Floor(preZone2pos.y);
        pre2Posz = Mathf.Floor(preZone2pos.z);
        pre2Pos = new Vector3(pre2Posx, pre2Posy, pre2Posz);

        //目標位置設定
        postZone2Pos = new Vector3(pre2Posx - 425.0f, pre2Posy, pre2Posz);

        //設定した後縮小位置設定用フラグをFalseにする
        setReduPosFlag = false;
    }

    //縮小
    private void ReducationZone2()
    {
        //縮小目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone2Pos, zone2MoveSpeed);
    }

    //縮小完了チェック
    private void Zone2ReducationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now2Pos, postZone2Pos);

        //縮小完了しているか
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //完了していたら親オブジェクトの変数を加算
                parentSaftyZoneScript.zone2redu = true;
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
        postZone2Pos = pre2Pos;

        //設定した後拡大後位置設定用フラグをFalseにする
        setMagPosFlag = false;
    }

    //拡大
    private void MagnificationZone2()
    {
        //拡大目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone2Pos, zone2MoveSpeed);
    }

    //拡大完了チェック
    private void Zone2MagnificationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now2Pos, postZone2Pos);

        //拡大完了しているか
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //親オブジェクトの変数を加算
                parentSaftyZoneScript.zone2mag = true;

                endMagFlag = true;
            }
        }
    }

    //現在位置を取得
    private void GetNowPos()
    {
        //現在位置を取得
        nowZone2Pos = this.gameObject.transform.position;

        //取得した座標を小数点以下切り捨て
        now2Posx = Mathf.Floor(nowZone2Pos.x);
        now2Posy = Mathf.Floor(nowZone2Pos.y);
        now2Posz = Mathf.Floor(nowZone2Pos.z);

        //Vector3型にする
        now2Pos = new Vector3(now2Posx, now2Posy, now2Posz);
    }
}
