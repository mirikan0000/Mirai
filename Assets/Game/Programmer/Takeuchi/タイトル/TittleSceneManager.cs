using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleSceneManager : MonoBehaviour
{
    [SerializeField]
    [Header("管理用フラグ")]
    public bool sceneChangeFlag;  //シーン遷移用フラグ
    public bool playerEnterFlag;  //プレイヤーが起動できる位置にいるかどうか

    void Start()
    {
        //フラグの初期化
        FlagInitialize();
    }

    void Update()
    {
        //プレイヤーがロボットを起動したらシーン遷移
        ChangeNextScene();
    }

    //フラグ初期化
    private void FlagInitialize()
    {
        sceneChangeFlag = false;
        playerEnterFlag = false;
    }

    //シーン遷移
    private void ChangeNextScene()
    {
        if (playerEnterFlag == true && playerEnterFlag == true)
        {
            //シーン遷移
            //SceneManager.LoadScene("遷移するシーン名");
        }
    }
}
