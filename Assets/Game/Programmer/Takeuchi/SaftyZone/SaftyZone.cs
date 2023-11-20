using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class SaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�g�k�p")]
    public bool reducationFlag;             //���u���k�������邩
    public bool magnificationFlag;          //���u���g�傳���邩
    public bool destroyFlag;                //�g��I����폜���邽�߂̃t���O
    public bool delayFlag;                  //�҂����ԗp�t���O
    public float delayTime;                 //�҂�����
    public float destroyTime;               //�j��܂ł̎���
    private float timer;                    //�҂����Ԍv���p

    [Header("�e�k���i�K���Ƃ̑҂�����(�k��������ɑ҂���)")]
    public float redu1DelayTime;            //��i�K�ڂ̎��̑҂�����
    public float redu2DelayTime;            //��i�K�ڂ̎��̑҂�����
    public float redu3DelayTime;            //�O�i�K�ڂ̎��̑҂�����
    public float redu4DelayTime;            //�l�i�K�ڂ̎��̑҂�����
    public float redu5DelayTime;            //�ܒi�K�ڂ̎��̑҂�����
    public float redu6DelayTime;            //�Z�i�K�ڂ̎��̑҂�����

    [Header("�e�g��i�K���Ƃ̑҂�����")]
    public float mag1DelayTime;             //��i�K�ڂ̑҂�����
    public float mag2DelayTime;             //��i�K�ڂ̑҂�����
    public float mag3DelayTime;             //�O�i�K�ڂ̑҂�����
    public float mag4DelayTime;             //�l�i�K�ڂ̑҂�����
    public float mag5DelayTime;             //�ܒi�K�ڂ̑҂�����
    public float mag6DelayTime;             //�Z�i�K�ڂ̑҂�����

    [Header("�e�q�I�u�W�F�N�g�ړ������p")]
    //redu = �k������
    //mag  = �g�劮��
    public bool zone1ReduEnd = false;
    public bool zone1MagEnd = false;
    public bool zone2ReduEnd = false;
    public bool zone2MagEnd = false;
    public bool zone3ReduEnd = false;
    public bool zone3MagEnd = false;
    public bool zone4ReduEnd = false;
    public bool zone4MagEnd = false;

    [Header("�e�q�I�u�W�F�N�g�k���i�K����p")]
    public bool zone1reduStage = false;
    public bool zone2reduStage = false;
    public bool zone3reduStage = false;
    public bool zone4reduStage = false;

    [Header("�e�q�I�u�W�F�N�g�g��i�K����p")]
    public bool zone1magStage = false;
    public bool zone2magStage = false;
    public bool zone3magStage = false;
    public bool zone4magStage = false;

    [Header("�o�O�`�F�b�N�p")]
    public bool bug = false;                //�ړ��I�����Ƀo�O������True�ɂ���
    public float bugTimer;                  //�o�O�������Ɉ�莞�Ԃň��u��j�󂷂�

    [Header("�I�u�W�F�N�g�擾�p")]
    GameObject saftyZoneSpawner;            //�e�I�u�W�F�N�g
    CreateSaftyZone saftyZoneSpwnerScript;  //�e�I�u�W�F�N�g�̃X�N���v�g
    GameObject zone1Obj;                    //
    Zone1 zone1Script;                      //
    GameObject zone2Obj;                    //
    Zone2 zone2Script;                      //
    GameObject zone3Obj;                    //
    Zone3 zone3Script;                      //
    GameObject zone4Obj;                    //
    Zone4 zone4Script;                      //

    void Start()
    {
        //�e�퐔�l������
        bugTimer = 0.0f;
        timer = 0.0f;

        //�e��t���O������
        FlagInitialize();

        //�e�I�u�W�F�N�g�ƃX�N���v�g���擾
        GetObjectAndScript();

        Debug.Log("���܂ꂽ��");
    }

    void Update()
    {
        if (bug == false)
        {
            //���u�̏k���i�K����
            CheckReducationStage();

            //���u�̊g��i�K����
            CheckMagnificationStage();
            
            //�k��������(���S�ɏk���d�؂������j
            if (zone1ReduEnd == true && zone2ReduEnd == true && zone3ReduEnd == true && zone4ReduEnd == true)
            {
                if (delayFlag == true)
                {
                    ReducationMoveEnd();
                }
                else
                {
                    reducationFlag = false;
                    magnificationFlag = true;
                }
            }

            //�g�劮����(���S�Ɋg�債���������j
            if (zone1MagEnd == true && zone2MagEnd == true && zone3MagEnd == true && zone4MagEnd == true)
            {
                destroyFlag = true;

                magnificationFlag = false;
            }
        }
        else
        {
            OccurredBug();
        }

        //���u�I�u�W�F�N�g�j��
        if (destroyFlag == true)
        {
            ObjDestroy();
        }
    }

    //�e��t���O������
    private void FlagInitialize()
    {
        reducationFlag = true;
        magnificationFlag = false;
        destroyFlag = false;
        delayFlag = true;
    }

    //�I�u�W�F�N�g�擾
    private void GetObjectAndScript()
    {
        //���u�����p�X�N���v�g�擾(�e�I�u�W�F�N�g)
        saftyZoneSpawner = transform.parent.gameObject;
        saftyZoneSpwnerScript = saftyZoneSpawner.GetComponent<CreateSaftyZone>();

        //�e���u�I�u�W�F�N�g�̃X�N���v�g�擾(�q�I�u�W�F�N�g)
        zone1Obj = transform.Find("ChildSaftyZone1").gameObject;
        zone1Script = zone1Obj.GetComponent<Zone1>();

        zone2Obj = transform.Find("ChildSaftyZone2").gameObject;
        zone2Script=zone2Obj.GetComponent<Zone2>();

        zone3Obj = transform.Find("ChildSaftyZone3").gameObject;
        zone3Script=zone3Obj.GetComponent<Zone3>();

        zone4Script=transform.Find("ChildSaftyZone4").gameObject.GetComponent<Zone4>();
        Debug.Log(zone4Script.preZone4pos);
    }

    //�҂����ԗp
    private void ReducationMoveEnd()
    {
        timer += Time.deltaTime;

        if (timer > delayTime)
        {
            reducationFlag = false;
            magnificationFlag = true;

            timer = 0.0f;
        }
    }

    //�I�u�W�F�N�g�j��
    private void ObjDestroy()
    {
        timer += Time.deltaTime;

        if (timer > destroyTime)
        {
            saftyZoneSpwnerScript.spawnCheck = true;
            timer = 0.0f;

            Destroy(this.gameObject);
        }
    }

    //���u�̏k���i�K����
    private void CheckReducationStage()
    {
        if (zone1reduStage == true && zone2reduStage == true && zone3reduStage == true && zone4reduStage == true)
        {
            //�҂�����
            switch (saftyZoneSpwnerScript.reduCount)
            {
                case 1.0f:  //��i�K��
                    Debug.Log("��i�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu1DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 2.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
                case 2.0f:  //��i�K��
                    Debug.Log("��i�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu2DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 3.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
                case 3.0f:  //�O�i�K��
                    Debug.Log("�O�i�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu3DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 4.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
                case 4.0f:  //�l�i�K��
                    Debug.Log("�l�i�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu4DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 5.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
                case 5.0f:  //�ܒi�K��
                    Debug.Log("�ܒi�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu5DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 6.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
                case 6.0f:  //�Z�i�K��
                    Debug.Log("�Z�i�K�ڏk������");
                    timer += Time.deltaTime;

                    if (timer > redu6DelayTime)
                    {
                        //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                        zone1reduStage = false;
                        zone2reduStage = false;
                        zone3reduStage = false;
                        zone4reduStage = false;

                        //���u�����̒i�K�ɂ���
                        saftyZoneSpwnerScript.reduCount = 0.0f;
                        saftyZoneSpwnerScript.magCount = 1.0f;

                        //�҂����Ԍv���p�ϐ���������
                        timer = 0.0f;
                    }
                    break;
            }
        }
    }

    //���u�̊g��i�K����
    private void CheckMagnificationStage()
    {
        if(zone1magStage==true&&zone2magStage==true&&zone3magStage==true&& zone4magStage == true)
        {
            //�g��i�K�ɉ����ď���
            if (saftyZoneSpwnerScript.magStageCount <= 1)  //��i�K�ȉ��̎�
            {
                timer += Time.deltaTime;

                if (timer > delayTime)
                {
                    //�e�I�u�W�F�N�g�̊g��i�K�J�E���g�p�ϐ���������

                }
            }
            else if (saftyZoneSpwnerScript.magStageCount > 1)  //��i�K�ȏ�̎�
            {
                switch (saftyZoneSpwnerScript.magCount)
                {
                    case 1.0f:  //��i�K��
                        Debug.Log("��i�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag1DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 2.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                    case 2.0f:  //��i�K��
                        Debug.Log("��i�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag2DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 3.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                    case 3.0f:  //�O�i�K��
                        Debug.Log("�O�i�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag3DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 4.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                    case 4.0f:  //�l�i�K��
                        Debug.Log("�l�i�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag4DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 5.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                    case 5.0f:  //�ܒi�K��
                        Debug.Log("�ܒi�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag5DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 6.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                    case 6.0f:  //�Z�i�K��
                        Debug.Log("�Z�i�K�ڊg�劮��");
                        timer += Time.deltaTime;

                        if (timer > mag6DelayTime)
                        {
                            //�e�q�I�u�W�F�N�g�̏k���i�K�����t���O�����Z�b�g
                            zone1magStage = false;
                            zone2magStage = false;
                            zone3magStage = false;
                            zone4magStage = false;

                            //���u�����̒i�K�ɂ���
                            saftyZoneSpwnerScript.magCount = 0.0f;

                            //�҂����Ԍv���p�ϐ���������
                            timer = 0.0f;
                        }
                        break;
                }
            }
            
        }
    }

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
