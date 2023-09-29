using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        // 地面に衝突したら自オブジェクト削除
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        // Playerと衝突したら、自オブジェクトを削除
        else if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
