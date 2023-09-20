//=============================================================================
//
// SoundManagerデバッグ用処理(SoundManager動作確認用)

//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDebug : MonoBehaviour
{
    public bool isSeDecision = false;
    public bool isSeCancel = false;
    public bool isBGM1 = false;
    public bool isBGM2 = false;
    public bool isStopBGM1 = false;
    public bool isStopBGM2 = false;
    public bool isPauseBGM1 = false;
    public bool isPauseBGM2 = false;
    public float fadeSpeed;
    public bool isFadeBGM1 = false;
    public bool isFadeBGM2 = false;
    public bool isFadeStopBGM1 = false;
    public bool isFadeStopBGM2 = false;
    public bool isFadePauseBGM1 = false;
    public bool isFadePauseBGM2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSeDecision)
        {
            SoundManager.Instance.PlaySe("魔王魂 効果音 システム49");
            isSeDecision = false;
        }
        if (isSeCancel)
        {
            SoundManager.Instance.PlaySe("魔王魂 効果音 システム48");
            isSeCancel = false;
        }
        if (isBGM1)
        {
            SoundManager.Instance.PlayBGM("魔王魂  ファンタジー01");
            isBGM1 = false;
        }
        if (isBGM2)
        {
            SoundManager.Instance.PlayBGM("魔王魂  ファンタジー02");
            isBGM2 = false;
        }
        if (isStopBGM1)
        {
            SoundManager.Instance.StopBGM("魔王魂  ファンタジー01");
            isStopBGM1 = false;
        }
        if (isStopBGM2)
        {
            SoundManager.Instance.StopBGM("魔王魂  ファンタジー02");
            isStopBGM2 = false;
        }
        if (isPauseBGM1)
        {
            SoundManager.Instance.PauseBGM("魔王魂  ファンタジー01");
            isPauseBGM1 = false;
        }
        if (isPauseBGM2)
        {
            SoundManager.Instance.PauseBGM("魔王魂  ファンタジー02");
            isPauseBGM2 = false;
        }
        if (isFadeBGM1)
        {
            SoundManager.Instance.PlayFadeBGM("魔王魂  ファンタジー01", fadeSpeed);
            isFadeBGM1 = false;
        }
        if (isFadeBGM2)
        {
            SoundManager.Instance.PlayFadeBGM("魔王魂  ファンタジー02", fadeSpeed);
            isFadeBGM2 = false;
        }
        if (isFadeStopBGM1)
        {
            SoundManager.Instance.StopFadeBGM("魔王魂  ファンタジー01", fadeSpeed);
            isFadeStopBGM1 = false;
        }
        if (isFadeStopBGM2)
        {
            SoundManager.Instance.StopFadeBGM("魔王魂  ファンタジー02", fadeSpeed);
            isFadeStopBGM2 = false;
        }
        if (isFadePauseBGM1)
        {
            SoundManager.Instance.PauseFadeBGM("魔王魂  ファンタジー01", fadeSpeed);
            isFadePauseBGM1 = false;
        }
        if (isFadePauseBGM2)
        {
            SoundManager.Instance.PauseFadeBGM("魔王魂  ファンタジー02", fadeSpeed);
            isFadePauseBGM2 = false;
        }
    }
}
