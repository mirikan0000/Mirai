//=============================================================================
//
// FPSマネージャー処理(FPS固定,計測処理)

//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSManager : SingletonMonoBehaviour<FPSManager>
{
    [Header("FPS")]
    public int fps = 60;
    [Header("何秒毎にFPSを計測するか")]
    [SerializeField, Range(0.1f, 1.0f)]
    float everyCalcurationTime = 0.1f;
    [Header("FPS表示用テキスト")]
    public Text printFpsText;
    float printFps;
    int frameCount;
    float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = fps;

        frameCount = 0;
        prevTime = 0.0f;
        printFps = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= everyCalcurationTime)
        {
            printFps = frameCount / time;
            if (printFpsText != null)
            {
                printFpsText.text = "FPS:" + printFps.ToString();
            }

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
