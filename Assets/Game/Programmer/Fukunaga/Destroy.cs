using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        // 地面に衝突したら自オブジェクト削除
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        // Playerと衝突したら、自オブジェクトを削除
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
