using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestCube : MonoBehaviour
{
    [SerializeField]
    [Header("�e��ϐ�")]
    public VisualEffect walkEffectObj;  //�����Ă���Ƃ��̃G�t�F�N�g�I�u�W�F�N�g
    public Vector3 effectSpawnPos;      //�G�t�F�N�g�����ʒu
    public float delayTimer,            //�҂����Ԍv���p
                 timerLimit;            //�ő�҂�����
    public bool destroyFlag = false;    //�I�u�W�F�N�g�j��t���O

    public AudioSource walkSE;          //���s��SE���Đ�����R���|�[�l���g
    public AudioClip source1;           //���s���ɍĐ�����SE

    private GameObject parentSpawner;   //�e�I�u�W�F�N�g
    private SpawnTestCube parentScript; //�e�I�u�W�F�N�g�̃X�N���v�g

    void Start()
    {
        //�e��ϐ�������
        delayTimer = 0.0f;

        //�e�I�u�W�F�N�g�̃X�N���v�g�擾
        parentSpawner = transform.parent.gameObject;
        parentScript = parentSpawner.GetComponent<SpawnTestCube>();

        //AudioSource�擾
        walkSE = GetComponent<AudioSource>();
    }

    void Update()
    {
        //�t���O�Ŕj��
        if (destroyFlag == true)
        {
            //�҂����Ԍv���J�n
            delayTimer += Time.deltaTime;

            

            //�҂����Ԃ����Ŕj��
            if (delayTimer > timerLimit)
            {
                //�e�I�u�W�F�N�g�̐������邽�߂̃t���O��True�ɂ���
                parentScript.spawnFlag = true;

                //�j��t���O��False�ɂ���
                destroyFlag = false;

                //�^�C�}�[������
                delayTimer = 0.0f;

                //�I�u�W�F�N�g�j��
                Destroy(this.gameObject);
            }
        }
    }

    //�n�ʂɓ���������
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            //�G�t�F�N�g����
            Instantiate(walkEffectObj, effectSpawnPos, Quaternion.identity);

            //���s��SE�Đ�
            walkSE.PlayOneShot(source1);

            //�j��t���O��True�ɂ���
            destroyFlag = true;
        }
    }

}
