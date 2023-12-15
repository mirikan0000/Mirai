using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class TitleSceneSelect : MonoBehaviour
{
    [Header("次シーン遷移時_削除シーンリスト")]
    public List<string> unLoadSceneNameList;
    [Header("次シーン遷移時_読み込みシーン")]
    public List<string> LoadSceneNameList;
    //[Header("ローディングテキスト")]
    public Text loadText;

    [Header("ゲーム終了確認シーン名")]
    public string CautionSceneName;
    private bool isLoadCautionScene;
    [Header("フェードアウトパネル")]
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
          
            // シーン削除
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
    
        //入力を受け付けるかどうか
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

                    // 現在の色情報を取得
                    Color color = imageComponent.color;

                    // アルファチャンネルを設定
                    color.a = 0;

                    // 色情報を設定し直す
                    imageComponent.color = color;
                    // シーン読み込み
                    SceneManager.Instance.LoadScene(LoadSceneNameList[0]);
                    isLoad = true;
                    isPlayOtherScene = true;
                    // シーン変更
                    SceneManager.Instance.ChangeScene();
                }


            }
   
            if (!isLoadChange)
            {
                // シーン変更
              //  SceneManager.Instance.ChangeScene();
                 isDestroyScene = true;
                isLoadChange = true;
            }
        }
        

   

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