using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Player2�ƏՓ˂�����A���I�u�W�F�N�g���폜
        if (collision.gameObject.tag == "Player2")
        {
            Destroy(gameObject);
        }

    }
}
