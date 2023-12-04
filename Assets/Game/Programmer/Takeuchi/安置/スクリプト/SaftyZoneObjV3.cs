using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaftyZoneObjV3 : MonoBehaviour
{
    [SerializeField]
    [Header("���u�g�k�p")]
    public bool reducationFlag;     //���u�k���p�t���O
    public bool magnificationFlag;  //���u�g��p�t���O
    public bool destroyFlag;        //���u�j��p�t���O

    private float timer;            //�҂����Ԍv���p

    [Header("���u�̒i�K")]
    public float maxReduStage;      //�ő�k����(�C���X�y�N�^�[�Őݒ�j
    public float maxMagStage;       //�ő�g���(�C���X�y�N�^�[�Őݒ�)
    public float nowReduStage;      //���݂̏k����
    public float nowMagStage;       //���݂̊g���
    public float nextReduStage;     //���̈��u�̏k���i�K
    public float nextMagStage;      //���̈��u�̊g��i�K

    [Header("�e�k���i�K���Ƃ̑҂�����")]
    public float redu1DelayTime;  //��i�K�ڏk���O�̑҂�����
    public float redu2DelayTime;  //��i�K�ڏk���O�̑҂�����
    public float redu3DelayTime;  //�O�i�K�ڏk���O�̑҂�����
    public float redu4DelayTime;  //�l�i�K�ڏk���O�̑҂�����
    public float redu5DelayTime;  //�ܒi�K�ڏk���O�̑҂�����
    public float redu6DelayTime;  //�Z�i�K�ڏk���O�̑҂�����

    [Header("�e�g��i�K���Ƃ̑҂�����")]
    public float mag1DelayTime;  //��i�K�ڊg��O�̑҂�����
    public float mag2DelayTime;  //��i�K�ڊg��O�̑҂�����
    public float mag3DelayTime;  //�O�i�K�ڊg��O�̑҂�����
    public float mag4DelayTime;  //�l�i�K�ڊg��O�̑҂�����
    public float mag5DelayTime;  //�ܒi�K�ڊg��O�̑҂�����
    public float mag6DelayTime;  //�Z�i�K�ڊg��O�̑҂�����

    [Header("�e�q�I�u�W�F�N�g�ړ������p")]  //�ړ�����������{�P����
    //�k���̎��p
    public float zone1NowReduStage;
    public float zone2NowReduStage;
    public float zone3NowReduStage;
    public float zone4NowReduStage;
    //�k�������S�ɏI��������p True�@�ňړ�����
    public bool zone1ReduEndFlag;
    public bool zone2ReduEndFlag;
    public bool zone3ReduEndFlag;
    public bool zone4ReduEndFlag;
    //�g��̎��p
    public float zone1NowMagStage;
    public float zone2NowMagStage;
    public float zone3NowMagStage;
    public float zone4NowMagStage;
    //�g�傪���S�ɏI��������p True�@�ňړ�����
    public bool zone1MagEndFlag;
    public bool zone2MagEndFlag;
    public bool zone3MagEndFlag;
    public bool zone4MagEndFlag;

    [Header("�I�u�W�F�N�g�擾�p")]
    GameObject spawnerObj;      //�e�I�u�W�F�N�g
    Spawner spawnerScript;      //�e�I�u�W�F�N�g�̃X�N���v�g

    void Start()
    {
        //�ϐ�������
        VariableInitialize();

        //�t���O������
        FlagInitialize();
    }

    void Update()
    {
        //���u�̏k���i�K�m�F
        CheckReducationStage();

        //���u�̊g��i�K�m�F
        CheckMagnificationStage();

        //���u�̔j�󏈗�
        if (destroyFlag == true)
        {
            spawnerScript.spawnFlag = true;

            Destroy(this.gameObject);
        }
    }

    //�ϐ�������
    private void VariableInitialize()
    {
        timer = 0.0f;
        nowReduStage = 0.0f;
        nowMagStage = 0.0f;
        zone1NowReduStage = 0.0f;
        zone2NowReduStage = 0.0f;
        zone3NowReduStage = 0.0f;
        zone4NowReduStage = 0.0f;
        zone1NowMagStage = 0.0f;
        zone2NowMagStage = 0.0f;
        zone3NowMagStage = 0.0f;
        zone4NowMagStage = 0.0f;

        nextReduStage = 1.0f;
        nextMagStage = 0.0f;

        //�e�I�u�W�F�N�g�ƃX�N���v�g�擾
        spawnerObj = transform.parent.gameObject;
        spawnerScript = spawnerObj.GetComponent<Spawner>();
    }

    //�t���O�̏�����
    private void FlagInitialize()
    {
        reducationFlag = false;
        reducationFlag = true;
        magnificationFlag = false;
        destroyFlag = false;

        zone1ReduEndFlag = false;
        zone2ReduEndFlag = false;
        zone3ReduEndFlag = false;
        zone4ReduEndFlag = false;

        zone1MagEndFlag = false;
        zone2MagEndFlag = false;
        zone3MagEndFlag = false;
        zone4MagEndFlag = false;
    }

    //���u�̏k���i�K�m�F
    private void CheckReducationStage()
    {
        //�e�q�I�u�W�F�N�g�̌��ݏk���i�K�ɉ����ďk���񐔂����Z
        if(zone1NowReduStage == 0.0f && zone2NowReduStage == 0.0f &&
            zone3NowReduStage == 0.0f && zone4NowReduStage == 0.0f && nowReduStage == 0.0f)
        {
            //�����u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu1DelayTime)
            {
                nowReduStage = 1.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==1.0f&&zone2NowReduStage==1.0f&&
            zone3NowReduStage == 1.0f && zone4NowReduStage == 1.0f)
        {
            //�����u�̗\��
            nextReduStage = 2.0f;

            //�����u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu2DelayTime)
            {
                nowReduStage = 2.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==2.0f&&zone2NowReduStage==2.0f&&
            zone3NowReduStage == 2.0f && zone4NowReduStage == 2.0f)
        {
            //��O���u�̗\��
            nextReduStage = 3.0f;

            //��O���u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu3DelayTime)
            {
                nowReduStage = 3.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage==3.0f&&zone2NowReduStage==3.0f&&
            zone3NowReduStage == 3.0f && zone4NowReduStage == 3.0f)
        {
            //��l���u�̗\��
            nextReduStage = 4.0f;

            //��l���u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu4DelayTime)
            {
                nowReduStage = 4.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowReduStage == 4.0f && zone2NowReduStage == 4.0f &&
            zone3NowReduStage == 4.0f && zone4NowReduStage == 4.0f)
        {
            //��܈��u�̗\��
            nextReduStage = 5.0f;

            //��܈��u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu5DelayTime)
            {
                nowReduStage = 5.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowReduStage == 5.0f && zone2NowReduStage == 5.0f &&
            zone3NowReduStage == 5.0f && zone4NowReduStage == 5.0f)
        {
            //��Z���u�̗\��
            nextReduStage = 6.0f;

            //��Z���u�̑҂�����
            timer += Time.deltaTime;

            if (timer > redu6DelayTime)
            {
                nowReduStage = 6.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowReduStage == 6.0f && zone2NowReduStage == 6.0f &&
            zone3NowReduStage == 6.0f && zone3NowReduStage == 6.0f)
        {
            nowReduStage = 7.0f;
        }

        //�k����������
        if (nowReduStage > maxReduStage)
        {
            //�q�I�u�W�F�N�g�̏k�������t���O�Ǘ�
            zone1ReduEndFlag = true;
            zone2ReduEndFlag = true;
            zone3ReduEndFlag = true;
            zone4ReduEndFlag = true;

            EndReducation();
        }
    }

    //���u�̊g��i�K�m�F
    private void CheckMagnificationStage()
    {
        //�e�q�I�u�W�F�N�g�̌��ݏk���i�K�ɉ����ďk���񐔂����Z
        if(zone1NowMagStage == 0.0f && zone2NowMagStage == 0.0f &&
            zone3NowMagStage == 0.0f && zone4NowMagStage == 0.0f && nowMagStage == 0.0f&& reducationFlag == false)
        {
            //���u���g��̑҂�����
            timer += Time.deltaTime;

            if (timer > mag1DelayTime)
            {
                nowMagStage = 1.0f;
                magnificationFlag = true;

                timer = 0.0f;
            }
        }
        else if(zone1NowMagStage == 1.0f && zone2NowMagStage == 1.0f &&
            zone3NowMagStage == 1.0f && zone4NowMagStage == 1.0f)
        {
            //���u���g��̑҂�����
            timer += Time.deltaTime;
            
            if (timer > mag2DelayTime)
            {
                nowMagStage = 2.0f;

                timer = 0.0f;
            }
        }
        else if(zone1NowMagStage==2.0f&&zone2NowMagStage==2.0f&&
            zone3NowMagStage == 2.0f && zone4NowMagStage == 2.0f)
        {
            //���u��O�g��̑҂�����
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > mag3DelayTime)
            {
                nowMagStage = 3.0f;
                Debug.Log(nowMagStage);
                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 3.0f && zone2NowMagStage == 3.0f &&
            zone3NowMagStage == 3.0f && zone4NowMagStage == 3.0f)
        {
            //���u��l�g��̑҂�����
            timer += Time.deltaTime;

            if (timer > mag4DelayTime)
            {
                nowMagStage = 4.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 4.0f && zone2NowMagStage == 4.0f &&
            zone3NowMagStage == 4.0f && zone4NowMagStage == 4.0f)
        {
            //���u��܊g��̑҂�����
            timer += Time.deltaTime;

            if (timer > mag5DelayTime)
            {
                nowMagStage = 5.0f;

                timer = 0.0f;
            }
        }
        else if (zone1NowMagStage == 5.0f && zone2NowMagStage == 5.0f &&
            zone3NowMagStage == 5.0f && zone4NowMagStage == 5.0f)
        {
            //���u��Z�g��̑҂�����
            timer += Time.deltaTime;

            if (timer > mag6DelayTime)
            {
                nowMagStage = 6.0f;

                timer = 0.0f;
            }
        }

        //�g�劮������
        if (nowMagStage > maxMagStage)
        {
            //�e�q�I�u�W�F�N�g�̊g�劮���t���O�Ǘ�
            zone1MagEndFlag = true;
            zone2MagEndFlag = true;
            zone3MagEndFlag = true;
            zone4MagEndFlag = true;

            EndMagnification();
        }
    }

    //�k����������
    private void EndReducation()
    {
        if (reducationFlag == true)
        {
            //�q�I�u�W�F�N�g�p�̏k�������t���O���S��True�Ȃ�
            if (zone1ReduEndFlag == true && zone2ReduEndFlag == true &&
                zone3ReduEndFlag == true && zone4ReduEndFlag == true)
            {
                //���u�k���p�t���O��False��
                reducationFlag = false;

                //���݂̏k���񐔂��O��
                nowReduStage = 0.0f;

                //���݂̊g��񐔂��P��
                //nowMagStage = 1.0f;
            }
        }
    }

    //�g�劮������
    private void EndMagnification()
    {
        if (magnificationFlag == true)
        {
            //�q�I�u�W�F�N�g�p�̊g�劮���t���O���S��True�Ȃ�
            if (zone1MagEndFlag == true &&
                zone2MagEndFlag == true &&
                zone3MagEndFlag == true &&
                zone4MagEndFlag == true)
            {
                //���u�g��p�t���O��False��
                magnificationFlag = false;

                //���݂̊g��񐔂��O��
                nowMagStage = 0.0f;

                //���u�j��p�t���O��True��
                destroyFlag = true;
            }
        }
    }
}
