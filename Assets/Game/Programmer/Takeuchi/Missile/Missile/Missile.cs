using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class Missile : Weapon
{
    [SerializeField]
    [Header("ミサイル用")]
    public float flyingSpeed = 3.0f;            //ミサイルの飛行速度
    public float missileMaxLifeTime = 20.0f;    //ミサイルの最大飛行時間
    public float missileLifeTime;               //ミサイルの飛行時間
    public float missileMaxHomingTime = 10.0f;  //ミサイルの最大追尾時間
    public float missileHomingTime;             //ミサイルの追尾時間
    public int missileMode;                     //追尾か追尾じゃないか
    public bool missileFireCheck = false;       //ミサイルが発射されたかどうか

    [Header("追尾用")]
    public GameObject player1Obj;  //Player1のオブジェクト
    public GameObject player2Obj;  //Player2のオブジェクト
    public GameObject targetObj;   //ターゲットのオブジェクト
    public NavMeshAgent missile;   //ナビメッシュ取得用
    public int missileShootor;     //誰がミサイルを生成したのか

    public float heightValue;   //ナビメッシュからの高さ

    [Header("親オブジェクト取得用")]
    public GameObject parentPlayerObj;  //ミサイルを発射したPlayerのオブジェクト

    [Header("エフェクト用")]
    public ParticleSystem explosionEffect;  //爆発のエフェクトオブジェクト
    public AudioSource explosionSE;         //爆発時の音声

    void Start()
    {
        //各種変数初期化
        missileLifeTime = 0.0f;
        missileHomingTime = 0.0f;

        //ナビメッシュ取得
        missile = GetComponent<NavMeshAgent>();

        //音声ファイル取得
        explosionSE = GetComponent<AudioSource>();

        //親オブジェクトの取得と飛行状態の判定
        GetParentAndFireCheck();
    }

    // Update is called once per frame
    void Update()
    {
        //敵を補足する
        CaptureEnemy();
     
        //ミサイルを飛ばす
        MoveMissile();
    }

    //当たったら爆発エフェクト&爆発音再生&ミサイル削除
    private void OnTriggerEnter(Collider other)
    {
         //爆発エフェクト再生&破壊
        SpawnEffectAndSEAndDestroy();
    }

    //爆発エフェクト再生
    private void SpawnEffectAndSEAndDestroy()
    {
        if (explosionEffect != null)  //爆発エフェクトオブジェクトが入っているか
        {
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            
            //各時間を初期化
            missileLifeTime = 0.0f;
            missileHomingTime = 0.0f;
        }
        else
        {
            Debug.Log("爆発エフェクトオブジェクトが設定されていない");
        }

        //爆発音声再生
        if (explosionSE != null)  //爆発音が設定されているか
        {
            //Instantiate(explosionSE, this.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("爆発エフェクトの音声が設定されていない");
        }

        //ミサイル本体を破壊
        Destroy(this.gameObject);
    }

    //ミサイルを飛ばす
    private void MoveMissile()
    {
        if (targetObj != null)  //ターゲットが設定されているか
        {
            switch (missileMode)
            {
                case 0:
                    //ミサイルの飛行時間が最大になるまで飛ばす
                    if (missileLifeTime < missileMaxLifeTime)
                    {
                        
                    }
                    else  //最大になったら爆発
                    {
                        SpawnEffectAndSEAndDestroy();
                    }
                    break;
                case 1:
                    //ミサイルの追尾時間が最大になるまで敵に向かって飛ばす(AINavigation使用)
                    if (missileHomingTime < missileMaxHomingTime)
                    {
                        missile.SetDestination(targetObj.transform.position);

                        //高さを変更して飛ばす
                        ChangeMissileHeight();
                    }
                    else  //最大になったら爆発
                    {
                        SpawnEffectAndSEAndDestroy();
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("敵が設定されていません");
        }
        
    }

    //ミサイルの高さを調整
    private void ChangeMissileHeight()
    {
        Vector3 newPos = transform.position;
        newPos.y = GetNavMeshHeight() + heightValue;
        transform.position = newPos;
    }

    //ナビメッシュの高さを取得
    private float GetNavMeshHeight()
    {
        NavMeshHit hit;

        if(NavMesh.SamplePosition(transform.position,out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position.y;
        }
        else
        {
            return 0.0f;
        }
    }

    //敵を補足する
    private void CaptureEnemy()
    {
        if (missileShootor > 0 && missileShootor < 5)
        {
            switch (missileShootor)
            {
                case 1:  //Player1が生成した時
                    targetObj = GameObject.FindWithTag("Player2");
                    break;
                case 2:  //Player2が生成した時
                    targetObj = GameObject.FindWithTag("Player1");
                    break;
                case 3:  //Playerじゃないオブジェクトが生成した時
                    Debug.Log("ミサイルが出現しました");
                    break;
                case 4:  //テスト用
                    targetObj = GameObject.Find("TargetObj(Clone)");
                    break;
            }
        }
        else
        {
            Debug.Log("予期せぬミサイルが生成されました");
        }
    }

    //発射した親オブジェクトのスクリプトを取得し、飛行状態か判定
    private void GetParentAndFireCheck()
    {
        //親オブジェクト取得
        parentPlayerObj = transform.parent.gameObject;

        
        if (parentPlayerObj != null)  //Nullチェック
        {
            //生成した親オブジェクトの名前で飛行状態かの判定
            //誰が発射したのかも判定
            if (parentPlayerObj.gameObject.name == "1P")
            {
                missileShootor = 1;
                missileFireCheck = true;
                
            }
            else if (parentPlayerObj.gameObject.name == "2P")
            {
                missileShootor = 2;
                missileFireCheck = true;
            }
            else if (parentPlayerObj.gameObject.name == "EmpObjManage")
            {
                missileShootor = 4;
                missileFireCheck = true;
            }
            else
            {
                missileShootor = 3;
                missileFireCheck = false;
            }
        }
        else
        {
            Debug.Log("ミサイルを生成した親オブジェクトが見つかりません");
        }
    }
}
