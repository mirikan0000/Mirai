using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // ���f�[�^�̍Đ����u���i�[����ϐ�
    private AudioSource audio;

    // ���f�[�^���i�[����ϐ��iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���X�^�[�g����AudioSource�i���Đ����u�j�̃R���|�[�l���g��������
        audio = gameObject.AddComponent<AudioSource>();

    }

    /// <summary>
    /// �Փ˂�����
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂��������Bullet�^�O���t���Ă���Ƃ�
        if (collision.gameObject.tag == "Bullet")
        {
            // ���isound�j����x�����iPlayOneShot�j�Đ�����
            audio.PlayOneShot(sound);
        }
    }
}
