using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragreceiver : MonoBehaviour
{
    public bool misileflog;
    public bool armorflog;
    public bool Hpflog;
    public bool penetratBulletflog;

    //�⋋�����擾�p
    public bool speedUpItemFlag;       //�X�s�[�h�A�b�v�̃A�C�e�����擾���Ă��邩
    public bool healItemFlag;          //�񕜂̃A�C�e�����擾���Ă��邩
    public bool shieldItemFlag;        //�V�[���h�̃A�C�e�����擾���Ă��邩
    public bool pierceBulletItemFlag;  //�ђʒe�̃A�C�e�����擾���Ă��邩

   [SerializeField] private PlayerHealth P_Health;//HP�t���O����HP�����炷���߂̔�
    private void Start()
    {
        penetratBulletflog = false;
        misileflog = false;

        //�⋋�����擾�p
        speedUpItemFlag = false;
        healItemFlag = false;
        shieldItemFlag = false;
        penetratBulletflog = false;
    }
    private void Update()
    {
        
    }

    //�⋋�����擾����
    private void OnCollisionEnter(Collision collision)
    {
        //�X�s�[�h�A�b�v�̃A�C�e�����擾������
        if (collision.gameObject.name == "SpeedUpItem(Clone)")
        {
            speedUpItemFlag = true;
        }
        //�񕜂̃A�C�e�����擾������
        if (collision.gameObject.name == "HealItem(Clone)")
        {
            healItemFlag = true;
        }
        //�V�[���h�̃A�C�e�����擾������
        if(collision.gameObject.name == "ShieldItem(Clone)")
        {
            shieldItemFlag = true;
        }
        //�ђʒe�̃A�C�e�����擾������
        if(collision.gameObject.name == "PierceBulletItem(Clone)")
        {
            penetratBulletflog = true;
        }
    }
}
