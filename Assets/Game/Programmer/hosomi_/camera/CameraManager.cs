using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public int cameraShakerIndex; // �C���f�b�N�X���C���X�y�N�^�[����ݒ�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraShaker.GetInstance(cameraShakerIndex)?.ShakeCamera(2, 0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CameraShaker.GetInstance(cameraShakerIndex)?.ShakeCamera(2, 0.1f);
        Debug.Log("chu");
    }
}
