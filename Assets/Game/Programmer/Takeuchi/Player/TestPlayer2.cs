using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2 : TestParentPlayer
{
    [SerializeField]
    [Header("現在位置のマップ番号")]
    public int testP2NowMapNum;

    void Start()
    {
        base.TestParentPlayerInitialize();

        //現在位置のマップ番号の初期化
        testP2NowMapNum = 0;
    }


    void FixedUpdate()
    {
        testP2NowMapNum = testPlayerMapNum;
        //Debug.Log(p2NowMapNum);
        //移動用
        TestPlayerMove();
    }

    //プレイヤー操作
    public override void TestPlayerMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //上
        {
            transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))  //下
        {
            transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))  //左
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))  //右
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
    }
}
