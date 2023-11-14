using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    [Header("äeéÌïœêî")]
    private bool destroyFlag = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyFlag = true;
        }
    }
}
