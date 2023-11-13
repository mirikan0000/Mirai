using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTestCube : MonoBehaviour
{
    [SerializeField]
    [Header("生成関係")]
    public GameObject testCubeObj;  //生成するオブジェクト
    public Vector3 spawnCubePos;    //生成する位置
    public bool spawnFlag = true;   //生成するためのフラグ

    void Start()
    {
        
    }

   
    void Update()
    {
        if (spawnFlag == true)
        {
            //生成するオブジェクトのNullチェック
            if (testCubeObj != null)
            {
                //生成
                var parent = this.transform;

                Instantiate(testCubeObj, spawnCubePos, Quaternion.identity, parent);

                //生成するためのフラグをFalseにする
                spawnFlag = false;
            }
            else
            {
                Debug.Log("生成するオブジェクトが設定されていません");
            }
        }
    }
}
