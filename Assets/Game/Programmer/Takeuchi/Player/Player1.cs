using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : ParentPlayer
{
    [SerializeField]
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int p1NowMapNum;

    void Start()
    {
        base.ParentPlayerInitialize();

        //���݈ʒu�̃}�b�v�ԍ���������
        //p1NowMapNum = playerMapNum;
        //Debug.Log(p1NowMapNum);
    }


    void FixedUpdate()
    {
        p1NowMapNum = playerMapNum;
        //Debug.Log(p1NowMapNum);

        //����p
        PlayerMove();
    }

    //�v���C���[����
    public override void PlayerMove()
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
