using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTest : MonoBehaviour
{
    public int nextStageCount;  //次の段階
    public Vector3 preScale;    //初期サイズ
    public Vector3 nowScale;    //現在のサイズ

    private bool flag;

    void Start()
    {
        nextStageCount = 0;
        preScale = this.transform.localScale;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag==true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log(nextStageCount);
                GetNowScale();

                flag = false;
            }

            if (Input.GetKey(KeyCode.R))
            {
                if (nextStageCount == 0)
                {
                    nextStageCount = nextStageCount + 1;
                    this.transform.localScale = new Vector3(3, 300, 3);

                    flag = false;
                }
                else if (nextStageCount == 1)
                {
                    nextStageCount = nextStageCount + 1;
                    this.transform.localScale = new Vector3(1, 300, 1);

                    flag = false;
                }

            }

            if (Input.GetKey(KeyCode.M))
            {
                if (nextStageCount == 2)
                {
                    nextStageCount = nextStageCount + 1;
                    this.transform.localScale = new Vector3(3, 300, 3);

                    flag = false;
                }
                else if (nextStageCount == 3)
                {
                    nextStageCount = nextStageCount + 1;
                    this.transform.localScale = new Vector3(9, 300, 9);
                    flag = false; 
                }
            }
        }

        if (flag == false)
        {
            if (Input.GetKey(KeyCode.F))
            {
                flag = true;
            }
        }
        
    }

    private void GetNowScale()
    {
        nowScale = this.transform.localScale;
    }
}
