using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleTestPlayer : MonoBehaviour
{
    public float tPmoveSpeed;
    private Rigidbody rb;
    [SerializeField] float rot_angle = 1f;
    public Animator animator;

    [Header("�v���C���[�ړ��֌W")]
    public bool playerMoveFlag;

    [Header("�e�I�u�W�F�N�g�֌W")]
    private GameObject parentGameObj;
    private Tittle_Manager parentScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Transform childTransform = transform.GetChild(0); // 0�͎q�I�u�W�F�N�g�̃C���f�b�N�X
        animator = childTransform.GetComponent<Animator>();

        //�e�I�u�W�F�N�g�擾����
        parentGameObj = transform.parent.gameObject;
        parentScript = parentGameObj.GetComponent<Tittle_Manager>();

        playerMoveFlag = true;
    }

    void Update()
    {  // �L�[���͂Ɋ�Â��Ĉړ��x�N�g�����v�Z
        Vector3 moveVector = Vector3.zero;

        // �v���C���[�̌������擾
        Vector3 playerDirection = transform.forward;
        if (animator)
        {
            animator.SetBool("Walk",true);
        }

        //�ړ�����
        if (parentScript.playerTurnFlag == false && parentScript.playerMoveStopFlag == false)
        {
            playerMoveFlag = true;
        }
        else if (parentScript.playerTurnFlag == true && parentScript.playerMoveStopFlag == false)
        {
            playerMoveFlag = false;

            //�v���C���[�̌�������]
            transform.Rotate(new Vector3(0, 90, 0));

            parentScript.playerTurnFlag = false;
        }
        else if (parentScript.playerTurnFlag == false && parentScript.playerMoveStopFlag == true)
        {
            playerMoveFlag = false;
        }

        if (playerMoveFlag == true)
        {
           moveVector += playerDirection;

           // �ړ��x�N�g���ɑ������|���Ĉʒu���X�V
           transform.position += moveVector.normalized * tPmoveSpeed * Time.deltaTime;

        }
        //if (Input.GetKey(KeyCode.W))
        //{
        //    moveVector += playerDirection;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    moveVector -= playerDirection;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(new Vector3(0, rot_angle, 0));

        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(new Vector3(0, -rot_angle, 0));
        //}

        // �ړ��x�N�g���ɑ������|���Ĉʒu���X�V
        //transform.position += moveVector.normalized * tPmoveSpeed * Time.deltaTime;
    }
}
