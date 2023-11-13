using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1 : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�擾�p")]
    public GameObject childSaftyZone1Obj;  //���g�̃Q�[���I�u�W�F�N�g
    public string childSaftyZone1ObjName;  //���g�̖��O
    GameObject parentSaftyZoneObj;         //�e�I�u�W�F�N�g
    SaftyZone parentSaftyZoneScript;       //�e�I�u�W�F�N�g�̃X�N���v�g
    [Header("�ړ��p")]
    public Vector3 preZone1pos;            //���g�̏����ʒu
    public Vector3 postZone1Pos;           //���g�̈ړ���̖ڕW�ʒu
    public Vector3 nowZone1Pos;            //���g�̌��݈ʒu
    public float zone1MoveSpeed = 0.0001f;             //�ړ����x

    public Vector3 pre1Pos;                //�����ʒu�p(�����_�ȉ��؂�̂�
    float pre1Posx, pre1Posy, pre1Posz;    

    public Vector3 now1Pos;          �@�@�@//���݈ʒu�p(�����_�ȉ��؂�̂�
    float now1Posx, now1Posy, now1Posz;

    private float dis;                     //�덷���o�����p

    [Header("�k���p")]
    public bool setReduPosFlag = true;     //�k����̈ʒu���Z�b�g���邽�߂̃t���O
    public bool endReduFlag = false;       //�k������������
    [Header("�g��p")]
    public bool setMagPosFlag = false;     //�g���̈ʒu���Z�b�g���邽�߂̃t���O
    public bool endMagFlag = false;        //�g�劮��������

    void Start()
    {
        //�e�퐔�l�̏�����
        dis = 0.0f;

        //�e��t���O�̏���������
        Zone1FlagInitialize();

        //���u�̖��O�Ə����ʒu���擾
        childSaftyZone1Obj = this.gameObject;
        childSaftyZone1ObjName = childSaftyZone1Obj.name;
        preZone1pos = childSaftyZone1Obj.transform.position;
        zone1MoveSpeed = 0.01f;
        //�e�I�u�W�F�N�g�̃X�N���v�g�擾
        parentSaftyZoneObj = transform.parent.gameObject;
        parentSaftyZoneScript = parentSaftyZoneObj.GetComponent<SaftyZone>();
    }

    void Update()
    {
        //�k��
        if (parentSaftyZoneScript.reducationFlag == true)
        {
            if (setReduPosFlag == true)
            {
                //�k����̈ʒu��ݒ�
                SetReducationZone1Pos();
            }

            //�k��
            ReducationZone1();

            //�k�������`�F�b�N
            Zone1ReducationCheck();
        }

        //�g��
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagPosFlag == true)
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
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //�k����̖ڕW�ʒu��ݒ�
    private void SetReducationZone1Pos()
    {
        //�����ʒu�������_�ȉ��؂�̂�
        pre1Posx = Mathf.Floor(preZone1pos.x);
        pre1Posy = Mathf.Floor(preZone1pos.y);
        pre1Posz = Mathf.Floor(preZone1pos.z);
        pre1Pos = new Vector3(pre1Posx, pre1Posy, pre1Posz);

        //�ڕW�ʒu�ݒ�
        postZone1Pos = new Vector3(pre1Posx + 425.0f, pre1Posy, pre1Posz);

        //�ݒ肵����k���ʒu�ݒ�p�t���O��False�ɂ���
        setReduPosFlag = false;
    }

    //�k��
    private void ReducationZone1()
    {
        //�k���ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone1Pos, zone1MoveSpeed);
    }

    //�k�������`�F�b�N
    private void Zone1ReducationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        //���݈ʒu�ƖڕW�ʒu�̋������v�Z
        dis = Vector3.Distance(now1Pos, postZone1Pos);

        //�k���������Ă��邩
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //�������Ă�����e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone1redu = true;
                endReduFlag = true;

                //�������Ă�����g���ʒu�ݒ�p�t���O��True�ɂ���
                setMagPosFlag = true;
            }
        }
    }

    //�g���̈ʒu��ݒ�
    private void SetMagnificationZone1Pos()
    {
        //�ڕW�ʒu�ɏ����ʒu��ݒ�
        postZone1Pos = pre1Pos;

        //�ݒ肵����g���ʒu�ݒ�p�t���O��False�ɂ���
        setMagPosFlag = false;
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
                parentSaftyZoneScript.zone1mag = true;

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
