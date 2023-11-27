using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Positive Butten 7"))
        {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetButton("Positive Butten 7"))
        {
            GetComponent<AudioSource>().Stop();
        }
        if (Input.GetButtonUp("Positive Butten 7"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
