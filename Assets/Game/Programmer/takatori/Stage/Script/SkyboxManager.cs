using UnityEngine;

public class SkyboxManager : SingletonMonoBehaviour<SkyboxManager>
{
    public Material morningSkybox;
    public Material daySkybox;
    public Material nightSkybox;

    private const float secondsInDay = 300f; // 1日の秒数

    public void UpdateSkybox(float seconds)
    {
        // ここで時間に応じてスカイボックスを切り替えるロジックを実装
        float t = seconds / secondsInDay; // 0から1の範囲に正規化

        // 画像の遷移を滑らかにする
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        if (t < 0.25f)
        {
            // 朝から昼への遷移
            RenderSettings.skybox = morningSkybox;
        }
        else if (t < 0.75f)
        {
            // 昼から夜への遷移
            RenderSettings.skybox = daySkybox;
        }
        else
        {
            // 夜から朝への遷移
            RenderSettings.skybox = nightSkybox;
        }

        // 画像の遷移を滑らかにする
        RenderSettings.skybox.SetFloat("_Blend", smoothT);
    }
}
