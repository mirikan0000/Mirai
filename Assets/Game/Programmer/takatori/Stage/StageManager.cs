using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects;
    [SerializeField]
    private Vector3[] StagePos;
    public GameObject Panel;
    void Start()
    {
        Panel.GetComponent<TittleSceneFadeOut>().fadein=true;   
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
        for (int i = 0; i < StagePos.Length; ++i)
        {
            Vector3 temp = StagePos[i];
            int randamIndex = Random.Range(0, StagePos.Length);
            StagePos[i] = StagePos[randamIndex];
            StagePos[randamIndex] = temp;

        }

    }
}
