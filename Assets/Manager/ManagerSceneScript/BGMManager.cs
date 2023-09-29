using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    public enum Type
    {
        Title,
      Game,
      End
    }
    [System.Serializable]
    public struct BGMSettingStruct
    {
        public Type type;
        public AudioClip bgm;
    }
    public List<BGMSettingStruct> bgmSettingStructList;

    private string bgmNameNow;
    public float fadeBgmSpeed;

    private string beforeActiveScneneName;

    private static bool isInit;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //string activeSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //if (activeSceneName == beforeActiveScneneName && !isInit)
        //{
        //    return;
        //}
        //beforeActiveScneneName = activeSceneName;
        //isInit = false;


        //GameObject stageNumObj = GameObject.FindGameObjectWithTag("stageNum");
        //if (stageNumObj != null)
        //{
        //   // StageNum stageNum = stageNumObj.GetComponent<StageNum>();
        //    string bgmName = "";
        //    switch (stageNum.worldNo)
        //    {
        //        case 0:
        //            bgmName = bgmSettingStructList.Find(a => a.type == Type.Title).bgm.name;
        //            if (bgmName != bgmNameNow)
        //            {
        //                SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            break;
        //        case 1:
        //            if (stageNum.stageNo <= 6)
        //            {
        //                bgmName = bgmSettingStructList.Find(a => a.type == Type.W1).bgm.name;
        //                SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            else
        //            {
        //                //bgmName = bgmSettingStructList.Find(a => a.type == Type.W1Boss).bgm.name;
        //                //SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            break;

        //        case 2:
        //            if (stageNum.stageNo <= 6)
        //            {
        //                bgmName = bgmSettingStructList.Find(a => a.type == Type.W2).bgm.name;
        //                SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            else
        //            {
        //                //bgmName = bgmSettingStructList.Find(a => a.type == Type.W2Boss).bgm.name;
        //                //SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            break;
        //        case 3:
        //            if (stageNum.stageNo <= 6)
        //            {
        //                bgmName = bgmSettingStructList.Find(a => a.type == Type.W3).bgm.name;
        //                SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            else
        //            {
        //                //bgmName = bgmSettingStructList.Find(a => a.type == Type.W3Boss).bgm.name;
        //                //SoundManager.Instance.PlayFadeBGM(bgmName, fadeBgmSpeed);
        //            }
        //            break;
        //    }
        //    bgmNameNow = bgmName;
        //}
    }
    public void Stop()
    {
        SoundManager.Instance.StopFadeBGM(bgmNameNow, fadeBgmSpeed);
        //Debug.Log("abc");
    }
    public void StopFadeNone()
    {
        SoundManager.Instance.StopBGM(bgmNameNow);
    }
}
