using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : Weapon
{
    //”­Ë‹——£‚ğ’²®‚·‚é‚½‚ß
    float rangeOffset = 0.0f;
    //’eŠÛ‚ÌˆÚ“®‘¬“x
    public float move_Speed = 15.0f;
    //’eŠÛ‚Ì„‘Ì
    Rigidbody rb;
    //’eŠÛ‚Ì‰æ‘œ
    public GameObject Image_Bullet;
 

    //ˆÚ“®•ûŒü
    private Vector3 direction;
    //©‰ó‚Ü‚Å‚ÌŠÔ
    public float DestroyTime;
    public int damage = 20; // ’e‚Ìƒ_ƒ[ƒW

    private void Start()
    {
        //„‘Ì‚ğæ“¾‚·‚é
        rb = GetComponent<Rigidbody>();
      
        //”­Ë‹——£‚ğæ“¾‚·‚é(’eŠÛ—\‘ªü‚ğŒvZ‚·‚é‚É•Û‘¶‚·‚é)
        if (PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //’eŠÛ‘¬“x‚ğ•Û‘¶‚·‚é
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3•bŒã‚Å©•ª‚ğ”j‰ó‚·‚é
        Destroy(this.gameObject,DestroyTime);

        //’eŠÛ‚Ì‰æ‘œ‚ğì¬‚·‚é
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        //eqŠÖŒW‚ğİ’è‚·‚é
        image.transform.parent = this.transform;

        direction = transform.forward;
      
    }

    // Update is called once per frame
   private void Update()
    {
       
        //”­Ë‹——£‚ğ‡‚í‚¹‚éˆ×‚É©•ª‚ğ—Í‚ğ‚ ‚°‚é
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }

        //’eŠÛ”­Ë‚ÌˆÚ“®
        //•Ç‚Æ‚ ‚Á‚½‚çˆÚ“®•ûŒü‚ÖˆÚ“®‚·‚é
        transform.position += direction * move_Speed * Time.deltaTime;
    }

    int count = 0;
    private void OnCollisionEnter(Collision collision)
    {
        //•Ç‚Æ‚ ‚Á‚½‚çˆÚ“®•ûŒü‚ğİ’è‚·‚é
        if (collision.gameObject.tag.Equals("Area"))
        {
            Vector3 dir = Vector3.Reflect(direction, collision.GetContact(0).normal);
            direction = dir;
        }
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
          
            // ƒvƒŒƒCƒ„[‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, collision.gameObject.GetComponent<PlayerHealth>().armorflog);
            // ’eŠÛ‚ğÁ‚·
            Destroy(gameObject);
        }
    }
  
}
