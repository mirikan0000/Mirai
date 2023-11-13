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
    private float initialDistance; // 初期距離

    private bool first = true;

    void Start()
    {
        // 親の親オブジェクトからPlayerManager_クラスを取得
        PlayerManager_ playerManager = GetComponentInParent<PlayerManager_>();

        if (playerManager != null)
        {
            // PlayerManagerからカメラを取得
            mainCamera = playerManager.GetMainCamera();
        }
        else
        {
            Debug.LogError("PlayerManager not found in parent objects.");
        }

        // 初期距離
        initialDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
    }

    void Update()
    {
        if (mainCamera == null)
        {
            return; // カメラが取得できない場合は処理を中断
        }

        // カメラで回転
        if (isRotation)
        {
            transform.rotation = mainCamera.transform.rotation;
        }

        // カメラの正面を向く
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);

        // カメラで拡大縮小
        if (isZoom)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            var scale = distance / initialDistance * size;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
