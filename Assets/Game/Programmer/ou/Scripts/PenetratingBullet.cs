using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : Weapon
{
    //’eŠÛ‚ÌˆÚ“®‘¬“x
    public float move_Speed = 15.0f;
    //ˆÚ“®•ûŒü
    private Vector3 direction;
    //©‰ó‚Ü‚Å‚ÌŠÔ
    public float DestroyTime;
    public int damage = 70; // ’e‚Ìƒ_ƒ[ƒW
    private void Start()
    {
        transform.Rotate(90f, 0f, 0f, Space.Self);
        //3•bŒã‚Å©•ª‚ğ”j‰ó‚·‚é
        Destroy(this.gameObject, DestroyTime);
        direction = transform.TransformDirection(Vector3.up);

    }

    // Update is called once per frame
    private void Update()
    {
        // ‰ñ“]‚ğ‰Á‚¦‚éi—á‚¦‚ÎAY²‚ğ’†S‚É‰ñ“]j
        float rotationSpeed = 500.0f;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //’eŠÛ”­Ë‚ÌˆÚ“®
        //•Ç‚Æ‚ ‚Á‚½‚çˆÚ“®•ûŒü‚ÖˆÚ“®‚·‚é
        transform.position += direction * move_Speed * Time.deltaTime;
    }
}
