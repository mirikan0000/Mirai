using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("安置の中心地点生成用")]
    public GameObject saftyZoneObj;  //生成する安置のオブジェクト
    public Transform saftyRangeA;    //生成する範囲指定地点A
    public Transform saftyRangeB;    //生成する範囲指定地点B
    public bool spawnCheck = false;  //生成できるかどうか(true = 生成可能、false = 生成不可）
    public bool spawnType = true;    //生成位置のフラグ
    public float delayTime;          //待ち時間

    void Start()
    {
        spawnCheck = true;
    }

    void Update()
    {
        if (spawnType == true)
        {
            if (spawnCheck == true)
            {
                //ランダム生成
                Invoke("SaftyZoneRandomSpawn", delayTime);

                spawnCheck = false;
            }
        }
        else
        {
            if (spawnCheck == true)
            {
                //中心に生成
                Invoke("SaftyZoneCenterSpawn", delayTime);

                spawnCheck = false;
            }
        }
    }

    //生成位置をランダムで指定
    private void SaftyZoneRandomSpawn()
    {
        float x = Random.Range(saftyRangeA.position.x, saftyRangeB.position.x);
        float y = Random.Range(saftyRangeA.position.y, saftyRangeB.position.y);
        float z = Random.Range(saftyRangeA.position.z, saftyRangeB.position.z);

        //安置を生成
        Instantiate(saftyZoneObj, new Vector3(x, y, z), Quaternion.identity);
    }

    //生成位置を中心にする
    private void SaftyZoneCenterSpawn()
    {
        Instantiate(saftyZoneObj, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
