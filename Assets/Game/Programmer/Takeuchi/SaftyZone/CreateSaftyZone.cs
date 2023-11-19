using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("���u�̒��S�n�_�����p")]
    public GameObject saftyZoneObj;   //����������u�̃I�u�W�F�N�g
    public Transform saftyRangeA;     //��������͈͎w��n�_A
    public Transform saftyRangeB;     //��������͈͎w��n�_B
    public float delayTime;           //�����܂ł̑҂�����
    public bool spawnCheck;           //�����ł��邩�ǂ���(true = �����\�Afalse = �����s�j

    private bool spawnType;           //�����ʒu�̃t���O
    private float timer;              //�҂����Ԍv���p

    [Header("���u�̒i�K")]
    public float reduStageCount;        //�k�����̒i�K��
    public float magStageCount;         //�g�厞�̒i�K��
    public float reduCount;             //���݂̏k���i�K
    public float magCount;              //���݂̊g��i�K

    void Start()
    {
        //�e��ϐ�������
        timer = 0.0f;
        spawnCheck = true;
        spawnType = true;
        reduCount = 0;
        magCount = 0;
    }

    void Update()
    {
        if (spawnType == true)
        {
            if (spawnCheck == true)
            {
                //�����_������
                SaftyZoneRandomSpawn();
            }
        }
        else
        {
            if (spawnCheck == true)
            {
                //���S�ɐ���
                SaftyZoneCenterSpawn();

                spawnCheck = false;
            }
        }
    }

    //�����ʒu�������_���Ŏw��
    private void SaftyZoneRandomSpawn()
    {
        var parent = this.transform;

        //�͈͓��̃����_���ȍ��W��ݒ�
        float x = Random.Range(saftyRangeA.position.x, saftyRangeB.position.x);
        float y = Random.Range(saftyRangeA.position.y, saftyRangeB.position.y);
        float z = Random.Range(saftyRangeA.position.z, saftyRangeB.position.z);

        //�҂����Ԍv���J�n
        timer += Time.deltaTime;

        //�҂����Ԃ���������
        if (timer > delayTime)
        {
            //���u���q�I�u�W�F�N�g�Ƃ��Đ���
            Instantiate(saftyZoneObj, new Vector3(x, y, z), Quaternion.identity, parent);

            //���u�̏k���i�K����i�K�ڂɂ���
            reduCount = 1;

            spawnCheck = false;

            //�҂����ԏ�����
            timer = 0.0f;
        }
    }

    //�����ʒu�𒆐S�ɂ���
    private void SaftyZoneCenterSpawn()
    {
        var parent = this.transform;

        //���u���q�I�u�W�F�N�g�Ƃ��Đ���
        Instantiate(saftyZoneObj, new Vector3(0, 0, 0), Quaternion.identity, parent);
    }
}
