using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField]
    [Header("��������I�u�W�F�N�g")]
    public GameObject speedBox;  //�X�s�[�h�A�b�v�̃I�u�W�F�N�g
    public GameObject powerBox;  //�p���[�A�b�v�̃I�u�W�F�N�g

    [Header("�⋋�������p�I�u�W�F�N�g�֌W")]
    public GameObject dropperObj;  //�⋋���𐶐�����I�u�W�F�N�g
    private Dropper dropperScript; //�⋋���𐶐�����I�u�W�F�N�g�̃X�N���v�g

    [Header("�e��ϐ�")]
    public bool openFlag = false;     //�A�C�e�������p�̃t���O
    public bool destroyFlag = false;  //�⋋���j��p�̃t���O
    private int itemNum;              //��������A�C�e�������߂�ϐ�

    void Start()
    {
        //�e��ϐ�������
        itemNum = 0;

        //�⋋���𐶐�����I�u�W�F�N�g�̃X�N���v�g���擾
        dropperObj = GameObject.Find("EmpObjDropper");
        dropperScript = dropperObj.GetComponent<Dropper>();
    }

    
    void Update()
    {
        if (openFlag == true)
        {
            //�����_���ŋ����A�C�e���𐶐�
            DropRandomItem();
        }

        if (destroyFlag == true)
        {
            //�⋋���𐶐�����I�u�W�F�N�g�̌��ݐ�������-1
            dropperScript.dropCount--;

            //�I�u�W�F�N�g�j��
            Destroy(this.gameObject);
        }
    }

    //�����_���ŋ����A�C�e���𐶐�
    private void DropRandomItem()
    {
        //��������A�C�e���������_���Ō��߂�
        itemNum = Random.Range(1, 3);

        //���܂����l�ŃA�C�e���𐶐�
        switch (itemNum)
        {
            case 1:  //�X�s�[�h�A�b�v�̃A�C�e���𐶐�
                Instantiate(speedBox, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 2:  //�p���[�A�b�v�̃A�C�e���𐶐�
                Instantiate(powerBox, this.gameObject.transform.position, Quaternion.identity);
                break;
        }

        openFlag = false;

        destroyFlag = true;
    }
}
