using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class SaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�g�k�p")]
    public bool reducationFlag = false;     //���u���k�������邩
    public bool magnificationFlag = false;  //���u���g�傳���邩
    public bool destroyFlag = false;        //�g��I����폜���邽�߂̃t���O
    public bool delayFlag = true;           //�҂����ԗp�t���O
    public float delayTime;                 //�҂�����
    public float destroyTime;               //�j��܂ł̎���

    [Header("�k���i�K���Ƃ̕ϐ�")]
    public float redu1DelayTime;            //��i�K�ڂ̎��̑҂�����
    public float redu2DelayTime;
    public float redu3DelayTime;
    public float redu4DelayTime;

    [Header("�e�q�I�u�W�F�N�g�ړ������p")]
    public bool zone1redu = false;
    public bool zone1mag = false;
    public bool zone2redu = false;
    public bool zone2mag = false;
    public bool zone3redu = false;
    public bool zone3mag = false;
    public bool zone4redu = false;
    public bool zone4mag = false;

    [Header("�e�q�I�u�W�F�N�g�k���i�K����p")]
    public int reduStageNum = 1;                 //���i�K�ڂ�
    public bool zone1reduStage = false;
    public bool zone2reduStage = false;
    public bool zone3reduStage = false;
    public bool zone4reduStage = false;

    [Header("�e�q�I�u�W�F�N�g�g��i�K����p")]
    public int magStageNum = 0;                  //���i�K�ڂ�
    public bool zone1magStage = false;
    public bool zone2magStage = false;
    public bool zone3magStage = false;
    public bool zone4magStage = false;

    [Header("�o�O�`�F�b�N�p")]
    public bool bug = false;                //�ړ��I�����Ƀo�O������True�ɂ���
    public float bugTimer;                  //�o�O�������Ɉ�莞�Ԃň��u��j�󂷂�

    [Header("���u�����Ǘ��I�u�W�F�N�g�p")]
    GameObject saftyZoneSpawner;
    CreateSaftyZone saftyZoneSpwnerScript;

    void Start()
    {
        //�e�퐔�l������
        bugTimer = 0.0f;

        //���u�����p�X�N���v�g�擾
        saftyZoneSpawner = GameObject.Find("SaftyZoneSpawner");
        saftyZoneSpwnerScript = saftyZoneSpawner.GetComponent<CreateSaftyZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bug == false)
        {
            //���u�̏k���i�K����
            CheckReducationStage();

            //���u�̊g��i�K����

            //�k��������(�e�i�K�܂ňړ������Ƃ��j
            switch (reduStageNum)
            {
                case 0:  //�������(�������Ȃ��j
                    break;
                case 1:  //�����u
                    redu1DelayTime += Time.deltaTime;
                    break;

            }
            //�k��������(���S�ɏk���d�؂������j
            if (zone1redu == true && zone2redu == true && zone3redu == true && zone4redu == true)
            {
                if (delayFlag == true)
                {
                    Invoke("DelayRedu", delayTime);
                }
                else
                {
                    reducationFlag = false;
                    magnificationFlag = true;
                }
            }

            //�g�劮����(���S�Ɋg�債���������j
            if (zone1mag == true && zone2mag == true && zone3mag == true && zone4mag == true)
            {
                destroyFlag = true;

                magnificationFlag = false;
            }
        }
        else
        {
            OccurredBug();
        }

        if (destroyFlag == true)
        {
            Invoke("ObjDestroy", destroyTime);
        }
    }

    //�҂����ԗp
    private void DelayRedu()
    {
        reducationFlag = false;
        magnificationFlag = true;

        Debug.Log("�҂����Ԕ���");
    }

    //�I�u�W�F�N�g�j��
    private void ObjDestroy()
    {
        saftyZoneSpwnerScript.spawnCheck = true;
        Destroy(this.gameObject);
    }

    //���u�̏k���i�K����
    private void CheckReducationStage()
    {
        if (zone1reduStage == true && zone2reduStage == true && zone3reduStage == true && zone4reduStage == true)
        {
            //�e�q�I�u�W�F�N�g���P�i�K���̏k���������Ă���i�K��i�߂�
            reduStageNum++;

            //�e�q�I�u�W�F�N�g�̏k���i�K�t���O��False�ɂ���
            zone1reduStage = false;
            zone2reduStage = false;
            zone3reduStage = false;
            zone4reduStage = false;
        }
    }

    //���u�̊g��i�K����


    //�o�O�������p
    private void OccurredBug()
    {
        bugTimer += Time.deltaTime;

        if (bugTimer >= 3.0f)
        {
            destroyFlag = true;
        }
        else
        {
            Debug.Log(bugTimer);
        }
    }
}
