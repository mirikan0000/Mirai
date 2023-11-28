using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine_LineRender : MonoBehaviour
{
    public float maxDistance = 50; // 弾丸の最大飛距離
    public int maxReflectTimes = 5; // 反射回数の最大値

    private LineRenderer lineRender; // LineRenderer コンポーネント
    private List<Vector3> renderPoints; // 弾丸予測軌道の点のリスト
    public Weapon BulletAngle_Owner;
    public float offsetY;
    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>(); // LineRenderer コンポーネントを取得
    }
    private void Start()
    {
        BulletAngle_Owner = GetComponentInParent<Weapon>();
    }
    void Update()
    {
        renderPoints = new List<Vector3>();
        renderPoints.Add(transform.position + new Vector3(0, offsetY, 0)); //プレイヤーの座標をコピーした座標
        float bullet_angle = BulletAngle_Owner.GetRotAngle(); // 弾丸の射角を取得
        bullet_angle *= Mathf.Deg2Rad; // 射角をラジアンに変換
        Vector3 initialVelocity = transform.forward;
        initialVelocity.y　=　Mathf.Sin(bullet_angle);
        renderPoints.AddRange(GetRenderPoints(transform.position + new Vector3(0, offsetY, 0), 
            initialVelocity,maxDistance, maxReflectTimes)); // 弾丸予測軌道の点を計算しリストに追加

        lineRender.positionCount = renderPoints.Count; // LineRenderer の点の数を設定
        lineRender.SetPositions(renderPoints.ToArray()); // LineRenderer に予測軌道の点を設定
    }

    // 以下の GetRenderPoints メソッドは前回の修正通りで使ってください。

    private List<Vector3> GetRenderPoints(Vector3 start, Vector3 dir, float dis, int times)
    {
        var hitPosList = new List<Vector3>(); // 衝突点のリスト
        while (dis > 0 && times > 0)
        {
            RaycastHit hit;
            if (!Physics.Raycast(start, dir, out hit, dis))
            {
                break; // 衝突しなかった場合、ループ終了
            }

            hitPosList.Add(hit.point); // 衝突点をリストに追加
            var reflectDir = Vector3.Reflect(dir, hit.normal); // 反射ベクトルを計算
            dis -= hit.distance; // 残りの距離を減少
            times--;
            start = hit.point;
            dir = reflectDir;
        }

        if (hitPosList.Count <= 1)
        {
            Vector3 point;
            point = start + dir * dis; // 仮の点を計算しリストに追加
            hitPosList.Add(point);
        }
        return hitPosList; // 計算された衝突点リストを返す
    }
}
