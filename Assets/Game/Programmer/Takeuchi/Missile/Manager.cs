using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト生成用")]
    public GameObject missileObj;  //生成するミサイルのオブジェクト
    public Transform missile_point;      //ミサイルを生成する位置
    public GameObject targetObj;   //生成するターゲットのオブジェクト
    public Transform target_point;       //ターゲットを生成する位置
    public bool targetCheck = true;  //ターゲット生成用フラグ

    [Header("各種数値管理用")]
    public int targetHP;    //ターゲットの現在のHP
    public int missileCntMax = 20;  //一気に生成するミサイルの最大数
    public int missileCnt;  //生成されているミサイルの数
    

    void Start()
    {
        //各数値の初期化
        missileCnt = 0;
        targetHP = 0;

        //ターゲット生成
        Instantiate(targetObj, target_point.transform.position, Quaternion.identity);
    }

    void Update()
    {
        //キーボードでオブジェクトを生成
        SpawnObject();
    }

    //各オブジェクト生成用
    private void SpawnObject()
    {
        if (missileObj != null)
        {
            //if (missileCnt < missileCntMax && Input.GetKeyDown(KeyCode.Z))
            //{
            //    Instantiate(missileObj, missile_point.transform.position, Quaternion.identity);
            //    missileCnt++;
            //}

            //子オブジェクトとして生成
            if (missileCnt < missileCntMax && Input.GetKeyDown(KeyCode.Z))
            {
                var parent = this.transform;
                
                Instantiate(missileObj, missile_point.transform.position, Quaternion.identity, parent);
                missileCnt++;
            }
        }

        if (targetObj != null)
        {
            if (targetCheck == true && Input.GetKeyDown(KeyCode.X))
            {
                Instantiate(targetObj, target_point.transform.position, Quaternion.identity);
                targetCheck = false;
            }
        }
    }
}
