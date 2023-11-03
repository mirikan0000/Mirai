using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone4 : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト取得用")]
    public GameObject childSaftyZone4Obj;  //自身のゲームオブジェクト
    public string childSaftyZone4ObjName;  //自身の名前
    GameObject parentSaftyZoneObj;         //親オブジェクト
    SaftyZone parentSaftyZoneScript;       //親オブジェクトのスクリプト
    [Header("移動用")]
    public Vector3 preZone4pos;            //自身の初期位置
    public Vector3 postZone4Pos;           //自身の移動後の目標位置
    public Vector3 nowZone4Pos;            //自身の現在位置
    public int zone4MoveSpeed;           //移動速度

    public Vector3 pre4Pos;                //初期位置用(小数点以下切り捨て
    float pre4Posx, pre4Posy, pre4Posz;    

    public Vector3 now4Pos;               　　　　//現在位置用(小数点以下切り捨て
    float now4Posx, now4Posy, now4Posz;

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
        Zone4FlagInitialize();

        //安置の名前と初期位置を取得
        childSaftyZone4Obj = this.gameObject;
        childSaftyZone4ObjName = childSaftyZone4Obj.name;
        preZone4pos = childSaftyZone4Obj.transform.position;

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
    private void Zone4FlagInitialize()
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
        pre4Posx = Mathf.Floor(preZone4pos.x);
        pre4Posy = Mathf.Floor(preZone4pos.y);
        pre4Posz = Mathf.Floor(preZone4pos.z);
        pre4Pos = new Vector3(pre4Posx, pre4Posy, pre4Posz);

        //目標位置設定
        postZone4Pos = new Vector3(pre4Posx, pre4Posy, pre4Posz - 425.0f);

        //設定した後縮小位置設定用フラグをFalseにする
        setReduPosFlag = false;
    }

    //縮小
    private void ReducationZone2()
    {
        //縮小目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone4Pos, zone4MoveSpeed);
    }

    //縮小完了チェック
    private void Zone2ReducationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now4Pos, postZone4Pos);

        //縮小完了しているか
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //完了していたら親オブジェクトの変数を加算
                parentSaftyZoneScript.zone4redu = true;
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
        postZone4Pos = pre4Pos;

        //設定した後拡大後位置設定用フラグをFalseにする
        setMagPosFlag = false;
    }

    //拡大
    private void MagnificationZone2()
    {
        //拡大目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, postZone4Pos, zone4MoveSpeed);
    }

    //拡大完了チェック
    private void Zone2MagnificationCheck()
    {
        //現在位置を取得
        GetNowPos();

        dis = Vector3.Distance(now4Pos, postZone4Pos);

        //拡大完了しているか
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //親オブジェクトの変数を加算
                parentSaftyZoneScript.zone4mag = true;

                endMagFlag = true;
            }
        }
    }

    //現在地取得用
    private void GetNowPos()
    {
        //現在位置を取得
        nowZone4Pos = this.gameObject.transform.position;

        //取得した座標を小数点以下切り捨て
        now4Posx = Mathf.Floor(nowZone4Pos.x);
        now4Posy = Mathf.Floor(nowZone4Pos.y);
        now4Posz = Mathf.Floor(nowZone4Pos.z);

        //Vector3型にする
        now4Pos = new Vector3(now4Posx, now4Posy, now4Posz);
    }
}
