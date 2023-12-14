using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tittle_Manager : MonoBehaviour
{
    [SerializeField]
    [Header("プレイヤー生成時のエフェクト関係")]
    public ParticleSystem spawnEffect;
    public Transform spawnEffectPos;
    public bool spawnEffectSpawnFlag;

    [Header("プレイヤーのオブジェクト関係")]
    public GameObject playerObj;
    public Transform playerSpawnPos;
    public bool spawnPlayerFlag;

    [Header("起動エリアに入った時のエフェクト関係")]
    public ParticleSystem enterAreaEffect;
    public Transform enterAreaEffectPos;
    public bool enterAreaEffectSpawnFlag;
    public bool playerEnterFlag;
    public float enterAreaEffectTimer;     //プレイヤーが起動エリアに入っている時間計測用
    public float maxEnterAreaEffectTimer;  

    [Header("起動時のエフェクト関係")]
    public ParticleSystem launchEffect;
    public Transform launchEffectPosRight;
    public Transform launchEffectPosLeft;
    public bool launchEffectSpawnFlag;
    public bool launchFlag;

    [Header("シーン遷移用")]
    [Header("フェードアウト用")]
    public Canvas canvas;           //フェードアウト用キャンバス
    public SceneFader canvasScript; //キャンバスについているスクリプト
    public bool sceneChangeFlag;
    public bool fadeEnd;
    public bool operationFlag;
    [Header("操作説明関係")]
    public Image operationImage;  //操作説明用画像

    void Start()
    {
        //フラグ初期化処理
        FlagInitialize();

        //プレイヤーが起動エリアに入っている時間初期化
        enterAreaEffectTimer = 0.0f;

        //フェードアウト用スクリプト取得
        canvasScript = canvas.GetComponent<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクト生成処理
        SpawnObject();

        //シーン遷移処理
        SceneFadeAndChange();
    }

    //フラグ初期化処理
    private void FlagInitialize()
    {
        spawnEffectSpawnFlag = true;
        spawnPlayerFlag = false;
        enterAreaEffectSpawnFlag = false;
        playerEnterFlag = false;
        launchEffectSpawnFlag = false;
        launchFlag = false;
        sceneChangeFlag = false;
        fadeEnd = false;
        operationFlag = false;
    }

    //オブジェクト生成処理
    private void SpawnObject()
    {
        var parent = this.transform;

        //プレイヤー生成時のエフェクト生成
        if (spawnEffectSpawnFlag == true)
        {
            if (spawnEffect != null)
            {
                Instantiate(spawnEffect, spawnEffectPos.transform.position, Quaternion.Euler(-90, 0, 0), parent);
                spawnEffectSpawnFlag = false;
            }
        }

        //プレイヤー生成
        if (spawnPlayerFlag == true)
        {
            if (playerObj != null)
            {
                Instantiate(playerObj, playerSpawnPos.transform.position, Quaternion.identity, parent);
                spawnPlayerFlag = false;
            }
        }

        //起動エリアに入った時のエフェクト生成
        if (enterAreaEffectSpawnFlag == true)
        {
            if (enterAreaEffect != null)
            {
                Instantiate(enterAreaEffect, enterAreaEffectPos.transform.position, Quaternion.Euler(-90, 0, 0), parent);
                enterAreaEffectSpawnFlag = false;
            }

        }

        //起動時のエフェクト生成
        if(launchEffectSpawnFlag == true)
        {
            if (launchEffect != null)
            {
                Instantiate(launchEffect, launchEffectPosRight.transform.position, Quaternion.identity, parent);
                Instantiate(launchEffect, launchEffectPosLeft.transform.position, Quaternion.identity, parent);
                launchEffectSpawnFlag = false;
            }
        }
    }

    //シーン遷移処理
    private void SceneFadeAndChange()
    {
        if (sceneChangeFlag == true)
        {
            //フェードアウト
            canvasScript.CallCoroutine();

            if (fadeEnd == true)
            {
                //操作説明画像表示
                ShowOperatingInstructions();

                if (operationFlag == true)
                {
                    //シーン遷移
                    //SceneManager.LoadScene("GameScene");
                    Debug.Log("シーン遷移");

                    //シーン遷移繊維用フラグをFalse
                    sceneChangeFlag = false;
                }
            }
        }
    }

    //操作説明表示
    private void ShowOperatingInstructions()
    {
        //操作説明スライドを画面に表示

        //キー入力待ち

        //フェードアウト
        canvasScript.CallCoroutine();
    }
}
