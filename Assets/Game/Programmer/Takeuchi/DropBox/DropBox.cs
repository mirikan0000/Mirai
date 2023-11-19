using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField]
    [Header("��������I�u�W�F�N�g")]
    public GameObject healItem;          //�񕜂̃I�u�W�F�N�g
    public GameObject speedUpItem;       //�X�s�[�h�A�b�v�̃I�u�W�F�N�g
    public GameObject pierceBulletItem;  //�ђʒe�̃I�u�W�F�N�g
    public GameObject shieldItem;        //�V�[���h�̃I�u�W�F�N�g

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
        itemNum = Random.Range(1, 5);

        //���܂����l�ŃA�C�e���𐶐�
        switch (itemNum)
        {
            case 1:  //�񕜂̃A�C�e���𐶐�
                Instantiate(healItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 2:  //�X�s�[�h�A�b�v�̃A�C�e���𐶐�
                Instantiate(speedUpItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 3:  //�ђʒe�̃A�C�e���𐶐�
                Instantiate(pierceBulletItem, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 4:  //�V�[���h�̃A�C�e���𐶐�
                Instantiate(shieldItem, this.gameObject.transform.position, Quaternion.identity);
                break;
        }

        openFlag = false;

        destroyFlag = true;
    }
}
