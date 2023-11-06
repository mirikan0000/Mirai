using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [SerializeField]
    [Header("�z�u����I�u�W�F�N�g")]
    public GameObject mapObj;  //�X�e�[�W��ɔz�u����I�u�W�F�N�g

    [Header("�����͈͎w��p�I�u�W�F�N�g")]
    public GameObject createRangeA;  //��������͈�A
    public GameObject createRangeB;  //��������͈�B

    [Header("Navmesh�����p")]
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

        //Navmesh����
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�͈͓��ɃI�u�W�F�N�g�������_���ɔz�u����
    private void MapCreate()
    {
        var parent = this.transform;

        float x = Random.Range(createRangeA.transform.position.x, createRangeB.transform.position.x);
        float y = Random.Range(createRangeA.transform.position.y, createRangeB.transform.position.y);
        float z = Random.Range(createRangeA.transform.position.z, createRangeB.transform.position.z);

        Instantiate(mapObj, new Vector3(x, y, z), Quaternion.identity, parent);
    }
}
