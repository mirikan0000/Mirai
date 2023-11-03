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
                                            
    [Header("各子オブジェクト移動完了用")]
    public bool zone1redu = false;
    public bool zone1mag = false;
    public bool zone2redu = false;
    public bool zone2mag = false;
    public bool zone3redu = false;
    public bool zone3mag = false;
    public bool zone4redu = false;
    public bool zone4mag = false;

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
