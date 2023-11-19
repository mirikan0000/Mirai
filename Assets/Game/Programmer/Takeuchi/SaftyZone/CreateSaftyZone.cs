using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("安置の中心地点生成用")]
    public GameObject saftyZoneObj;   //生成する安置のオブジェクト
    public Transform saftyRangeA;     //生成する範囲指定地点A
    public Transform saftyRangeB;     //生成する範囲指定地点B
    public float delayTime;           //生成までの待ち時間
    public bool spawnCheck;           //生成できるかどうか(true = 生成可能、false = 生成不可）

    private bool spawnType;           //生成位置のフラグ
    private float timer;              //待ち時間計測用

    [Header("安置の段階")]
    public float reduStageCount;        //縮小時の段階数
    public float magStageCount;         //拡大時の段階数
    public float reduCount;             //現在の縮小段階
    public float magCount;              //現在の拡大段階

    void Start()
    {
        //各種変数初期化
        timer = 0.0f;
        spawnCheck = true;
        spawnType = true;
        reduCount = 0;
        magCount = 0;
    }

    void Update()
    {
        if (spawnType == true)
        {
            if (spawnCheck == true)
            {
                //ランダム生成
                SaftyZoneRandomSpawn();
            }
        }
        else
        {
            if (spawnCheck == true)
            {
                //中心に生成
                SaftyZoneCenterSpawn();

                spawnCheck = false;
            }
        }
    }

    //生成位置をランダムで指定
    private void SaftyZoneRandomSpawn()
    {
        var parent = this.transform;

        //範囲内のランダムな座標を設定
        float x = Random.Range(saftyRangeA.position.x, saftyRangeB.position.x);
        float y = Random.Range(saftyRangeA.position.y, saftyRangeB.position.y);
        float z = Random.Range(saftyRangeA.position.z, saftyRangeB.position.z);

        //待ち時間計測開始
        timer += Time.deltaTime;

        //待ち時間が超えたら
        if (timer > delayTime)
        {
            //安置を子オブジェクトとして生成
            Instantiate(saftyZoneObj, new Vector3(x, y, z), Quaternion.identity, parent);

            //安置の縮小段階を一段階目にする
            reduCount = 1;

            spawnCheck = false;

            //待ち時間初期化
            timer = 0.0f;
        }
    }

    //生成位置を中心にする
    private void SaftyZoneCenterSpawn()
    {
        var parent = this.transform;

        //安置を子オブジェクトとして生成
        Instantiate(saftyZoneObj, new Vector3(0, 0, 0), Quaternion.identity, parent);
    }
}
