using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Player1と衝突したら、自オブジェクトを削除
        if (collision.gameObject.tag == "Player1")
        {
            Destroy(gameObject);
        }

    }
}
