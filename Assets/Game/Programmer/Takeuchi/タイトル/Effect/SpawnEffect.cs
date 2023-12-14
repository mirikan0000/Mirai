using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField]
    [Header("�e�I�u�W�F�N�g�擾�p")]
    private GameObject parentObj;
    private Tittle_Manager parentScript;

    [Header("�p�[�e�B�N���V�X�e���p")]
    private ParticleSystem spawnEffect;
    private float playTimer;     //�Đ����Ԍv��
    public float maxPlayTimer;   //�ő�Đ�����

    void Start()
    {
        //�e�I�u�W�F�N�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<Tittle_Manager>();

        //�p�[�e�B�N���V�X�e���擾
        spawnEffect = GetComponent<ParticleSystem>();

        //�Đ����ԏ�����
        playTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playTimer += Time.deltaTime;

        if (playTimer > maxPlayTimer)
        {
            spawnEffect.Stop();
        }
    }

    //�G�t�F�N�g�Đ��I�����m
    private void OnParticleSystemStopped()
    {
        //�V�[���J�ڗp�t���O�Ǘ�
        parentScript.spawnPlayerFlag = true;

        Destroy(this.gameObject);
    }
}
