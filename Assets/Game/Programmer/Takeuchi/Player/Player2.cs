using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : ParentPlayer
{
    [SerializeField]
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int p2NowMapNum;

    void Start()
    {
        base.ParentPlayerInitialize();

        //���݈ʒu�̃}�b�v�ԍ��̏�����
        p2NowMapNum = 0;
    }

    void FixedUpdate()
    {
        p2NowMapNum = playerMapNum;
        //Debug.Log(p2NowMapNum);
        //�ړ��p
        PlayerMove();
    }

    //�v���C���[����
    public override void PlayerMove()
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
