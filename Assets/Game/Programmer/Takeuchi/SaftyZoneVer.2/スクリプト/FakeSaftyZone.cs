using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FakeSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("�g�k�p")]
    public bool setScaleFlag;    //�g�k��̖ڕW�T�C�Y��ݒ肷�邽�߂̃t���O
    public bool moveFlag;        //�g�k�p�t���O

    [Header("�ڕW�T�C�Y")]
    //�k����
    public Vector3 postReduScale1;  //�����u�̃T�C�Y
    public Vector3 postReduScale2;  //�����u�̃T�C�Y
    public Vector3 postReduScale3;  //��O���u�̃T�C�Y
    public Vector3 postReduScale4;  //��l���u�̃T�C�Y
    public Vector3 postReduScale5;  //��܈��u�̃T�C�Y
    public Vector3 postReduScale6;  //��Z���u�̃T�C�Y
    //�g�厞
    public Vector3 postMagScale1;   //�����u�̃T�C�Y
    public Vector3 postMagScale2;   //�����u�̃T�C�Y
    public Vector3 postMagScale3;   //��O���u�̃T�C�Y
    public Vector3 postMagScale4;   //��l���u�̃T�C�Y
    public Vector3 postMagScale5;   //��܈��u�̃T�C�Y
    public Vector3 postMagScale6;   //��Z���u�̃T�C�Y

    public Vector3 preScale;        //���u�̏����T�C�Y
    private float preScalex, preScaley, preScalez;

    [Header("�I�u�W�F�N�g�擾�p")]
    private GameObject parentObj;      //�e�I�u�W�F�N�g
    private SaftyZoneV2 parentScript;  //�e�I�u�W�F�N�g�̃X�N���v�g

    void Start()
    {
        //�e�I�u�W�F�N�g�擾
        GetParent();  //�e�I�u�W�F�N�g�A�e�I�u�W�F�N�g�̃X�N���v�g�A�e�I�u�W�F�N�g�̃T�C�Y

        //�����T�C�Y�擾
        GetPreScale();

        //�ڕW�T�C�Y������
        PostScaleInitialize();

        //�t���O������
        setScaleFlag = true;
        moveFlag = true;

        //�ڕW�T�C�Y��ݒ�
        if (setScaleFlag == true)
        {
            SetPostReduScale();
        }

    }


    void Update()
    {
        //�T�C�Y�ύX
        if (moveFlag == true)
        {
            ChangeScale();
        }

        //�g��i�K�̎��ɍ폜
        if (parentScript.magnificationFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    //�ڕW�T�C�Y������
    private void PostScaleInitialize()
    {
        //�k����̖ڕW�T�C�Y
        postReduScale1 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale4 = new Vector3(0.0f, 0.0f, 0.0f);
        postReduScale2 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale5 = new Vector3(0.0f, 0.0f, 0.0f);
        postReduScale3 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale6 = new Vector3(0.0f, 0.0f, 0.0f);

        //�g���̖ڕW�T�C�X
        postMagScale1 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale4 = new Vector3(0.0f, 0.0f, 0.0f);
        postMagScale2 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale5 = new Vector3(0.0f, 0.0f, 0.0f);
        postMagScale3 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale6 = new Vector3(0.0f, 0.0f, 0.0f);
    }

    //�e�I�u�W�F�N�g�擾
    private void GetParent()
    {
        //�e�I�u�W�F�N�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneV2>();
    }

    //�����T�C�Y�擾
    private void GetPreScale()
    {
        preScalex = this.transform.localScale.x;
        preScaley = this.transform.localScale.y;
        preScalez = this.transform.localScale.z;
        preScale = new Vector3(preScalex, preScaley, preScalez);
    }

    //�ڕW�T�C�Y��ݒ�
    private void SetPostReduScale()
    {
        //�k���񐔂ɂ���ĖڕW�ʒu��ݒ�
        switch (parentScript.maxReduStage)
        {
            case 1.0f:  //���̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = preScale;
                //�ݒ�p�t���O�Ǘ�
                setScaleFlag = false;
                break;
            case 2.0f:  //���̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(5.0f, preScaley, 5.0f);
                postReduScale2 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = postReduScale1;
                postMagScale2 = preScale;

                setScaleFlag = false;
                break;
            case 3.0f:  //�O��̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(6.4f, preScaley, 6.4f);
                postReduScale2 = new Vector3(3.8f, preScaley, 3.8f);
                postReduScale3 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = postReduScale2;
                postMagScale2 = postReduScale1;
                postMagScale3 = preScale;

                setScaleFlag = false;
                break;
            case 4.0f:  //�l��̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(7.0f, preScaley, 7.0f);
                postReduScale2 = new Vector3(5.0f, preScaley, 5.0f);
                postReduScale3 = new Vector3(3.0f, preScaley, 3.0f);
                postReduScale4 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = postReduScale3;
                postMagScale2 = postReduScale2;
                postMagScale3 = postReduScale1;
                postMagScale4 = preScale;

                setScaleFlag = false;
                break;
            case 5.0f:  //�܉�̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(7.4f, preScaley, 7.4f);
                postReduScale2 = new Vector3(5.8f, preScaley, 5.8f);
                postReduScale3 = new Vector3(4.2f, preScaley, 4.2f);
                postReduScale4 = new Vector3(2.6f, preScaley, 2.6f);
                postReduScale5 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = postReduScale4;
                postMagScale2 = postReduScale3;
                postMagScale3 = postReduScale2;
                postMagScale4 = postReduScale1;
                postMagScale5 = preScale;

                setScaleFlag = false;
                break;
            case 6.0f:  //�Z��̎�
                //�k���ڕW�ʒu
                postReduScale1 = new Vector3(7.7f, preScaley, 7.7f);
                postReduScale2 = new Vector3(6.4f, preScaley, 6.4f);
                postReduScale3 = new Vector3(5.1f, preScaley, 5.1f);
                postReduScale4 = new Vector3(3.8f, preScaley, 3.8f);
                postReduScale5 = new Vector3(2.5f, preScaley, 2.5f);
                postReduScale6 = new Vector3(1.0f, preScaley, 1.0f);

                //�g��ڕW�ʒu
                postMagScale1 = postReduScale5;
                postMagScale2 = postReduScale4;
                postMagScale3 = postReduScale3;
                postMagScale4 = postReduScale2;
                postMagScale5 = postReduScale1;
                postMagScale6 = preScale;

                setScaleFlag = false;
                break;
        }
    }

    //�T�C�Y�ύX
    private void ChangeScale()
    {
        //�e�I�u�W�F�N�g�̍ő�g�k�i�K���ɉ����ď���
        switch (parentScript.maxReduStage)
        {
            case 1.0f:  //�k���񐔂����̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                break;
            case 2.0f:  //���̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
               else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                break;
            case 3.0f:  //�O��̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                else if (parentScript.nextReduStage == 3.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale3;
                }
                break;
            case 4.0f:  //�l��̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                else if (parentScript.nextReduStage == 3.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale3;
                }
                else if (parentScript.nextReduStage == 4.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale4;
                }
                break;
            case 5.0f:  //�܉�̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                else if (parentScript.nextReduStage == 3.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale3;
                }
                else if (parentScript.nextReduStage == 4.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale4;
                }
                else if (parentScript.nextReduStage == 5.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale5;
                }
                break;
            case 6.0f:  //�Z��̎�
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                else if (parentScript.nextReduStage == 3.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale3;
                }
                else if (parentScript.nextReduStage == 4.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale4;
                }
                else if (parentScript.nextReduStage == 5.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale5;
                }
                else if (parentScript.nextReduStage == 6.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale6;
                }
                break;
        }
    }
}
