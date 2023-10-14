using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //発射距離を調整するため
    public float rangeOffset = 0.0f;
    //弾丸の移動速度
    public float move_Speed = 15.0f;
    public float BulletLifeTime;
    //弾丸の剛体
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //剛体を取得する
        rb = GetComponent<Rigidbody>();

        //発射距離を取得する(弾丸予測線を計算する時に保存する)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //弾丸速度を保存する
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3秒後で自分を破壊する
        Destroy(this.gameObject, BulletLifeTime);
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

      
    }
    public int damage = 20; // 弾のダメージ

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1")|| collision.gameObject.CompareTag("Player2"))
        {
            // プレイヤーにダメージを与える
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            // 弾丸を消す
            Destroy(gameObject);
        }


    }
}
