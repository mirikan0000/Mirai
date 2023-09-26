using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameManager : SingletonMonoBehaviour<SceneNameManager>
{
    [System.Serializable]
    public struct SceneNameStruct
    {
        public string sceneNameBefore;
        public string sceneNameAfter;
        public Sprite sceneSprite;
    }
    public List<SceneNameStruct> sceneNameList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Sprite GetSceneSprite(string sceneNameBefore)
    {
        return sceneNameList.Find(a => a.sceneNameBefore == sceneNameBefore).sceneSprite;
    }
    public string GetSceneNameAfter(string sceneNameBefore)
    {
        return sceneNameList.Find(a => a.sceneNameBefore == sceneNameBefore).sceneNameAfter;
    }
    public bool IsSceneNameBefore(string sceneNameBefore)
    {
        if (sceneNameList.FindIndex(a => a.sceneNameBefore == sceneNameBefore) != -1)
        {
            return true;
        }
        return false;
    }
}
