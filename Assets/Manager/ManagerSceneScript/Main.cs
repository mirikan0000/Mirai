
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("[���C���V�[���ȊO�͕ϐ��̒��g�͋��]")]

    [Header("�ǂݍ��ރV�[�������X�g")]
    public List<string> loadSceneNameList;
    [Header("�ŏ��ɓǂݍ��܂��V�[����")]
    public string loadSceneStart;

    // ���C���V�[���ł͖���������
    public static bool isNotMain = false;

    private void Awake()
    {
        // ���C����ǂݍ���ł��Ȃ������ꍇ
        bool isNotLoad = false;
        foreach (Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
        {
            if (scene.name == "ManagerScene")
            {
                isNotLoad = true;
            }
        }
        if (!isNotLoad)
        {
            isNotMain = true;
            Application.LoadLevelAdditive("ManagerScene");
        }
        // ���C����ǂݍ��ݍς̏ꍇ
        else
        {
            foreach (string loadSceneName in loadSceneNameList)
            {
                SceneManager.Instance.LoadScene(loadSceneName);
            }
            if (!isNotMain)
            {
                if (loadSceneStart != "")
                {
                    SceneManager.Instance.LoadScene(loadSceneStart);
                }
            }
            SceneManager.Instance.ChangeScene();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}