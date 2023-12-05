using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    private const float secondsInRound = 300f; // 1������̂ɂ�����b�� (5�� = 300�b)
    [SerializeField] private ClockController clock;
    void Start()
    {
        // ���z�̏����̉�]��ݒ�
        float initialRotation = (Time.time % secondsInRound) / secondsInRound * 360f;
        transform.eulerAngles = new Vector3(initialRotation, 0, 0);
    }

    void Update()
    {
        // ClockController����b�j�̊p�x�ƕb�����擾
        float secondHandAngle = ClockController.GetSecondHandAngle(Time.time);
        float seconds = ClockController.GetSeconds(Time.time);

        // ���z�̈ړ�
        MoveSun(secondHandAngle, seconds);
    }

    void MoveSun(float secondHandAngle, float seconds)
    {
        // �Q�[�������ԂɑΉ����鑾�z�̉�]���x���v�Z
        float sunRotationSpeed = 360f / secondsInRound;

        // �b�j�̎��񐔂��v�Z
        float rotations = seconds / secondsInRound;

        // ���z�̎���Ǝ��v�̉�]�����킹�Ĉړ�
        transform.rotation = Quaternion.Euler(secondHandAngle, 0, 0);
        transform.RotateAround(Vector3.zero, Vector3.up, 6f * rotations * 360f * Time.deltaTime); // 6f�͎��v�̕b�j�̎��񑬓x
    }
}
