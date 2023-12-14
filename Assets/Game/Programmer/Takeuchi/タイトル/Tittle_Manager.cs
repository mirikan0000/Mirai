using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tittle_Manager : MonoBehaviour
{
    [SerializeField]
    [Header("�v���C���[�������̃G�t�F�N�g�֌W")]
    public ParticleSystem spawnEffect;
    public Transform spawnEffectPos;
    public bool spawnEffectSpawnFlag;

    [Header("�v���C���[�̃I�u�W�F�N�g�֌W")]
    public GameObject playerObj;
    public Transform playerSpawnPos;
    public bool spawnPlayerFlag;

    [Header("�N���G���A�ɓ��������̃G�t�F�N�g�֌W")]
    public ParticleSystem enterAreaEffect;
    public Transform enterAreaEffectPos;
    public bool enterAreaEffectSpawnFlag;
    public bool playerEnterFlag;
    public float enterAreaEffectTimer;     //�v���C���[���N���G���A�ɓ����Ă��鎞�Ԍv���p
    public float maxEnterAreaEffectTimer;  

    [Header("�N�����̃G�t�F�N�g�֌W")]
    public ParticleSystem launchEffect;
    public Transform launchEffectPosRight;
    public Transform launchEffectPosLeft;
    public bool launchEffectSpawnFlag;
    public bool launchFlag;

    [Header("�V�[���J�ڗp")]
    [Header("�t�F�[�h�A�E�g�p")]
    public Canvas canvas;           //�t�F�[�h�A�E�g�p�L�����o�X
    public SceneFader canvasScript; //�L�����o�X�ɂ��Ă���X�N���v�g
    public bool sceneChangeFlag;
    public bool fadeEnd;
    public bool operationFlag;
    [Header("��������֌W")]
    public Image operationImage;  //��������p�摜

    void Start()
    {
        //�t���O����������
        FlagInitialize();

        //�v���C���[���N���G���A�ɓ����Ă��鎞�ԏ�����
        enterAreaEffectTimer = 0.0f;

        //�t�F�[�h�A�E�g�p�X�N���v�g�擾
        canvasScript = canvas.GetComponent<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g��������
        SpawnObject();

        //�V�[���J�ڏ���
        SceneFadeAndChange();
    }

    //�t���O����������
    private void FlagInitialize()
    {
        spawnEffectSpawnFlag = true;
        spawnPlayerFlag = false;
        enterAreaEffectSpawnFlag = false;
        playerEnterFlag = false;
        launchEffectSpawnFlag = false;
        launchFlag = false;
        sceneChangeFlag = false;
        fadeEnd = false;
        operationFlag = false;
    }

    //�I�u�W�F�N�g��������
    private void SpawnObject()
    {
        var parent = this.transform;

        //�v���C���[�������̃G�t�F�N�g����
        if (spawnEffectSpawnFlag == true)
        {
            if (spawnEffect != null)
            {
                Instantiate(spawnEffect, spawnEffectPos.transform.position, Quaternion.Euler(-90, 0, 0), parent);
                spawnEffectSpawnFlag = false;
            }
        }

        //�v���C���[����
        if (spawnPlayerFlag == true)
        {
            if (playerObj != null)
            {
                Instantiate(playerObj, playerSpawnPos.transform.position, Quaternion.identity, parent);
                spawnPlayerFlag = false;
            }
        }

        //�N���G���A�ɓ��������̃G�t�F�N�g����
        if (enterAreaEffectSpawnFlag == true)
        {
            if (enterAreaEffect != null)
            {
                Instantiate(enterAreaEffect, enterAreaEffectPos.transform.position, Quaternion.Euler(-90, 0, 0), parent);
                enterAreaEffectSpawnFlag = false;
            }

        }

        //�N�����̃G�t�F�N�g����
        if(launchEffectSpawnFlag == true)
        {
            if (launchEffect != null)
            {
                Instantiate(launchEffect, launchEffectPosRight.transform.position, Quaternion.identity, parent);
                Instantiate(launchEffect, launchEffectPosLeft.transform.position, Quaternion.identity, parent);
                launchEffectSpawnFlag = false;
            }
        }
    }

    //�V�[���J�ڏ���
    private void SceneFadeAndChange()
    {
        if (sceneChangeFlag == true)
        {
            //�t�F�[�h�A�E�g
            canvasScript.CallCoroutine();

            if (fadeEnd == true)
            {
                //��������摜�\��
                ShowOperatingInstructions();

                if (operationFlag == true)
                {
                    //�V�[���J��
                    //SceneManager.LoadScene("GameScene");
                    Debug.Log("�V�[���J��");

                    //�V�[���J�ڑ@�ۗp�t���O��False
                    sceneChangeFlag = false;
                }
            }
        }
    }

    //��������\��
    private void ShowOperatingInstructions()
    {
        //��������X���C�h����ʂɕ\��

        //�L�[���͑҂�

        //�t�F�[�h�A�E�g
        canvasScript.CallCoroutine();
    }
}
