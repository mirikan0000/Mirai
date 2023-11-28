using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragreceiver : MonoBehaviour
{
    public bool misileflog;
    public bool armorflog;
    public bool Hpflog;
    public bool penetratBulletflog;

    //補給物資取得用
    public bool speedUpItemFlag;       //スピードアップのアイテムを取得しているか
    public bool healItemFlag;          //回復のアイテムを取得しているか
    public bool shieldItemFlag;        //シールドのアイテムを取得しているか
    public bool pierceBulletItemFlag;  //貫通弾のアイテムを取得しているか

   [SerializeField] private PlayerHealth P_Health;//HPフラグからHPを減らすための箱
    private void Start()
    {
        penetratBulletflog = false;
        misileflog = false;

        //補給物資取得用
        speedUpItemFlag = false;
        healItemFlag = false;
        shieldItemFlag = false;
        penetratBulletflog = false;
    }
    private void Update()
    {
        
    }

    //補給物資取得処理
    private void OnCollisionEnter(Collision collision)
    {
        //スピードアップのアイテムを取得した時
        if (collision.gameObject.name == "SpeedUpItem(Clone)")
        {
            speedUpItemFlag = true;
        }
        //回復のアイテムを取得した時
        if (collision.gameObject.name == "HealItem(Clone)")
        {
            healItemFlag = true;
        }
        //シールドのアイテムを取得した時
        if(collision.gameObject.name == "ShieldItem(Clone)")
        {
            shieldItemFlag = true;
        }
        //貫通弾のアイテムを取得した時
        if(collision.gameObject.name == "PierceBulletItem(Clone)")
        {
            penetratBulletflog = true;
        }
    }
}
