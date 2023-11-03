using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2 : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�擾�p")]
    public GameObject childSaftyZone2Obj;  //���g�̃Q�[���I�u�W�F�N�g
    public string childSaftyZone2ObjName;  //���g�̖��O
    GameObject parentSaftyZoneObj;         //�e�I�u�W�F�N�g
    SaftyZone parentSaftyZoneScript;       //�e�I�u�W�F�N�g�̃X�N���v�g
    [Header("�ړ��p")]
    public Vector3 preZone2pos;            //���g�̏����ʒu
    public Vector3 postZone2Pos;           //���g�̈ړ���̖ڕW�ʒu
    public Vector3 nowZone2Pos;            //���g�̌��݈ʒu
    public int zone2MoveSpeed;           //�ړ����x

    public Vector3 pre2Pos;                       //�����ʒu�p(�����_�ȉ��؂�̂�
    float pre2Posx, pre2Posy, pre2Posz;    

    public Vector3 now2Pos;                       //���݈ʒu�p(�����_�ȉ��؂�̂�
    float now2Posx, now2Posy, now2Posz;

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
        Zone2FlagInitialize();

        //���u�̖��O�Ə����ʒu���擾
        childSaftyZone2Obj = this.gameObject;
        childSaftyZone2ObjName = childSaftyZone2Obj.name;
        preZone2pos = childSaftyZone2Obj.transform.position;

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
    private void Zone2FlagInitialize()
    {
        setReduPosFlag = true;
        endReduFlag = false;
        setMagPosFlag = false;
        endMagFlag = false;
    }

    //�k����̖ڕW�ʒu��ݒ�
    private void SetReducationZone2Pos()
    {
        //�����ʒu�������_�ȉ��؂�̂�
        pre2Posx = Mathf.Floor(preZone2pos.x);
        pre2Posy = Mathf.Floor(preZone2pos.y);
        pre2Posz = Mathf.Floor(preZone2pos.z);
        pre2Pos = new Vector3(pre2Posx, pre2Posy, pre2Posz);

        //�ڕW�ʒu�ݒ�
        postZone2Pos = new Vector3(pre2Posx - 425.0f, pre2Posy, pre2Posz);

        //�ݒ肵����k���ʒu�ݒ�p�t���O��False�ɂ���
        setReduPosFlag = false;
    }

    //�k��
    private void ReducationZone2()
    {
        //�k���ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone2Pos, zone2MoveSpeed);
    }

    //�k�������`�F�b�N
    private void Zone2ReducationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now2Pos, postZone2Pos);

        //�k���������Ă��邩
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //�������Ă�����e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone2redu = true;
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
        postZone2Pos = pre2Pos;

        //�ݒ肵����g���ʒu�ݒ�p�t���O��False�ɂ���
        setMagPosFlag = false;
    }

    //�g��
    private void MagnificationZone2()
    {
        //�g��ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone2Pos, zone2MoveSpeed);
    }

    //�g�劮���`�F�b�N
    private void Zone2MagnificationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now2Pos, postZone2Pos);

        //�g�劮�����Ă��邩
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //�e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone2mag = true;

                endMagFlag = true;
            }
        }
    }

    //���݈ʒu���擾
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone2Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        now2Posx = Mathf.Floor(nowZone2Pos.x);
        now2Posy = Mathf.Floor(nowZone2Pos.y);
        now2Posz = Mathf.Floor(nowZone2Pos.z);

        //Vector3�^�ɂ���
        now2Pos = new Vector3(now2Posx, now2Posy, now2Posz);
    }
}
