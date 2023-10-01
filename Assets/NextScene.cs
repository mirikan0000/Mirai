using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    private bool isDestroyScene;
    public List<string> unLoadSceneNameList;
    private string loadSceneName;
    private bool isEnd;
   public GameObject Player1;
   public GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        // èâä˙ílÇÕfalseÇ…ê›íË
        isEnd = false;
      //  SceneManager.Instance.LoadScene("EndScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1.GetComponent<PlayerHealth>().isEnd || Player2.GetComponent<PlayerHealth>().isEnd)
        {
            isEnd = true;
        }
        if (isEnd)
        {
            Debug.Log("once!!!");
            SceneManager.Instance.LoadScene("EndScene");
            isDestroyScene = true;
        }


        if (isDestroyScene)
        {

            // ÉVÅ[ÉìçÌèú
            foreach (string unLoadSceneName in unLoadSceneNameList)
            {
                if (unLoadSceneName != loadSceneName)
                {
                    foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                    {
                        if (scene.name == unLoadSceneName)
                        {
                            SceneManager.Instance.DestroyScene(unLoadSceneName);
                            //     SceneManager.Instance.ChangeScene();
                        }
                    }
                }
            }

            isDestroyScene = false;
        }
    }
}
