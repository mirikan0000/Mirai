using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : MonoBehaviour
{
    bool isUpdateData = false;
    //”­Ë‹——£‚ğ’²®‚·‚é‚½‚ß
    float rangeOffset = 0.0f;
    //’eŠÛ‚ÌˆÚ“®‘¬“x
    public float move_Speed = 15.0f;
    //’eŠÛ”­Ë‚Ì‰ñ“]Šp“x
    float gun_rotAngle = 0.0f;
    //’eŠÛ”­Ë‚Ì‰Šú‘¬“x
    float move_SpeedZ = 0.0f;
    float move_SpeedY = 0.0f;
    //’eŠÛ‚Ì„‘Ì
    Rigidbody rb;
    //ŠÔ
    float timer = 0.0f;

    //‘O‚ÌŠp“x
    float lastAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isUpdateData = true;

        //„‘Ì‚ğæ“¾‚·‚é
        rb = GetComponent<Rigidbody>();

        //”­Ë‹——£‚ğæ“¾‚·‚é(’eŠÛ—\‘ªü‚ğŒvZ‚·‚é‚É•Û‘¶‚·‚é)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //’eŠÛ‘¬“x‚ğ•Û‘¶‚·‚é
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3•bŒã‚Å©•ª‚ğ”j‰ó‚·‚é
        Destroy(this.gameObject, 3f);
    }

    void UpdateData()
    {
        //’eŠÛ”­Ë‚Ì‰ñ“]Šp“x‚ğŒvZ‚·‚é
        gun_rotAngle = 360.0f - this.transform.localEulerAngles.x;
        //c•ûŒü‚Ì‰Šú‘¬“x
        move_SpeedY = gun_rotAngle == 360.0f ? 0.0f : Mathf.Sin(gun_rotAngle * Mathf.Deg2Rad) * move_Speed;
        //‰¡•ûŒü‚Ì‰Šú‘¬“x
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

        //”­Ë‹——£‚ğ‡‚í‚¹‚éˆ×‚É©•ª‚ğ—Í‚ğ‚ ‚°‚é
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }
        //’eŠÛ”­Ë‚ÌˆÚ“®
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));

        //ŠÔ‚ğŒvZ‚·‚é
        timer += Time.deltaTime;

        //c•ûŒü‚Ì‘¬“x
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


        //‰ñ“]
        transform.Rotate(new Vector3(angle, 0, 0));
    }
}
