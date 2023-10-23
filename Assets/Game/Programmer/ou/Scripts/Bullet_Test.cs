using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : MonoBehaviour
{
    bool isUpdateData = false;
    //���ˋ����𒲐����邽��
    float rangeOffset = 0.0f;
    //�e�ۂ̈ړ����x
    public float move_Speed = 15.0f;
    //�e�۔��ˎ��̉�]�p�x
    float gun_rotAngle = 0.0f;
    //�e�۔��ˎ��̏������x
    float move_SpeedZ = 0.0f;
    float move_SpeedY = 0.0f;
    //�e�ۂ̍���
    Rigidbody rb;
    //����
    float timer = 0.0f;

    //�O�̊p�x
    float lastAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isUpdateData = true;

        //���̂��擾����
        rb = GetComponent<Rigidbody>();

        //���ˋ������擾����(�e�ۗ\�������v�Z���鎞�ɕۑ�����)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //�e�ۑ��x��ۑ�����
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3�b��Ŏ�����j�󂷂�
        Destroy(this.gameObject, 3f);
    }

    void UpdateData()
    {
        //�e�۔��ˎ��̉�]�p�x���v�Z����
        gun_rotAngle = 360.0f - this.transform.localEulerAngles.x;
        //�c�����̏������x
        move_SpeedY = gun_rotAngle == 360.0f ? 0.0f : Mathf.Sin(gun_rotAngle * Mathf.Deg2Rad) * move_Speed;
        //�������̏������x
        move_SpeedZ = gun_rotAngle == 90.0f ? 0.0f : Mathf.Cos(gun_rotAngle * Mathf.Deg2Rad) * move_Speed;

        lastAngle = gun_rotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpdateData)
        {
            UpdateData();
            isUpdateData = false;
        }

        //���ˋ��������킹��ׂɎ�����͂�������
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }
        //�e�۔��˂̈ړ�
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));

        //���Ԃ��v�Z����
        timer += Time.deltaTime;

        //�c�����̑��x
        float speedY = move_SpeedY + timer * (Physics2D.gravity.y + rangeOffset);

        float rot_ByGravity = Mathf.Atan2(speedY, move_SpeedZ) * Mathf.Rad2Deg;

        float angle = lastAngle - rot_ByGravity;

        //Debug
        Debug.Log("Time.deltaTime : " + Time.deltaTime);
        Debug.Log("gun_rotAngle : " + gun_rotAngle);
        Debug.Log("lastAngle : " + lastAngle);
        Debug.Log("rot_ByGravity : " + rot_ByGravity);
        Debug.Log("Angle : " + angle);

        lastAngle = rot_ByGravity;


        //��]
        transform.Rotate(new Vector3(angle, 0, 0));
    }
}
