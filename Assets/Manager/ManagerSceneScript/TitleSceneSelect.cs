using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class TitleSceneSelect : MonoBehaviour
{
    [Header("次シーン遷移時_削除シーンリスト")]
    public List<string> unLoadSceneNameList;
    [Header("次シーン遷移時_読み込みシーンリスト")]
    public List<string> loadSceneNameList;
    //[Header("ローディングテキスト")]
    public Text loadText;

    [Header("ゲーム終了確認シーン名")]
    public string CautionSceneName;
    private bool isLoadCautionScene;
    [Header("フェードアウトパネル")]
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
          
            // シーン削除
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

        //入力を受け付けるかどうか
        if (!isPlayOtherScene)
        {
            if (fadeout)
            {
                panel.GetComponent<FadeOut>().Fadeout();
            }
          
            // 次のステージ(下)に移動
            //if (InputManager.Instance.GetButtonDown("UI", "Down") && !isLoad)
            //{
            //    ButtonNo++;
            //    //SE_Select.Play();
            //}
            //// 前のステージ(上)に移動
            //if (InputManager.Instance.GetButtonDown("UI", "Up") && !isLoad)
            //{
            //    ButtonNo--;
            //    //SE_Select.Play();
            //}
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("読み込み出来た");
                fadeout = true;
                if (panel.GetComponent<Image>().color.a>=1.0f)
                {
                    // シーン読み込み
                    SceneManager.Instance.LoadScene("GameScene");
                    isLoad = true;
                    isPlayOtherScene = true;
                    // シーン変更
                    SceneManager.Instance.ChangeScene();
                }
             

                
            }
            if (InputManager.Instance.GetButtonDown("UI", "Click"))
            {
                Debug.Log("読み込み出来た");
                fadeout = true;
                panel.GetComponent<FadeOut>().Fadeout();
                if (panel.GetComponent<Image>().color.a >= 1.0f)
                {
                    // シーン読み込み
                    SceneManager.Instance.LoadScene("GameScene");
                    isLoad = true;
                    isPlayOtherScene = true;
                    // シーン変更
                    SceneManager.Instance.ChangeScene();
                }


            }
            // ステージ決定
            //if (InputManager.Instance.GetButtonDown("UI", "Click"))
            //{
            //    Debug.Log("読み込み出来た");
            //    // シーン読み込み
            //    SceneManager.Instance.LoadScene(CautionSceneName);
            //    // シーン変更
            //    SceneManager.Instance.ChangeScene();
            // ゲーム終了
            //if (ButtonNo == 4)
            //    {
            //        // シーン読み込み
            //        SceneManager.Instance.LoadScene(cautionSceneName);
            //        // シーン変更
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
                // シーン変更
                SceneManager.Instance.ChangeScene();
                    isDestroyScene = true;
                isLoadChange = true;
            }
        }
        

        // ゲームを終了
        // 今読み込まれているシーンの中で、確認画面が既に読み込まれていたら

        if (InputManager.Instance.GetButtonDown("UI", "Cancel") && !isLoad)
        {
            if (!isLoad)
            {
                // シーン読み込み
                SceneManager.Instance.LoadScene(CautionSceneName);
                // シーン変更
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