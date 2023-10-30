using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Player2と衝突したら、自オブジェクトを削除
        if (collision.gameObject.tag == "Player2")
        {
            Destroy(gameObject);
        }

    }
}
