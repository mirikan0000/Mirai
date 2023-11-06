using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCube2 : MonoBehaviour
{
    // ��������v���n�u�i�[�p
    public GameObject PrefabCube1;
    public float spawnTime;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", 120f, spawnTime);
    }

    void SpawnObject()
    {
        // �v���n�u�̈ʒu�������_���Őݒ�
        float x = Random.Range(-200.0f, 650.0f);
        float z = Random.Range(-200.0f, 650.0f);
        Vector3 pos = new Vector3(x, 180.0f, z);

        // �v���n�u�𐶐�
        Instantiate(PrefabCube1, pos, Quaternion.identity);
    }
   
    
}
