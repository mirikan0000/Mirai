using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : Weapon
{
    //���ˋ����𒲐����邽��
    float rangeOffset = 0.0f;
    //�e�ۂ̈ړ����x
    public float move_Speed = 15.0f;
    //�e�ۂ̍���
    Rigidbody rb;
    //�e�ۂ̉摜
    public GameObject Image_Bullet;
 

    //�ړ�����
    private Vector3 direction;
    //����܂ł̎���
    public float DestroyTime;
    public int damage = 20; // �e�̃_���[�W

    private void Start()
    {
        //���̂��擾����
        rb = GetComponent<Rigidbody>();
      
        //���ˋ������擾����(�e�ۗ\�������v�Z���鎞�ɕۑ�����)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //�e�ۑ��x��ۑ�����
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3�b��Ŏ�����j�󂷂�
        Destroy(this.gameObject,DestroyTime);

        //�e�ۂ̉摜���쐬����
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        //�e�q�֌W��ݒ肷��
        image.transform.parent = this.transform;

        direction = transform.forward;
      
    }

    // Update is called once per frame
   private void Update()
    {
       
        //���ˋ��������킹��ׂɎ�����͂�������
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }

        //�e�۔��˂̈ړ�
        //�ǂƂ�������ړ������ֈړ�����
        transform.position += direction * move_Speed * Time.deltaTime;
    }

    int count = 0;
    private void OnCollisionEnter(Collision collision)
    {
        //�ǂƂ�������ړ�������ݒ肷��
        if (collision.gameObject.tag.Equals("Area"))
        {
            Vector3 dir = Vector3.Reflect(direction, collision.GetContact(0).normal);
            direction = dir;
        }
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
          
            // �v���C���[�Ƀ_���[�W��^����
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, collision.gameObject.GetComponent<PlayerHealth>().armorflog);
            // �e�ۂ�����
            Destroy(gameObject);
        }
    }
  
}
