using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // インスタンスを複数持つためのリスト
    private static List<CameraShaker> instances = new List<CameraShaker>();

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    private void Awake()
    {
        instances.Add(this); // リストに追加
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (cam!=null)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (cinemachineBasicMultiChannelPerlin != null)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
                shakeTimer = time;
            }
        }
        
           
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (cinemachineBasicMultiChannelPerlin != null)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
    }

    // インスタンスを返すメソッドを追加
    public static CameraShaker GetInstance(int index)
    {
        if (index >= 0 && index < instances.Count)
        {
            return instances[index];
        }
        return null;
    }
}
