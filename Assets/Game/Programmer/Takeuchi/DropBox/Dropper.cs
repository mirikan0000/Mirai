using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    [Header("生成するオブジェクト")]
    public GameObject dropObj;  //空から降らせるオブジェクト

    [Header("生成する範囲用オブジェクト")]
    public GameObject dropRangeA;  //生成範囲指定用オブジェクトA
    public GameObject dropRangeB;  //生成範囲指定用オブジェクトB

    [Header("各種変数")]
    public int dropCount;     　　//生成されている数
    public int maxDropCount;  　　//生成できる最大量
    public float delayTime;   　　//再生成までの時間
    private float timer;          //生成からの経過時間
    public bool dropFlag = false; //生成するかどうか
    private float x, y, z;    　　//生成する位置


    void Start()
    {
        //各種変数初期化
        dropCount = 0;
        timer = 0.0f;

    }

    void Update()
    {
        //経過時間
        timer += Time.deltaTime;

        //生成可能になっているか
        if (dropFlag == true)
        {
            //最大投下量になるまで生成
            if (maxDropCount > dropCount)
            {
                if (timer > delayTime)
                {
                    //補給箱生成
                    Drop();

                    timer = 0.0f;
                }
            }
            else
            {
                dropFlag = false;
            }
        }
        else
        {
            Debug.Log("最大量投下しました");
        }
    }

    //補給箱を生成する
    private void Drop()
    {
        //落とすオブジェクトが設定されているか
        if (dropObj != null)
        {
            //親オブジェクトの設定
            var parent = this.transform;

            //範囲内のランダムな位置を設定
            x = Random.Range(dropRangeA.transform.position.x, dropRangeB.transform.position.x);
            y = Random.Range(dropRangeA.transform.position.y, dropRangeB.transform.position.y);
            z = Random.Range(dropRangeA.transform.position.z, dropRangeB.transform.position.z);

            //設定した位置にオブジェクトを子オブジェクトとして生成
            Instantiate(dropObj, new Vector3(x, y, z), Quaternion.identity, parent);

            //生成数カウント用変数を加算
            dropCount += 1;

            //生成可能にする
            dropFlag = true;
        }
    }
}
