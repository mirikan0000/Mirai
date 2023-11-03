using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform target1; // �v���C���[1�̈ʒu
    public Transform target2; // �v���C���[2�̈ʒu
    public Camera miniMapCamera; // �~�j�}�b�v�p�̃J����

    public Vector3 offset = new Vector3(0, 20, 0); // �J�����̍����ƈʒu����
    public float minZoomLevel = 80.0f; // �ŏ����
    public float maxZoomLevel = 475.0f; // �ő����
    public float zoomSpeed = 5.0f; // �Y�[���̑��x
    private float zoomLevel; // �Y�[�����x��
    public float distance;
    void Start()
    {
        zoomLevel = minZoomLevel; // �����Y�[�����x�����ő�l�ɐݒ�
    }

    void LateUpdate()
    {
        // �v���C���[�Ԃ̋������v�Z
        distance = Vector3.Distance(target1.position, target2.position);

        // �J�����̈ʒu��ݒ�
        Vector3 targetPosition = (target1.position + target2.position) / 2;
        miniMapCamera.transform.position = new Vector3(targetPosition.x, offset.y, targetPosition.z);

        // �J�����̊p�x�𒲐�
        miniMapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        // �����ɉ����ăY�[�����x���𒲐�
        float newZoomLevel = Mathf.Clamp(zoomLevel + distance,minZoomLevel,maxZoomLevel);
        miniMapCamera.orthographicSize = newZoomLevel;
    }
}