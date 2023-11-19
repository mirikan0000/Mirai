using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildZone1 : MonoBehaviour
{
    [SerializeField]
    [Header("���u�ړ��p")]
    public float zone1MoveSpeed;  //���u�̈ړ����x
    public Vector3 pre1Pos;       //���u�̏����ʒu
    //���u�̖ڕW�ʒu(�k���p)
    public Vector3 zone1PostReduPos1;
    public Vector3 zone1PostReduPos2;
    public Vector3 zone1PostReduPos3;
    public Vector3 zone1PostReduPos4;
    public Vector3 zone1PostReduPos5;
    public Vector3 zone1PostReduPos6;
    //���u�̖ڕW�ʒu(�g��p)
    public Vector3 zone1PostMagPos1;
    public Vector3 zone1PostMagPos2;
    public Vector3 zone1PostMagPos3;
    public Vector3 zone1PostMagPos4;
    public Vector3 zone1PostMagPos5;
    public Vector3 zone1PostMagPos6;
    public Vector3 zone1NowPos;        //���݈ʒu

    private bool setPosFlag;             //���u�̖ڕW�ʒu�ݒ�p�t���O
    private Vector3 preZone1Pos;         //���u�̏����ʒu
    private float prePosx, prePosy, prePosz;
    private Vector3 nowZone1Pos;         //���u�̌��݈ʒu
    private float nowPosx, nowPosy, nowPosz;
    private float maxDistance = 600.0f;  //�ő�ړ���
    private float reduDistance;          //�k�����ړ���
    private float magDistance;           //�g�厞�ړ���
    private float dis;                   //�덷���o�����p

    [Header("�I�u�W�F�N�g���X�N���v�g�擾�p")]
    private GameObject parentObj;        //�e�I�u�W�F�N�g
    private SaftyZoneV2 parentScript;  //�e�I�u�W�F�N�g�̃X�N���v�g

    void Start()
    {
        //�ϐ�������
        VariableInitialize();
    }

    
    void Update()
    {
        //�k��
        //�e�X�N���v�g�̏k���p�t���O��True�Ȃ�
        if (parentScript.reducationFlag == true)
        {
            //�ړ�
            MoveReducation();

            //�ړ������`�F�b�N
            ReducationCheck();
        }

        //�g��
        //�e�X�N���v�g�̊g��p�t���O��True�Ȃ�
        if (parentScript.magnificationFlag == true)
        {
            //�ړ�
            MoveMagnification();

            //�ړ������`�F�b�N
            MagnificationCheck();
        }
    }

    //�ϐ�������
    private void VariableInitialize()
    {
        //���u�̏����ʒu�擾
        preZone1Pos = this.transform.position;
        prePosx = Mathf.Floor(preZone1Pos.x);
        prePosy = Mathf.Floor(preZone1Pos.y);
        prePosz = Mathf.Floor(preZone1Pos.z);
        pre1Pos = new Vector3(prePosx, prePosy, prePosz);

        //�ڕW�ʒu�ݒ�p�t���O������
        setPosFlag = true;

        //�덷���o�����p�ϐ�������
        dis = 0.0f;

        //�e�I�u�W�F�N�g���X�N���v�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneV2>();

        //���u�̈ړ��ʂ�ݒ�
        SetMoveDistance();

        //�ڕW�ʒu�̐ݒ�
        SetPostPos();
    }

    //���݈ʒu�擾
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone1Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        nowPosx = Mathf.Floor(nowZone1Pos.x);
        nowPosy = Mathf.Floor(nowZone1Pos.y);
        nowPosz = Mathf.Floor(nowZone1Pos.z);

        //���݈ʒu�ݒ�
        zone1NowPos = new Vector3(nowPosx, nowPosy, nowPosz);
    }

    //�ړ��ʂ�ݒ�
    private void SetMoveDistance()
    {
        //�ő�ړ��ʂ��k���񐔂Ŋ���
        reduDistance = maxDistance / parentScript.maxReduStage;

        //�ő�ړ��ʂ��g��񐔂Ŋ���
        magDistance = maxDistance / parentScript.maxMagStage;
    }

    //�ڕW�ʒu�̐ݒ�
    private void SetPostPos()
    {
        if (setPosFlag == true)
        {
            //�ݒ肵���k���񐔂ŖڕW�n�_��ݒ�
            switch (parentScript.maxReduStage)
            {
                case 1:  //�k���񐔂����̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos1.x - reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);
                    break;

                case 2:  //�k���񐔂����̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos2 = new Vector3(zone1PostReduPos1.x + reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos2.x - reduDistance, zone1PostReduPos2.y, zone1PostReduPos2.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos2 = new Vector3(zone1PostMagPos1.x - reduDistance, zone1PostMagPos1.y, zone1PostMagPos1.z);
                    break;

                case 3:  //�k���񐔂��O��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos2 = new Vector3(zone1PostReduPos1.x + reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos3 = new Vector3(zone1PostReduPos2.x + reduDistance, zone1PostReduPos2.y, zone1PostReduPos2.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos3.x - reduDistance, zone1PostReduPos3.y, zone1PostReduPos3.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos2 = new Vector3(zone1PostMagPos1.x - reduDistance, zone1PostMagPos1.y, zone1PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos3 = new Vector3(zone1PostMagPos2.x - reduDistance, zone1PostMagPos2.y, zone1PostMagPos2.z);
                    break;

                case 4:  //�k���񐔂��l��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos2 = new Vector3(zone1PostReduPos1.x + reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos3 = new Vector3(zone1PostReduPos2.x + reduDistance, zone1PostReduPos2.y, zone1PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos4 = new Vector3(zone1PostReduPos3.x + reduDistance, zone1PostReduPos3.y, zone1PostReduPos3.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos4.x - reduDistance, zone1PostReduPos4.y, zone1PostReduPos4.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos2 = new Vector3(zone1PostMagPos1.x - reduDistance, zone1PostMagPos1.y, zone1PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos3 = new Vector3(zone1PostMagPos2.x - reduDistance, zone1PostMagPos2.y, zone1PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos4 = new Vector3(zone1PostMagPos3.x - reduDistance, zone1PostMagPos3.y, zone1PostMagPos3.z);
                    break;

                case 5:  //�k���񐔂��܉�̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos2 = new Vector3(zone1PostReduPos1.x + reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos3 = new Vector3(zone1PostReduPos2.x + reduDistance, zone1PostReduPos2.y, zone1PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos4 = new Vector3(zone1PostReduPos3.x + reduDistance, zone1PostReduPos3.y, zone1PostReduPos3.z);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos5 = new Vector3(zone1PostReduPos4.x + reduDistance, zone1PostReduPos4.y, zone1PostReduPos4.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos5.x - reduDistance, zone1PostReduPos5.y, zone1PostReduPos5.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos2 = new Vector3(zone1PostMagPos1.x - reduDistance, zone1PostMagPos1.y, zone1PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos3 = new Vector3(zone1PostMagPos2.x - reduDistance, zone1PostMagPos2.y, zone1PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos4 = new Vector3(zone1PostMagPos3.x - reduDistance, zone1PostMagPos3.y, zone1PostMagPos3.z);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos5 = new Vector3(zone1PostMagPos4.x - reduDistance, zone1PostMagPos4.y, zone1PostMagPos4.z);
                    break;

                case 6:  //�k���񐔂��Z��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone1PostReduPos1 = new Vector3(pre1Pos.x + reduDistance, pre1Pos.y, pre1Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos2 = new Vector3(zone1PostReduPos1.x + reduDistance, zone1PostReduPos1.y, zone1PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos3 = new Vector3(zone1PostReduPos2.x + reduDistance, zone1PostReduPos2.y, zone1PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos4 = new Vector3(zone1PostReduPos3.x + reduDistance, zone1PostReduPos3.y, zone1PostReduPos3.z);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos5 = new Vector3(zone1PostReduPos4.x + reduDistance, zone1PostReduPos4.y, zone1PostReduPos4.z);
                    //�Z�i�K�ڂ̖ڕW���_��ݒ�
                    zone1PostReduPos6 = new Vector3(zone1PostReduPos5.x + reduDistance, zone1PostReduPos5.y, zone1PostReduPos5.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos1 = new Vector3(zone1PostReduPos6.x - reduDistance, zone1PostReduPos6.y, zone1PostReduPos6.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos2 = new Vector3(zone1PostMagPos1.x - reduDistance, zone1PostMagPos1.y, zone1PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos3 = new Vector3(zone1PostMagPos2.x - reduDistance, zone1PostMagPos2.y, zone1PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos4 = new Vector3(zone1PostMagPos3.x - reduDistance, zone1PostMagPos3.y, zone1PostMagPos3.z);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos5 = new Vector3(zone1PostMagPos4.x - reduDistance, zone1PostMagPos4.y, zone1PostMagPos4.z);
                    //�Z�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone1PostMagPos6 = new Vector3(zone1PostMagPos5.x - reduDistance, zone1PostMagPos5.y, zone1PostMagPos5.z);
                    break;
            }

            //�ڕW�n�_�ݒ�p�t���O��False
            setPosFlag = false;
        }
    }

    //�k���ړ�
    private void MoveReducation()
    {
        //�e�X�N���v�g�̌��݂̈��u�̒i�K�ɂ���Ĉړ�
        switch (parentScript.nowReduStage)
        {
            case 1.0f:  //���ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos1, zone1MoveSpeed);
                break;
            case 2.0f:  //���ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos2, zone1MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos3, zone1MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos4, zone1MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos5, zone1MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostReduPos6, zone1MoveSpeed);
                break;
        }
    }

    //�k�������`�F�b�N
    private void ReducationCheck()
    {
        //���݈ʒu�擾
        GetNowPos();

        //���݂̏k���񐔂ɂ���ď���
        switch (parentScript.nowReduStage)
        {
            case 1.0f:  //���ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone1NowPos, zone1PostReduPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowReduStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }

    //�g��ړ�
    private void MoveMagnification()
    {
        //�e�X�N���v�g�̌��݂̈��u�̒i�K�ɂ���Ĉړ�
        switch (parentScript.nowMagStage)
        {
            case 1.0f:  //���ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos1, zone1MoveSpeed);
                break;
            case 2.0f:  //���ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos2, zone1MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos3, zone1MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos4, zone1MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos5, zone1MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone1PostMagPos6, zone1MoveSpeed);
                break;
        }
    }

    //�g�劮���`�F�b�N
    private void MagnificationCheck()
    {
        //���݈ʒu�擾
        GetNowPos();

        //���݂̊g��񐔂ɂ���ď���
        switch (parentScript.nowMagStage)
        {
            case 1.0f:  //���ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone1Pos, zone1PostMagPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone1NowMagStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }
}
