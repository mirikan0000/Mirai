using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavmeshSurface : MonoBehaviour
{
    [SerializeField]
    [Header("Navmesh生成用")]
    private NavmeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        //Navmesh生成
        //surface = GetComponent<NavmeshSurface>();
        //surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
