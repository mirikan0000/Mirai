using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2 : TestParentPlayer
{
    [SerializeField]
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int testP2NowMapNum;

    void Start()
    {
        base.TestParentPlayerInitialize();

        //���݈ʒu�̃}�b�v�ԍ��̏�����
        testP2NowMapNum = 0;
    }


    void FixedUpdate()
    {
        testP2NowMapNum = testPlayerMapNum;
        //Debug.Log(p2NowMapNum);
        //�ړ��p
        TestPlayerMove();
    }

    //�v���C���[����
    public override void TestPlayerMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //��
        {
            transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))  //��
        {
            transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))  //��
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))  //�E
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
    }
}
