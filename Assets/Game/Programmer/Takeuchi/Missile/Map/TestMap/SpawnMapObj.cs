using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapObj : MonoBehaviour
{
    [SerializeField]
    [Header("生成するマップのオブジェクト")]
    public GameObject mapObj;

    private Vector3 mapObjSpawnPos;    //
    public bool spawnFlag;             //

    void Start()
    {
        spawnFlag = true;

        mapObjSpawnPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnFlag == true)
        {
            var parent = this.transform;
            Instantiate(mapObj, mapObjSpawnPos, Quaternion.identity, parent);

            spawnFlag = false;
        }
    }
}
