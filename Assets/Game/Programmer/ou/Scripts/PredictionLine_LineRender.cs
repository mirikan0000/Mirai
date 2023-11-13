using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine_LineRender : MonoBehaviour
{
    public float maxDistance = 50; // �e�ۂ̍ő�򋗗�
    public int maxReflectTimes = 5; // ���ˉ񐔂̍ő�l

    private LineRenderer lineRender; // LineRenderer �R���|�[�l���g
    private List<Vector3> renderPoints; // �e�ۗ\���O���̓_�̃��X�g
    public PlayerManager_ BulletAngle_Owner;
    public float offsetY;
    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>(); // LineRenderer �R���|�[�l���g���擾
       
    }
    private void Start()
    {
        BulletAngle_Owner = GetComponentInParent<PlayerManager_>();
        if (BulletAngle_Owner == null)
        {
            Debug.LogError("NotBulletAngle_Owner!!!!!!!!!!!!");
        }
    }
    void Update()
    {
        renderPoints = new List<Vector3>();
        renderPoints.Add(transform.position + new Vector3(0, offsetY, 0)); // �e�ۂ̔��ˈʒu�����X�g�ɒǉ�

        float bullet_angle = BulletAngle_Owner.gun_rotAngle; // �e�ۂ̎ˊp���擾

        bullet_angle *= Mathf.Deg2Rad; // �ˊp�����W�A���ɕϊ�
        Vector3 initialVelocity = new Vector3(transform.forward.x, Mathf.Sin(bullet_angle), transform.forward.z);
        initialVelocity = initialVelocity.normalized * maxDistance; // �����x�x�N�g���𒲐�

        renderPoints.AddRange(GetRenderPoints(transform.position, initialVelocity,
            maxDistance, maxReflectTimes)); // �e�ۗ\���O���̓_���v�Z�����X�g�ɒǉ�

        lineRender.positionCount = renderPoints.Count; // LineRenderer �̓_�̐���ݒ�
        lineRender.SetPositions(renderPoints.ToArray()); // LineRenderer �ɗ\���O���̓_��ݒ�
    }

    // �ȉ��� GetRenderPoints ���\�b�h�͑O��̏C���ʂ�Ŏg���Ă��������B

    private List<Vector3> GetRenderPoints(Vector3 start, Vector3 dir, float dis, int times)
    {
        var hitPosList = new List<Vector3>(); // �Փ˓_�̃��X�g

        while (dis > 0 && times > 0)
        {
            RaycastHit hit;
            if (!Physics.Raycast(start, dir, out hit, dis))
            {
                break; // �Փ˂��Ȃ������ꍇ�A���[�v�I��
            }

            hitPosList.Add(hit.point); // �Փ˓_�����X�g�ɒǉ�
            var reflectDir = Vector3.Reflect(dir, hit.normal); // ���˃x�N�g�����v�Z
            dis -= hit.distance; // �c��̋���������
            times--;
            start = hit.point;
            dir = reflectDir;
        }

        if (hitPosList.Count <= 1)
        {
            Vector3 point;
            point = start + dir * 10; // ���̓_���v�Z�����X�g�ɒǉ�
            hitPosList.Add(point);
        }

        return hitPosList; // �v�Z���ꂽ�Փ˓_���X�g��Ԃ�
    }
}
