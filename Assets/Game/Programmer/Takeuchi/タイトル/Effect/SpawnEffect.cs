using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField]
    [Header("親オブジェクト取得用")]
    private GameObject parentObj;
    private Tittle_Manager parentScript;

    [Header("パーティクルシステム用")]
    private ParticleSystem spawnEffect;
    private float playTimer;     //再生時間計測
    public float maxPlayTimer;   //最大再生時間

    void Start()
    {
        //親オブジェクト取得
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<Tittle_Manager>();

        //パーティクルシステム取得
        spawnEffect = GetComponent<ParticleSystem>();

        //再生時間初期化
        playTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playTimer += Time.deltaTime;

        if (playTimer > maxPlayTimer)
        {
            spawnEffect.Stop();
        }
    }

    //エフェクト再生終了検知
    private void OnParticleSystemStopped()
    {
        //シーン遷移用フラグ管理
        parentScript.spawnPlayerFlag = true;

        Destroy(this.gameObject);
    }
}
