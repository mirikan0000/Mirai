using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine_LineRender : MonoBehaviour
{
    //�`�拗��(�\�����̒���)
    public float maxDistance = 50;

    //���ˉ�
    public int maxReflectTimes = 5;

    //Component
    private LineRenderer lineRender;

    //�`��|�C���g���L�^����
    private List<Vector3> renderPoints;

    //Instantiate���@
    //private List<GameObject> line_Points;
    //public GameObject PredictionLine;

    void Start()
    {
    }

    private void Awake()
    {
        //LineRender�R���|�[�l���g���擾����
        lineRender = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //���X�g������������
        renderPoints = new List<Vector3>();
        //���X�g�ɊJ�n�ʒu���L�^����
        renderPoints.Add(transform.position);

        //���ˊp�x���擾����
        float bullet_angle = PlayerPrefs.GetFloat("Bullet_Angle");
        //�O�������擾����
        Vector3 forward = transform.forward;
        //���ˊp�x�ɂ��O�������v�Z����
        bullet_angle *= Mathf.Deg2Rad;
        forward.y = Mathf.Sin(bullet_angle);

        //���X�g�ɔ��˃|�C���g���L�^����
        renderPoints.AddRange(GetRenderPoints(transform.position, forward,
            maxDistance, maxReflectTimes));

        //Instantiate���@
        //for (int i = 0; i < _renderPoints.Count; i++) 
        //{
        //    GameObject gb = Instantiate(PredictionLine, _renderPoints[i], Quaternion.identity);
        //    line_Points.Add(gb);
        //}

        //LineRender���@
        //LineRender�f�[�^��ݒ肷��
        lineRender.positionCount = renderPoints.Count;
        lineRender.SetPositions(renderPoints.ToArray());
    }

    //���˃|�C���g���v�Z����
    private List<Vector3> GetRenderPoints(Vector3 start, Vector3 dir, float dis, int times)
    {
        var hitPosList = new List<Vector3>();

        //���˃|�C���g���v�Z����
        while (dis > 0 && times > 0)
        {
            RaycastHit hit;
            if (!Physics.Raycast(start, dir, out hit, dis))
            {
                break;
            }

            hitPosList.Add(hit.point);
            var reflectDir = Vector3.Reflect(dir, hit.normal);
            dis -= (hit.point - start).magnitude;
            times--;
            start = hit.point;
            dir = reflectDir;
        }

        //�Ō�̃|�C���g���v�Z����
        if (hitPosList.Count <= 1)
        {
            Vector3 point;
            point = start + dir * 10;

            hitPosList.Add(point);
        }   

        return hitPosList;
    }

    //Instantiate���@
    //~PredictionLine_LineRender()
    //{
    //    for (int i = 0; i < line_Points.Count; i++)
    //    {
    //        Destroy(line_Points[i]);
    //    }
    //}
}
