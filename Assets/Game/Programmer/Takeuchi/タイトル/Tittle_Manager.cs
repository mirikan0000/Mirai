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
    public float launchedTimer;   //起動後の経過時間

    [Header("シーン遷移用")]
    [Header("フェードアウト用")]
    public Canvas canvas;           //フェードアウト用キャンバス
    public SceneFader canvasScript; //キャンバスについているスクリプト
    public bool sceneChangeFlag;
    public bool sceneFadeFlag;
    public bool fadeEnd;
    public bool operationFlag;
    [Header("操作説明関係")]
    public GameObject operationObj;  //操作説明用画像オブジェクト
    public Image operationImage;     //操作説明用画像オブジェクトのImageコンポーネント
    public float operationTimer;     //操作説明用画像表示までの経過時間
    [Header("ゲーム終了確認シーン名")]
    public string CautionSceneName;
    public bool isLoad;
    [Header("次シーン遷移時_削除シーンリスト")]
    public string unLoadSceneName;
    void Start()
    {
        //フラグ初期化処理
        FlagInitialize();

        //経過時間初期化
        enterAreaEffectTimer = 0.0f;
        launchedTimer = 0.0f;
        operationTimer = 0.0f;

        //フェードアウト用スクリプト取得
        canvasScript = canvas.GetComponent<SceneFader>();

        //操作説明用画像のコンポーネント取得
        operationObj = GameObject.Find("Operarion");
        operationImage = operationObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクト生成処理
        SpawnObject();

        //シーンフェイド処理
        SceneFade();

        //操作説明用画像非表示とシーン遷移
        HiddenOperationImageAndSceneChange();

     
    
        
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
        sceneFadeFlag = false;
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

                launchFlag = true;
            }
        }

        //起動からの経過時間計測
        if (launchFlag == true)
        {
            launchedTimer += Time.deltaTime;

            if (launchedTimer > 2.0f)
            {
                sceneFadeFlag = true;
                launchedTimer = 0.0f;
                launchFlag = false;
            }
        }
    }

    //シーンフェイド処理
    private void SceneFade()
    {
        if (sceneFadeFlag == true)
        {
            //フェードアウト
            canvasScript.CallCoroutine();

            if (fadeEnd == true)
            {
                operationTimer += Time.deltaTime;

                if (operationTimer > 1.0f)
                {
                    operationFlag = true;
                    sceneFadeFlag = false;
                    fadeEnd = false;
                    operationTimer = 0.0f;
                }

                //操作説明画像表示
                ShowOperatingInstructions();
            }
        }
    }

    //操作説明表示
    private void ShowOperatingInstructions()
    {
        if (operationFlag == true)
        {       // シーン削除
           SceneManager.Instance.DestroyScene(unLoadSceneName);
            //操作説明スライドを画面に表示
            operationImage.enabled = true;
            SceneManager.Instance.LoadScene(CautionSceneName);
            // シーン変更
            SceneManager.Instance.ChangeScene();
            isLoad = true;
        }

        
    }

    //操作説明用画像非表示とシーン遷移
    private void HiddenOperationImageAndSceneChange()
    {
        if (operationFlag == true)
        {
            //キー入力待ち
            if (Input.GetKeyDown(KeyCode.V))
            {
                //キー入力後画像を非表示にする
                operationImage.enabled = false;
                operationFlag = false;
                sceneChangeFlag = true;
                operationTimer = 0.0f;
            }
        }

        if (sceneChangeFlag == true)
        {
            operationTimer += Time.deltaTime;

            if (operationTimer > 1.0f)
            {
                Debug.Log("コーションシーンネーム");
                        // シーン読み込み
                
            
            }
        }
    }
}
