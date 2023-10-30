using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //”­Ë‹——£‚ğ’²®‚·‚é‚½‚ß
    float rangeOffset = 0.0f;
    //’eŠÛ‚ÌˆÚ“®‘¬“x
    public float move_Speed = 15.0f;
    //’eŠÛ‚Ì„‘Ì
    Rigidbody rb;
    //’eŠÛ‚Ì‰æ‘œ
    public GameObject Image_Bullet;

    //“–‚½‚é‚©‚Ç‚¤‚©
    private bool IsCollision;
    //ˆÚ“®•ûŒü
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //„‘Ì‚ğæ“¾‚·‚é
        rb = GetComponent<Rigidbody>();

        //”­Ë‹——£‚ğæ“¾‚·‚é(’eŠÛ—\‘ªü‚ğŒvZ‚·‚é‚É•Û‘¶‚·‚é)
        if(PlayerPrefs.HasKey("Bullet_RangeOffset"))
            rangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        //’eŠÛ‘¬“x‚ğ•Û‘¶‚·‚é
        PlayerPrefs.SetFloat("Bullet_Speed", move_Speed);

        //3•bŒã‚Å©•ª‚ğ”j‰ó‚·‚é
        Destroy(this.gameObject, 3f);

        //’eŠÛ‚Ì‰æ‘œ‚ğì¬‚·‚é
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        //eqŠÖŒW‚ğİ’è‚·‚é
        image.transform.parent = this.transform;

        //ˆÚ“®•ûŒü‚ğ‰Šú‰»‚·‚é
        IsCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //”­Ë‹——£‚ğ‡‚í‚¹‚éˆ×‚É©•ª‚ğ—Í‚ğ‚ ‚°‚é
        if (rangeOffset != 0.0f)
        {
            rb.AddForce(new Vector3(0, rangeOffset * Time.deltaTime, 0));
        }

        //’eŠÛ”­Ë‚ÌˆÚ“®
        //•Ç‚Æ‚ ‚Á‚½‚çˆÚ“®•ûŒü‚ÖˆÚ“®‚·‚é
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
        //•Ç‚Æ‚ ‚Á‚½‚çˆÚ“®•ûŒü‚ğİ’è‚·‚é
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Vector3 dir = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            direction = dir;

            IsCollision = true;
        }
    }
}
