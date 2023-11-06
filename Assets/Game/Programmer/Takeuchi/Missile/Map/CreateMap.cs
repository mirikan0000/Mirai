using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [SerializeField]
    [Header("配置するオブジェクト")]
    public GameObject mapObj;  //ステージ上に配置するオブジェクト

    [Header("生成範囲指定用オブジェクト")]
    public GameObject createRangeA;  //生成する範囲A
    public GameObject createRangeB;  //生成する範囲B

    [Header("Navmesh生成用")]
    private NavMeshSurface surface;

    void Start()
    {
        if (mapObj != null && createRangeA != null && createRangeB != null)
        {
            for(int i = 0; i < 40; i++)
            {
                MapCreate();
            }
        }

        //Navmesh生成
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //範囲内にオブジェクトをランダムに配置する
    private void MapCreate()
    {
        var parent = this.transform;

        float x = Random.Range(createRangeA.transform.position.x, createRangeB.transform.position.x);
        float y = Random.Range(createRangeA.transform.position.y, createRangeB.transform.position.y);
        float z = Random.Range(createRangeA.transform.position.z, createRangeB.transform.position.z);

        Instantiate(mapObj, new Vector3(x, y, z), Quaternion.identity, parent);
    }
}
