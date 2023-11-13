using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("RotByCamera")]
    public bool isRotation = false;

    [Header("ScaByCamera")]
    public bool isZoom = false;

    [Header("Sca%")]
    public float size = 0.1F;

    private Camera mainCamera; // MainCamera
    private float initialDistance; // ��������

    private bool first = true;

    void Start()
    {
        // �e�̐e�I�u�W�F�N�g����PlayerManager_�N���X���擾
        PlayerManager_ playerManager = GetComponentInParent<PlayerManager_>();

        if (playerManager != null)
        {
            // PlayerManager����J�������擾
            mainCamera = playerManager.GetMainCamera();
        }
        else
        {
            Debug.LogError("PlayerManager not found in parent objects.");
        }

        // ��������
        initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
    }

    void Update()
    {
        if (mainCamera == null)
        {
            return; // �J�������擾�ł��Ȃ��ꍇ�͏����𒆒f
        }

        // �J�����ŉ�]
        if (isRotation)
        {
            transform.rotation = mainCamera.transform.rotation;
        }

        // �J�����̐��ʂ�����
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);

        // �J�����Ŋg��k��
        if (isZoom)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            var scale = distance / initialDistance * size;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
