using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCube : MonoBehaviour
{


    // ��������v���n�u�i�[�p
    public GameObject PrefabCube;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 30�t���[�����ɃV�[���Ƀv���n�u�𐶐�
        if (Time.frameCount % 3201 == 0)
        {
            // �v���n�u�̈ʒu�������_���Őݒ�
            float x = Random.Range(-200.0f, 650.0f);
            float z = Random.Range(-200.0f, 650.0f);
            Vector3 pos = new Vector3(x, 180.0f, z);

            // �v���n�u�𐶐�
            Instantiate(PrefabCube, pos, Quaternion.identity);
           
        }
    }
}
