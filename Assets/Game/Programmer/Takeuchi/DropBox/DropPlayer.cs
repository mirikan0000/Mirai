using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    public static DropPlayer Instance;

    [SerializeField]
    [Header("�e��ϐ�")]
    public float moveSpeed;          //�ړ����x
    public float defaultSpeed;       //���̈ړ����x
    public float power;              //�U����
    public float defaultPawer;       //���̍U����
    public float speedTimer;         //�X�s�[�h�A�b�v�̎���
    public float speedLimit;         //�X�s�[�h�A�b�v�̌��E����
    public float powerTimer;         //�p���[�A�b�v�̎���
    public float powerLimit;         //�p���[�A�b�v�̌��E����
    public bool speedFlag = false;   //�X�s�[�h�A�b�v���Ă��邩
    public bool powerFlag = false;   //�p���[�A�b�v���Ă��邩
    public bool openBoxFlag = false; //�⋋�����J���邽�߂̃t���O
    Rigidbody rb;
    DropBox dropboxScript;           //�⋋���̃X�N���v�g

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        //�e��ϐ�������
        moveSpeed = defaultSpeed;
        power = defaultPawer;
        speedTimer = 0.0f;
        powerTimer = 0.0f;

        //RigidBody�擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�X�s�[�h�A�b�v
        if (speedFlag == true)
        {
            speedTimer += Time.deltaTime;

            moveSpeed = 10.0f;

            if (speedTimer > speedLimit)
            {
                speedFlag = false;
                moveSpeed = defaultSpeed;
                speedTimer = 0.0f;
            }
        }

        //�p���[�A�b�v
        if (powerFlag == true)
        {
            powerTimer += Time.deltaTime;

            power = 300.0f;

            if (powerTimer > powerLimit)
            {
                powerFlag = false;
                power = defaultPawer;
                powerTimer = 0.0f;
            }
        }

        PlayerMove();
    }

    //�⋋���ɓ��������Ƃ��̏���
    private void OnCollisionEnter(Collision collision)
    {
        //���������I�u�W�F�N�g���X�s�[�h�A�b�v�̂��̂�������
        if (collision.gameObject.name == "DropBoxSpeed(Clone)")
        {
            //�ړ����xUP
            speedFlag = true;
            speedTimer = 0.0f;
            Debug.Log("�����I�I");
        }
        else if (collision.gameObject.name == "DropBoxPower(Clone)")
        {
            //�U����UP
            powerFlag = true;
            powerTimer = 0.0f;
            Debug.Log("�U���̓A�b�v�I�I");
        }
    }

    //�����ɐG��Ă�Ԃ̏���
    private void OnCollisionStay(Collision collision)
    {
        //Player���⋋���ɐG��Ă��鎞
        if (collision.gameObject.name == "DropBox(Clone)")
        {
            //�⋋���̃X�N���v�g���擾
            dropboxScript = collision.gameObject.GetComponent<DropBox>();

            Debug.Log("�⋋�����J������");
            openBoxFlag = true;
        }
    }

    //�⋋�����痣�ꂽ���̏���
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "DropBox(Clone)")
        {
            dropboxScript = null;

            Debug.Log("�⋋������͂Ȃꂽ");
            openBoxFlag = false;
        }
    }

    //Player�̈ړ�
    private void PlayerMove()
    {
        //�O�㍶�E�ړ�
        //�O
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, moveSpeed * Time.deltaTime);
        }

        //��
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -moveSpeed * Time.deltaTime);
        }

        //�E
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //��
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //�������J����
        if (openBoxFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                dropboxScript.openFlag = true;
            }
        }
    }
}
