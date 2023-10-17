using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    [SerializeField]
   private List<GameObject> objects;
    [SerializeField]
    private Vector3[] StagePos;
 
    void Start()
    {
        ChangeStage();
    }


    void ChangeStage()
    {
        Sfuffle(StagePos);
        for (int i = 0; i < objects.Count; ++i)
        {
            objects[i].transform.position = StagePos[i];
        }
    }
    void Sfuffle(Vector3[] StagePos)
    {
        for (int i=0;i< StagePos.Length;++i)
        {
            Vector3 temp = StagePos[i];
            int randamIndex = Random.Range(0, StagePos.Length);
            StagePos[i] = StagePos[randamIndex];
            StagePos[randamIndex] = temp;

        }

    }
}
