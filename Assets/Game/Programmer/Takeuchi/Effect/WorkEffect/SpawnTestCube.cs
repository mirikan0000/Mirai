using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTestCube : MonoBehaviour
{
    [SerializeField]
    [Header("�����֌W")]
    public GameObject testCubeObj;  //��������I�u�W�F�N�g
    public Vector3 spawnCubePos;    //��������ʒu
    public bool spawnFlag = true;   //�������邽�߂̃t���O

    void Start()
    {
        
    }

   
    void Update()
    {
        if (spawnFlag == true)
        {
            //��������I�u�W�F�N�g��Null�`�F�b�N
            if (testCubeObj != null)
            {
                //����
                var parent = this.transform;

                Instantiate(testCubeObj, spawnCubePos, Quaternion.identity, parent);

                //�������邽�߂̃t���O��False�ɂ���
                spawnFlag = false;
            }
            else
            {
                Debug.Log("��������I�u�W�F�N�g���ݒ肳��Ă��܂���");
            }
        }
    }
}
