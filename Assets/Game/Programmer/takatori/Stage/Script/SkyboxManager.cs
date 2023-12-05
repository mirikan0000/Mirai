using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : SingletonMonoBehaviour<SkyboxManager>
{
    public Material morningSkybox;
    public Material nightSkybox;

    private const float secondsInDay = 24 * 60 * 60; // 1日の秒数

    public void UpdateSkybox(float seconds)
    {
        // ここで時間に応じてスカイボックスを切り替えるロジックを実装
        float t = seconds / secondsInDay; // 0から1の範囲に正規化
        RenderSettings.skybox = (t < 0.5f) ? morningSkybox : nightSkybox;
    }
}
