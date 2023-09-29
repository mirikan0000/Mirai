using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("�v���C���[���ʗp")]
    public PlayerNum playerNum;
    public enum PlayerNum
    {
        Player1,Player2
    }
    [Header("���݈ʒu�̃}�b�v�ԍ�")]
    public int playerMapNum;

    public void ParentPlayerInitialize()
    {
        playerMapNum = 0;
    }

    public void ParentPlayerUpdate()
    {
        //����p�֐�
        PlayerMove();
    }

    //�v���C���[����p�֐�(�I�[�o�[���C�h����)
    public virtual void PlayerMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //��
        {
            transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))  //��
        {
            transform.position -= new Vector3(0, 0, 3) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))  //��
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))  //�E
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
    }

    //����̃G���A�ɓ�������
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnterArea");

        //Area�^�O���t���Ă��邩
        if (other.gameObject.CompareTag("Area"))
        {
            //�X�N���v�g�擾
            MapArea otherMapAreaScript = other.gameObject.GetComponent<MapArea>();

            //�擾�ł�����
            if (otherMapAreaScript != null)
            {
                //AreaNum�ɂ����Player�̌��݂̃}�b�v�ԍ����X�V
                switch (otherMapAreaScript.areaNum)
                {
                    case MapArea.AreaNum.Area1:
                        playerMapNum = 1;
                        break;
                    case MapArea.AreaNum.Area2:
                        playerMapNum = 2;
                        break;
                    case MapArea.AreaNum.Area3:
                        playerMapNum = 3;
                        break;
                    case MapArea.AreaNum.Area4:
                        playerMapNum = 4;
                        break;
                    case MapArea.AreaNum.Area5:
                        playerMapNum = 5;
                        break;
                    case MapArea.AreaNum.Area6:
                        playerMapNum = 6;
                        break;
                    case MapArea.AreaNum.Area7:
                        playerMapNum = 7;
                        break;
                    case MapArea.AreaNum.Area8:
                        playerMapNum = 8;
                        break;
                    case MapArea.AreaNum.Area9:
                        playerMapNum = 9;
                        break;
                }

                //Debug.Log(nowMapNum);
            }
        }
    }
}
