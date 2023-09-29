using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100; // �ő�HP
    private int currentHP;  // ���݂�HP

    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[�����X�g")]
    public List<string> loadSceneNameList;
    private bool isDestroyScene;
    private IEnumerable<string> unLoadSceneNameList;
    private string loadSceneName;

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
        SceneManager.Instance.LoadScene("EndScene");

        if (isDestroyScene)
        {

            // �V�[���폜
            foreach (string unLoadSceneName in unLoadSceneNameList)
            {
                if (unLoadSceneName != loadSceneName)
                {
                    foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                    {
                        if (scene.name == unLoadSceneName)
                        {
                            SceneManager.Instance.DestroyScene(unLoadSceneName);
                        }
                    }
                }
            }
            isDestroyScene = false;
        }
    }
}
