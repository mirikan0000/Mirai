using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    [Header("安置生成用")]
    public GameObject saftyZonebj;  //生成する安置のオブジェクト
    public Transform rangeA;        //生成する範囲A
    public Transform rangeB;        //生成する範囲B
    public float spawnDelayTime;    //生成までの待ち時間
    public bool spawnFlag;          //生成用フラグ

    private Vector3 spawnPos;       //生成する座標
    private float timer;            //待ち時間計測用

    void Start()
    {
        //変数初期化
        spawnFlag = true;
        timer = 0.0f;
    }

    void Update()
    {
        //生成用フラグがTrueなら安置生成
        if (spawnFlag == true)
        {
            //安置生成
            SpawnSaftyZone();
        }
    }

    //安置生成
    private void SpawnSaftyZone()
    {
        var parent = this.transform;

        //生成する座標をランダムに設定
        float x = Random.Range(rangeA.position.x, rangeB.position.x);
        float y = Random.Range(rangeA.position.y, rangeB.position.y);
        float z = Random.Range(rangeA.position.z, rangeB.position.z);
        spawnPos = new Vector3(x, y, z);

        //待ち時間計測開始
        timer += Time.deltaTime;

        //待ち時間経過後
        if (timer > spawnDelayTime)
        {
            //安置を子オブジェクトとして生成
            Instantiate(saftyZonebj, spawnPos, Quaternion.identity, parent);

            //生成用フラグをFalseに
            spawnFlag = false;

            //待ち時間を初期化
            timer = 0.0f;
        }
    }
}
