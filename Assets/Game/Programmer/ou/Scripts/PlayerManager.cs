using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool is_start;
    bool is_aiming;
    bool is_moveable = true;
    float move_speed = 5.0f;

    float rot_angle = 0.1f;
    float gunBarrel_rotSpeed = 0.5f;

    float gun_rotAngle = 0.0f;

    public float Bullet_RangeOffset = 0;
    float BulletPosOffset = 1.0f;

    public GameObject Buttet;
    public GameObject PredictionLine;

    public int PredictionLineNumber = 66;
    List<GameObject> PredictionLine_List = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        is_start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_start)
        {
            if (is_moveable)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(0, 0, move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector3(0, 0, -move_speed * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(new Vector3(0, -rot_angle, 0));
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(new Vector3(0, rot_angle, 0));
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //—\‘ªü
                //“ñ‚Â•û–@(d—ÍAŽOŠpŠÖ”‚Å–Í‹[•ú•¨ü)
                //d—Í
                is_moveable = false;
                is_aiming = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //”­ŽË
                //“ñ‚Â•û–@(d—ÍAŽOŠpŠÖ”‚Å–Í‹[•ú•¨ü)
                //d—Í
                is_moveable = true;
                is_aiming = false;

                for (int i = PredictionLine_List.Count - 1; i >= 0; i--) 
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                GameObject buttle = Instantiate(Buttet, transform.position, transform.rotation);
                buttle.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                buttle.transform.Translate(new Vector3(0, 0, BulletPosOffset));
            }
            if (is_aiming)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
                }

                for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
                {
                    Destroy(PredictionLine_List[i]);
                }
                PredictionLine_List = new List<GameObject>();

                DrewPredictionLine();
            }
        }
    }

    void DrewPredictionLine()
    {
        float angle_y = gun_rotAngle * Mathf.Deg2Rad;
        float angle_xz = transform.eulerAngles.y * Mathf.Deg2Rad;

        float Bullet_Speed = PlayerPrefs.GetFloat("Bullet_Speed");

        for (int i = 0; i < PredictionLineNumber; i++)
        {
            float t = i * 0.05f;
            float X = (BulletPosOffset + Bullet_Speed * t) * Mathf.Cos(angle_y);
            float x = X * Mathf.Sin(angle_xz) + transform.position.x;
            float z = X * Mathf.Cos(angle_xz) + transform.position.z;

            float Bullet_Gravity = Physics2D.gravity.y;
            if ((Physics2D.gravity.y + Bullet_RangeOffset) <= 0)
            {
                Bullet_Gravity += Bullet_RangeOffset;
                PlayerPrefs.SetFloat("Bullet_RangeOffset", Bullet_RangeOffset);
            }
            float y = (BulletPosOffset + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);
            PredictionLine_List.Add(gb);
        }
    }
}