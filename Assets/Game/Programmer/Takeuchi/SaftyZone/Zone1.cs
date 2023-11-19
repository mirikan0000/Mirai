using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1 : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�擾�p")]
    GameObject parentSaftyZoneObj;               //�e�I�u�W�F�N�g
    SaftyZone parentSaftyZoneScript;             //�e�I�u�W�F�N�g�̃X�N���v�g
    GameObject grandParentObj;                   //�e�̐e�I�u�W�F�N�g
    CreateSaftyZone grandParentScript;           //�e�̐e�I�u�W�F�N�g�̃X�N���v�g

    [Header("�ړ��p")]
    public Vector3 preZone1pos;                  //���g�̏����ʒu
    public Vector3 postZone1Pos;                 //���g�̈ړ���̖ڕW�ʒu
    public Vector3 nowZone1Pos;                  //���g�̌��݈ʒu
    public float zone1MoveSpeed;                 //�ړ����x
    Vector3 pre1Pos;                             //�����ʒu�p(�����_�ȉ��؂�̂�
    float pre1Posx, pre1Posy, pre1Posz;    
    Vector3 now1Pos;                 �@�@�@      //���݈ʒu�p(�����_�ȉ��؂�̂�
    float now1Posx, now1Posy, now1Posz;
    private float dis;                           //�덷���o�����p

    [Header("�k���p")]
    public bool setReduDistanceFlag;             //�i�K���Ƃ̈ړ��ʂ�ݒ肷�邽�߂̃t���O
    public bool endReduFlag;                     //�k������������
    //�k���i�K����p
    public float zone1ReduCount;                 //�k���i�K�J�E���g�p�ϐ�

    private float reduMoveDistance;              //���u�k�����̈ړ���
    private float reduMoveMaxDistance = 600.0f;  //���u�k�����̍ő�ړ���

    [Header("�g��p")]
    public bool setMagDistanceFlag;              //�i�K���Ƃ̈ړ��ʂ�ݒ肷��ׂ̃t���O
    public bool endMagFlag;                      //�g�劮��������
    //�g��i�K����p
    public bool zone1MagStage1;
    public bool zone1MagStage2;
    public bool zone1MagStage3;
    public bool zone1MagStage4;
    public bool zone1MagStage5;
    public bool zone1MagStage6;

    private float magMoveDistance;               //���u�g�厞�̈ړ���
    private float magMoveMaxDistance = 600.0f;   //���u�g�厞�̍ő�ړ���

    void Start()
    {
        //�e�퐔�l�̏�����
        dis = 0.0f;
        reduMoveDistance = 0.0f;
        magMoveDistance = 0.0f;
        zone1ReduCount = 1.0f;

        //�e��t���O�̏���������
        Zone1FlagInitialize();

        //�����ʒu���擾
        GetZone1PrePos();

        //�e�I�u�W�F�N�g�̃X�N���v�g�擾
        parentSaftyZoneObj = transform.parent.gameObject;
        parentSaftyZoneScript = parentSaftyZoneObj.GetComponent<SaftyZone>();

        //�e�̐e�I�u�W�F�N�g�̃X�N���v�g���擾
        grandParentObj = transform.parent.parent.gameObject;
        grandParentScript = grandParentObj.GetComponent<CreateSaftyZone>();
    }

    void Update()
    {
        //�k��
        if (parentSaftyZoneScript.reducationFlag == true)
        {
            if (setReduDistanceFlag == true)
            {
                //�k���i�K�̐��ɉ����Ĉړ��ʂ�ݒ�
                SetReducationZone1MoveDistance();
            }

            //�k��
            ReducationMoveZone1();

            //�k�������`�F�b�N
            Zone1ReducationCheck();
        }

        //�g��
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagDistanceFlag == true)
            {
                //�g���̈ʒu��ݒ�
                SetMagnificationZone1Pos();
            }

            //�g��
            MagnificationZone1();

            //�g�劮���`�F�b�N
            Zone1MagnificationCheck();
        }
    }

    //�t���O�̏���������
    private void Zone1FlagInitialize()
    {
        //�k���p
        setReduDistanceFlag = true;
        endReduFlag = false;
        //�g��p
        setMagDistanceFlag = false;
        endMagFlag = false;

        
    }

    //�����ʒu���擾
    private void GetZone1PrePos()
    {
        //�����ʒu���擾
        preZone1pos = this.transform.position;

        //�����ʒu�������_�ȉ��؂�̂�
        pre1Posx = Mathf.Floor(preZone1pos.x);
        pre1Posy = Mathf.Floor(preZone1pos.y);
        pre1Posz = Mathf.Floor(preZone1pos.z);
        pre1Pos = new Vector3(pre1Posx, pre1Posy, pre1Posz);
    }
    //�k����̖ڕW�ʒu�܂ł̈ړ��ʂ�ݒ�
    private void SetReducationZone1MoveDistance()
    {
        //�ő�k���i�K�ɉ����Ĉړ��ʌv�Z
        if (grandParentScript.reduStageCount > 1)  //�k���i�K���Q�ȏ�̎�
        {
            reduMoveDistance = reduMoveMaxDistance / grandParentScript.reduStageCount;
        }
        else  //�k���i�K��1�ȉ��̎�
        {
            reduMoveDistance = reduMoveMaxDistance;
        }
        //�ړ��ʐݒ�p�t���O��False�ɂ���
        setReduDistanceFlag = false;
    }

    //�k��
    private void ReducationMoveZone1()
    {
        //�k���i�K���ɉ����ĖڕW�n�_�����߂Ĉړ�
        if (grandParentScript.reduStageCount <= 1)  //��i�K�ȉ��̎�
        {
            //�����ʒu����ő�ړ��ʕ��ړ��������W��ڕW�ʒu�ɐݒ�
            postZone1Pos = new Vector3(pre1Pos.x + reduMoveMaxDistance, pre1Pos.y, pre1Pos.z);

            //�k���ڕW�n�_�܂ňړ�
            transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
        }
        else if (grandParentScript.reduStageCount > 1)  //��i�K�ȏ�̎�
        {
            //���݈ʒu�擾
            GetNowPos();

            //���݈ʒu����ړ��ʕ��ړ��������W��ڕW�ʒu�ɐݒ�
            postZone1Pos = new Vector3(now1Pos.x + reduMoveDistance, now1Pos.y, now1Pos.z);

            //�k���ڕW�n�_�܂ňړ�
            transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);

            //�k���i�K����p�ϐ����Z
            zone1ReduCount = zone1ReduCount + 1.0f;
        }
    }

    //�k�������`�F�b�N
    private void Zone1ReducationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        //���݈ʒu�ƖڕW�ʒu�̋������v�Z
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //�k���������Ă��邩(�덷�P�܂ł͋��e�j
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //�k���i�K�ɉ����ď���
                if (grandParentScript.reduStageCount <= 1)  //
                {
                    //�������Ă�����e�I�u�W�F�N�g�̃t���O��true
                    parentSaftyZoneScript.zone1reduStage = true;
                  
                    endReduFlag = true;

                    //�������Ă�����g���ʒu�ݒ�p�t���O��True�ɂ���
                    setMagDistanceFlag = true;
                }
                else if (grandParentScript.reduStageCount > 1)  //
                {
                    //���݂̏k���i�K���ݒ肵���k���i�K�����������Ƃ�
                    if (zone1ReduCount < grandParentScript.reduStageCount)
                    {
                        parentSaftyZoneScript.zone1reduStage = true;
                    }
                    else if (zone1ReduCount >= grandParentScript.reduStageCount)
                    {
                        parentSaftyZoneScript.zone1ReduEnd = true;

                    }
                }
            }
        }
    }

    //�g���̈ʒu��ݒ�
    private void SetMagnificationZone1Pos()
    {
        //�ڕW�ʒu�ɏ����ʒu��ݒ�
        postZone1Pos = pre1Pos;

        //�ݒ肵����g���ʒu�ݒ�p�t���O��False�ɂ���
        setMagDistanceFlag = false;
    }

    //�g��
    private void MagnificationZone1()
    {
        //�g��ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
    }

    //�g�劮���`�F�b�N
    private void Zone1MagnificationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        //���݈ʒu�ƖڕW�ʒu�̋������v�Z
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //�g�劮�����Ă��邩
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //�e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone1MagEnd = true;

                endMagFlag = true;
            }
        }
    }

    //���ݒn�擾�p
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone1Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        now1Posx = Mathf.Floor(nowZone1Pos.x);
        now1Posy = Mathf.Floor(nowZone1Pos.y);
        now1Posz = Mathf.Floor(nowZone1Pos.z);

        //Vector3�^�ɂ���
        now1Pos = new Vector3(now1Posx, now1Posy, now1Posz);
    }
}
