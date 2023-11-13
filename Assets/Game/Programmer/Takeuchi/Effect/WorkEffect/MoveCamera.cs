using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    public Vector3 prePos,      //リセット用初期位置
                   preRotate;   //リセット用初期角度
    public Transform target;    //回転の中心になる位置
    public float moveSpeed;     //移動速度

    void Start()
    {
        //初期位置取得
        prePos = this.transform.position;
        preRotate = this.transform.localEulerAngles;
    }


    void Update()
    {
        //回転させる
        CameraMove();
    }

    private void CameraMove()
    {
        //右回転
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rightAxis = transform.TransformDirection(Vector3.down);
            transform.RotateAround(target.position, rightAxis, moveSpeed * Time.deltaTime);
        }
        //左回転
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 leftAxis = transform.TransformDirection(Vector3.up);
            transform.RotateAround(target.position, leftAxis, moveSpeed * Time.deltaTime);
        }
        //上回転
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 upAxis = transform.TransformDirection(Vector3.right);
            transform.RotateAround(target.position, upAxis, moveSpeed * Time.deltaTime);
        }
        //下回転
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 downAxis = transform.TransformDirection(Vector3.left);
            transform.RotateAround(target.position, downAxis, moveSpeed * Time.deltaTime);
        }

        //位置リセット
        if (Input.GetKey(KeyCode.R))
        {
            this.transform.position = prePos;
            this.transform.eulerAngles = preRotate;
        }
    }
}
