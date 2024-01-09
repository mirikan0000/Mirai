using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    [SerializeField]
    [Header("Managerオブジェクト取得用")]
    private GameObject managerObj;
    private Tittle_Manager managerScript;

    [Header("プレイヤー侵入状態管理用")]
    public bool playerEnterFlag;
    public float enterTimer;
    private float enterTimerLimit = 5.0f;

    [Header("起動管理")]
    public bool luFlag;

    void Start()
    {
        //Managerオブジェクトのスクリプト取得
        managerObj = GameObject.Find("Manager");
        managerScript = managerObj.GetComponent<Tittle_Manager>();

        //フラグ初期化処理
        FlagInitialize();

        //各種数値初期化
        enterTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    //フラグ初期化処理
    private void FlagInitialize()
    {
        luFlag = true;
    }

    //プレイヤーの侵入状態でフラグ管理
    private void OnTriggerStay(Collider other)
    {
        if (luFlag == true)
        {
            if (other.gameObject.tag == "Player1")
            {
                managerScript.playerMoveStopFlag = true;

                enterTimer += Time.deltaTime;

                if (enterTimer > enterTimerLimit)
                {
                    managerScript.launchEffectSpawnFlag = true;
                    luFlag = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            managerScript.playerEnterFlag = true;
            managerScript.enterAreaEffectSpawnFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            managerScript.playerEnterFlag = false;

            enterTimer = 0.0f;
        }
    }
}
