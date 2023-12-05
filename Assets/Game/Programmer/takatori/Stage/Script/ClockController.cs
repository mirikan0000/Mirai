using UnityEngine;

public class ClockController : MonoBehaviour
{
    public bool sec;   // 秒針の有無
    public bool secTick;   // 秒針を秒ごとに動かすか

    public GameObject second;

   [SerializeField] private float currentSeconds; // 現在の経過秒数
    private float degreesPerSecond = 360f / (5 * 60); // 毎秒何度進むか

    void Update()
    {
        if (secTick)
        {
            // 毎フレーム秒針を進める
            currentSeconds += Time.deltaTime;
            float rotationAngle = currentSeconds * degreesPerSecond;
            second.transform.rotation = Quaternion.Euler(0, 0, -rotationAngle);
        }
        // スカイボックスを更新
        SkyboxManager.Instance.UpdateSkybox(currentSeconds);
    }

    // 秒針の角度を返す静的メソッド
    public static float GetSecondHandAngle(float seconds)
    {
        // 0秒から始まって五分で一周する時計なので、360度が5分（300秒）で一周
        float degreesPerSecond = 360f / (5 * 60);
        return seconds * degreesPerSecond;
    }

    // 現在の秒数を返す静的メソッド
    public static float GetSeconds(float time)
    {
        return time % (5 * 60); // 5分ごとにリセット
    }
}
