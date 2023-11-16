using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // �ő�HP
    private int currentHP;  // ���݂�HP

    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[�����X�g")]
  //  public List<string> loadSceneNameList;
    public string loadSceneName;
    public bool isEnd;
    public List<string> unLoadSceneNameList;
    public bool hitflog; //�R�����g�V�X�e���p�̃t���O

    private void Start()
    {
        currentHP = maxHP; // ����HP��ݒ�
        isEnd = false;
    }
    private void Update()
    {
        hitflog = false;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // �_���[�W���󂯂�
        hitflog = true;
        if (currentHP <= 0)
        {
            Die(); // HP��0�ȉ��ɂȂ����玀�S���������s
        }
    }

    void Die()
    {
        // ���S���̏����������ɋL�q�i��F�Q�[���I�[�o�[��ʂ̕\���Ȃǁj
        // ���̗�ł̓v���C���[�I�u�W�F�N�g�𖳌��ɂ��܂��B
        gameObject.SetActive(false);
        isEnd = true;
    }
}
