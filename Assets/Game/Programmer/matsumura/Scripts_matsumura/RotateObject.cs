using UnityEngine;

public class RotateObjectToTarget : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // 回転速度
    public Vector3 targetRotation = new Vector3(0, 0, 180); // 目標の回転角度

    private void Update()
    {
        // 目標の回転角度に向けてオブジェクトを回転させる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
    }
}
