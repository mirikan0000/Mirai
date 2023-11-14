using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    public Vector3 prePos,      //���Z�b�g�p�����ʒu
                   preRotate;   //���Z�b�g�p�����p�x
    public Transform target;    //��]�̒��S�ɂȂ�ʒu
    public float moveSpeed;     //�ړ����x

    void Start()
    {
        //�����ʒu�擾
        prePos = this.transform.position;
        preRotate = this.transform.localEulerAngles;
    }


    void Update()
    {
        //��]������
        CameraMove();
    }

    private void CameraMove()
    {
        //�E��]
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rightAxis = transform.TransformDirection(Vector3.down);
            transform.RotateAround(target.position, rightAxis, moveSpeed * Time.deltaTime);
        }
        //����]
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 leftAxis = transform.TransformDirection(Vector3.up);
            transform.RotateAround(target.position, leftAxis, moveSpeed * Time.deltaTime);
        }
        //���]
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 upAxis = transform.TransformDirection(Vector3.right);
            transform.RotateAround(target.position, upAxis, moveSpeed * Time.deltaTime);
        }
        //����]
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 downAxis = transform.TransformDirection(Vector3.left);
            transform.RotateAround(target.position, downAxis, moveSpeed * Time.deltaTime);
        }

        //�ʒu���Z�b�g
        if (Input.GetKey(KeyCode.R))
        {
            this.transform.position = prePos;
            this.transform.eulerAngles = preRotate;
        }
    }
}
