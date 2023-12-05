using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class CreateNavmesh : MonoBehaviour
{
    [Header("Navmeshê∂ê¨óp")]
    private NavMeshSurface surface;

    private bool createFlag;  //
    private float timer;      //

    void Start()
    {
        createFlag = true;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (createFlag == true)
        {
            timer += Time.deltaTime;

            if (timer > 1.0f)
            {
                //Navmeshê∂ê¨
                surface = GetComponent<NavMeshSurface>();
                surface.BuildNavMesh();

                timer = 0.0f;
                createFlag = false;
            }
        }
    }
}
