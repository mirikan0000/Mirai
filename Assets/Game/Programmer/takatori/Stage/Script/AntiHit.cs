using UnityEngine;

public class AntiHit : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] [Header("�t�B�[���h�_���[�W")] private int FieldDamage = 5;
    [SerializeField] private PlayerHealth playerHealth1P;
    [SerializeField] private PlayerHealth playerHealth2P;

    // �O��_���[�W��^��������
    private float lastDamageTime;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            TryApplyDamage(playerHealth1P);
        }
        else if (other.CompareTag("Player2"))
        {
            TryApplyDamage(playerHealth2P);
        }
    }

    void Start()
    {
        playerHealth1P = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        playerHealth2P = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
        lastDamageTime = Time.time; // �Q�[���J�n���ɏ�����
    }

    void TryApplyDamage(PlayerHealth playerHealth)
    {
        // 5�b���ƂɃ_���[�W��^����
        if (Time.time - lastDamageTime >= 5f)
        {
            // �_���[�W��^����
            playerHealth.TakeDamage(FieldDamage, false);

            // �O��_���[�W��^�������Ԃ��X�V
            lastDamageTime = Time.time;
        }
    }
}
