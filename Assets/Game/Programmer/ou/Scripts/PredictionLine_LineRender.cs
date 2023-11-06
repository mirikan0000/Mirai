using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine_LineRender : MonoBehaviour
{
    //描画距離(予測線の長さ)
    public float maxDistance = 50;

    //反射回数
    public int maxReflectTimes = 5;

    //Component
    private LineRenderer lineRender;

    //描画ポイントを記録する
    private List<Vector3> renderPoints;

    //Instantiate方法
    //private List<GameObject> line_Points;
    //public GameObject PredictionLine;

    void Start()
    {
    }

    private void Awake()
    {
        //LineRenderコンポーネントを取得する
        lineRender = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //リストを初期化する
        renderPoints = new List<Vector3>();
        //リストに開始位置を記録する
        renderPoints.Add(transform.position);

        //反射角度を取得する
        float bullet_angle = PlayerPrefs.GetFloat("Bullet_Angle");
        //前方向を取得する
        Vector3 forward = transform.forward;
        //反射角度により前方向を計算する
        bullet_angle *= Mathf.Deg2Rad;
        forward.y = Mathf.Sin(bullet_angle);

        //リストに反射ポイントを記録する
        renderPoints.AddRange(GetRenderPoints(transform.position, forward,
            maxDistance, maxReflectTimes));

        //Instantiate方法
        //for (int i = 0; i < _renderPoints.Count; i++) 
        //{
        //    GameObject gb = Instantiate(PredictionLine, _renderPoints[i], Quaternion.identity);
        //    line_Points.Add(gb);
        //}

        //LineRender方法
        //LineRenderデータを設定する
        lineRender.positionCount = renderPoints.Count;
        lineRender.SetPositions(renderPoints.ToArray());
    }

    //反射ポイントを計算する
    private List<Vector3> GetRenderPoints(Vector3 start, Vector3 dir, float dis, int times)
    {
        var hitPosList = new List<Vector3>();

        //反射ポイントを計算する
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

        //最後のポイントを計算する
        if (hitPosList.Count <= 1)
        {
            Vector3 point;
            point = start + dir * 10;

            hitPosList.Add(point);
        }   

        return hitPosList;
    }

    //Instantiate方法
    //~PredictionLine_LineRender()
    //{
    //    for (int i = 0; i < line_Points.Count; i++)
    //    {
    //        Destroy(line_Points[i]);
    //    }
    //}
}
