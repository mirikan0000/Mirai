using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Player1�ƏՓ˂�����A���I�u�W�F�N�g���폜
        if (collision.gameObject.tag == "Player1")
        {
            Destroy(gameObject);
        }

    }
}
