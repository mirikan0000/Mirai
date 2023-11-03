using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone4 : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�擾�p")]
    public GameObject childSaftyZone4Obj;  //���g�̃Q�[���I�u�W�F�N�g
    public string childSaftyZone4ObjName;  //���g�̖��O
    GameObject parentSaftyZoneObj;         //�e�I�u�W�F�N�g
    SaftyZone parentSaftyZoneScript;       //�e�I�u�W�F�N�g�̃X�N���v�g
    [Header("�ړ��p")]
    public Vector3 preZone4pos;            //���g�̏����ʒu
    public Vector3 postZone4Pos;           //���g�̈ړ���̖ڕW�ʒu
    public Vector3 nowZone4Pos;            //���g�̌��݈ʒu
    public int zone4MoveSpeed;           //�ړ����x

    public Vector3 pre4Pos;                //�����ʒu�p(�����_�ȉ��؂�̂�
    float pre4Posx, pre4Posy, pre4Posz;    

    public Vector3 now4Pos;               �@�@�@�@//���݈ʒu�p(�����_�ȉ��؂�̂�
    float now4Posx, now4Posy, now4Posz;

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
        Zone4FlagInitialize();

        //���u�̖��O�Ə����ʒu���擾
        childSaftyZone4Obj = this.gameObject;
        childSaftyZone4ObjName = childSaftyZone4Obj.name;
        preZone4pos = childSaftyZone4Obj.transform.position;

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
    private void Zone4FlagInitialize()
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
        pre4Posx = Mathf.Floor(preZone4pos.x);
        pre4Posy = Mathf.Floor(preZone4pos.y);
        pre4Posz = Mathf.Floor(preZone4pos.z);
        pre4Pos = new Vector3(pre4Posx, pre4Posy, pre4Posz);

        //�ڕW�ʒu�ݒ�
        postZone4Pos = new Vector3(pre4Posx, pre4Posy, pre4Posz - 425.0f);

        //�ݒ肵����k���ʒu�ݒ�p�t���O��False�ɂ���
        setReduPosFlag = false;
    }

    //�k��
    private void ReducationZone2()
    {
        //�k���ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone4Pos, zone4MoveSpeed);
    }

    //�k�������`�F�b�N
    private void Zone2ReducationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now4Pos, postZone4Pos);

        //�k���������Ă��邩
        if (dis <= 1)
        {
            if (endReduFlag == false)
            {
                //�������Ă�����e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone4redu = true;
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
        postZone4Pos = pre4Pos;

        //�ݒ肵����g���ʒu�ݒ�p�t���O��False�ɂ���
        setMagPosFlag = false;
    }

    //�g��
    private void MagnificationZone2()
    {
        //�g��ڕW�n�_�܂ňړ�
        transform.position = Vector3.MoveTowards(transform.position, postZone4Pos, zone4MoveSpeed);
    }

    //�g�劮���`�F�b�N
    private void Zone2MagnificationCheck()
    {
        //���݈ʒu���擾
        GetNowPos();

        dis = Vector3.Distance(now4Pos, postZone4Pos);

        //�g�劮�����Ă��邩
        if (dis <= 1)
        {
            if (endMagFlag == false)
            {
                //�e�I�u�W�F�N�g�̕ϐ������Z
                parentSaftyZoneScript.zone4mag = true;

                endMagFlag = true;
            }
        }
    }

    //���ݒn�擾�p
    private void GetNowPos()
    {
        //���݈ʒu���擾
        nowZone4Pos = this.gameObject.transform.position;

        //�擾�������W�������_�ȉ��؂�̂�
        now4Posx = Mathf.Floor(nowZone4Pos.x);
        now4Posy = Mathf.Floor(nowZone4Pos.y);
        now4Posz = Mathf.Floor(nowZone4Pos.z);

        //Vector3�^�ɂ���
        now4Pos = new Vector3(now4Posx, now4Posy, now4Posz);
    }
}
