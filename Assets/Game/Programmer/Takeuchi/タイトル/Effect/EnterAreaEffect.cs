using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAreaEffect : MonoBehaviour
{
    [SerializeField]
    [Header("�p�[�e�B�N���V�X�e���擾�p")]
    private ParticleSystem enterAreaEffect;

    [Header("�e�I�u�W�F�N�g�֌W")]
    private GameObject parentObj;
    private Tittle_Manager parentScript;

    void Start()
    {
        //�p�[�e�B�N���V�X�e���擾
        enterAreaEffect = GetComponent<ParticleSystem>();

        //�e�I�u�W�F�N�g�̃X�N���v�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<Tittle_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[���N���G���A����o����G�t�F�N�g��~
        if (parentScript.playerEnterFlag == false)
        {
            enterAreaEffect.Stop();
        }
    }

    //�G�t�F�N�g�Đ��I�����m
    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
