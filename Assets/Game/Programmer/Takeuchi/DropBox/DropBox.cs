using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField]
    [Header("生成するオブジェクト")]
    public GameObject healItem;          //回復のオブジェクト
    public GameObject speedUpItem;       //スピードアップのオブジェクト
    public GameObject pierceBulletItem;  //貫通弾のオブジェクト
    public GameObject shieldItem;        //シールドのオブジェクト

    [Header("親オブジェクト＆スクリプト")]
    private GameObject parentObj;  //補給箱を生成するオブジェクト
    private Dropper parentScript; //補給箱を生成するオブジェクトのスクリプト

    [Header("各種変数")]
    public bool openFlag = false;     //アイテム生成用のフラグ
    public bool destroyFlag = false;  //補給箱破壊用のフラグ
    private int itemNum;              //生成するアイテムを決める変数

    void Start()
    {
        //各種変数初期化
        itemNum = 0;

        //補給箱を生成するオブジェクトのスクリプトを取得
        parentObj = this.transform.parent.gameObject;
        parentScript = parentObj.GetComponent<Dropper>();
    }

    
    void Update()
    {
        if (openFlag == true)
        {
            //ランダムでアイテムを生成
            DropRandomItem();
        }

        if (destroyFlag == true)
        {
            //補給箱を生成するオブジェクトの現在生成数を-1
            parentScript.dropCount = parentScript.dropCount - 1;

            //オブジェクト破壊
            Destroy(this.gameObject);
        }
    }

    //ランダムで強化アイテムを生成
    private void DropRandomItem()
    {
        //生成するアイテムをランダムで決める
        itemNum = Random.Range(1, 5);

        //決まった値でアイテムを生成
        switch (itemNum)
        {
            case 1:  //回復のアイテムを生成
                Instantiate(healItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 2:  //スピードアップのアイテムを生成
                Instantiate(speedUpItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 3:  //貫通弾のアイテムを生成
                Instantiate(pierceBulletItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 4:  //シールドのアイテムを生成
                Instantiate(shieldItem, this.gameObject.transform.position, Quaternion.identity);
                break;
        }

        openFlag = false;

        destroyFlag = true;
    }
}
