using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //���ˋ����𒲐����邽��
    public float rangeOffset = 0.0f;
    //�e�ۂ̈ړ����x
    public float move_Speed = 15.0f;
    public float BulletLifeTime;
    //�e�ۂ̍���
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //���̂��擾����
        rb = GetComponent<Rigidbody>();

        //���ˋ������擾����(�e�ۗ\�������v�Z���鎞�ɕۑ�����)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //�e�ۑ��x��ۑ�����
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3�b��Ŏ�����j�󂷂�
        Destroy(this.gameObject, BulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //���ˋ��������킹��ׂɎ�����͂�������
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }

        //�e�۔��˂̈ړ�
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));

      
    }
    public int damage = 20; // �e�̃_���[�W

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1")|| collision.gameObject.CompareTag("Player2"))
        {
            // �v���C���[�Ƀ_���[�W��^����
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            // �e�ۂ�����
            Destroy(gameObject);
        }


    }
}
