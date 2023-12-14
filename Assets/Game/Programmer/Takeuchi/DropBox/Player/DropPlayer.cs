using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("各種変数")]
    public float maxHealth;          //プレイヤーの最大HP
    public float nowHealth;         //プレイヤーの現在のHP
    public float moveSpeed;          //移動速度
    public float defaultSpeed;       //元の移動速度
    public float speedTimer;         //スピードアップの時間
    public float speedLimit;         //スピードアップの限界時間
    public bool speedFlag = false;   //スピードアップしているか
    public bool openBoxFlag = false; //補給箱を開けるためのフラグ
    private bool shieldFlag;         //シールドを取得しているか
    Rigidbody rb;
    DropBox dropboxScript;           //補給箱のスクリプト

    public GameObject shieldObj;

    public bool GetOpenBoxFlag()
    {
        return openBoxFlag;
    }

    void Start()
    {
        //各種変数初期化
        moveSpeed = defaultSpeed;
        speedTimer = 0.0f;
        nowHealth = maxHealth;
        shieldFlag = false;

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

        PlayerMove();
    }

    //補給箱に当たったときの処理
    private void OnCollisionEnter(Collision collision)
    {
        //当たったオブジェクトがスピードアップのものだった時
        if (collision.gameObject.name == "SpeedUpItem(Clone)")
        {
            //移動速度UP
            speedFlag = true;
            speedTimer = 0.0f;
            Debug.Log("加速");
        }
        else if (collision.gameObject.name == "HealItem(Clone)")
        {
            nowHealth = maxHealth;
            Debug.Log("回復");
        }
        else if (collision.gameObject.name == "PierceBulletItem(Clone)")
        {
            Debug.Log("貫通弾取得");
        }
        else if (collision.gameObject.name == "ShieldItem(Clone)")
        {
            shieldFlag = true;
            Debug.Log("シールド取得");
        }
        else if (collision.gameObject.name == "DropBox(Clone)")
        {
            nowHealth -= 2;
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

        //左右回転
        //右回転
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0.0f, -50 * Time.deltaTime, 0.0f);
        }

        //左回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += new Vector3(0.0f, 50 * Time.deltaTime, 0.0f);
        }

        //物資を開ける
        if (openBoxFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                dropboxScript.openFlag = true;
            }
        }

        //シールド展開
        if (shieldFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                var parent = this.transform;

                Instantiate(shieldObj, this.transform.position, Quaternion.identity, parent);
            }
        }
    }
}
