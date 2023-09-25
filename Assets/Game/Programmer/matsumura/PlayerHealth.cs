using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // �ő�HP
    private int currentHP;  // ���݂�HP

    private void Start()
    {
        currentHP = maxHP; // ����HP��ݒ�
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // �_���[�W���󂯂�
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
    }
}
