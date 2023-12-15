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
    public float launchedTimer;   //�N����̌o�ߎ���

    [Header("�V�[���J�ڗp")]
    [Header("�t�F�[�h�A�E�g�p")]
    public Canvas canvas;           //�t�F�[�h�A�E�g�p�L�����o�X
    public SceneFader canvasScript; //�L�����o�X�ɂ��Ă���X�N���v�g
    public bool sceneChangeFlag;
    public bool sceneFadeFlag;
    public bool fadeEnd;
    public bool operationFlag;
    [Header("��������֌W")]
    public GameObject operationObj;  //��������p�摜�I�u�W�F�N�g
    public Image operationImage;     //��������p�摜�I�u�W�F�N�g��Image�R���|�[�l���g
    public float operationTimer;     //��������p�摜�\���܂ł̌o�ߎ���
    [Header("�Q�[���I���m�F�V�[����")]
    public string CautionSceneName;
    public bool isLoad;
    [Header("���V�[���J�ڎ�_�폜�V�[�����X�g")]
    public string unLoadSceneName;
    void Start()
    {
        //�t���O����������
        FlagInitialize();

        //�o�ߎ��ԏ�����
        enterAreaEffectTimer = 0.0f;
        launchedTimer = 0.0f;
        operationTimer = 0.0f;

        //�t�F�[�h�A�E�g�p�X�N���v�g�擾
        canvasScript = canvas.GetComponent<SceneFader>();

        //��������p�摜�̃R���|�[�l���g�擾
        operationObj = GameObject.Find("Operarion");
        operationImage = operationObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g��������
        SpawnObject();

        //�V�[���t�F�C�h����
        SceneFade();

        //��������p�摜��\���ƃV�[���J��
        HiddenOperationImageAndSceneChange();

     
    
        
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
        sceneFadeFlag = false;
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

                launchFlag = true;
            }
        }

        //�N������̌o�ߎ��Ԍv��
        if (launchFlag == true)
        {
            launchedTimer += Time.deltaTime;

            if (launchedTimer > 2.0f)
            {
                sceneFadeFlag = true;
                launchedTimer = 0.0f;
                launchFlag = false;
            }
        }
    }

    //�V�[���t�F�C�h����
    private void SceneFade()
    {
        if (sceneFadeFlag == true)
        {
            //�t�F�[�h�A�E�g
            canvasScript.CallCoroutine();

            if (fadeEnd == true)
            {
                operationTimer += Time.deltaTime;

                if (operationTimer > 1.0f)
                {
                    operationFlag = true;
                    sceneFadeFlag = false;
                    fadeEnd = false;
                    operationTimer = 0.0f;
                }

                //��������摜�\��
                ShowOperatingInstructions();
            }
        }
    }

    //��������\��
    private void ShowOperatingInstructions()
    {
        if (operationFlag == true)
        {       // �V�[���폜
           SceneManager.Instance.DestroyScene(unLoadSceneName);
            //��������X���C�h����ʂɕ\��
            operationImage.enabled = true;
            SceneManager.Instance.LoadScene(CautionSceneName);
            // �V�[���ύX
            SceneManager.Instance.ChangeScene();
            isLoad = true;
        }

        
    }

    //��������p�摜��\���ƃV�[���J��
    private void HiddenOperationImageAndSceneChange()
    {
        if (operationFlag == true)
        {
            //�L�[���͑҂�
            if (Input.GetKeyDown(KeyCode.V))
            {
                //�L�[���͌�摜���\���ɂ���
                operationImage.enabled = false;
                operationFlag = false;
                sceneChangeFlag = true;
                operationTimer = 0.0f;
            }
        }

        if (sceneChangeFlag == true)
        {
            operationTimer += Time.deltaTime;

            if (operationTimer > 1.0f)
            {
                Debug.Log("�R�[�V�����V�[���l�[��");
                        // �V�[���ǂݍ���
                
            
            }
        }
    }
}
