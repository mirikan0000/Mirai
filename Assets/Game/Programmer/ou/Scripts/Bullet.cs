using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //発射距離を調整するため
    float rangeOffset = 0.0f;
    //弾丸の移動速度
    public float move_Speed = 15.0f;
    //弾丸の剛体
    Rigidbody rb;
    //弾丸の画像
    public GameObject Image_Bullet;


    //当たるかどうか
    private bool IsCollision;
    //移動方向
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //剛体を取得する
        rb = GetComponent<Rigidbody>();

        //発射距離を取得する(弾丸予測線を計算する時に保存する)
        if(PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //弾丸速度を保存する
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3秒後で自分を破壊する
        Destroy(this.gameObject, 3f);

        //弾丸の画像を作成
     //   GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
     //   image.transform.parent = this.transform;
        //弾丸の画像を作成する
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        //親子関係を設定する
        image.transform.parent = this.transform;

        //移動方向を初期化する
        IsCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //発射距離を合わせる為に自分を力をあげる
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }

        //弾丸発射の移動
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));
        //壁とあったら移動方向へ移動する
        if (IsCollision)
        {
            //transform.Translate(new Vector3(direction));

            transform.position += direction * move_Speed * Time.deltaTime;
            return;
        }
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //壁とあったら移動方向を設定する
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Vector3 dir = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            direction = dir;

            IsCollision = true;
        }
    }
}
