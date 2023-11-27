using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    // �e�̏����i�[����\����
    public struct BulletInfo
    {
        public Vector2 position;
        //public string bulletType;
    }

    // �t�B�[���h�ɂ��邷�ׂĂ̒e�̃��X�g
    private List<GameObject> bullets = new List<GameObject>();

    // �V�[������2D�}�b�v�ɓn�����߂̒e�̏��z��
    private List<BulletInfo> bulletInfoList = new List<BulletInfo>();
    int bulletCount;

    // �e�̐���\������UI Text��Inspector�E�B���h�E����ݒ�
    public Text bulletCountText;

    private void Start()
    {
        // �V�[�����̂��ׂĂ̒e���擾���AbulletCount�ɐݒ�
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
        bulletCount = allBullets.Length;

        // �V�[�����̂��ׂĂ̒e�̐����X�V
        UpdateBulletCountText();
    }

    // �e�𐶐�����֐����O������Ă΂ꂽ�Ɖ���
    // �e�𐶐�������A������Ă�Œe���Ǘ����X�g�ɒǉ�
    public void AddBullet(GameObject bullet, Vector2 position/*, string bulletType*/)
    {
        bullets.Add(bullet);

        // �e�̏������X�g�ɒǉ�
        BulletInfo bulletInfo;
        bulletInfo.position = position;
        //bulletInfo.bulletType = bulletType;
        bulletInfoList.Add(bulletInfo);

        // �V�[�����̂��ׂĂ̒e�̐����X�V
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
    }

    // �e���폜����֐����O������Ă΂ꂽ�Ɖ���
    public void RemoveBullet(GameObject bullet)
    {
        bullets.Remove(bullet);

        // �e�̏������X�g����폜
        int indexToRemove = -1;
        for (int i = 0; i < bulletInfoList.Count; i++)
        {
            // Vector2��Vector3���r���邽�߂�Vector2�ɕϊ�
            Vector2 bulletPosition = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            if (bulletInfoList[i].position == bulletPosition)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove >= 0)
        {
            bulletInfoList.RemoveAt(indexToRemove);
        }

        // �V�[�����̂��ׂĂ̒e�̐����X�V
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
    }

    // �e�̏����擾����֐�
    public List<BulletInfo> GetBulletInfoList()
    {
        return bulletInfoList;
    }

    // �t�B�[���h�ɂ���e�̐����擾����֐�
    public int GetBulletCount()
    {
        //Debug.Log("�e�̐�: " + bulletCount);
        return bulletCount;
    }

    // �e�̏����擾���A���O�ɕ\������֐�
    public void LogBulletInfo()
    {
        //Debug.Log("�e�̏��:");
        foreach (var bulletInfo in bulletInfoList)
        {
            //Debug.Log("�ʒu: " + bulletInfo.position/* + ", ���: " + bulletInfo.bulletType*/);
        }
    }

    // �e�̐���UI Text�ɕ\�����郁�\�b�h
    private void UpdateBulletCountText()
    {
        // �e�̐���UI Text�ɔ��f
        if (bulletCountText != null)
        {
            bulletCountText.text = "�e�̐�: " + GetBulletCount().ToString();
        }
    }

    void Update()
    {
        // �V�[�����̂��ׂĂ̒e�̐����X�V
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        // �V�[�����̂��ׂĂ̒e�̐����X�V
        UpdateBulletCountText();
        LogBulletInfo();
    }
}
