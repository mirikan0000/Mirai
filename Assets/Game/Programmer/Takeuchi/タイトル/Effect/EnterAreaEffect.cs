using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAreaEffect : MonoBehaviour
{
    [SerializeField]
    [Header("パーティクルシステム取得用")]
    private ParticleSystem enterAreaEffect;

    [Header("親オブジェクト関係")]
    private GameObject parentObj;
    private Tittle_Manager parentScript;

    void Start()
    {
        //パーティクルシステム取得
        enterAreaEffect = GetComponent<ParticleSystem>();

        //親オブジェクトのスクリプト取得
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<Tittle_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが起動エリアから出たらエフェクト停止
        if (parentScript.playerEnterFlag == false)
        {
            enterAreaEffect.Stop();
        }
    }

    //エフェクト再生終了検知
    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
