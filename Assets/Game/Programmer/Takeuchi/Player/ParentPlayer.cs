using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("プレイヤー識別用")]
    public PlayerNum playerNum;
    public enum PlayerNum
    {
        Player1,Player2
    }
    [Header("現在位置のマップ番号")]
    public int playerMapNum;

    public void ParentPlayerInitialize()
    {
        playerMapNum = 0;
    }

    public void ParentPlayerUpdate()
    {
        //操作用関数
        PlayerMove();
    }

    //プレイヤー操作用関数(オーバーライドする)
    public virtual void PlayerMove()
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

    //特定のエリアに入ったら
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnterArea");

        //Areaタグが付いているか
        if (other.gameObject.CompareTag("Area"))
        {
            //スクリプト取得
            MapArea otherMapAreaScript = other.gameObject.GetComponent<MapArea>();

            //取得できたら
            if (otherMapAreaScript != null)
            {
                //AreaNumによってPlayerの現在のマップ番号を更新
                switch (otherMapAreaScript.areaNum)
                {
                    case MapArea.AreaNum.Area1:
                        playerMapNum = 1;
                        break;
                    case MapArea.AreaNum.Area2:
                        playerMapNum = 2;
                        break;
                    case MapArea.AreaNum.Area3:
                        playerMapNum = 3;
                        break;
                    case MapArea.AreaNum.Area4:
                        playerMapNum = 4;
                        break;
                    case MapArea.AreaNum.Area5:
                        playerMapNum = 5;
                        break;
                    case MapArea.AreaNum.Area6:
                        playerMapNum = 6;
                        break;
                    case MapArea.AreaNum.Area7:
                        playerMapNum = 7;
                        break;
                    case MapArea.AreaNum.Area8:
                        playerMapNum = 8;
                        break;
                    case MapArea.AreaNum.Area9:
                        playerMapNum = 9;
                        break;
                }

                //Debug.Log(nowMapNum);
            }
        }
    }
}
