using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBullet : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 normal;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �|�C���g
        // Update�̒��Œl����Ɏ擾���邱�ƁB
        direction = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Area"))
        {
            normal = collision.contacts[0].normal;

            Vector3 result = Vector3.Reflect(direction, normal);

            rb.velocity = result;

            // direction�̍X�V
            direction = rb.velocity;
        }
    }
}
