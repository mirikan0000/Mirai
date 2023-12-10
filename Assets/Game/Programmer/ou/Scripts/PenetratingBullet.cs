using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : Weapon
{
    //�e�ۂ̈ړ����x
    public float move_Speed = 15.0f;
    //�ړ�����
    private Vector3 direction;
    //����܂ł̎���
    public float DestroyTime;
    public int damage = 70; // �e�̃_���[�W
    private void Start()
    {
        transform.Rotate(90f, 0f, 0f, Space.Self);
        //3�b��Ŏ�����j�󂷂�
        Destroy(this.gameObject, DestroyTime);
        direction = transform.TransformDirection(Vector3.up);

    }

    // Update is called once per frame
    private void Update()
    {
        // ��]��������i�Ⴆ�΁AY���𒆐S�ɉ�]�j
        float rotationSpeed = 500.0f;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //�e�۔��˂̈ړ�
        //�ǂƂ�������ړ������ֈړ�����
        transform.position += direction * move_Speed * Time.deltaTime;
    }
}
