using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    [Header("��������I�u�W�F�N�g")]
    public GameObject dropObj;  //�󂩂�~�点��I�u�W�F�N�g

    [Header("��������͈͗p�I�u�W�F�N�g")]
    public GameObject dropRangeA;  //�����͈͎w��p�I�u�W�F�N�gA
    public GameObject dropRangeB;  //�����͈͎w��p�I�u�W�F�N�gB

    [Header("�e��ϐ�")]
    public int dropCount;     �@�@//��������Ă��鐔
    public int maxDropCount;  �@�@//�����ł���ő��
    public float delayTime;   �@�@//�Đ����܂ł̎���
    private float timer;          //��������̌o�ߎ���
    public bool dropFlag = false; //�������邩�ǂ���
    private float x, y, z;    �@�@//��������ʒu


    void Start()
    {
        //�e��ϐ�������
        dropCount = 0;
        timer = 0.0f;

    }

    void Update()
    {
        //�o�ߎ���
        timer += Time.deltaTime;

        //�����\�ɂȂ��Ă��邩
        if (dropFlag == true)
        {
            //�ő哊���ʂɂȂ�܂Ő���
            if (maxDropCount > dropCount)
            {
                if (timer > delayTime)
                {
                    //�⋋������
                    Drop();

                    timer = 0.0f;
                }
            }
            else
            {
                dropFlag = false;
            }
        }
        else
        {
            Debug.Log("�ő�ʓ������܂���");
        }
    }

    //�⋋���𐶐�����
    private void Drop()
    {
        //���Ƃ��I�u�W�F�N�g���ݒ肳��Ă��邩
        if (dropObj != null)
        {
            //�e�I�u�W�F�N�g�̐ݒ�
            var parent = this.transform;

            //�͈͓��̃����_���Ȉʒu��ݒ�
            x = Random.Range(dropRangeA.transform.position.x, dropRangeB.transform.position.x);
            y = Random.Range(dropRangeA.transform.position.y, dropRangeB.transform.position.y);
            z = Random.Range(dropRangeA.transform.position.z, dropRangeB.transform.position.z);

            //�ݒ肵���ʒu�ɃI�u�W�F�N�g���q�I�u�W�F�N�g�Ƃ��Đ���
            Instantiate(dropObj, new Vector3(x, y, z), Quaternion.identity, parent);

            //�������J�E���g�p�ϐ������Z
            dropCount += 1;

            //�����\�ɂ���
            dropFlag = true;
        }
    }
}
