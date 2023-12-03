using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHit : MonoBehaviour
{
    // OnTriggerEnterは、他のColliderがこのColliderに触れると呼ばれます
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")|| other.CompareTag("Player2")) // もし他のColliderが"Player"タグを持っていたら
        {
            Debug.Log("Playerがトリガーに入りました！");
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
