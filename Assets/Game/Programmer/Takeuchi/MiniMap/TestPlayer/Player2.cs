using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2: ParentPlayer
{
    [SerializeField]
    [Header("現在位置のマップ番号")]
    public int p2NowMapNum;
    public GameObject[] feels;
    void Start()
    {
        base.ParentPlayerInitialize();
        // ランダムにナンバーを選択
        int number = Random.Range(0, feels.Length);

        // 選択されたナンバーに応じて位置を設定
        if (number >= 0 && number < feels.Length)
        {
            this.transform.position = feels[number].transform.position;
        }

        //現在位置のマップ番号を初期化
        p2NowMapNum = playerMapNum;
    }


    void FixedUpdate()
    {
        p2NowMapNum = playerMapNum;

    }

    //プレイヤー操作
    //public override void PlayerMove()
    //{
    //    if (Input.GetKey(KeyCode.W))  //上
    //    {
    //        transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.S))  //下
    //    {
    //        transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.A))  //左
    //    {
    //        transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))  //右
    //    {
    //        transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
    //    }
    //}
}
