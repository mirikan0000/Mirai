using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraTest : MonoBehaviour
{
    [SerializeField]
    [Header("ミニマップカメラの位置,移動用")]
    Vector3 miniMapCameraTargetPos;   //ミニマップカメラの現在の位置情報
    Vector3 miniMapCameraDefaultPos;  //ミニマップカメラの基準位置
    Transform miniMapCameraTransform;  //ミニマップカメラの位置情報
    public float miniMapCameraMoveSpeed;  //ミニマップカメラの移動スピード

    [Header("各プレイヤーのマップ番号用")]
    public int player1MapNum;
    public int player2MapNum;

    [Header("プレイヤー同士の位置比較用")]
    public bool checkMapNum = false;  //Player同士のマップ番号が同じかどうか
    public int bothPlayersNum;  //Player同士のマップ番号が同じ時のマップ番号

    //Player1の現在のマップ番号を取得する用
    TestPlayer1 player1Script;          //Player2のスクリプトを入れる用
    private GameObject player1Obj;  //Player1のオブジェクトを入れる用

    //Player2の現在のマップ番号を取得する用
    TestPlayer2 player2Script;          //Player2のスクリプトを入れる用
    private GameObject player2Obj;  //Player2のオブジェクトを入れる用



    void Start()
    {
        //Player1のマップ番号初期化
        player1MapNum = 0;
        //Player2のマップ番号初期化
        player2MapNum = 0;

        //ミニマップカメラの移動スピードを設定
        miniMapCameraMoveSpeed = 10f;

        //ミニマップカメラの位置情報を取得
        miniMapCameraTransform = this.GetComponent<Transform>();
        Debug.Log(miniMapCameraTransform);
        //ミニマップカメラのデフォルト位置を設定
        miniMapCameraDefaultPos = new Vector3(0, 25, 0);
    }

    void FixedUpdate()
    {
        GetPlayer1();  //Player1のオブジェクト、スクリプト、現在位置のマップ番号を取得
        GetPlayer2();  //Player2のオブジェクト、スクリプト、現在位置のマップ番号を取得

        ComparePlayerMapNum();  //各プレイヤーの位置によってフラグを管理

        //フラグによってカメラを移動
        if (checkMapNum == true)
        {
            MoveMiniMapCamera();  //カメラを移動させる
        }
        else
        {
            //フラグがfalseならミニマップカメラをデフォルトの位置に戻す
            miniMapCameraTransform.position = Vector3.MoveTowards(miniMapCameraTransform.position, miniMapCameraDefaultPos, miniMapCameraMoveSpeed * Time.deltaTime);
        }
    }

    //カメラを移動させる
    private void MoveMiniMapCamera()
    {
        //両プレイヤーがいるマップにフォーカスする
        switch (bothPlayersNum)
        {
            case 0:  //カメラをデフォルト位置に
                miniMapCameraTargetPos = miniMapCameraDefaultPos;
                break;
            case 1:  //カメラをマップ番号１のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(-10, 9, -10);
                break;
            case 2:  //カメラをマップ番号２のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(0, 9, -10);
                break;
            case 3:  //カメラをマップ番号３のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(10, 9, -10);
                break;
            case 4:  //カメラをマップ番号４のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(-10, 9, 0);
                break;
            case 5:  //カメラをマップ番号５のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(0, 9, 0);
                break;
            case 6:  //カメラをマップ番号６のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(10, 9, 0);
                break;
            case 7:  //カメラをマップ番号７のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(-10, 9, 10);
                break;
            case 8:  //カメラをマップ番号８のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(0, 9, 10);
                break;
            case 9:  //カメラをマップ番号９のマップにフォーカスする
                miniMapCameraTargetPos = new Vector3(10, 9, 10);
                break;
        }

        //ミニマップカメラの位置を移動
        miniMapCameraTransform.position = Vector3.MoveTowards(miniMapCameraTransform.position, miniMapCameraTargetPos, miniMapCameraMoveSpeed * Time.deltaTime);
    }

    //Player1のオブジェクト、スクリプト、現在位置のマップ番号を取得
    private void GetPlayer1()
    {
        //Player1のオブジェクトを取得
        player1Obj = GameObject.FindWithTag("TestPlayer1");
        //Debug.Log(player1Obj);

        //player1ObjNullチェック
        if (player1Obj != null)
        {
            //Player1のスクリプトを取得
            player1Script = player1Obj.GetComponent<TestPlayer1>();

            //player1ScriptNullチェック
            if (player1Script != null)
            {
                //Player1の現在位置のマップ番号取得
                player1MapNum = player1Script.testP1NowMapNum;
                //Debug.Log(player1MapNum);
            }
            else
            {
                Debug.Log("Player1のスクリプトが見つかりません");
            }
        }
        else
        {
            Debug.Log("Player1のオブジェクトが見つかりません");
        }
    }

    //Player2のオブジェクト、スクリプト、現在位置のマップ番号を取得
    private void GetPlayer2()
    {
        //Player2のオブジェクトを取得
        player2Obj = GameObject.FindWithTag("TestPlayer2");
        //Debug.Log(player2Obj);

        //Player2ObjNullチェック
        if (player2Obj != null)
        {
            //Player2のスクリプトを取得
            player2Script = player2Obj.GetComponent<TestPlayer2>();
            //Debug.Log(player2Script);

            //player2ScriptNullチェック
            if (player2Script != null)
            {
                player2MapNum = player2Script.testP2NowMapNum;
                //Debug.Log(player2MapNum);
            }
            else
            {
                Debug.Log("Player2のスクリプトが見つかりません");
            }
        }
        else
        {
            Debug.Log("Player2のオブジェクトが見つかりません");
        }
    }

    //各プレイヤーの位置によってフラグを管理
    private void ComparePlayerMapNum()
    {
        if (player1MapNum == player2MapNum)
        {
            checkMapNum = true;
            bothPlayersNum = player1MapNum;
            Debug.Log(bothPlayersNum);
        }
        else
        {
            checkMapNum = false;
            Debug.Log(checkMapNum);
        }
    }
}
