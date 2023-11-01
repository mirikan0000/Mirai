using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�̒��S�n�_�����p")]
    public GameObject saftyZoneObj;  //����������u�̃I�u�W�F�N�g
    public Transform saftyRangeA;    //��������͈͎w��n�_A
    public Transform saftyRangeB;    //��������͈͎w��n�_B
    public bool spawnCheck = false;  //�����ł��邩�ǂ���(true = �����\�Afalse = �����s�j
    public bool spawnType = true;    //�����ʒu�̃t���O
    public float delayTime;          //�҂�����

    void Start()
    {
        spawnCheck = true;
    }

    void Update()
    {
        if (spawnType == true)
        {
            if (spawnCheck == true)
            {
                //�����_������
                Invoke("SaftyZoneRandomSpawn", delayTime);

                spawnCheck = false;
            }
        }
        else
        {
            if (spawnCheck == true)
            {
                //���S�ɐ���
                Invoke("SaftyZoneCenterSpawn", delayTime);

                spawnCheck = false;
            }
        }
    }

    //�����ʒu�������_���Ŏw��
    private void SaftyZoneRandomSpawn()
    {
        float x = Random.Range(saftyRangeA.position.x, saftyRangeB.position.x);
        float y = Random.Range(saftyRangeA.position.y, saftyRangeB.position.y);
        float z = Random.Range(saftyRangeA.position.z, saftyRangeB.position.z);

        //���u�𐶐�
        Instantiate(saftyZoneObj, new Vector3(x, y, z), Quaternion.identity);
    }

    //�����ʒu�𒆐S�ɂ���
    private void SaftyZoneCenterSpawn()
    {
        Instantiate(saftyZoneObj, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
