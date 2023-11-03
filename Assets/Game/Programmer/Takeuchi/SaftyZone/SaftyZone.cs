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
                                            
    [Header("�e�q�I�u�W�F�N�g�ړ������p")]
    public bool zone1redu = false;
    public bool zone1mag = false;
    public bool zone2redu = false;
    public bool zone2mag = false;
    public bool zone3redu = false;
    public bool zone3mag = false;
    public bool zone4redu = false;
    public bool zone4mag = false;

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
