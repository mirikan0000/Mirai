using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        // �n�ʂɏՓ˂����玩�I�u�W�F�N�g�폜
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        // Player�ƏՓ˂�����A���I�u�W�F�N�g���폜
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
