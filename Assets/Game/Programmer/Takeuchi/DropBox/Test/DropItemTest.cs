using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemTest : MonoBehaviour
{
    public enum ItemName
    {
        HealItem,SpeedUpItem,PierceBulletItem,ShieldItem
    }
    [SerializeField]
    [Header("各種変数")]
    public ItemName itemName;  //アイテム識別用
    //各オブジェクトごとに必要な値のみ設定
    public float healPoint;    //回復量
    public float speedValue;   //加速量
    public int pierceCount;    //貫通する最大回数
    public int shieldCount;    //最大防御回数
    private bool destroyFlag = false;

    void Start()
    {
        destroyFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyFlag = true;
        }
    }
}
