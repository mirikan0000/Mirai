using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleTestPlayer : MonoBehaviour
{
    public float tPmoveSpeed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, tPmoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -tPmoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(tPmoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-tPmoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
