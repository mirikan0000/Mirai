using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : ParentPlayer
{
    [SerializeField]
    [Header("現在位置のマップ番号")]
    public int p1NowMapNum;

    void Start()
    {
        base.ParentPlayerInitialize();

        //現在位置のマップ番号を初期化
        //p1NowMapNum = playerMapNum;
        //Debug.Log(p1NowMapNum);
    }


    void FixedUpdate()
    {
        p1NowMapNum = playerMapNum;
        //Debug.Log(p1NowMapNum);

        //操作用
        PlayerMove();
    }

    //プレイヤー操作
    public override void PlayerMove()
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
