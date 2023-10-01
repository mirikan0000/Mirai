//=============================================================================
//
// �V�[���}�l�[�W���[����(�V�[���Ǘ�����)

//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    AsyncOperation asyncOperation;
    private bool isChange = false;

    private List<string> addSceneNameList = new List<string>();
    private List<string> destroySceneNameList = new List<string>();

    private bool isLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoad)
        {
            foreach (string destroySceneName in destroySceneNameList)
            {
                if (destroySceneName != null)
                {
                    bool isDestory = false;
                    foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                    {
                        if (scene.name == destroySceneName)
                        {
                            isDestory = true;
                        }
                    }
                    if (isDestory)
                    {
                        UnityEngine.SceneManagement.SceneManager.UnloadScene(destroySceneName);
                    }
                }
               
            }
            destroySceneNameList.Clear();
        }
    }

    /// <summary>
    /// �V�[���ǂݍ���
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (sceneName == "")
        {
            Debug.Log("�ǂݍ��ݎ��s");
            return;
        }

        Scene[] sceneArray = UnityEngine.SceneManagement.SceneManager.GetAllScenes();
        foreach(Scene scene in sceneArray)
        {
            if (scene.name == sceneName)
            {
                Debug.Log("�ǂݍ��ݎ��s");
                return;
            }
        }

        isLoad = true;

        addSceneNameList.Add(sceneName);
        if (addSceneNameList.Count == 1)
        {
            StartCoroutine("LoadSceneCoroutine");
        }
    }
    /// <summary>
    /// �V�[���ǂݍ���
    /// </summary>
    public void LoadScenes(string sceneName)
    {
        if (addSceneNameList.Contains(sceneName))
        {
            return;
        }

        isLoad = true;
        addSceneNameList.Add(sceneName);

        if (addSceneNameList.Count == 1)
        {
            StartCoroutine("LoadSceneCoroutine");
        }
    }
    /// <summary>
    /// �V�[���ύX(�ǂݍ��񂾃V�[����)
    /// </summary>
    public void ChangeScene()
    {
        isChange = true;
    }
    /// <summary>
    /// �V�[���폜
    /// </summary>
    public void DestroyScene(string sceneName)
    {
      //  SoundManager.Instance.SoundDataClear();
        destroySceneNameList.Add(sceneName);
    }

   
    /// <summary>
    /// �V�[���ǂݍ��݃R���[�`��
    /// </summary>
    private IEnumerator LoadSceneCoroutine()
    {
        if (addSceneNameList.Count > 0)
        {
            asyncOperation = Application.LoadLevelAdditiveAsync(addSceneNameList[0]);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    if (isChange)
                    {
                        asyncOperation.allowSceneActivation = true;
                 
                        if (addSceneNameList.Count > 0)
                        {
                            addSceneNameList.RemoveAt(0);
                            StartCoroutine("LoadSceneCoroutine");
                        }
                        else
                        {
                            isChange = false;
                            isLoad = false;
                        }
                    }
                }

                yield return null;
            }
            if (addSceneNameList.Count != 0)
            {
                addSceneNameList.RemoveAt(0);
            }
            if (addSceneNameList.Count > 0)
            {
                StartCoroutine("LoadSceneCoroutine");
            }
            else
            {
                isChange = false;
                isLoad = false;
            }
        }
    }
    /// <summary>
    /// LoadScene�ɃZ�b�g�����V�[�����ǂݍ��܂�Ă��邩
    /// </summary>
    public bool GetIsLoad()
    {
        return isLoad;
    }
}
