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

        //’eŠÛ‚Ì‰æ‘œ‚ğì¬
        GameObject image = Instantiate(Image_Bullet, transform.position, Quaternion.identity);
        image.transform.parent = this.transform;
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
        transform.Translate(new Vector3(0, 0, move_Speed * Time.deltaTime));
    }
}
