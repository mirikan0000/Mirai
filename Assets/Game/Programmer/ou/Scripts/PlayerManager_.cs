using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_ : MonoBehaviour
{
    //�Q�[���͍X�V���Ă��邩(�Ⴆ�΁F�J�n��ʁA�N���A��ʂȂǂł͂Ȃ����)
    bool is_start;

    //�Ə������킹�Ă���(�\����)
    bool is_aiming;

    //���ˁA�����[�h�͈ړ��ł��Ȃ�����
    bool is_moveable = true;

    //�v���C���[�̈ړ����x
    float move_speed = 5.0f;

    //�v���C���[�̉�]���x
    float rot_angle = 0.1f;

    //���ˊp�x�̒������x(��])
    float gunBarrel_rotSpeed = 0.5f;

    //���̔��ˊp�x(�C���A�Z�b�g���Ȃ����߁A��U�L�^����)
    float gun_rotAngle = 0.0f;

    //���ˋ����̒����ϐ�(Public�^)
    public float Bullet_RangeOffset = 0;

    //���ˈʒu�𒲐�����ϐ�(�v���C���[�̒��ł͂Ȃ��āA�O�Ŕ��˂��邽��)
    //�e�ۂƃv���C���[��������(����������)�A�e�ۂ͕ςȕ����ɂȂ邽�߁B
    public float bulletCreatePosOffset = 1.0f;

    //�e��(Public�^)
    public GameObject Buttet;
    //�e�ۗ\����(Public�^)
    public GameObject PredictionLine;

    //�e�ۗ\�������\�����邽�߂̕`�搔
    public int PredictionLineNumber = 66;

    //�e�ۗ\�����̌v�Z���ʃ��X�g(�`��ʒu��ۑ�����)
    List<GameObject> PredictionLine_List = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���J�n(�X�V��������)
        is_start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_start)
        {
            //��
            //�v���C���[�ړ�
            if (is_moveable)
            {
                //Keyboard
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(0, 0, move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector3(0, 0, -move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(new Vector3(0, -rot_angle, 0));
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(new Vector3(0, rot_angle, 0));
                }
            }

            //�e�ۗ\����
            //Space�L�[������������ƒe�ۗ\������`�悷��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //�\����
                //����@(�d�́A�O�p�֐��Ŗ͋[������)
                //�d��
                //�ړ��֎~
                is_moveable = false;
                //�Ə���(�\������`�悷�邽��)
                is_aiming = true;
            }

            //�e�۔���
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //����
                //����@(�d�́A�O�p�֐��Ŗ͋[������)
                //�d��
                //���˂�����ňړ���������
                is_moveable = true;
                //�Ə��ς�
                is_aiming = false;

                //�e�ۗ\�����̌v�Z���ʃ��X�g���N���A
                for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                //�e�ې���
                GameObject buttle = Instantiate(Buttet, transform.position, transform.rotation);
                //�e�ۂ̊p�x���v���C���[�ƈ�v����
                buttle.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                //�e�ۈʒu�̓v���C���[�̑O�ɂ���
                buttle.transform.Translate(new Vector3(0, 0, bulletCreatePosOffset));
            }
            //�Ə����̂��߁A�e�ۗ\������`�悷��
            //���ˊp�x�������ł���
            if (is_aiming)
            {
                //���ˊp�x�𒲐�����
                //���ݒ� ���ˊp�x�͈̔�:0��~90��
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
                }

                //���ˊp�x�𒲐��������߁A
                //�e�ۗ\�����̌v�Z���ʃ��X�g���N���A���čČv�Z����K�v
                for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                //�e�ۗ\�����̌v�Z�ƕ`�悷��
                DrewPredictionLine();
            }
        }
    }

    //�e�ۗ\�����̌v�Z�ƕ`��
    void DrewPredictionLine()
    {
        //���ˊp�x�����W�A���ɂ���
        float angle_y = gun_rotAngle * Mathf.Deg2Rad;
        //�v���C���[��]�p�x�����W�A���ɂ���
        float angle_xz = transform.eulerAngles.y * Mathf.Deg2Rad;

        //�e�ۂ̔��ˑ��x���Q�b�g����(Script�uBullet�v�ŕۑ����Ă���)
        float Bullet_Speed = PlayerPrefs.GetFloat("Bullet_Speed");

        //�e�ۗ\�������v�Z����
        for (int i = 0; i < PredictionLineNumber; i++)
        {
            //���ԊԊu
            float t = i * 0.05f;

            //�����ʂ̈ʒu(XZ���W)���v�Z����
            float X = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Cos(angle_y);
            float x = X * Mathf.Sin(angle_xz) + transform.position.x;
            float z = X * Mathf.Cos(angle_xz) + transform.position.z;

            //�d�͂Ɣ��ˋ��������킹�邽�߁A�v�Z����
            float Bullet_Gravity = Physics2D.gravity.y;
            //�d�� < = 0 
            if ((Physics2D.gravity.y + Bullet_RangeOffset) <= 0)
            {
                //���ˋ��������킹��"�d��"���v�Z����
                Bullet_Gravity += Bullet_RangeOffset;
                //���ˋ�����ۑ�����(Script�uBullet�v�Ōv�Z���鎞�Ɏg������)
                PlayerPrefs.SetFloat("Bullet_RangeOffset", Bullet_RangeOffset);
            }

            //�c�����̂x���W���v�Z����
            float y = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //�e�ۗ\������`�悷��
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);
            //�e�ۗ\�����̌v�Z���ʃ��X�g�ɕۑ�����
            PredictionLine_List.Add(gb);
        }
    }
}