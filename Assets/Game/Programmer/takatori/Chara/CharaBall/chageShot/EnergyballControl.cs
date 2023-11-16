using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class EnergyballControl : MonoBehaviour
{
    public bool shot = false; //�ˏo����p�t���O
    //Prefab�������e�I�u�W�F�N�g
    [SerializeField]
    private GameObject energyballPrefab_S; //Prefab������S�T�C�Y�̃G�l���M�[�e
    [SerializeField]
    private GameObject energyballPrefab_M; //Prefab������M�T�C�Y�̃G�l���M�[�e
    [SerializeField]
    private GameObject energyballPrefab_L; //Prefab������L�T�C�Y�̃G�l���M�[�e

    //�������Đ��������I�u�W�F�N�g
    private GameObject enemyball_S; //��������S�T�C�Y�̃G�l���M�[�e
    private GameObject enemyball_M; //��������M�T�C�Y�̃G�l���M�[�e
    private GameObject enemyball_L; //��������L�T�C�Y�̃G�l���M�[�e

    //�`���[�W��Ԃ̔���p�t���O
    private bool sState = false; //S�e�p
    private bool mState = false; //M�e�p
    private bool lState = false; //L�e�p

    //�p�����[�^
    public float shotSpeed; //���˃X�s�[�h
    private float time = 0.0f; //�o�ߎ���
    [SerializeField]
    private float chargetime = 3.0f; //�`���[�W����

    //�G�t�F�N�g
    [SerializeField]
    private GameObject chargeEB; //�`���[�W�G�t�F�N�g
    private VisualEffect chargeEB_VFX; //�`���[�WVFX
    private AudioSource chargeEB_SE; //�`���[�WSE
    [SerializeField]
    private GameObject hazeEB; //�����G�t�F�N�g
    private VisualEffect hazeEB_VFX; //����VFX
    private AudioSource hazeEB_SE; //����SE
    [SerializeField]
    private GameObject fireEB; //�ΉԃG�t�F�N�g
    private VisualEffect fireEB_VFX; //�Ή�VFX
    private AudioSource fireEB_SE; //�Ή�SE
    PlayerManager_ Player;
    //���̑�
    Rigidbody rb;
    private PlayerController playerController; //�v���C���[�R���g���[���p�X�N���v�g
    int count_s = 0; //��������S�T�C�Y�e�̐�
    int count_m = 0; //��������M�T�C�Y�e�̐�
    int count_l = 0; //��������L�T�C�Y�e�̐�
    int max = 1; //�����ł���e�̐�

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>(); //PlayerController�̃R���|�[�l���g�����擾
        chargeEB_VFX = chargeEB.transform.GetComponent<VisualEffect>();
        chargeEB_SE = chargeEB.transform.GetComponent<AudioSource>();
        hazeEB_VFX = hazeEB.transform.GetComponent<VisualEffect>();
        hazeEB_SE = hazeEB.transform.GetComponent<AudioSource>();
        fireEB_VFX = fireEB.transform.GetComponent<VisualEffect>();
        fireEB_SE = fireEB.transform.GetComponent<AudioSource>();

        //�G�t�F�N�g��~
        chargeEB_VFX.SendEvent("StopPlay");
        hazeEB_VFX.SendEvent("StopPlay");
        fireEB_VFX.SendEvent("StopPlay");
        chargeEB_SE.Stop();
        hazeEB_SE.Stop();
        fireEB_SE.Stop();
    }
    void Update()
    {
        //�{�^������͂��Ă���Ƃ�
      //  if (Player.energyballFlug) Generate(); 
      //�G�l���M�[�e�����p���\�b�h

        //�{�^���𗣂����Ƃ�
       // else Shot(); //�G�l���M�[�e���˗p���\�b�h
    }

    //�G�l���M�[�e�̐�������
    void Generate()
    {
        time += Time.deltaTime; //�`���[�W���Ԃ̉��Z

        if (time >= 0.5f && time < chargetime) //�`���[�W���Ԃ�0.5�b�ȏ�3�b�����̂Ƃ�
        {
            if (count_s == max) return; //����ɒB�����Ƃ��́A�������Ȃ�
            else
            {
                OnChargeEffect();
                enemyball_S = Instantiate(energyballPrefab_S, transform.position, Quaternion.identity); //S�e�𐶐�
                enemyball_S.transform.parent = this.transform; //S�e�̐e�𔭎˃|�C���g�ɐݒ聨�L�����̈ړ��ɒǔ������邽��
                rb = enemyball_S.GetComponent<Rigidbody>(); //S�e��rigidbody���擾
                rb.isKinematic = true; //�͂̉e���̖�����
                sState = true; //S�e�̏����������
                count_s++; //S�e�̐����������Z
            }
        }
        else if (time >= chargetime && time < chargetime * 2) //�`���[�W���Ԃ�3�b�ȏ�6�b�����̂Ƃ�
        {
            if (count_m == max) return;
            else
            {
                Destroy(enemyball_S); //S�e�̍폜
                sState = false; //S�e�̏����������

                //�ȉ���S�e�Ɠ��l
                enemyball_M = Instantiate(energyballPrefab_M, transform.position, Quaternion.identity);
                enemyball_M.transform.parent = this.transform;
                rb = enemyball_M.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                mState = true;
                count_m++;
            }
        }
        else if (time >= chargetime * 2) //�`���[�W���Ԃ�6�b�ȏ�̂Ƃ�
        {
            if (count_l == max) return;
            else
            {
                Destroy(enemyball_M);
                mState = false;

                enemyball_L = Instantiate(energyballPrefab_L, transform.position, Quaternion.identity);
                enemyball_L.transform.parent = this.transform;
                rb = enemyball_L.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                lState = true;
                count_l++;
            }
        }
    }
    //�G�l���M�[�e�̔��ˏ���
    void Shot()
    {
        OnShotEffect();
        this.transform.DetachChildren(); //�e�q�֌W�̐؂藣�����L�����̈ړ��ɒǔ����Ȃ�����

        if (sState)
        {
            OnShotEffect(); //S�e�̔��˃G�t�F�N�g
            rb.isKinematic = false; //�͂̉e���̗L����
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse); //�͂̕t�^�i�����A�����A�͂̎�ނ̐ݒ�j
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up); //���˕������擾
            enemyball_L.transform.rotation = rotation; //�e�𔭎˕����ɐݒ�
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>(); //���������e��EnergyballControl���擾
            energyballControl.shot = true; //EnergyballControl��shot�t���O��true�ɕύX
            Destroy(enemyball_S, 2.0f); //��莞�Ԃ̌o�߂ŏ���
            sState = false; //S�e�̏����������
        }
        else if (mState) //M�e�AL�e����L�Ɠ��l
        {
            OnShotEffect();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            enemyball_L.transform.rotation = rotation;
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>();
            energyballControl.shot = true;
            Destroy(enemyball_M, 2.0f);
            mState = false;
        }
        else if (lState)
        {
            OnShotEffect();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            enemyball_L.transform.rotation = rotation;
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>();
            energyballControl.shot = true;
            Destroy(enemyball_L, 2.0f);
            lState = false;
        }
        //�e���ƃ`���[�W�o�ߎ��Ԃ̃��Z�b�g
        count_s = 0;
        count_m = 0;
        count_l = 0;
        time = 0.0f;
    }
    private void OnChargeEffect()
    {
        //�`���[�W���̃G�t�F�N�g�J�n
        chargeEB_VFX.SendEvent("OnPlay");
        hazeEB_VFX.SendEvent("OnPlay");
        chargeEB_SE.Play();
        hazeEB_SE.Play();
    }
    private void OnShotEffect() //���˃G�t�F�N�g
    {
        //�`���[�W���̃G�t�F�N�g�I��
        chargeEB_VFX.SendEvent("StopPlay");
        hazeEB_VFX.SendEvent("StopPlay");
        chargeEB_SE.Stop();
        hazeEB_SE.Stop();

        //���ˎ��̃G�t�F�N�g�J�n
        fireEB_VFX.Play();
        fireEB_SE.Play();
    }
}
