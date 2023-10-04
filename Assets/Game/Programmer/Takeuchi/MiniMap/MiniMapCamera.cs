using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField]
    [Header("�~�j�}�b�v�J�����̈ʒu,�ړ��p")]
    Vector3 miniMapCameraTargetPos;   //�~�j�}�b�v�J�����̌��݂̈ʒu���
    Vector3 miniMapCameraDefaultPos;  //�~�j�}�b�v�J�����̊�ʒu
    Transform miniMapCameraTransform;  //�~�j�}�b�v�J�����̈ʒu���
    public float miniMapCameraMoveSpeed;  //�~�j�}�b�v�J�����̈ړ��X�s�[�h

    [Header("�e�v���C���[�̃}�b�v�ԍ��p")]
    public int player1MapNum;  //Player1�̌��݈ʒu
    public int player2MapNum;  //Player2�̌��݈ʒu

    [Header("�v���C���[���m�̈ʒu��r�p")]
    public bool checkMapNum = false;  //Player���m�̃}�b�v�ԍ����������ǂ���
    public int bothPlayersNum;  //Player���m�̃}�b�v�ԍ����������̃}�b�v�ԍ�

    [Header("�\���I�u�W�F�N�g�̊g�k�p")]
    public float objScaleX;// = 1.0f;  //�\���I�u�W�F�N�g�̑傫��X����
    public float objScaleY;// = 0.2f;  //�\���I�u�W�F�N�g�̑傫��Y����
    public float objScaleZ;// = 1.0f;  //�\���I�u�W�F�N�g�̑傫��Z����
    private GameObject p1FakeObj;  //�~�j�}�b�v�ɕ\������Player1�̃I�u�W�F�N�g
    private GameObject p2FakeObj;  //�~�j�}�b�v�ɕ\������Player2�̃I�u�W�F�N�g

    //Player1�̌��݂̃}�b�v�ԍ����擾����p
    Player1 player1Script;          //Player2�̃X�N���v�g������p
    private GameObject player1Obj;  //Player1�̃I�u�W�F�N�g������p

    //Player2�̌��݂̃}�b�v�ԍ����擾����p
    Player2 player2Script;          //Player2�̃X�N���v�g������p
    private GameObject player2Obj;  //Player2�̃I�u�W�F�N�g������p



    void Start()
    {
        //Player1�̃}�b�v�ԍ�������
        player1MapNum = 0;
        //Player2�̃}�b�v�ԍ�������
        player2MapNum = 0;

        //�~�j�}�b�v�J�����̈ړ��X�s�[�h��ݒ�
        miniMapCameraMoveSpeed = 10f;

        //�~�j�}�b�v�J�����̈ʒu�����擾
        miniMapCameraTransform = this.GetComponent<Transform>();
        //Debug.Log(miniMapCameraTransform);

        //�~�j�}�b�v�J�����̃f�t�H���g�ʒu��ݒ�
        miniMapCameraDefaultPos = new Vector3(0, 25, 0);
    }

    void FixedUpdate()
    {
        GetPlayer1();  //Player1�̃I�u�W�F�N�g�A�X�N���v�g�A���݈ʒu�̃}�b�v�ԍ����擾
        GetPlayer2();  //Player2�̃I�u�W�F�N�g�A�X�N���v�g�A���݈ʒu�̃}�b�v�ԍ����擾

        ComparePlayerMapNum();  //�e�v���C���[�̈ʒu�ɂ���ăt���O���Ǘ�

        //�t���O�ɂ���ăJ�������ړ�
        if (checkMapNum == true)
        {
            MoveMiniMapCamera();  //�J�������ړ�������

            //�\���I�u�W�F�N�g���k��
            ChangePlayerFakeObjScaleShrink();
        }
        else
        {
            //�t���O��false�Ȃ�~�j�}�b�v�J�������f�t�H���g�̈ʒu�ɖ߂�
            miniMapCameraTransform.position = Vector3.MoveTowards(miniMapCameraTransform.position, miniMapCameraDefaultPos, miniMapCameraMoveSpeed * Time.deltaTime);

            //�\���I�u�W�F�N�g���g��
            ChangePlayerFakeObjScaleExpand();
        }
    }

    //�J�������ړ�������
    private void MoveMiniMapCamera()
    {
        //���v���C���[������}�b�v�Ƀt�H�[�J�X����
        switch (bothPlayersNum)
        {
            case 0:  //�J�������f�t�H���g�ʒu��
                miniMapCameraTargetPos = miniMapCameraDefaultPos;
                break;
            case 1:  //�J�������}�b�v�ԍ��P�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(-10, 9, -10);
                break;
            case 2:  //�J�������}�b�v�ԍ��Q�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(0, 9, -10);
                break;
            case 3:  //�J�������}�b�v�ԍ��R�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(10, 9, -10);
                break;
            case 4:  //�J�������}�b�v�ԍ��S�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(-10, 9, 0);
                break;
            case 5:  //�J�������}�b�v�ԍ��T�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(0, 9, 0);
                break;
            case 6:  //�J�������}�b�v�ԍ��U�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(10, 9, 0);
                break;
            case 7:  //�J�������}�b�v�ԍ��V�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(-10, 9, 10);
                break;
            case 8:  //�J�������}�b�v�ԍ��W�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(0, 9, 10);
                break;
            case 9:  //�J�������}�b�v�ԍ��X�̃}�b�v�Ƀt�H�[�J�X����
                miniMapCameraTargetPos = new Vector3(10, 9, 10);
                break;
        }

        //�~�j�}�b�v�J�����̈ʒu���ړ�
        miniMapCameraTransform.position = Vector3.MoveTowards(miniMapCameraTransform.position, miniMapCameraTargetPos, miniMapCameraMoveSpeed * Time.deltaTime);
    }

    //Player1�̃I�u�W�F�N�g�A�X�N���v�g�A���݈ʒu�̃}�b�v�ԍ����擾
    private void GetPlayer1()
    {
        //Player1�̃I�u�W�F�N�g���擾
        player1Obj = GameObject.FindWithTag("Player1");

        //Player1�̃~�j�}�b�v�ɕ\������I�u�W�F�N�g���擾
        p1FakeObj = GameObject.Find("Player1FakeObj");
        objScaleX = p1FakeObj.transform.localScale.x;
        objScaleY = p1FakeObj.transform.localScale.y;
        objScaleZ = p1FakeObj.transform.localScale.z;
        Debug.Log(p1FakeObj.transform.localScale);

        //player1ObjNull�`�F�b�N
        if (player1Obj != null)
        {
            //Player1�̃X�N���v�g���擾
            Player1 player1Script = player1Obj.GetComponent<Player1>();

            //player1ScriptNull�`�F�b�N
            if (player1Script != null)
            {
                //Player1�̌��݈ʒu�̃}�b�v�ԍ��擾
                player1MapNum = player1Script.p1NowMapNum;
                //Debug.Log(player1MapNum);
            }
            else
            {
                Debug.Log("Player1�̃X�N���v�g��������܂���");
            }
        }
        else
        {
            Debug.Log("Player1�̃I�u�W�F�N�g��������܂���");
        }
    }

    //Player2�̃I�u�W�F�N�g�A�X�N���v�g�A���݈ʒu�̃}�b�v�ԍ����擾
    private void GetPlayer2()
    {
        //Player2�̃I�u�W�F�N�g���擾
        player2Obj = GameObject.FindWithTag("Player2");

        //Player2�̃~�j�}�b�v�ɕ\������I�u�W�F�N�g���擾
        p2FakeObj = GameObject.Find("Player2FakeObj");

        //Player2ObjNull�`�F�b�N
        if (player2Obj != null)
        {
            //Player2�̃X�N���v�g���擾
            player2Script = player2Obj.GetComponent<Player2>();
            //Debug.Log(player2Script);

            //player2ScriptNull�`�F�b�N
            if (player2Script != null)
            {
                player2MapNum = player2Script.p2NowMapNum;
                //Debug.Log(player2MapNum);
            }
            else
            {
                Debug.Log("Player2�̃X�N���v�g��������܂���");
            }
        }
        else
        {
            Debug.Log("Player2�̃I�u�W�F�N�g��������܂���");
        }
    }

    //�e�v���C���[�̈ʒu�ɂ���ăt���O���Ǘ�
    private void ComparePlayerMapNum()
    {
        if (player1MapNum == player2MapNum)
        {
            checkMapNum = true;
            bothPlayersNum = player1MapNum;
            //Debug.Log(bothPlayersNum);
        }
        else
        {
            checkMapNum = false;
            ChangePlayerFakeObjScaleShrink();
            //Debug.Log(checkMapNum);
        }
    }

    //�J�����̈ʒu�ɂ���ă~�j�}�b�v�ɕ\������v���C���[�̃I�u�W�F�N�g���g�傷��
    private void ChangePlayerFakeObjScaleExpand()
    {
        //�I�u�W�F�N�g�̃T�C�Y���g��
        if (objScaleX < 2.0f && objScaleZ < 2.0f)
        {
            objScaleX += 1.0f; // * Time.deltaTime;
            objScaleZ += 1.0f; // * Time.deltaTime;
        }

        //�I�u�W�F�N�g�̃T�C�Y��ύX
        p1FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
        p2FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
    }

    //�J�����̈ʒu�ɂ���ă~�j�}�b�v�ɕ\������v���C���[�̃I�u�W�F�N�g���g�傷��
    private void ChangePlayerFakeObjScaleShrink()
    {
        //�I�u�W�F�N�g�̃T�C�Y���k��
        if (objScaleX > 1.0f && objScaleZ > 1.0f)
        {
            objScaleX -= 1.0f; // * Time.deltaTime;
            objScaleZ -= 1.0f; // * Time.deltaTime;
        }

        //�I�u�W�F�N�g�̃T�C�Y��ύX
        p1FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
        p2FakeObj.transform.localScale = new Vector3(objScaleX, objScaleY, objScaleZ);
    }
}
