using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("�e��ϐ�")]
    public float maxHealth;          //�v���C���[�̍ő�HP
    public float nowHealth;         //�v���C���[�̌��݂�HP
    public float moveSpeed;          //�ړ����x
    public float defaultSpeed;       //���̈ړ����x
    public float speedTimer;         //�X�s�[�h�A�b�v�̎���
    public float speedLimit;         //�X�s�[�h�A�b�v�̌��E����
    public bool speedFlag = false;   //�X�s�[�h�A�b�v���Ă��邩
    public bool openBoxFlag = false; //�⋋�����J���邽�߂̃t���O
    private bool shieldFlag;         //�V�[���h���擾���Ă��邩
    Rigidbody rb;
    DropBox dropboxScript;           //�⋋���̃X�N���v�g

    public GameObject shieldObj;

    public bool GetOpenBoxFlag()
    {
        return openBoxFlag;
    }

    void Start()
    {
        //�e��ϐ�������
        moveSpeed = defaultSpeed;
        speedTimer = 0.0f;
        nowHealth = maxHealth;
        shieldFlag = false;

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

        PlayerMove();
    }

    //�⋋���ɓ��������Ƃ��̏���
    private void OnCollisionEnter(Collision collision)
    {
        //���������I�u�W�F�N�g���X�s�[�h�A�b�v�̂��̂�������
        if (collision.gameObject.name == "SpeedUpItem(Clone)")
        {
            //�ړ����xUP
            speedFlag = true;
            speedTimer = 0.0f;
            Debug.Log("����");
        }
        else if (collision.gameObject.name == "HealItem(Clone)")
        {
            nowHealth = maxHealth;
            Debug.Log("��");
        }
        else if (collision.gameObject.name == "PierceBulletItem(Clone)")
        {
            Debug.Log("�ђʒe�擾");
        }
        else if (collision.gameObject.name == "ShieldItem(Clone)")
        {
            shieldFlag = true;
            Debug.Log("�V�[���h�擾");
        }
        else if (collision.gameObject.name == "DropBox(Clone)")
        {
            nowHealth -= 2;
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

        //���E��]
        //�E��]
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0.0f, -50 * Time.deltaTime, 0.0f);
        }

        //����]
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += new Vector3(0.0f, 50 * Time.deltaTime, 0.0f);
        }

        //�������J����
        if (openBoxFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                dropboxScript.openFlag = true;
            }
        }

        //�V�[���h�W�J
        if (shieldFlag == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                var parent = this.transform;

                Instantiate(shieldObj, this.transform.position, Quaternion.identity, parent);
            }
        }
    }
}
