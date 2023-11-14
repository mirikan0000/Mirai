using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestCube : MonoBehaviour
{
    [SerializeField]
    [Header("各種変数")]
    public VisualEffect walkEffectObj;  //歩いているときのエフェクトオブジェクト
    public Vector3 effectSpawnPos;      //エフェクト生成位置
    public float delayTimer,            //待ち時間計測用
                 timerLimit;            //最大待ち時間
    public bool destroyFlag = false;    //オブジェクト破壊フラグ

    public AudioSource walkSE;          //歩行時SEを再生するコンポーネント
    public AudioClip source1;           //歩行時に再生するSE

    private GameObject parentSpawner;   //親オブジェクト
    private SpawnTestCube parentScript; //親オブジェクトのスクリプト

    void Start()
    {
        //各種変数初期化
        delayTimer = 0.0f;

        //親オブジェクトのスクリプト取得
        parentSpawner = transform.parent.gameObject;
        parentScript = parentSpawner.GetComponent<SpawnTestCube>();

        //AudioSource取得
        walkSE = GetComponent<AudioSource>();
    }

    void Update()
    {
        //フラグで破壊
        if (destroyFlag == true)
        {
            //待ち時間計測開始
            delayTimer += Time.deltaTime;

            

            //待ち時間が一定で破壊
            if (delayTimer > timerLimit)
            {
                //親オブジェクトの生成するためのフラグをTrueにする
                parentScript.spawnFlag = true;

                //破壊フラグをFalseにする
                destroyFlag = false;

                //タイマー初期化
                delayTimer = 0.0f;

                //オブジェクト破壊
                Destroy(this.gameObject);
            }
        }
    }

    //地面に当たったら
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            //エフェクト発生
            Instantiate(walkEffectObj, effectSpawnPos, Quaternion.identity);

            //歩行時SE再生
            walkSE.PlayOneShot(source1);

            //破壊フラグをTrueにする
            destroyFlag = true;
        }
    }

}
