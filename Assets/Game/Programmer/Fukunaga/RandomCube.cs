using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCube : MonoBehaviour
{


    // 生成するプレハブ格納用
    public GameObject PrefabCube;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 30フレーム毎にシーンにプレハブを生成
        if (Time.frameCount % 3201 == 0)
        {
            // プレハブの位置をランダムで設定
            float x = Random.Range(-200.0f, 650.0f);
            float z = Random.Range(-200.0f, 650.0f);
            Vector3 pos = new Vector3(x, 180.0f, z);

            // プレハブを生成
            Instantiate(PrefabCube, pos, Quaternion.identity);
           
        }
    }
}
