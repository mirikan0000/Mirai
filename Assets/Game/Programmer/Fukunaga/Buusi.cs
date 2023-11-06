using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bussi : MonoBehaviour
{
    public GameObject PrefabCube1;
    public GameObject PrefabCube2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            Instantiate(PrefabCube1, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            Instantiate(PrefabCube2, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
        }
    }
}
