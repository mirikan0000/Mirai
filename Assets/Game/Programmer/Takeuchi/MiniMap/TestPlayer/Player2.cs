using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2: ParentPlayer
{
    [SerializeField]
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int p2NowMapNum;
    public GameObject[] feels;
    void Start()
    {
        base.ParentPlayerInitialize();
        // �����_���Ƀi���o�[��I��
        int number = Random.Range(0, feels.Length);

        // �I�����ꂽ�i���o�[�ɉ����Ĉʒu��ݒ�
        if (number >= 0 && number < feels.Length)
        {
            this.transform.position = feels[number].transform.position;
        }

        //���݈ʒu�̃}�b�v�ԍ���������
        p2NowMapNum = playerMapNum;
    }


    void FixedUpdate()
    {
        p2NowMapNum = playerMapNum;

    }

    //�v���C���[����
    //public override void PlayerMove()
    //{
    //    if (Input.GetKey(KeyCode.W))  //��
    //    {
    //        transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.S))  //��
    //    {
    //        transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.A))  //��
    //    {
    //        transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))  //�E
    //    {
    //        transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
    //    }
    //}
}
