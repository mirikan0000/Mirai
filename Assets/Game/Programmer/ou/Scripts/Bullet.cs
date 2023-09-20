using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float RangeOffset = 0.0f;
    public float Move_Speed = 15.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(PlayerPrefs.HasKey("Bullet_RangeOffset"))
            RangeOffset = PlayerPrefs.GetFloat("Bullet_RangeOffset");

        PlayerPrefs.SetFloat("Bullet_Speed", Move_Speed);

        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (RangeOffset != 0.0f)
            rb.AddForce(new Vector3(0, RangeOffset, 0));
        transform.Translate(new Vector3(0, 0, Move_Speed * Time.deltaTime));
        //Rigidbody.AddForce(new Vector3(0, 0.5f, move_speed));
    }
}
