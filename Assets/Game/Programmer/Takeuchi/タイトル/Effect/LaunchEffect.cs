using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEffect : MonoBehaviour
{
    [SerializeField]
    [Header("�p�[�e�B�N���V�X�e���擾�p")]
    private ParticleSystem launchEffect;

    void Start()
    {
        //�p�[�e�B�N���V�X�e���擾
        launchEffect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�G�t�F�N�g�Đ��I�����m
    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
