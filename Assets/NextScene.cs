using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    private bool isDestroyScene;
    public List<string> unLoadSceneNameList;
    public string loadSceneName;
    private bool isFadeIn;
    private bool isFadeEnd;
    private bool hasLoadedScene;
    public GameObject Player1;
   public GameObject Player2;
    public GameObject FadeOutPanel;
    // 他の変数の定義などがあれば追加
    public GameLoop gameLoop;

    public void SetGameLoopReference(GameLoop loop)
    {
        gameLoop = loop;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 初期値はfalseに設定
        isFadeIn = false;
        hasLoadedScene = false;
      //  SceneManager.Instance.LoadScene("EndScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLoop != null && gameLoop.IsGameFinished())
        {
            if (isDestroyScene)
            {

                // シーン削除
                foreach (string unLoadSceneName in unLoadSceneNameList)
                {
                    if (unLoadSceneName != loadSceneName)
                    {
                        foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                        {
                            if (scene.name == unLoadSceneName)
                            {
                                Debug.Log("デストローイ！");
                                SceneManager.Instance.DestroyScene(unLoadSceneName);
                                //     SceneManager.Instance.ChangeScene();
                            }
                        }
                    }
                }

                isDestroyScene = false;
            }
            if (Player1.GetComponent<PlayerHealth>().isEnd || Player2.GetComponent<PlayerHealth>().isEnd)
            {
                isFadeIn = true;
            }

            if (isFadeIn)
            {
                FadeOutPanel.GetComponentInChildren<FadeOut>().Fadeout();

            }
            if (isFadeIn && !FadeOutPanel.GetComponentInChildren<FadeOut>().fadeout)
            {
                isFadeEnd = true;
            }
            if (isFadeEnd && !hasLoadedScene)
            {
                hasLoadedScene = true;
                Debug.Log("once!!!");
                // SceneManager.Instance.LoadScene(loadSceneName);
                FadeOutPanel.GetComponent<LoadingScene>().LoadNextScene();
                isDestroyScene = true;
                SceneManager.Instance.ChangeScene();
            }

        }
    }
}
