using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHit : MonoBehaviour
{
    // OnTriggerEnter�́A����Collider������Collider�ɐG���ƌĂ΂�܂�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")|| other.CompareTag("Player2")) // ��������Collider��"Player"�^�O�������Ă�����
        {
            Debug.Log("Player���g���K�[�ɓ���܂����I");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
