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
        Transform childTransform = transform.GetChild(0); // 0は子オブジェクトのインデックス
        animator = childTransform.GetComponent<Animator>();

    }

    void Update()
    {  // キー入力に基づいて移動ベクトルを計算
        Vector3 moveVector = Vector3.zero;
        // プレイヤーの向きを取得
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

        // 移動ベクトルに速さを掛けて位置を更新
        transform.position += moveVector.normalized * tPmoveSpeed * Time.deltaTime;
    }
}
