using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    private const float secondsInRound = 300f; // 1ü‚·‚é‚Ì‚É‚©‚©‚é•b” (5•ª = 300•b)
    [SerializeField] private ClockController clock;
    void Start()
    {
        // ‘¾—z‚Ì‰Šú‚Ì‰ñ“]‚ğİ’è
        float initialRotation = (Time.time % secondsInRound) / secondsInRound * 360f;
        transform.eulerAngles = new Vector3(initialRotation, 0, 0);
    }

    void Update()
    {
        // ClockController‚©‚ç•bj‚ÌŠp“x‚Æ•b”‚ğæ“¾
        float secondHandAngle = ClockController.GetSecondHandAngle(Time.time);
        float seconds = ClockController.GetSeconds(Time.time);

        // ‘¾—z‚ÌˆÚ“®
        MoveSun(secondHandAngle, seconds);
    }

    void MoveSun(float secondHandAngle, float seconds)
    {
        // ƒQ[ƒ€“àŠÔ‚É‘Î‰‚·‚é‘¾—z‚Ì‰ñ“]‘¬“x‚ğŒvZ
        float sunRotationSpeed = 360f / secondsInRound;

        // •bj‚Ìü‰ñ”‚ğŒvZ
        float rotations = seconds / secondsInRound;

        // ‘¾—z‚Ìü‰ñ‚ÆŒv‚Ì‰ñ“]‚ğ‡‚í‚¹‚ÄˆÚ“®
        transform.rotation = Quaternion.Euler(secondHandAngle, 0, 0);
        transform.RotateAround(Vector3.zero, Vector3.up, 6f * rotations * 360f * Time.deltaTime); // 6f‚ÍŒv‚Ì•bj‚Ìü‰ñ‘¬“x
    }
}
