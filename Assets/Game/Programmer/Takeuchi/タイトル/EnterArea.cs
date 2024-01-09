using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    [SerializeField]
    [Header("Manager�I�u�W�F�N�g�擾�p")]
    private GameObject managerObj;
    private Tittle_Manager managerScript;

    [Header("�v���C���[�N����ԊǗ��p")]
    public bool playerEnterFlag;
    public float enterTimer;
    private float enterTimerLimit = 5.0f;

    [Header("�N���Ǘ�")]
    public bool luFlag;

    void Start()
    {
        //Manager�I�u�W�F�N�g�̃X�N���v�g�擾
        managerObj = GameObject.Find("Manager");
        managerScript = managerObj.GetComponent<Tittle_Manager>();

        //�t���O����������
        FlagInitialize();

        //�e�퐔�l������
        enterTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    //�t���O����������
    private void FlagInitialize()
    {
        luFlag = true;
    }

    //�v���C���[�̐N����ԂŃt���O�Ǘ�
    private void OnTriggerStay(Collider other)
    {
        if (luFlag == true)
        {
            if (other.gameObject.tag == "Player1")
            {
                managerScript.playerMoveStopFlag = true;

                enterTimer += Time.deltaTime;

                if (enterTimer > enterTimerLimit)
                {
                    managerScript.launchEffectSpawnFlag = true;
                    luFlag = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            managerScript.playerEnterFlag = true;
            managerScript.enterAreaEffectSpawnFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            managerScript.playerEnterFlag = false;

            enterTimer = 0.0f;
        }
    }
}
