using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField]
    [Header("生成するオブジェクト")]
    public GameObject speedBox;  //スピードアップのオブジェクト
    public GameObject powerBox;  //パワーアップのオブジェクト

    [Header("補給箱生成用オブジェクト関係")]
    public GameObject dropperObj;  //補給箱を生成するオブジェクト
    private Dropper dropperScript; //補給箱を生成するオブジェクトのスクリプト

    [Header("各種変数")]
    public bool openFlag = false;     //アイテム生成用のフラグ
    public bool destroyFlag = false;  //補給箱破壊用のフラグ
    private int itemNum;              //生成するアイテムを決める変数

    void Start()
    {
        //各種変数初期化
        itemNum = 0;

        //補給箱を生成するオブジェクトのスクリプトを取得
        dropperObj = GameObject.Find("EmpObjDropper");
        dropperScript = dropperObj.GetComponent<Dropper>();
    }

    
    void Update()
    {
        if (openFlag == true)
        {
            //ランダムで強化アイテムを生成
            DropRandomItem();
        }

        if (destroyFlag == true)
        {
            //補給箱を生成するオブジェクトの現在生成数を-1
            dropperScript.dropCount--;

            //オブジェクト破壊
            Destroy(this.gameObject);
        }
    }

    //ランダムで強化アイテムを生成
    private void DropRandomItem()
    {
        //生成するアイテムをランダムで決める
        itemNum = Random.Range(1, 3);

        //決まった値でアイテムを生成
        switch (itemNum)
        {
            case 1:  //スピードアップのアイテムを生成
                Instantiate(speedBox, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 2:  //パワーアップのアイテムを生成
                Instantiate(powerBox, this.gameObject.transform.position, Quaternion.identity);
                break;
        }

        openFlag = false;

        destroyFlag = true;
    }
}
