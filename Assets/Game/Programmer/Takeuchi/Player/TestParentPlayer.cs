using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TestParentPlayer : MonoBehaviour
{
    [SerializeField]
    [Header("�v���C���[���ʗp")]
    public TestPlayerNum testPlayerNum;
    public enum TestPlayerNum
    {
        TestPlayer1,TestPlayer2
    }
    public int testPlayerMapNum;
    
    public void TestParentPlayerInitialize()
    {
        testPlayerMapNum = 0;
    }

    public void TestParentPlayerUpdate()
    {
        //����p�֐�
        TestPlayerMove();
    }

    //�v���C���[����p�֐�(�I�[�o�[���C�h����)
    public virtual void TestPlayerMove()
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
                        testPlayerMapNum = 1;
                        break;
                    case MapArea.AreaNum.Area2:
                        testPlayerMapNum = 2;
                        break;
                    case MapArea.AreaNum.Area3:
                        testPlayerMapNum = 3;
                        break;
                    case MapArea.AreaNum.Area4:
                        testPlayerMapNum = 4;
                        break;
                    case MapArea.AreaNum.Area5:
                        testPlayerMapNum = 5;
                        break;
                    case MapArea.AreaNum.Area6:
                        testPlayerMapNum = 6;
                        break;
                    case MapArea.AreaNum.Area7:
                        testPlayerMapNum = 7;
                        break;
                    case MapArea.AreaNum.Area8:
                        testPlayerMapNum = 8;
                        break;
                    case MapArea.AreaNum.Area9:
                        testPlayerMapNum = 9;
                        break;
                }

                //Debug.Log(nowMapNum);
            }
        }
    }
}
