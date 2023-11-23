using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject zoneObj;

    public int nextStageNum;

    public bool flag;
    private Vector3 spawnPos;

    public bool scaleChengeFlag;

    void Start()
    {
        spawnPos = this.transform.position;
        flag = true;
        scaleChengeFlag = true;
        nextStageNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(nextStageNum);
        var parent = this.transform;

        if (flag == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Instantiate(zoneObj, spawnPos, Quaternion.identity, parent);

                flag = false;
            }
        }

        if (flag == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            nextStageNum = nextStageNum + 1;
        }
    }
}
