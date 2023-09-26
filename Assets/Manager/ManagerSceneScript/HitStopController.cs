using System.Collections;
using UnityEngine;

public class HitStopController : SingletonMonoBehaviour<HitStopController>
{
    [Tooltip("スローにする時間の長さ（秒）")]
    public float SlowTimer = 0.1f;

    [Tooltip("射撃した時にヒットした時の音")]
    public float HitTimer = 2f;

    /// <summary>
    /// ヒットストップ効果をアクティブ化し、指定された時間だけ実行させる。
    /// </summary>
    /// <param name="_duration">ヒットストップ効果の時間</param>
    /// <param name="_isPlayer">ボスにヒットストップを発生させるか</param>
    public void ActivateHitStop(float _duration, bool _isPlayer)
    {
        StartCoroutine(HitStopAction(_duration, _isPlayer));
    }

    private IEnumerator HitStopAction(float _duration, bool _isPlayer)
    {
        if (!PauseManager.Instance.isPause)
        {
            // 時間の停止
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(_duration);

            if (_isPlayer)
            {
                // スローモーションに切り替える
                Time.timeScale = HitTimer;
                yield return new WaitForSecondsRealtime(HitTimer);
            }

            // 時間の再開

            Time.timeScale = 1f;
        }
        Debug.Log("ヒットストップ");
    }
}