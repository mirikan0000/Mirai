using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_ : MonoBehaviour
{
    //ゲームは更新しているか(例えば：開始画面、クリア画面などではない状態)
    bool is_start;

    //照準を合わせている(予測線)
    bool is_aiming;

    //発射、リロードは移動できないため
    bool is_moveable = true;

    //プレイヤーの移動速度
    float move_speed = 5.0f;

    //プレイヤーの回転速度
    float rot_angle = 0.1f;

    //発射角度の調整速度(回転)
    float gunBarrel_rotSpeed = 0.5f;

    //今の発射角度(砲塔アセットがないため、一旦記録する)
    float gun_rotAngle = 0.0f;

    //発射距離の調整変数(Public型)
    public float Bullet_RangeOffset = 0;

    //発射位置を調整する変数(プレイヤーの中ではなくて、前で発射するため)
    //弾丸とプレイヤーを被ったら(当たったら)、弾丸は変な方向になるため。
    public float bulletCreatePosOffset = 1.0f;

    //弾丸(Public型)
    public GameObject Buttet;
    //弾丸予測線(Public型)
    public GameObject PredictionLine;

    //弾丸予測線を構成するための描画数
    public int PredictionLineNumber = 66;

    //弾丸予測線の計算結果リスト(描画位置を保存する)
    List<GameObject> PredictionLine_List = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始(更新を許可する)
        is_start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_start)
        {
            //仮
            //プレイヤー移動
            if (is_moveable)
            {
                //Keyboard
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(0, 0, move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector3(0, 0, -move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(new Vector3(0, -rot_angle, 0));
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(new Vector3(0, rot_angle, 0));
                }
            }

            //弾丸予測線
            //Spaceキーを押し続けると弾丸予測線を描画する
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //予測線
                //二つ方法(重力、三角関数で模擬放物線)
                //重力
                //移動禁止
                is_moveable = false;
                //照準中(予測線を描画するため)
                is_aiming = true;
            }

            //弾丸発射
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //発射
                //二つ方法(重力、三角関数で模擬放物線)
                //重力
                //発射した後で移動を許可する
                is_moveable = true;
                //照準済み
                is_aiming = false;

                //弾丸予測線の計算結果リストをクリア
                for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                //弾丸生成
                GameObject buttle = Instantiate(Buttet, transform.position, transform.rotation);
                //弾丸の角度をプレイヤーと一致する
                buttle.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                //弾丸位置はプレイヤーの前にする
                buttle.transform.Translate(new Vector3(0, 0, bulletCreatePosOffset));
            }
            //照準中のため、弾丸予測線を描画する
            //発射角度も調整できる
            if (is_aiming)
            {
                //発射角度を調整する
                //仮設定 発射角度の範囲:0°~90°
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
                }

                //発射角度を調整したため、
                //弾丸予測線の計算結果リストをクリアして再計算する必要
                for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                //弾丸予測線の計算と描画する
                DrewPredictionLine();
            }
        }
    }

    //弾丸予測線の計算と描画
    void DrewPredictionLine()
    {
        //発射角度をラジアンにする
        float angle_y = gun_rotAngle * Mathf.Deg2Rad;
        //プレイヤー回転角度をラジアンにする
        float angle_xz = transform.eulerAngles.y * Mathf.Deg2Rad;

        //弾丸の発射速度をゲットする(Script「Bullet」で保存している)
        float Bullet_Speed = PlayerPrefs.GetFloat("Bullet_Speed");

        //弾丸予測線を計算する
        for (int i = 0; i < PredictionLineNumber; i++)
        {
            //時間間隔
            float t = i * 0.05f;

            //横平面の位置(XZ座標)を計算する
            float X = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Cos(angle_y);
            float x = X * Mathf.Sin(angle_xz) + transform.position.x;
            float z = X * Mathf.Cos(angle_xz) + transform.position.z;

            //重力と発射距離を合わせるため、計算する
            float Bullet_Gravity = Physics2D.gravity.y;
            //重力 < = 0 
            if ((Physics2D.gravity.y + Bullet_RangeOffset) <= 0)
            {
                //発射距離を合わせた"重力"を計算する
                Bullet_Gravity += Bullet_RangeOffset;
                //発射距離を保存する(Script「Bullet」で計算する時に使うため)
                PlayerPrefs.SetFloat("Bullet_RangeOffset", Bullet_RangeOffset);
            }

            //縦方向のＹ座標を計算する
            float y = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //弾丸予測線を描画する
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);
            //弾丸予測線の計算結果リストに保存する
            PredictionLine_List.Add(gb);
        }
    }
}