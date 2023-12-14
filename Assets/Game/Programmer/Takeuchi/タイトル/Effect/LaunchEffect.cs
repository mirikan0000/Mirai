using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEffect : MonoBehaviour
{
    [SerializeField]
    [Header("パーティクルシステム取得用")]
    private ParticleSystem launchEffect;

    void Start()
    {
        //パーティクルシステム取得
        launchEffect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //エフェクト再生終了検知
    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
