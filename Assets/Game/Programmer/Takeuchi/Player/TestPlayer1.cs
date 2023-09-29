using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer1 : TestParentPlayer
{
    [SerializeField]
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int testP1NowMapNum;

    void Start()
    {
        base.TestParentPlayerInitialize();

        //���݈ʒu�̃}�b�v�ԍ���������
        testP1NowMapNum = testPlayerMapNum;
        //Debug.Log(p1NowMapNum);
    }

    void FixedUpdate()
    {
        testP1NowMapNum = testPlayerMapNum;
        //Debug.Log(p1NowMapNum);

        //����p
        TestPlayerMove();
    }

    //�v���C���[����
    public override void TestPlayerMove()
    {
        if (Input.GetKey(KeyCode.W))  //��
        {
            transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))  //��
        {
            transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))  //��
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))  //�E
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
    }

}
