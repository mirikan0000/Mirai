using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetMouseButton(0))
        {
            GetComponent<AudioSource>().Stop();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
