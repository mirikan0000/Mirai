using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotPower : MonoBehaviour
{
    public Image shotPowerBar; 
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        //Vector3 screenPos = Camera.main.transform.position;
        //shotPowerBar.transform.position = new Vector3(screenPos.x + 500, screenPos.y + 50, screenPos.z);
        //shotPowerBar.transform.localScale = new Vector3(2.0f, 0.5f, 1); 
    }
}
