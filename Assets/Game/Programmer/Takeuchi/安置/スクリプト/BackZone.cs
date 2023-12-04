using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�ړ��p")]
    public float zone3MoveSpeed;  //���u�̈ړ����x
    public Vector3 pre3Pos;       //���u�̏����ʒu
    //���u�̖ڕW�ʒu(�k���p)
    public Vector3 zone3PostReduPos1;
    public Vector3 zone3PostReduPos2;
    public Vector3 zone3PostReduPos3;
    public Vector3 zone3PostReduPos4;
    public Vector3 zone3PostReduPos5;
    public Vector3 zone3PostReduPos6;
    //���u�̖ڕW�ʒu(�g��p)
    public Vector3 zone3PostMagPos1;
    public Vector3 zone3PostMagPos2;
    public Vector3 zone3PostMagPos3;
    public Vector3 zone3PostMagPos4;
    public Vector3 zone3PostMagPos5;
    public Vector3 zone3PostMagPos6;
    public Vector3 zone3NowPos;        //���݈ʒu

    private bool setPosFlag;             //���u�̖ڕW�ʒu�ݒ�p�t���O
    private Vector3 preZone3Pos;         //���u�̏����ʒu
    private float prePosx, prePosy, prePosz;
    private Vector3 nowZone3Pos;         //���u�̌��݈ʒu
    private float nowPosx, nowPosy, nowPosz;
    private float maxDistance = 600.0f;  //�ő�ړ���
    private float reduDistance;          //�k�����ړ���
    private float magDistance;           //�g�厞�ړ���
    private float dis;                   //�덷���o�����p

    [Header("�I�u�W�F�N�g���X�N���v�g�擾�p")]
    private GameObject parentObj;        //�e�I�u�W�F�N�g
    private SaftyZoneObjV3 parentScript;  //�e�I�u�W�F�N�g�̃X�N���v�g

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
        preZone3Pos = this.transform.position;
        prePosx = Mathf.Floor(preZone3Pos.x);
        prePosy = Mathf.Floor(preZone3Pos.y);
        prePosz = Mathf.Floor(preZone3Pos.z);
        pre3Pos = new Vector3(prePosx, prePosy, prePosz);

        //�ڕW�ʒu�ݒ�p�t���O������
        setPosFlag = true;

        //�덷���o�����p�ϐ�������
        dis = 0.0f;

        //�e�I�u�W�F�N�g���X�N���v�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneObjV3>();

        //���u�̈ړ��ʂ�ݒ�
        SetMoveDistance();

        //�ڕW�ʒu�̐ݒ�
        SetPostPos();
    }

    //���݈ʒu�擾
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone3Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        nowPosx = Mathf.Floor(nowZone3Pos.x);
        nowPosy = Mathf.Floor(nowZone3Pos.y);
        nowPosz = Mathf.Floor(nowZone3Pos.z);

        //���݈ʒu�ݒ�
        zone3NowPos = new Vector3(nowPosx, nowPosy, nowPosz);
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
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z + reduDistance);
                    break;

                case 2:  //�k���񐔂����̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z + reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    break;

                case 3:  //�k���񐔂��O��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z + reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    break;

                case 4:  //�k���񐔂��l��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z + reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    break;

                case 5:  //�k���񐔂��܉�̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos5 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos5.x, zone3PostReduPos5.y, zone3PostReduPos5.z + reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos5 = new Vector3(zone3PostMagPos4.x, zone3PostMagPos4.y, zone3PostMagPos4.z + reduDistance);
                    break;

                case 6:  //�k���񐔂��Z��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone3PostReduPos1 = new Vector3(pre3Pos.x, pre3Pos.y, pre3Pos.z - reduDistance);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos2 = new Vector3(zone3PostReduPos1.x, zone3PostReduPos1.y, zone3PostReduPos1.z - reduDistance);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos3 = new Vector3(zone3PostReduPos2.x, zone3PostReduPos2.y, zone3PostReduPos2.z - reduDistance);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos4 = new Vector3(zone3PostReduPos3.x, zone3PostReduPos3.y, zone3PostReduPos3.z - reduDistance);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos5 = new Vector3(zone3PostReduPos4.x, zone3PostReduPos4.y, zone3PostReduPos4.z - reduDistance);
                    //�Z�i�K�ڂ̖ڕW���_��ݒ�
                    zone3PostReduPos6 = new Vector3(zone3PostReduPos5.x, zone3PostReduPos5.y, zone3PostReduPos5.z - reduDistance);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos1 = new Vector3(zone3PostReduPos6.x, zone3PostReduPos6.y, zone3PostReduPos6.z + reduDistance);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos2 = new Vector3(zone3PostMagPos1.x, zone3PostMagPos1.y, zone3PostMagPos1.z + reduDistance);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos3 = new Vector3(zone3PostMagPos2.x, zone3PostMagPos2.y, zone3PostMagPos2.z + reduDistance);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos4 = new Vector3(zone3PostMagPos3.x, zone3PostMagPos3.y, zone3PostMagPos3.z + reduDistance);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos5 = new Vector3(zone3PostMagPos4.x, zone3PostMagPos4.y, zone3PostMagPos4.z + reduDistance);
                    //�Z�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone3PostMagPos6 = new Vector3(zone3PostMagPos5.x, zone3PostMagPos5.y, zone3PostMagPos5.z + reduDistance);
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
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos1, zone3MoveSpeed);
                break;
            case 2.0f:  //���ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos2, zone3MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos3, zone3MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos4, zone3MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos5, zone3MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostReduPos6, zone3MoveSpeed);
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
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone3NowPos, zone3PostReduPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowReduStage = 6.0f;

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
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos1, zone3MoveSpeed);
                break;
            case 2.0f:  //���ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos2, zone3MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos3, zone3MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos4, zone3MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos5, zone3MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone3PostMagPos6, zone3MoveSpeed);
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
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone3Pos, zone3PostMagPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone3NowMagStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }
}
