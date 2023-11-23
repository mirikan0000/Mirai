using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildZone4 : MonoBehaviour
{
    [SerializeField]
    [Header("���u�ړ��p")]
    public float zone4MoveSpeed;  //���u�̈ړ����x
    public Vector3 pre4Pos;       //���u�̏����ʒu
    //���u�̖ڕW�ʒu(�k���p)
    public Vector3 zone4PostReduPos1;
    public Vector3 zone4PostReduPos2;
    public Vector3 zone4PostReduPos3;
    public Vector3 zone4PostReduPos4;
    public Vector3 zone4PostReduPos5;
    public Vector3 zone4PostReduPos6;
    //���u�̖ڕW�ʒu(�g��p)
    public Vector3 zone4PostMagPos1;
    public Vector3 zone4PostMagPos2;
    public Vector3 zone4PostMagPos3;
    public Vector3 zone4PostMagPos4;
    public Vector3 zone4PostMagPos5;
    public Vector3 zone4PostMagPos6;
    public Vector3 zone4NowPos;        //���݈ʒu

    private bool setPosFlag;             //���u�̖ڕW�ʒu�ݒ�p�t���O
    private Vector3 preZone4Pos;         //���u�̏����ʒu
    private float prePosx, prePosy, prePosz;
    private Vector3 nowZone4Pos;         //���u�̌��݈ʒu
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
        preZone4Pos = this.transform.position;
        prePosx = Mathf.Floor(preZone4Pos.x);
        prePosy = Mathf.Floor(preZone4Pos.y);
        prePosz = Mathf.Floor(preZone4Pos.z);
        pre4Pos = new Vector3(prePosx, prePosy, prePosz);

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
        nowZone4Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        nowPosx = Mathf.Floor(nowZone4Pos.x);
        nowPosy = Mathf.Floor(nowZone4Pos.y);
        nowPosz = Mathf.Floor(nowZone4Pos.z);

        //���݈ʒu�ݒ�
        zone4NowPos = new Vector3(nowPosx, nowPosy, nowPosz);
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
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z - reduDistance);
                    break;

                case 2:  //�k���񐔂����̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos2 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos2.x, zone4PostReduPos2.y, zone4PostReduPos2.z - reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos2 = new Vector3(zone4PostMagPos1.x, zone4PostMagPos1.y, zone4PostMagPos1.z - reduDistance);
                    break;

                case 3:  //�k���񐔂��O��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos2 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z + reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos3 = new Vector3(zone4PostReduPos2.x, zone4PostReduPos2.y, zone4PostReduPos2.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos3.x, zone4PostReduPos3.y, zone4PostReduPos3.z - reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos2 = new Vector3(zone4PostMagPos1.x, zone4PostMagPos1.y, zone4PostMagPos1.z - reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos3 = new Vector3(zone4PostMagPos2.x, zone4PostMagPos2.y, zone4PostMagPos2.z - reduDistance);
                    break;

                case 4:  //�k���񐔂��l��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos2 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z + reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos3 = new Vector3(zone4PostReduPos2.x, zone4PostReduPos2.y, zone4PostReduPos2.z + reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos4 = new Vector3(zone4PostReduPos3.x, zone4PostReduPos3.y, zone4PostReduPos3.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos4.x, zone4PostReduPos4.y, zone4PostReduPos4.z - reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos2 = new Vector3(zone4PostMagPos1.x, zone4PostMagPos1.y, zone4PostMagPos1.z - reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos3 = new Vector3(zone4PostMagPos2.x, zone4PostMagPos2.y, zone4PostMagPos2.z - reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos4 = new Vector3(zone4PostMagPos3.x, zone4PostMagPos3.y, zone4PostMagPos3.z - reduDistance);
                    break;

                case 5:  //�k���񐔂��܉�̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos2 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z + reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos3 = new Vector3(zone4PostReduPos2.x, zone4PostReduPos2.y, zone4PostReduPos2.z + reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos4 = new Vector3(zone4PostReduPos3.x, zone4PostReduPos3.y, zone4PostReduPos3.z + reduDistance);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos5 = new Vector3(zone4PostReduPos4.x, zone4PostReduPos4.y, zone4PostReduPos4.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos5.x, zone4PostReduPos5.y, zone4PostReduPos5.z - reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos2 = new Vector3(zone4PostMagPos1.x, zone4PostMagPos1.y, zone4PostMagPos1.z - reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos3 = new Vector3(zone4PostMagPos2.x, zone4PostMagPos2.y, zone4PostMagPos2.z - reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos4 = new Vector3(zone4PostMagPos3.x, zone4PostMagPos3.y, zone4PostMagPos3.z - reduDistance);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos5 = new Vector3(zone4PostMagPos4.x, zone4PostMagPos4.y, zone4PostMagPos4.z - reduDistance);
                    break;

                case 6:  //�k���񐔂��Z��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone4PostReduPos1 = new Vector3(pre4Pos.x, pre4Pos.y, pre4Pos.z + reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos2 = new Vector3(zone4PostReduPos1.x, zone4PostReduPos1.y, zone4PostReduPos1.z + reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos3 = new Vector3(zone4PostReduPos2.x, zone4PostReduPos2.y, zone4PostReduPos2.z + reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos4 = new Vector3(zone4PostReduPos3.x, zone4PostReduPos3.y, zone4PostReduPos3.z + reduDistance);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos5 = new Vector3(zone4PostReduPos4.x, zone4PostReduPos4.y, zone4PostReduPos4.z + reduDistance);
                    //�Z�i�K�ڂ̖ڕW���_��ݒ�
                    zone4PostReduPos6 = new Vector3(zone4PostReduPos5.x, zone4PostReduPos5.y, zone4PostReduPos5.z + reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos1 = new Vector3(zone4PostReduPos6.x, zone4PostReduPos6.y, zone4PostReduPos6.z - reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos2 = new Vector3(zone4PostMagPos1.x, zone4PostMagPos1.y, zone4PostMagPos1.z - reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos3 = new Vector3(zone4PostMagPos2.x, zone4PostMagPos2.y, zone4PostMagPos2.z - reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos4 = new Vector3(zone4PostMagPos3.x, zone4PostMagPos3.y, zone4PostMagPos3.z - reduDistance);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos5 = new Vector3(zone4PostMagPos4.x, zone4PostMagPos4.y, zone4PostMagPos4.z - reduDistance);
                    //�Z�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone4PostMagPos6 = new Vector3(zone4PostMagPos5.x, zone4PostMagPos5.y, zone4PostMagPos5.z - reduDistance);
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
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos1, zone4MoveSpeed);
                break;
            case 2.0f:  //���ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos2, zone4MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos3, zone4MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos4, zone4MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos5, zone4MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostReduPos6, zone4MoveSpeed);
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
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone4NowPos, zone4PostReduPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowReduStage = 6.0f;

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
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos1, zone4MoveSpeed);
                break;
            case 2.0f:  //���ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos2, zone4MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos3, zone4MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos4, zone4MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos5, zone4MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone4PostMagPos6, zone4MoveSpeed);
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
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone4Pos, zone4PostMagPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone4NowMagStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }
}
