using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    private const float secondsInRound = 300f; // 1周するのにかかる秒数 (5分 = 300秒)
    [SerializeField] private ClockController clock;
    void Start()
    {
        // 太陽の初期の回転を設定
        float initialRotation = (Time.time % secondsInRound) / secondsInRound * 360f;
        transform.eulerAngles = new Vector3(initialRotation, 0, 0);
    }

    void Update()
    {
        // ClockControllerから秒針の角度と秒数を取得
        float secondHandAngle = ClockController.GetSecondHandAngle(Time.time);
        float seconds = ClockController.GetSeconds(Time.time);

        // 太陽の移動
        MoveSun(secondHandAngle, seconds);
    }

    void MoveSun(float secondHandAngle, float seconds)
    {
        // ゲーム内時間に対応する太陽の回転速度を計算
        float sunRotationSpeed = 360f / secondsInRound;

        // 秒針の周回数を計算
        float rotations = seconds / secondsInRound;

        // 太陽の周回と時計の回転を合わせて移動
        transform.rotation = Quaternion.Euler(secondHandAngle, 0, 0);
        transform.RotateAround(Vector3.zero, Vector3.up, 6f * rotations * 360f * Time.deltaTime); // 6fは時計の秒針の周回速度
    }
}
