using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TestParentPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("プレイヤー識別用")]
    public TestPlayerNum testPlayerNum;
    public enum TestPlayerNum
    {
        TestPlayer1,TestPlayer2
    }
    public int testPlayerMapNum;
    
    public void TestParentPlayerInitialize()
    {
        testPlayerMapNum = 0;
    }

    public void TestParentPlayerUpdate()
    {
        //操作用関数
        TestPlayerMove();
    }

    //プレイヤー操作用関数(オーバーライドする)
    public virtual void TestPlayerMove()
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
                        testPlayerMapNum = 1;
                        break;
                    case MapArea.AreaNum.Area2:
                        testPlayerMapNum = 2;
                        break;
                    case MapArea.AreaNum.Area3:
                        testPlayerMapNum = 3;
                        break;
                    case MapArea.AreaNum.Area4:
                        testPlayerMapNum = 4;
                        break;
                    case MapArea.AreaNum.Area5:
                        testPlayerMapNum = 5;
                        break;
                    case MapArea.AreaNum.Area6:
                        testPlayerMapNum = 6;
                        break;
                    case MapArea.AreaNum.Area7:
                        testPlayerMapNum = 7;
                        break;
                    case MapArea.AreaNum.Area8:
                        testPlayerMapNum = 8;
                        break;
                    case MapArea.AreaNum.Area9:
                        testPlayerMapNum = 9;
                        break;
                }

                //Debug.Log(nowMapNum);
            }
        }
    }
}
