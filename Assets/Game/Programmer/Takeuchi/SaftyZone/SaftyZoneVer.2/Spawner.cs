using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    [Header("���u�����p")]
    public GameObject saftyZonebj;  //����������u�̃I�u�W�F�N�g
    public Transform rangeA;        //��������͈�A
    public Transform rangeB;        //��������͈�B
    public float spawnDelayTime;    //�����܂ł̑҂�����
    public bool spawnFlag;          //�����p�t���O

    private Vector3 spawnPos;       //����������W
    private float timer;            //�҂����Ԍv���p

    void Start()
    {
        //�ϐ�������
        spawnFlag = true;
        timer = 0.0f;
    }

    void Update()
    {
        //�����p�t���O��True�Ȃ���u����
        if (spawnFlag == true)
        {
            //���u����
            SpawnSaftyZone();
        }
    }

    //���u����
    private void SpawnSaftyZone()
    {
        var parent = this.transform;

        //����������W�������_���ɐݒ�
        float x = Random.Range(rangeA.position.x, rangeB.position.x);
        float y = Random.Range(rangeA.position.y, rangeB.position.y);
        float z = Random.Range(rangeA.position.z, rangeB.position.z);
        spawnPos = new Vector3(x, y, z);

        //�҂����Ԍv���J�n
        timer += Time.deltaTime;

        //�҂����Ԍo�ߌ�
        if (timer > spawnDelayTime)
        {
            //���u���q�I�u�W�F�N�g�Ƃ��Đ���
            Instantiate(saftyZonebj, spawnPos, Quaternion.identity, parent);

            //�����p�t���O��False��
            spawnFlag = false;

            //�҂����Ԃ�������
            timer = 0.0f;
        }
    }
}
