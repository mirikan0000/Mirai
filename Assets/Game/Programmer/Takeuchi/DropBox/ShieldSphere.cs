using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSphere : MonoBehaviour
{
    [SerializeField]
    [Header("�V�[���h��HP")]
    public int shieldHP;  //�V�[���h�̖h���

    [Header("�I�u�W�F�N�g�擾�p")]
    GameObject parentObj;  //�e�I�u�W�F�N�g

    [Header("�ǉ�����}�e���A��")]
    public Material material;

    
    void Start()
    {
        //�e�I�u�W�F�N�g�擾
        //GetParentObj();
    }

    // Update is called once per frame
    void Update()
    {
        //ObjectRotation();

        if (shieldHP <= 0)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            shieldHP = shieldHP - 1;
        }
    }

    //�e�I�u�W�F�N�g�擾
    private void GetParentObj()
    {
        parentObj = transform.parent.gameObject;
    }

    //�Փˏ���
    private void OnTriggerEnter(Collider other)
    {
        //Stage�̃I�u�W�F�N�g�Ɛe�I�u�W�F�N�g�ȊO�ɓ���������
        //if(other.gameObject.name != parentObj.name &&
        //    other.gameObject.tag != "StageObject")
        //{
        //    shieldHP = shieldHP - 1;
        //}
        Debug.Log(other.gameObject.name);
    }

    //�I�u�W�F�N�g��]
    private void ObjectRotation()
    {
        transform.Rotate(new Vector3(10, 10, 10));
    }
}
