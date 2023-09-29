using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : ParentPlayer
{
    [SerializeField]
    [Header("現在位置のマップ番号")]
    public int p2NowMapNum;

    void Start()
    {
        base.ParentPlayerInitialize();

        //現在位置のマップ番号の初期化
        p2NowMapNum = 0;
    }

    void FixedUpdate()
    {
        p2NowMapNum = playerMapNum;
        //Debug.Log(p2NowMapNum);
        //移動用
        PlayerMove();
    }

    //プレイヤー操作
    public override void PlayerMove()
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
