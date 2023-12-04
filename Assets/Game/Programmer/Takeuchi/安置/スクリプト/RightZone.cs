using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�ړ��p")]
    public float zone2MoveSpeed;  //���u�̈ړ����x
    public Vector3 pre2Pos;       //���u�̏����ʒu
    //���u�̖ڕW�ʒu(�k���p)
    public Vector3 zone2PostReduPos1;
    public Vector3 zone2PostReduPos2;
    public Vector3 zone2PostReduPos3;
    public Vector3 zone2PostReduPos4;
    public Vector3 zone2PostReduPos5;
    public Vector3 zone2PostReduPos6;
    //���u�̖ڕW�ʒu(�g��p)
    public Vector3 zone2PostMagPos1;
    public Vector3 zone2PostMagPos2;
    public Vector3 zone2PostMagPos3;
    public Vector3 zone2PostMagPos4;
    public Vector3 zone2PostMagPos5;
    public Vector3 zone2PostMagPos6;
    public Vector3 zone2NowPos;        //���݈ʒu

    private bool setPosFlag;             //���u�̖ڕW�ʒu�ݒ�p�t���O
    private Vector3 preZone2Pos;         //���u�̏����ʒu
    private float prePosx, prePosy, prePosz;
    private Vector3 nowZone2Pos;         //���u�̌��݈ʒu
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
        preZone2Pos = this.transform.position;
        prePosx = Mathf.Floor(preZone2Pos.x);
        prePosy = Mathf.Floor(preZone2Pos.y);
        prePosz = Mathf.Floor(preZone2Pos.z);
        pre2Pos = new Vector3(prePosx, prePosy, prePosz);

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
        nowZone2Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        nowPosx = Mathf.Floor(nowZone2Pos.x);
        nowPosy = Mathf.Floor(nowZone2Pos.y);
        nowPosz = Mathf.Floor(nowZone2Pos.z);

        //���݈ʒu�ݒ�
        zone2NowPos = new Vector3(nowPosx, nowPosy, nowPosz);
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
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos1.x + reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);
                    break;

                case 2:  //�k���񐔂����̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos2 = new Vector3(zone2PostReduPos1.x - reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos2.x + reduDistance, zone2PostReduPos2.y, zone2PostReduPos2.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos2 = new Vector3(zone2PostMagPos1.x + reduDistance, zone2PostMagPos1.y, zone2PostMagPos1.z);
                    break;

                case 3:  //�k���񐔂��O��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos2 = new Vector3(zone2PostReduPos1.x - reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos3 = new Vector3(zone2PostReduPos2.x - reduDistance, zone2PostReduPos2.y, zone2PostReduPos2.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos3.x + reduDistance, zone2PostReduPos3.y, zone2PostReduPos3.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos2 = new Vector3(zone2PostMagPos1.x + reduDistance, zone2PostMagPos1.y, zone2PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos3 = new Vector3(zone2PostMagPos2.x + reduDistance, zone2PostMagPos2.y, zone2PostMagPos2.z);
                    break;

                case 4:  //�k���񐔂��l��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos2 = new Vector3(zone2PostReduPos1.x - reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos3 = new Vector3(zone2PostReduPos2.x - reduDistance, zone2PostReduPos2.y, zone2PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos4 = new Vector3(zone2PostReduPos3.x - reduDistance, zone2PostReduPos3.y, zone2PostReduPos3.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos4.x + reduDistance, zone2PostReduPos4.y, zone2PostReduPos4.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos2 = new Vector3(zone2PostMagPos1.x + reduDistance, zone2PostMagPos1.y, zone2PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos3 = new Vector3(zone2PostMagPos2.x + reduDistance, zone2PostMagPos2.y, zone2PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos4 = new Vector3(zone2PostMagPos3.x + reduDistance, zone2PostMagPos3.y, zone2PostMagPos3.z);
                    break;

                case 5:  //�k���񐔂��܉�̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos2 = new Vector3(zone2PostReduPos1.x - reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos3 = new Vector3(zone2PostReduPos2.x - reduDistance, zone2PostReduPos2.y, zone2PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos4 = new Vector3(zone2PostReduPos3.x - reduDistance, zone2PostReduPos3.y, zone2PostReduPos3.z);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos5 = new Vector3(zone2PostReduPos4.x - reduDistance, zone2PostReduPos4.y, zone2PostReduPos4.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos5.x + reduDistance, zone2PostReduPos5.y, zone2PostReduPos5.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos2 = new Vector3(zone2PostMagPos1.x + reduDistance, zone2PostMagPos1.y, zone2PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos3 = new Vector3(zone2PostMagPos2.x + reduDistance, zone2PostMagPos2.y, zone2PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos4 = new Vector3(zone2PostMagPos3.x + reduDistance, zone2PostMagPos3.y, zone2PostMagPos3.z);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos5 = new Vector3(zone2PostMagPos4.x + reduDistance, zone2PostMagPos4.y, zone2PostMagPos4.z);
                    break;

                case 6:  //�k���񐔂��Z��̎�
                    //��i�K�ڂ̏k���ڕW�n�_�ݒ�
                    zone2PostReduPos1 = new Vector3(pre2Pos.x - reduDistance, pre2Pos.y, pre2Pos.z);
                    //��i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos2 = new Vector3(zone2PostReduPos1.x - reduDistance, zone2PostReduPos1.y, zone2PostReduPos1.z);
                    //�O�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos3 = new Vector3(zone2PostReduPos2.x - reduDistance, zone2PostReduPos2.y, zone2PostReduPos2.z);
                    //�l�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos4 = new Vector3(zone2PostReduPos3.x - reduDistance, zone2PostReduPos3.y, zone2PostReduPos3.z);
                    //�ܒi�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos5 = new Vector3(zone2PostReduPos4.x - reduDistance, zone2PostReduPos4.y, zone2PostReduPos4.z);
                    //�Z�i�K�ڂ̖ڕW���_��ݒ�
                    zone2PostReduPos6 = new Vector3(zone2PostReduPos5.x - reduDistance, zone2PostReduPos5.y, zone2PostReduPos5.z);

                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos1 = new Vector3(zone2PostReduPos6.x + reduDistance, zone2PostReduPos6.y, zone2PostReduPos6.z);
                    //��i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos2 = new Vector3(zone2PostMagPos1.x + reduDistance, zone2PostMagPos1.y, zone2PostMagPos1.z);
                    //�O�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos3 = new Vector3(zone2PostMagPos2.x + reduDistance, zone2PostMagPos2.y, zone2PostMagPos2.z);
                    //�l�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos4 = new Vector3(zone2PostMagPos3.x + reduDistance, zone2PostMagPos3.y, zone2PostMagPos3.z);
                    //�ܒi�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos5 = new Vector3(zone2PostMagPos4.x + reduDistance, zone2PostMagPos4.y, zone2PostMagPos4.z);
                    //�Z�i�K�ڂ̊g��ڕW�n�_�ݒ�
                    zone2PostMagPos6 = new Vector3(zone2PostMagPos5.x + reduDistance, zone2PostMagPos5.y, zone2PostMagPos5.z);
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
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos1, zone2MoveSpeed);
                break;
            case 2.0f:  //���ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos2, zone2MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos3, zone2MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos4, zone2MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos5, zone2MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̏k��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostReduPos6, zone2MoveSpeed);
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
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̏k���`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(zone2NowPos, zone2PostReduPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowReduStage = 6.0f;

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
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos1, zone2MoveSpeed);
                break;
            case 2.0f:  //���ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos2, zone2MoveSpeed);
                break;
            case 3.0f:  //�O��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos3, zone2MoveSpeed);
                break;
            case 4.0f:  //�l��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos4, zone2MoveSpeed);
                break;
            case 5.0f:  //�܉�ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos5, zone2MoveSpeed);
                break;
            case 6.0f:  //�Z��ڂ̊g��
                transform.position = Vector3.MoveTowards(transform.position, zone2PostMagPos6, zone2MoveSpeed);
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
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos1);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 1.0f;

                    dis = 0.0f;
                }
                break;
            case 2.0f:  //���ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos2);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 2.0f;

                    dis = 0.0f;
                }
                break;
            case 3.0f:  //�O��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos3);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 3.0f;

                    dis = 0.0f;
                }
                break;
            case 4.0f:  //�l��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos4);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 4.0f;

                    dis = 0.0f;
                }
                break;
            case 5.0f:  //�܉�ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos5);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 5.0f;

                    dis = 0.0f;
                }
                break;
            case 6.0f:  //�Z��ڂ̊g��`�F�b�N
                //���݈ʒu�ƖڕW�ʒu�̋������v�Z
                dis = Vector3.Distance(nowZone2Pos, zone2PostMagPos6);

                //�k���������Ă��邩(�덷�P�܂ŋ��e)
                if (dis <= 1.0f)
                {
                    //�e�X�N���v�g�̕ϐ����Z
                    parentScript.zone2NowMagStage = 6.0f;

                    dis = 0.0f;
                }
                break;
        }
    }
}
