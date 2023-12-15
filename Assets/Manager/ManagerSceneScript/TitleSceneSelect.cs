using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class TitleSceneSelect : MonoBehaviour
{
    [Header("���V�[���J�ڎ�_�폜�V�[�����X�g")]
    public List<string> unLoadSceneNameList;
    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[��")]
    public List<string> LoadSceneNameList;
    //[Header("���[�f�B���O�e�L�X�g")]
    public Text loadText;

    [Header("�Q�[���I���m�F�V�[����")]
    public string CautionSceneName;
    private bool isLoadCautionScene;
    [Header("�t�F�[�h�A�E�g�p�l��")]
    public GameObject panel;


    public bool isLoad;
    private bool isPlayOtherScene;
    
    private bool isLoadChange;
    private bool isDestroyScene;
    private bool fade;

  
    void Start()
    {
        fade = false;
        isLoadChange = false;
        isDestroyScene = false;
        isPlayOtherScene = false;
    }

    private void Awake()
    {
        isLoad = false;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isDestroyScene)
        {
          
            // �V�[���폜
            foreach (string unLoadSceneName in unLoadSceneNameList)
            {
                if (unLoadSceneName != LoadSceneNameList[0])
                {
                    foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                    {
                        if (scene.name == unLoadSceneName)
                        {
                            SceneManager.Instance.DestroyScene(unLoadSceneName);
                        }
                    }
                }
            }
            isDestroyScene = false;
        }
    
        //���͂��󂯕t���邩�ǂ���
        if (!isPlayOtherScene)
        {
          
            if (fade)
            {
         
                panel.GetComponentInChildren<TittleSceneFadeOut>().Fadeout();
            }

            if (InputManager.Instance.GetButtonDown("UI", "Click"))
            {
               
             
                fade = true;
                panel.GetComponentInChildren<TittleSceneFadeOut>().Fadeout();
                if (!panel.GetComponentInChildren<TittleSceneFadeOut>().fadeout)
                {
                    Image imageComponent = panel.GetComponentInChildren<Image>();

                    // ���݂̐F�����擾
                    Color color = imageComponent.color;

                    // �A���t�@�`�����l����ݒ�
                    color.a = 0;

                    // �F����ݒ肵����
                    imageComponent.color = color;
                    // �V�[���ǂݍ���
                    SceneManager.Instance.LoadScene(LoadSceneNameList[0]);
                    isLoad = true;
                    isPlayOtherScene = true;
                    // �V�[���ύX
                    SceneManager.Instance.ChangeScene();
                }


            }
   
            if (!isLoadChange)
            {
                // �V�[���ύX
              //  SceneManager.Instance.ChangeScene();
                 isDestroyScene = true;
                isLoadChange = true;
            }
        }
        

   

        if (InputManager.Instance.GetButtonDown("UI", "Cancel") && !isLoad)
        {
            if (!isLoad)
            {
                // �V�[���ǂݍ���
                SceneManager.Instance.LoadScene(CautionSceneName);
                // �V�[���ύX
                SceneManager.Instance.ChangeScene();
                isLoad = true;
            }
        }

    }


    public void SetLoadFlag(bool flg)
    {
        if (flg)
        {
            isLoad = true;
        }
        else
        {
            isLoad = false;
            isPlayOtherScene = false;
            isLoadChange = false;
        }
    }
}