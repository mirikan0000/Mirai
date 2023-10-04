using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField]
    [Header("ミニマップカメラの位置,移動用")]
    Vector3 miniMapCameraTargetPos;   //ミニマップカメラの現在の位置情報
    Vector3 miniMapCameraDefaultPos;  //ミニマップカメラの基準位置
    Transform miniMapCameraTransform;  //ミニマップカメラの位置情報
    public float miniMapCameraMoveSpeed;  //ミニマップカメラの移動スピード

    [Header("各プレイヤーのマップ番号用")]
    public int player1MapNum;  //Player1の現在位置
    public int player2MapNum;  //Player2の現在位置

    [Header("プレイヤー同士の位置比較用")]
    public bool checkMapNum = false;  //Player同士のマップ番号が同じかどうか
    public int bothPlayersNum;  //Player同士のマップ番号が同じ時のマップ番号

    [Header("表示オブジェクトの拡縮用")]
    public float objScaleX;// = 1.0f;  //表示オブジェクトの大きさX方向
    public float objScaleY;// = 0.2f;  //表示オブジェクトの大きさY方向
    public float objScaleZ;// = 1.0f;  //表示オブジェクトの大きさZ方向
    private GameObject p1FakeObj;  //ミニマップに表示するPlayer1のオブジェクト
    private GameObject p2FakeObj;  //ミニマップに表示するPlayer2のオブジェクト

    //Player1の現在のマップ番号を取得する用
    Player1 player1Script;          //Player2のスクリプトを入れる用
    private GameObject player1Obj;  //Player1のオブジェクトを入れる用

    //Player2の現在のマップ番号を取得する用
    Player2 player2Script;          //Player2のスクリプトを入れる用
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
        //Debug.Log(miniMapCameraTransform);

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

            //表示オブジェクトを縮小
            ChangePlayerFakeObjScaleShrink();
        }
        else
        {
            //フラグがfalseならミニマップカメラをデフォルトの位置に戻す
            miniMapCameraTransform.position = Vector3.MoveTowards(miniMapCameraTransform.position, miniMapCameraDefaultPos, miniMapCameraMoveSpeed * Time.deltaTime);

            //表示オブジェクトを拡大
            ChangePlayerFakeObjScaleExpand();
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
        player1Obj = GameObject.FindWithTag("Player1");

        //Player1のミニマップに表示するオブジェクトを取得
        p1FakeObj = GameObject.Find("Player1FakeObj");
        objScaleX = p1FakeObj.transform.localScale.x;
        objScaleY = p1FakeObj.transform.localScale.y;
        objScaleZ = p1FakeObj.transform.localScale.z;
        Debug.Log(p1FakeObj.transform.localScale);

        //player1ObjNullチェック
        if (player1Obj != null)
        {
            //Player1のスクリプトを取得
            Player1 player1Script = player1Obj.GetComponent<Player1>();

            //player1ScriptNullチェック
            if (player1Script != null)
            {
                //Player1の現在位置のマップ番号取得
                player1MapNum = player1Script.p1NowMapNum;
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
        player2Obj = GameObject.FindWithTag("Player2");

        //Player2のミニマップに表示するオブジェクトを取得
        p2FakeObj = GameObject.Find("Player2FakeObj");

        //Player2ObjNullチェック
        if (player2Obj != null)
        {
            //Player2のスクリプトを取得
            player2Script = player2Obj.GetComponent<Player2>();
            //Debug.Log(player2Script);

            //player2ScriptNullチェック
            if (player2Script != null)
            {
                player2MapNum = player2Script.p2NowMapNum;
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
            //Debug.Log(bothPlayersNum);
        }
        else
        {
            checkMapNum = false;
            ChangePlayerFakeObjScaleShrink();
            //Debug.Log(checkMapNum);
        }
    }

    //カメラの位置によってミニマップに表示するプレイヤーのオブジェクトを拡大する
    private void ChangePlayerFakeObjScaleExpand()
    {
        //オブジェクトのサイズを拡大
        if (objScaleX < 2.0f && objScaleZ < 2.0f)
        {
            objScaleX += 1.0f; // * Time.deltaTime;
            objScaleZ += 1.0f; // * Time.deltaTime;
        }

        //オブジェクトのサイズを変更
        p1FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
        p2FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
    }

    //カメラの位置によってミニマップに表示するプレイヤーのオブジェクトを拡大する
    private void ChangePlayerFakeObjScaleShrink()
    {
        //オブジェクトのサイズを縮小
        if (objScaleX > 1.0f && objScaleZ > 1.0f)
        {
            objScaleX -= 1.0f; // * Time.deltaTime;
            objScaleZ -= 1.0f; // * Time.deltaTime;
        }

        //オブジェクトのサイズを変更
        p1FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
        p2FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
    }
}
