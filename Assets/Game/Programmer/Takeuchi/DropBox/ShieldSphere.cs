using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSphere : MonoBehaviour
{
    [SerializeField]
    [Header("シールドのHP")]
    public int shieldHP;  //シールドの防御回数

    [Header("オブジェクト取得用")]
    GameObject parentObj;  //親オブジェクト

    [Header("追加するマテリアル")]
    public Material material;

    
    void Start()
    {
        //親オブジェクト取得
        //GetParentObj();
    }

    // Update is called once per frame
    void Update()
    {
        //ObjectRotation();

        if (shieldHP <= 0)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            shieldHP = shieldHP - 1;
        }
    }

    //親オブジェクト取得
    private void GetParentObj()
    {
        parentObj = transform.parent.gameObject;
    }

    //衝突処理
    private void OnTriggerEnter(Collider other)
    {
        //Stageのオブジェクトと親オブジェクト以外に当たったら
        //if(other.gameObject.name != parentObj.name &&
        //    other.gameObject.tag != "StageObject")
        //{
        //    shieldHP = shieldHP - 1;
        //}
        Debug.Log(other.gameObject.name);
    }

    //オブジェクト回転
    private void ObjectRotation()
    {
        transform.Rotate(new Vector3(10, 10, 10));
    }
}
