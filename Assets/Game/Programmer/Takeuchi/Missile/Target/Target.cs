using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    [Header("接触したオブジェクト用")]
    public GameObject hitObj;

    // Start is called before the first frame update
    void Start()
    {
        hitObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        hitObj = other.gameObject;

        if (hitObj != null)
        {
            if (hitObj.gameObject.name == "TestBullet(Clone)")
            {
                GetComponent<Renderer>().material.color = Color.blue;
                Debug.Log(hitObj.name);
            }
        }
    }
}
