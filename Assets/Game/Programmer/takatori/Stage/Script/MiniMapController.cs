using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform target1; // プレイヤー1の位置
    public Transform target2; // プレイヤー2の位置
    public Camera miniMapCamera; // ミニマップ用のカメラ

    public Vector3 offset = new Vector3(0, 20, 0); // カメラの高さと位置調整
    public float minZoomLevel = 80.0f; // 最小より
    public float maxZoomLevel = 475.0f; // 最大引き
    public float zoomSpeed = 5.0f; // ズームの速度
    private float zoomLevel; // ズームレベル
    public float distance;
    void Start()
    {
        zoomLevel = minZoomLevel; // 初期ズームレベルを最大値に設定
    }

    void LateUpdate()
    {
        // プレイヤー間の距離を計算
        distance = Vector3.Distance(target1.position, target2.position);

        // カメラの位置を設定
        Vector3 targetPosition = (target1.position + target2.position) / 2;
        miniMapCamera.transform.position = new Vector3(targetPosition.x, offset.y, targetPosition.z);

        // カメラの角度を調整
        miniMapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        // 距離に応じてズームレベルを調整
        float newZoomLevel = Mathf.Clamp(zoomLevel + distance,minZoomLevel,maxZoomLevel);
        miniMapCamera.orthographicSize = newZoomLevel;
    }
}