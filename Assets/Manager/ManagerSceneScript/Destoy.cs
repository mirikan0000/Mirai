using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        // �n�ʂɏՓ˂����玩�I�u�W�F�N�g�폜
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        // Player�ƏՓ˂�����A���I�u�W�F�N�g���폜
        else if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
