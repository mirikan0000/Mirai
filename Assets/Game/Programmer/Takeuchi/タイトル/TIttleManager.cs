using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TIttleManager : MonoBehaviour
{
    [SerializeField]
    [Header("出現させるオブジェクト")]
    public GameObject playerObj;      //操作するためのプレイヤーオブジェクト

    [Header("出現前のエフェクト")]
    public ParticleSystem spawnEffect;  //プレイヤー生成前に再生

    [Header("ボタンを押したときのエフェクト")]
    public ParticleSystem buttonEffect;  //ボタンが押されたときに再生

    [Header("ロボット起動時のエフェクト")]
    public ParticleSystem launchEffect;  //ボタンが押されたときに再生(ボタンエフェクトよりも遅らせて再生）

    [Header("フェードアウト用")]
    public GameObject mask;  //

    private Vector3 playerSpawnPos;             //プレイヤーオブジェクト出現位置
    private Vector3 spawnEffectSpawnPos;        //プレイヤー出現時のエフェクト出現位置
    private Vector3 buttonEffectSpawnPos;       //ボタンが押されたときのエフェクト出現位置
    private Vector3 launchEffectSpawnPos_right; //ロボット起動時のエフェクト出現位置右
    private Vector3 launchEffectSpawnPos_left;  //ロボット起動時のエフェクト出現位置左

    [Header("フラグ管理")]
    public bool playerSpawnFlag;
    public bool spawnEffectFlag;
    public bool buttonEffectFlag;
    public bool launchEffectFlag;
    public bool sceneChangeFlag;

    void Start()
    {
        //フラグ初期化処理
        FlagInitialize();
    }


    void Update()
    {
        if (playerSpawnFlag == true)
        {
            Instantiate(playerObj, playerSpawnPos, Quaternion.identity);
            playerSpawnFlag = false;
        }

        if (spawnEffectFlag == true)
        {
            Instantiate(spawnEffect, spawnEffectSpawnPos, Quaternion.identity);
            spawnEffectFlag = false;
        }

        if (buttonEffectFlag == true)
        {
            Instantiate(buttonEffect, buttonEffectSpawnPos, Quaternion.identity);
            buttonEffectFlag = false;
        }

        if (launchEffectFlag == true)
        {
            Instantiate(launchEffect, launchEffectSpawnPos_left, Quaternion.identity);
            Instantiate(launchEffect, launchEffectSpawnPos_right, Quaternion.identity);
            launchEffectFlag = false;
        }

        if (sceneChangeFlag == true)
        {
            //シーン遷移
            SceneFadeOutChange();
        }
    }

    //フラグ初期化処理
    private void FlagInitialize()
    {
        playerSpawnFlag = false;
        spawnEffectFlag = false;
        buttonEffectFlag = false;
        launchEffectFlag = false;
        sceneChangeFlag = false;
    }

    //フェードアウトを伴うシーン遷移
    private void SceneFadeOutChange()
    {
        //raw imageの取得
    }
}
