
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("[メインシーン以外は変数の中身は空に]")]

    [Header("読み込むシーン名リスト")]
    public List<string> loadSceneNameList;
    [Header("最初に読み込まれるシーン名")]
    public string loadSceneStart;

    // メインシーンでは無かったか
    public static bool isNotMain = false;

    private void Awake()
    {
        // メインを読み込んでいなかった場合
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
        // メインを読み込み済の場合
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