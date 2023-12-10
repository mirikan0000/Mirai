using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : Weapon
{
    //弾丸の移動速度
    public float move_Speed = 15.0f;
    //移動方向
    private Vector3 direction;
    //自壊までの時間
    public float DestroyTime;
    public int damage = 70; // 弾のダメージ
    private void Start()
    {
        transform.Rotate(90f, 0f, 0f, Space.Self);
        //3秒後で自分を破壊する
        Destroy(this.gameObject, DestroyTime);
        direction = transform.TransformDirection(Vector3.up);

    }

    // Update is called once per frame
    private void Update()
    {
        // 回転を加える（例えば、Y軸を中心に回転）
        float rotationSpeed = 500.0f;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //弾丸発射の移動
        //壁とあったら移動方向へ移動する
        transform.position += direction * move_Speed * Time.deltaTime;
    }
}
