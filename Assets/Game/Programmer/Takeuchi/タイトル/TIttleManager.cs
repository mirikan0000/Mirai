using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TIttleManager : MonoBehaviour
{
    [SerializeField]
    [Header("�o��������I�u�W�F�N�g")]
    public GameObject playerObj;      //���삷�邽�߂̃v���C���[�I�u�W�F�N�g

    [Header("�o���O�̃G�t�F�N�g")]
    public ParticleSystem spawnEffect;  //�v���C���[�����O�ɍĐ�

    [Header("�{�^�����������Ƃ��̃G�t�F�N�g")]
    public ParticleSystem buttonEffect;  //�{�^���������ꂽ�Ƃ��ɍĐ�

    [Header("���{�b�g�N�����̃G�t�F�N�g")]
    public ParticleSystem launchEffect;  //�{�^���������ꂽ�Ƃ��ɍĐ�(�{�^���G�t�F�N�g�����x�点�čĐ��j

    [Header("�t�F�[�h�A�E�g�p")]
    public GameObject mask;  //

    private Vector3 playerSpawnPos;             //�v���C���[�I�u�W�F�N�g�o���ʒu
    private Vector3 spawnEffectSpawnPos;        //�v���C���[�o�����̃G�t�F�N�g�o���ʒu
    private Vector3 buttonEffectSpawnPos;       //�{�^���������ꂽ�Ƃ��̃G�t�F�N�g�o���ʒu
    private Vector3 launchEffectSpawnPos_right; //���{�b�g�N�����̃G�t�F�N�g�o���ʒu�E
    private Vector3 launchEffectSpawnPos_left;  //���{�b�g�N�����̃G�t�F�N�g�o���ʒu��

    [Header("�t���O�Ǘ�")]
    public bool playerSpawnFlag;
    public bool spawnEffectFlag;
    public bool buttonEffectFlag;
    public bool launchEffectFlag;
    public bool sceneChangeFlag;

    void Start()
    {
        //�t���O����������
        FlagInitialize();
    }


    void Update()
    {
        if (playerSpawnFlag == true)
        {
            Instantiate(playerObj, playerSpawnPos, Quaternion.identity);
            playerSpawnFlag = false;
        }

        if (spawnEffectFlag == true)
        {
            Instantiate(spawnEffect, spawnEffectSpawnPos, Quaternion.identity);
            spawnEffectFlag = false;
        }

        if (buttonEffectFlag == true)
        {
            Instantiate(buttonEffect, buttonEffectSpawnPos, Quaternion.identity);
            buttonEffectFlag = false;
        }

        if (launchEffectFlag == true)
        {
            Instantiate(launchEffect, launchEffectSpawnPos_left, Quaternion.identity);
            Instantiate(launchEffect, launchEffectSpawnPos_right, Quaternion.identity);
            launchEffectFlag = false;
        }

        if (sceneChangeFlag == true)
        {
            //�V�[���J��
            SceneFadeOutChange();
        }
    }

    //�t���O����������
    private void FlagInitialize()
    {
        playerSpawnFlag = false;
        spawnEffectFlag = false;
        buttonEffectFlag = false;
        launchEffectFlag = false;
        sceneChangeFlag = false;
    }

    //�t�F�[�h�A�E�g�𔺂��V�[���J��
    private void SceneFadeOutChange()
    {
        //raw image�̎擾
    }
}
