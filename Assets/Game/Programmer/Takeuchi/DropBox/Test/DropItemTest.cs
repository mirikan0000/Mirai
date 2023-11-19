using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemTest : MonoBehaviour
{
    public enum ItemName
    {
        HealItem,SpeedUpItem,PierceBulletItem,ShieldItem
    }
    [SerializeField]
    [Header("�e��ϐ�")]
    public ItemName itemName;  //�A�C�e�����ʗp
    //�e�I�u�W�F�N�g���ƂɕK�v�Ȓl�̂ݐݒ�
    public float healPoint;    //�񕜗�
    public float speedValue;   //������
    public int pierceCount;    //�ђʂ���ő��
    public int shieldCount;    //�ő�h���
    private bool destroyFlag = false;

    void Start()
    {
        destroyFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyFlag = true;
        }
    }
}
