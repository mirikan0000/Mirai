using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class Missile : Weapon
{
    [SerializeField]
    [Header("�~�T�C���p")]
    public float flyingSpeed = 3.0f;            //�~�T�C���̔�s���x
    public float missileMaxLifeTime = 20.0f;    //�~�T�C���̍ő��s����
    public float missileLifeTime;               //�~�T�C���̔�s����
    public float missileMaxHomingTime = 10.0f;  //�~�T�C���̍ő�ǔ�����
    public float missileHomingTime;             //�~�T�C���̒ǔ�����
    public int missileMode;                     //�ǔ����ǔ�����Ȃ���
    public bool missileFireCheck = false;       //�~�T�C�������˂��ꂽ���ǂ���

    [Header("�ǔ��p")]
    public GameObject player1Obj;  //Player1�̃I�u�W�F�N�g
    public GameObject player2Obj;  //Player2�̃I�u�W�F�N�g
    public GameObject targetObj;   //�^�[�Q�b�g�̃I�u�W�F�N�g
    public NavMeshAgent missile;   //�i�r���b�V���擾�p
    public int missileShootor;     //�N���~�T�C���𐶐������̂�

    public float heightValue;   //�i�r���b�V������̍���

    [Header("�e�I�u�W�F�N�g�擾�p")]
    public GameObject parentPlayerObj;  //�~�T�C���𔭎˂���Player�̃I�u�W�F�N�g

    [Header("�G�t�F�N�g�p")]
    public ParticleSystem explosionEffect;  //�����̃G�t�F�N�g�I�u�W�F�N�g
    public AudioSource explosionSE;         //�������̉���

    void Start()
    {
        //�e��ϐ�������
        missileLifeTime = 0.0f;
        missileHomingTime = 0.0f;

        //�i�r���b�V���擾
        missile = GetComponent<NavMeshAgent>();

        //�����t�@�C���擾
        explosionSE = GetComponent<AudioSource>();

        //�e�I�u�W�F�N�g�̎擾�Ɣ�s��Ԃ̔���
        GetParentAndFireCheck();
    }

    // Update is called once per frame
    void Update()
    {
        //�G��⑫����
        CaptureEnemy();
     
        //�~�T�C�����΂�
        MoveMissile();
    }

    //���������甚���G�t�F�N�g&�������Đ�&�~�T�C���폜
    private void OnTriggerEnter(Collider other)
    {
         //�����G�t�F�N�g�Đ�&�j��
        SpawnEffectAndSEAndDestroy();
    }

    //�����G�t�F�N�g�Đ�
    private void SpawnEffectAndSEAndDestroy()
    {
        if (explosionEffect != null)  //�����G�t�F�N�g�I�u�W�F�N�g�������Ă��邩
        {
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            
            //�e���Ԃ�������
            missileLifeTime = 0.0f;
            missileHomingTime = 0.0f;
        }
        else
        {
            Debug.Log("�����G�t�F�N�g�I�u�W�F�N�g���ݒ肳��Ă��Ȃ�");
        }

        //���������Đ�
        if (explosionSE != null)  //���������ݒ肳��Ă��邩
        {
            //Instantiate(explosionSE, this.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("�����G�t�F�N�g�̉������ݒ肳��Ă��Ȃ�");
        }

        //�~�T�C���{�̂�j��
        Destroy(this.gameObject);
    }

    //�~�T�C�����΂�
    private void MoveMissile()
    {
        if (targetObj != null)  //�^�[�Q�b�g���ݒ肳��Ă��邩
        {
            switch (missileMode)
            {
                case 0:
                    //�~�T�C���̔�s���Ԃ��ő�ɂȂ�܂Ŕ�΂�
                    if (missileLifeTime < missileMaxLifeTime)
                    {
                        
                    }
                    else  //�ő�ɂȂ����甚��
                    {
                        SpawnEffectAndSEAndDestroy();
                    }
                    break;
                case 1:
                    //�~�T�C���̒ǔ����Ԃ��ő�ɂȂ�܂œG�Ɍ������Ĕ�΂�(AINavigation�g�p)
                    if (missileHomingTime < missileMaxHomingTime)
                    {
                        missile.SetDestination(targetObj.transform.position);

                        //������ύX���Ĕ�΂�
                        ChangeMissileHeight();
                    }
                    else  //�ő�ɂȂ����甚��
                    {
                        SpawnEffectAndSEAndDestroy();
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("�G���ݒ肳��Ă��܂���");
        }
        
    }

    //�~�T�C���̍����𒲐�
    private void ChangeMissileHeight()
    {
        Vector3 newPos = transform.position;
        newPos.y = GetNavMeshHeight() + heightValue;
        transform.position = newPos;
    }

    //�i�r���b�V���̍������擾
    private float GetNavMeshHeight()
    {
        NavMeshHit hit;

        if(NavMesh.SamplePosition(transform.position,out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position.y;
        }
        else
        {
            return 0.0f;
        }
    }

    //�G��⑫����
    private void CaptureEnemy()
    {
        if (missileShootor > 0 && missileShootor < 5)
        {
            switch (missileShootor)
            {
                case 1:  //Player1������������
                    targetObj = GameObject.FindWithTag("Player2");
                    break;
                case 2:  //Player2������������
                    targetObj = GameObject.FindWithTag("Player1");
                    break;
                case 3:  //Player����Ȃ��I�u�W�F�N�g������������
                    Debug.Log("�~�T�C�����o�����܂���");
                    break;
                case 4:  //�e�X�g�p
                    targetObj = GameObject.Find("TargetObj(Clone)");
                    break;
            }
        }
        else
        {
            Debug.Log("�\�����ʃ~�T�C������������܂���");
        }
    }

    //���˂����e�I�u�W�F�N�g�̃X�N���v�g���擾���A��s��Ԃ�����
    private void GetParentAndFireCheck()
    {
        //�e�I�u�W�F�N�g�擾
        parentPlayerObj = transform.parent.gameObject;

        
        if (parentPlayerObj != null)  //Null�`�F�b�N
        {
            //���������e�I�u�W�F�N�g�̖��O�Ŕ�s��Ԃ��̔���
            //�N�����˂����̂�������
            if (parentPlayerObj.gameObject.name == "1P")
            {
                missileShootor = 1;
                missileFireCheck = true;
                
            }
            else if (parentPlayerObj.gameObject.name == "2P")
            {
                missileShootor = 2;
                missileFireCheck = true;
            }
            else if (parentPlayerObj.gameObject.name == "EmpObjManage")
            {
                missileShootor = 4;
                missileFireCheck = true;
            }
            else
            {
                missileShootor = 3;
                missileFireCheck = false;
            }
        }
        else
        {
            Debug.Log("�~�T�C���𐶐������e�I�u�W�F�N�g��������܂���");
        }
    }
}
