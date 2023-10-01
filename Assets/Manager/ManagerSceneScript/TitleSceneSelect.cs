using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class TitleSceneSelect : MonoBehaviour
{
    [Header("���V�[���J�ڎ�_�폜�V�[�����X�g")]
    public List<string> unLoadSceneNameList;
    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[�����X�g")]
    public List<string> loadSceneNameList;
    //[Header("���[�f�B���O�e�L�X�g")]
    public Text loadText;

    [Header("�Q�[���I���m�F�V�[����")]
    public string CautionSceneName;
    private bool isLoadCautionScene;
    [Header("�t�F�[�h�A�E�g�p�l��")]
    public GameObject panel;


    public bool isLoad;
    private bool isPlayOtherScene;
    string loadSceneName;
    private bool isLoadChange;
    private bool isDestroyScene;
    private bool fadeout;

  
    void Start()
    {
        fadeout = false;
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
                if (unLoadSceneName != loadSceneName)
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
            if (fadeout)
            {
                panel.GetComponent<FadeOut>().Fadeout();
            }
          
            // ���̃X�e�[�W(��)�Ɉړ�
            //if (InputManager.Instance.GetButtonDown("UI", "Down") && !isLoad)
            //{
            //    ButtonNo++;
            //    //SE_Select.Play();
            //}
            //// �O�̃X�e�[�W(��)�Ɉړ�
            //if (InputManager.Instance.GetButtonDown("UI", "Up") && !isLoad)
            //{
            //    ButtonNo--;
            //    //SE_Select.Play();
            //}
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�ǂݍ��ݏo����");
                fadeout = true;
                if (panel.GetComponent<Image>().color.a>=1.0f)
                {
                    // �V�[���ǂݍ���
                    SceneManager.Instance.LoadScene("GameScene");
                    isLoad = true;
                    isPlayOtherScene = true;
                    // �V�[���ύX
                    SceneManager.Instance.ChangeScene();
                }
             

                
            }
            if (InputManager.Instance.GetButtonDown("UI", "Click"))
            {
                Debug.Log("�ǂݍ��ݏo����");
                fadeout = true;
                panel.GetComponent<FadeOut>().Fadeout();
                if (panel.GetComponent<Image>().color.a >= 1.0f)
                {
                    // �V�[���ǂݍ���
                    SceneManager.Instance.LoadScene("GameScene");
                    isLoad = true;
                    isPlayOtherScene = true;
                    // �V�[���ύX
                    SceneManager.Instance.ChangeScene();
                }


            }
            // �X�e�[�W����
            //if (InputManager.Instance.GetButtonDown("UI", "Click"))
            //{
            //    Debug.Log("�ǂݍ��ݏo����");
            //    // �V�[���ǂݍ���
            //    SceneManager.Instance.LoadScene(CautionSceneName);
            //    // �V�[���ύX
            //    SceneManager.Instance.ChangeScene();
            // �Q�[���I��
            //if (ButtonNo == 4)
            //    {
            //        // �V�[���ǂݍ���
            //        SceneManager.Instance.LoadScene(cautionSceneName);
            //        // �V�[���ύX
            //        SceneManager.Instance.ChangeScene();
            //        isLoad = true;

            //    }
            //    else
            //    {
            //        if (ButtonNo == 0)
            //        {
            //            BGMManager.Instance.Stop();
            //        }

            //        loadSceneName = loadSceneNameList[ButtonNo];
            //        SceneManager.Instance.LoadScene(loadSceneName);

            //        isLoad = true;
            //        isPlayOtherScene = true;

            //    }

            // }
            if (!isLoadChange)
            {
                // �V�[���ύX
                SceneManager.Instance.ChangeScene();
                    isDestroyScene = true;
                isLoadChange = true;
            }
        }
        

        // �Q�[�����I��
        // ���ǂݍ��܂�Ă���V�[���̒��ŁA�m�F��ʂ����ɓǂݍ��܂�Ă�����

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