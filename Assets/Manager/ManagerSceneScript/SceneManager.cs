//=============================================================================
//
// シーンマネージャー処理(シーン管理処理)

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
    /// シーン読み込み
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (sceneName == "")
        {
            Debug.Log("読み込み失敗");
            return;
        }

        Scene[] sceneArray = UnityEngine.SceneManagement.SceneManager.GetAllScenes();
        foreach(Scene scene in sceneArray)
        {
            if (scene.name == sceneName)
            {
                Debug.Log("読み込み失敗");
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
    /// シーン読み込み
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
    /// シーン変更(読み込んだシーンに)
    /// </summary>
    public void ChangeScene()
    {
        isChange = true;
    }
    /// <summary>
    /// シーン削除
    /// </summary>
    public void DestroyScene(string sceneName)
    {
      //  SoundManager.Instance.SoundDataClear();
        destroySceneNameList.Add(sceneName);
    }

   
    /// <summary>
    /// シーン読み込みコルーチン
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
    /// LoadSceneにセットしたシーンが読み込まれているか
    /// </summary>
    public bool GetIsLoad()
    {
        return isLoad;
    }
}
