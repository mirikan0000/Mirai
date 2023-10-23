using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("RotByCamera")]
    public bool isRotation = false;

    [Header("ScaByCamera")]
    public bool isZoom = false;

    [Header("Sca%")]
    public float size = 0.1F;

    Camera camera;//MainCamera

    private float _distance;//‰Šú‹——£

    private bool first = true;

    void Start()
    {
        camera = Camera.main;

        //‰Šú‹——£
        _distance = Vector3.Distance(camera.transform.position, transform.position);
    }

    void Update()
    {
        //ƒJƒƒ‰‚Å‰ñ“]
        if (isRotation)
        {
            transform.rotation = camera.transform.rotation;
        }

        //ƒJƒƒ‰‚ÅŠg‘åk¬
        if (isZoom)
        {
            float distance = Vector3.Distance(camera.transform.position, transform.position);

            var scale = distance / _distance * size;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

}
