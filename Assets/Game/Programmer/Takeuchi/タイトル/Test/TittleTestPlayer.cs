using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleTestPlayer : MonoBehaviour
{
    public float tPmoveSpeed;
    private Rigidbody rb;
    [SerializeField] float rot_angle = 1f;
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Transform childTransform = transform.GetChild(0); // 0�͎q�I�u�W�F�N�g�̃C���f�b�N�X
        animator = childTransform.GetComponent<Animator>();

    }

    void Update()
    {  // �L�[���͂Ɋ�Â��Ĉړ��x�N�g�����v�Z
        Vector3 moveVector = Vector3.zero;
        // �v���C���[�̌������擾
        Vector3 playerDirection = transform.forward;
        if (animator)
        {
            animator.SetBool("Walk",true);
        }
      

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += playerDirection;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= playerDirection;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, rot_angle, 0));
           
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -rot_angle, 0));
        }

        // �ړ��x�N�g���ɑ������|���Ĉʒu���X�V
        transform.position += moveVector.normalized * tPmoveSpeed * Time.deltaTime;
    }
}
