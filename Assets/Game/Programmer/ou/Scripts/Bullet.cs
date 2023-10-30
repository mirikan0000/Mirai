using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //���ˋ����𒲐����邽��
    float rangeOffset = 0.0f;
    //�e�ۂ̈ړ����x
    public float move_Speed = 15.0f;
    //�e�ۂ̍���
    Rigidbody rb;
    //�e�ۂ̉摜
    public GameObject Image_Bullet;

    //�����邩�ǂ���
    private bool IsCollision;
    //�ړ�����
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //���̂��擾����
        rb = GetComponent<Rigidbody>();

        //���ˋ������擾����(�e�ۗ\�������v�Z���鎞�ɕۑ�����)
        if(PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //�e�ۑ��x��ۑ�����
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3�b��Ŏ�����j�󂷂�
        Destroy(this.gameObject, 3f);

        //�e�ۂ̉摜���쐬����
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        //�e�q�֌W��ݒ肷��
        image.transform.parent = this.transform;

        //�ړ�����������������
        IsCollision = false;
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
        //�ǂƂ�������ړ������ֈړ�����
        if (IsCollision)
        {
            //transform.Translate(new Vector3(direction));

            transform.position += direction * move_Speed * Time.deltaTime;
            return;
        }
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�ǂƂ�������ړ�������ݒ肷��
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Vector3 dir = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            direction = dir;

            IsCollision = true;
        }
    }
}
