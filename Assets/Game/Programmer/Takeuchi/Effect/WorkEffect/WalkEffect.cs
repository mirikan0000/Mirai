using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WalkEffect : MonoBehaviour
{
    [SerializeField]
    VisualEffect effect;     //�G�t�F�N�g
    public float stopTimer;  //��������̎��Ԍv��
    public float timerLimit; //��������

    void Start()
    {
        //�e��ϐ�������
        stopTimer = 0.0f;
    }

    void Update()
    {
        //���Ԍv��
        stopTimer += Time.deltaTime;

        //������~�Ɣj��
        if (stopTimer > timerLimit)
        {
            //�G�t�F�N�g��~
            effect.SendEvent("OnStop");

            //�^�C�}�[������
            stopTimer = 0.0f;

            //�I�u�W�F�N�g�j��
            Destroy(this.gameObject);
        }
    }
}
