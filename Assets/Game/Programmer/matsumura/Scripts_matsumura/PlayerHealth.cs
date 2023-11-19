using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("�̗̓Q�[�W")]
    public int maxHP = 100; // �ő�HP
    private int currentHP;  // ���݂�HP

    //11/20�ǉ���
    [SerializeField] private int healValeu;         //�񕜗�
    [SerializeField] private GameObject shieldObj;  //�V�[���h�̃I�u�W�F�N�g
    private bool shieldFlag = false;

    [Header("�A�[�}�[�Q�[�W")]
    public int maxArmor = 50; // �ő�A�[�}�[
    private int currentArmor; // ���݂̃A�[�}�[
    public bool armorflog;    
    [Header("���V�[���J�ڎ�_�ǂݍ��݃V�[�����X�g")]
    public string loadSceneName;
    public bool isEnd;
    public List<string> unLoadSceneNameList;
    public bool hitflog; //�R�����g�V�X�e���p�̃t���O

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        currentHP = maxHP; // ����HP��ݒ�
        currentArmor = maxArmor; // �����A�[�}�[��ݒ�
        isEnd = false;
    }

    private void Update()
    {
        hitflog = false;
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
        }

        hitflog = true;

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
        isEnd = true;
    }

    //�A�C�e���擾������
    private void OnCollisionEnter(Collision collision)
    {
        //�񕜃A�C�e�����擾������
        if (collision.gameObject.name == "HealItem(Clone)")
        {
            if (currentHP < maxHP)
            {
                currentHP = currentHP + healValeu;
            }
        }

        //�V�[���h�A�C�e�����擾������
        if (collision.gameObject.name == "ShieldItem(Clone)")
        {
            shieldFlag = true;
            //var parent = this.transform;

            ////�V�[���h���q�I�u�W�F�N�g�Ƃ��Đ���
            //Instantiate(shieldObj, this.gameObject.transform.position, Quaternion.identity, parent);
        }
    }
}
