using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objects;
    [SerializeField]
    Vector3[] StagePos;
 
    void Start()
    {
        ChangeStage();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            ChangeStage();
        }
    }
    void ChangeStage()
    {
        Sfuffle(StagePos);
        for (int i = 0; i < objects.Count; ++i)
        {
            objects[i].transform.position = StagePos[i];
            Debug.Log(StagePos[i]);
        }
    }
    void Sfuffle(Vector3[] num)
    {
        for (int i=0;i<num.Length;++i)
        {
            Vector3 temp = num[i];
            int randamIndex = Random.Range(0,num.Length);
            num[i] = num[randamIndex];
            num[randamIndex] = temp;
        }
    }
}
