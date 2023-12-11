using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("�̗̓Q�[�W")]
    public int maxHP = 100; // �ő�HP
    private int currentHP;  // ���݂�HP

    private Fragreceiver fragreceiver;
    [SerializeField] private int healValeu;         //�񕜗�

    [Header("�A�[�}�[�Q�[�W")]
    public int maxArmor = 50; // �ő�A�[�}�[
    private int currentArmor; // ���݂̃A�[�}�[
    public bool armorflog;    
    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[�����X�g")]
    public string loadSceneName;
    public bool isEnd;
    public List<string> unLoadSceneNameList;
    public bool hitflog; //�R�����g�V�X�e���p�̃t���O
    public int cameraShakerIndex; //�v���C���[���Ƃ̃J�����̎���

    //Player�̃q�b�g���m�F����SE�ƐԂ��q�b�g�G�t�F�N�g���o���B
    [SerializeField] private PlayerSound hitSE;
  //  [SerializeField]
//    private GameObject PlayerOBJ;
    [SerializeField] private GameObject animationUI;
    public float GetCurrentHP()
    {
        return currentHP;
    }
    public void SetCurrentHP(int currenthp)
    {
        currentHP = currenthp;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    
    private void Start()
    {
        currentHP = maxHP; // ����HP��ݒ�
        currentArmor = maxArmor; // �����A�[�}�[��ݒ�
        isEnd = false;

        fragreceiver = GetComponent<Fragreceiver>();
    }
    private void Update()
    {
        hitflog = false;
        if (currentHP <= 0)
        {
            Die(); // HP��0�ȉ��ɂȂ����玀�S���������s
        }

        //�񕜏���
        HealPlayer();

        //�A�[�}�[�񕜏���
        ArmorHeal();
    }
    public void TakeDamage(int damage, bool useArmor)
    {
        if (useArmor && currentArmor > 0)
        {
            // �A�[�}�[���g���ꍇ���A�[�}�[������ꍇ�̓A�[�}�[�����
            int remainingArmor = Mathf.Max(0, currentArmor - damage);
            int damageToHealth = Mathf.Max(0, damage - currentArmor);

            currentArmor = remainingArmor;
            currentHP -= damageToHealth;
           
        }
        else
        {
            // �A�[�}�[���g��Ȃ��ꍇ�܂��̓A�[�}�[���Ȃ��ꍇ�͒���HP�����
            currentHP -= damage;
            hitSE.PlayHitSE();
            StartCoroutine(Blink());
            animationUI.GetComponent<Animation>().Play();
        }

        CameraShaker.GetInstance(cameraShakerIndex)?.ShakeCamera(5, 0.5f);
        hitflog = true;
    }

    void Die()
    {
        // ���S���̏����������ɋL�q�i��F�Q�[���I�[�o�[��ʂ̕\���Ȃǁj
        // ���̗�ł̓v���C���[�I�u�W�F�N�g�𖳌��ɂ��܂��B
      //  gameObject.SetActive(false);
        isEnd = true;
    }

    // �ǉ�: �_�ŏ����̃R���[�`��
    private IEnumerator Blink()
    {
        float blinkDuration = 1f; // �_�ł̊��ԁi�b�j
        float blinkSpeed = 5f; // �_�ł̑���

        float startTime = Time.time;

        while (Time.time - startTime < blinkDuration)
        {
           // PlayerOBJ.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f / blinkSpeed);

           // PlayerOBJ.GetComponent<Renderer>().material.color =Color.white;
            yield return new WaitForSeconds(1f / blinkSpeed);
        }

        hitflog = false; // �_�ŏI����A�t���O�����Z�b�g
    }

    //�񕜏���
    private void HealPlayer()
    {
        if (fragreceiver.healItemFlag == true)
        {
            currentHP = currentHP + healValeu;

            fragreceiver.healItemFlag = false;
        }
    }

    //�A�[�}�[�񕜏���
    private void ArmorHeal()
    {
        if (fragreceiver.shieldItemFlag == true)
        {
            currentArmor = maxArmor;

            fragreceiver.shieldItemFlag = false;
        }
    }
}
