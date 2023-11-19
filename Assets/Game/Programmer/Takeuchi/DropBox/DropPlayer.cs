using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    public static DropPlayer Instance;

    [SerializeField]
    [Header("各種変数")]
    public float moveSpeed;          //移動速度
    public float defaultSpeed;       //元の移動速度
    public float power;              //攻撃力
    public float defaultPawer;       //元の攻撃力
    public float speedTimer;         //スピードアップの時間
    public float speedLimit;         //スピードアップの限界時間
    public float powerTimer;         //パワーアップの時間
    public float powerLimit;         //パワーアップの限界時間
    public bool speedFlag = false;   //スピードアップしているか
    public bool powerFlag = false;   //パワーアップしているか
    public bool openBoxFlag = false; //補給箱を開けるためのフラグ
    Rigidbody rb;
    DropBox dropboxScript;           //補給箱のスクリプト

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        //各種変数初期化
        moveSpeed = defaultSpeed;
        power = defaultPawer;
        speedTimer = 0.0f;
        powerTimer = 0.0f;

        //RigidBody取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //スピードアップ
        if (speedFlag == true)
        {
            speedTimer += Time.deltaTime;

            moveSpeed = 10.0f;

            if (speedTimer > speedLimit)
            {
                speedFlag = false;
                moveSpeed = defaultSpeed;
                speedTimer = 0.0f;
            }
        }

        //パワーアップ
        if (powerFlag == true)
        {
            powerTimer += Time.deltaTime;

            power = 300.0f;

            if (powerTimer > powerLimit)
            {
                powerFlag = false;
                power = defaultPawer;
                powerTimer = 0.0f;
            }
        }

        PlayerMove();
    }

    //補給箱に当たったときの処理
    private void OnCollisionEnter(Collision collision)
    {
        //当たったオブジェクトがスピードアップのものだった時
        if (collision.gameObject.name == "DropBoxSpeed(Clone)")
        {
            //移動速度UP
            speedFlag = true;
            speedTimer = 0.0f;
            Debug.Log("加速！！");
        }
        else if (collision.gameObject.name == "DropBoxPower(Clone)")
        {
            //攻撃力UP
            powerFlag = true;
            powerTimer = 0.0f;
            Debug.Log("攻撃力アップ！！");
        }
    }

    //物資に触れてる間の処理
    private void OnCollisionStay(Collision collision)
    {
        //Playerが補給箱に触れている時
        if (collision.gameObject.name == "DropBox(Clone)")
        {
            //補給箱のスクリプトを取得
            dropboxScript = collision.gameObject.GetComponent<DropBox>();

            Debug.Log("補給箱を開けれるよ");
            openBoxFlag = true;
        }
    }

    //補給箱から離れた時の処理
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "DropBox(Clone)")
        {
            dropboxScript = null;

            Debug.Log("補給箱からはなれた");
            openBoxFlag = false;
        }
    }

    //Playerの移動
    private void PlayerMove()
    {
        //前後左右移動
        //前
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, moveSpeed * Time.deltaTime);
        }

        //後
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -moveSpeed * Time.deltaTime);
        }

        //右
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //左
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //物資を開ける
        if (openBoxFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                dropboxScript.openFlag = true;
            }
        }
    }
}
