using UnityEngine;

public class Bulletkari : MonoBehaviour
{
    public int damage = 20; // �e�̃_���[�W

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�Ƀ_���[�W��^����
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage,true);
            // �e�ۂ�����
            Destroy(gameObject);
        }

      
    }
}
