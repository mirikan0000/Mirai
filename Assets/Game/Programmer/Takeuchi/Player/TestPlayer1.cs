using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer1 : TestParentPlayer
{
    [SerializeField]
    [Header("現在位置のマップ番号")]
    public int testP1NowMapNum;

    void Start()
    {
        base.TestParentPlayerInitialize();

        //現在位置のマップ番号を初期化
        testP1NowMapNum = testPlayerMapNum;
        //Debug.Log(p1NowMapNum);
    }

    void FixedUpdate()
    {
        testP1NowMapNum = testPlayerMapNum;
        //Debug.Log(p1NowMapNum);

        //操作用
        TestPlayerMove();
    }

    //プレイヤー操作
    public override void TestPlayerMove()
    {
        if (Input.GetKey(KeyCode.W))  //上
        {
            transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))  //下
        {
            transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))  //左
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))  //右
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
    }

}
