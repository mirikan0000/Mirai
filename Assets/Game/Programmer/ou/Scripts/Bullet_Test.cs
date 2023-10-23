using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : MonoBehaviour
{
    bool isUpdateData = false;
    //発射距離を調整するため
    float rangeOffset = 0.0f;
    //弾丸の移動速度
    public float move_Speed = 15.0f;
    //弾丸発射時の回転角度
    float gun_rotAngle = 0.0f;
    //弾丸発射時の初期速度
    float move_SpeedZ = 0.0f;
    float move_SpeedY = 0.0f;
    //弾丸の剛体
    Rigidbody rb;
    //時間
    float timer = 0.0f;

    //前の角度
    float lastAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isUpdateData = true;

        //剛体を取得する
        rb = GetComponent<Rigidbody>();

        //発射距離を取得する(弾丸予測線を計算する時に保存する)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //弾丸速度を保存する
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3秒後で自分を破壊する
        Destroy(this.gameObject, 3f);
    }

    void UpdateData()
    {
        //弾丸発射時の回転角度を計算する
        gun_rotAngle = 360.0f - this.transform.localEulerAngles.x;
        //縦方向の初期速度
        move_SpeedY = gun_rotAngle == 360.0f ? 0.0f : Mathf.Sin(gun_rotAngle * Mathf.Deg2Rad) * move_Speed;
        //横方向の初期速度
        move_SpeedZ = gun_rotAngle == 90.0f ? 0.0f : Mathf.Cos(gun_rotAngle * Mathf.Deg2Rad) * move_Speed;

        lastAngle = gun_rotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpdateData)
        {
            UpdateData();
            isUpdateData = false;
        }

        //発射距離を合わせる為に自分を力をあげる
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }
        //弾丸発射の移動
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));

        //時間を計算する
        timer += Time.deltaTime;

        //縦方向の速度
        float speedY = move_SpeedY + timer * (Physics2D.gravity.y + rangeOffset);

        float rot_ByGravity = Mathf.Atan2(speedY, move_SpeedZ) * Mathf.Rad2Deg;

        float angle = lastAngle - rot_ByGravity;

        //Debug
        Debug.Log("Time.deltaTime : " + Time.deltaTime);
        Debug.Log("gun_rotAngle : " + gun_rotAngle);
        Debug.Log("lastAngle : " + lastAngle);
        Debug.Log("rot_ByGravity : " + rot_ByGravity);
        Debug.Log("Angle : " + angle);

        lastAngle = rot_ByGravity;


        //回転
        transform.Rotate(new Vector3(angle, 0, 0));
    }
}
