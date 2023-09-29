using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSceneChange : SingletonMonoBehaviour<LightSceneChange>
{
    public string[] lightSceneNameArray;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isActive = false;
        foreach (string lightSceneName in lightSceneNameArray)
        {
            if (lightSceneName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
            {
                isActive = true;
            }
        }
        if (!isActive)
        {
            foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
            {
                foreach (string lightSceneName in lightSceneNameArray)
                {
                    if (scene.name == lightSceneName && scene.isLoaded == true)
                    {
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
                    }
                }
            }
        }

    }
}
