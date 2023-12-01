using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleSceneManager : MonoBehaviour
{
    [SerializeField]
    [Header("�Ǘ��p�t���O")]
    public bool sceneChangeFlag;  //�V�[���J�ڗp�t���O
    public bool playerEnterFlag;  //�v���C���[���N���ł���ʒu�ɂ��邩�ǂ���

    void Start()
    {
        //�t���O�̏�����
        FlagInitialize();
    }

    void Update()
    {
        //�v���C���[�����{�b�g���N��������V�[���J��
        ChangeNextScene();
    }

    //�t���O������
    private void FlagInitialize()
    {
        sceneChangeFlag = false;
        playerEnterFlag = false;
    }

    //�V�[���J��
    private void ChangeNextScene()
    {
        if (playerEnterFlag == true && playerEnterFlag == true)
        {
            //�V�[���J��
            //SceneManager.LoadScene("�J�ڂ���V�[����");
        }
    }
}
