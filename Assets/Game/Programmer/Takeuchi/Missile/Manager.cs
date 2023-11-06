using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�����p")]
    public GameObject missileObj;  //��������~�T�C���̃I�u�W�F�N�g
    public Transform missile_point;      //�~�T�C���𐶐�����ʒu
    public GameObject targetObj;   //��������^�[�Q�b�g�̃I�u�W�F�N�g
    public Transform target_point;       //�^�[�Q�b�g�𐶐�����ʒu
    public bool targetCheck = true;  //�^�[�Q�b�g�����p�t���O

    [Header("�e�퐔�l�Ǘ��p")]
    public int targetHP;    //�^�[�Q�b�g�̌��݂�HP
    public int missileCntMax = 20;  //��C�ɐ�������~�T�C���̍ő吔
    public int missileCnt;  //��������Ă���~�T�C���̐�
    

    void Start()
    {
        //�e���l�̏�����
        missileCnt = 0;
        targetHP = 0;

        //�^�[�Q�b�g����
        Instantiate(targetObj, target_point.transform.position, Quaternion.identity);
    }

    void Update()
    {
        //�L�[�{�[�h�ŃI�u�W�F�N�g�𐶐�
        SpawnObject();
    }

    //�e�I�u�W�F�N�g�����p
    private void SpawnObject()
    {
        if (missileObj != null)
        {
            //if (missileCnt < missileCntMax && Input.GetKeyDown(KeyCode.Z))
            //{
            //    Instantiate(missileObj, missile_point.transform.position, Quaternion.identity);
            //    missileCnt++;
            //}

            //�q�I�u�W�F�N�g�Ƃ��Đ���
            if (missileCnt < missileCntMax && Input.GetKeyDown(KeyCode.Z))
            {
                var parent = this.transform;
                
                Instantiate(missileObj, missile_point.transform.position, Quaternion.identity, parent);
                missileCnt++;
            }
        }

        if (targetObj != null)
        {
            if (targetCheck == true && Input.GetKeyDown(KeyCode.X))
            {
                Instantiate(targetObj, target_point.transform.position, Quaternion.identity);
                targetCheck = false;
            }
        }
    }
}
