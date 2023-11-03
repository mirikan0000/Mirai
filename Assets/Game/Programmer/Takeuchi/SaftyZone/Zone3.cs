using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone3 : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�擾�p")]
    public GameObject childSaftyZone3Obj;  //���g�̃Q�[���I�u�W�F�N�g
    public string childSaftyZone3ObjName;  //���g�̖��O
    GameObject parentSaftyZoneObj;         //�e�I�u�W�F�N�g
    SaftyZone parentSaftyZoneScript;       //�e�I�u�W�F�N�g�̃X�N���v�g
    [Header("�ړ��p")]
    public Vector3 preZone3pos;            //���g�̏����ʒu
    public Vector3 postZone3Pos;           //���g�̈ړ���̖ڕW�ʒu
    public Vector3 nowZone3Pos;            //���g�̌��݈ʒu
    public int zone3MoveSpeed;           //�ړ����x

    public Vector3 pre3Pos;                       //�����ʒu�p(�����_�ȉ��؂�̂�
    float pre3Posx, pre3Posy, pre3Posz;    

    public Vector3 now3Pos;               �@�@�@�@//���݈ʒu�p(�����_�ȉ��؂�̂�
    float now3Posx, now3Posy, now3Posz;

    private float dis;                     //�덷���o�����p

    [Header("�k���p")]
    public bool setReduPosFlag = true;     //�k����̈ʒu���Z�b�g���邽�߂̃t���O
    public bool endReduFlag = false;       //�k������������
    [Header("�g��p")]
    public bool setMagPosFlag = false;     //�g���̈ʒu���Z�b�g���邽�߂̃t���O
    public bool endMagFlag = false;        //�g�劮��������

    void Start()
    {
        //�e��t���O�̏�����
        Zone3FlagInitialize();

        //���u�̖��O�Ə����ʒu���擾
        childSaftyZone3Obj = this.gameObject;
        childSaftyZone3ObjName = childSaftyZone3Obj.name;
        preZone3pos = childSaftyZone3Obj.transform.position;

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
                SetReducationZone2Pos();
            }

            //�k��
            ReducationZone2();

            //�k�������`�F�b�N
            Zone2ReducationCheck();
        }

        //�g��
        if (parentSaftyZoneScript.magnificationFlag == true)
        {
            if (setMagPosFlag == true)
            {
                //�g���̈ʒu��ݒ�
                SetMagnificationZone2Pos();
            }

            //�g��
            MagnificationZone2();

            //�g�劮���`�F�b�N
            Zone2MagnificationCheck();
        }
    }

    //�e��t���O�̏�����
    private void Zone3FlagInitialize()
    {
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //�k����̖ڕW�ʒu��ݒ�
    private void SetReducationZone2Pos()
    {
        ////�����ʒu�������_�ȉ��؂�̂�
        pre3Posx = Mathf.Floor(preZone3pos.x);
        pre3Posy = Mathf.Floor(preZone3pos.y);
        pre3Posz = Mathf.Floor(preZone3pos.z);
        pre3Pos = new Vector3(pre3Posx, pre3Posy, pre3Posz);

        //�ڕW�ʒu�ݒ�
        postZone3Pos = new Vector3(pre3Posx, pre3Posy, pre3Posz + 425.0f);

        //�ݒ肵����k���ʒu�ݒ�p�t���O��False�ɂ���
        setReduPosFlag = false;
    }

    //�k��
    private void ReducationZone2()
    {
        //�k���ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone3Pos, zone3MoveSpeed);
    }

    //�k�������`�F�b�N
    private void Zone2ReducationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now3Pos, postZone3Pos);

        //�k���������Ă��邩
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //�������Ă�����e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone3redu = true;
                endReduFlag = true;

                //�������Ă�����g���ʒu�ݒ�p�t���O��True�ɂ���
                setMagPosFlag = true;
            }
        }
    }

    //�g���̈ʒu��ݒ�
    private void SetMagnificationZone2Pos()
    {
        //�ڕW�ʒu�ɏ����ʒu��ݒ�
        postZone3Pos = pre3Pos;

        //�ݒ肵����g���ʒu�ݒ�p�t���O��False�ɂ���
        setMagPosFlag = false;
    }

    //�g��
    private void MagnificationZone2()
    {
        //�g��ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone3Pos, zone3MoveSpeed);
    }

    //�g�劮���`�F�b�N
    private void Zone2MagnificationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now3Pos, postZone3Pos);

        //�g�劮�����Ă��邩
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //�e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone3mag = true;

                endMagFlag = true;
            }
        }
    }

    //���ݒn�擾�p
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone3Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        now3Posx = Mathf.Floor(nowZone3Pos.x);
        now3Posy = Mathf.Floor(nowZone3Pos.y);
        now3Posz = Mathf.Floor(nowZone3Pos.z);

        //Vector3�^�ɂ���
        now3Pos = new Vector3(now3Posx, now3Posy, now3Posz);
    }
}
