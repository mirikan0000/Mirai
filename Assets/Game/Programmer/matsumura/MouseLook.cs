using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f; // マウス感度
    public Transform player; // プレイヤーキャラクターのTransform

    private float rotationX = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
    }

    private void Update()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // プレイヤーを水平方向に回転
        player.Rotate(Vector3.up * mouseX);

        // カメラを垂直方向に回転（上限・下限を設定）
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
