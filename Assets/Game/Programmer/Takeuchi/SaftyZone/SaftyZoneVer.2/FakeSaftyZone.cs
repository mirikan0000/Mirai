using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�g�k�p")]
    public Vector3 preScale;  //�����T�C�Y
    public float scaleDistance;
    public bool changeScaleFlag;

    //�ڕW�T�C�Y
    public Vector3 postReduScale1;
    public Vector3 postReduScale2;
    public Vector3 postReduScale3;
    public Vector3 postReduScale4;
    public Vector3 postReduScale5;
    public Vector3 postReduScale6;

    private float preScalex, preScaley, preScalez;
    [Header("�I�u�W�F�N�g�擾�p")]
    GameObject parentObj;        //�e�I�u�W�F�N�g
    SaftyZoneV2 parentScript;  //�e�I�u�W�F�N�g�̃X�N���v�g

    void Start()
    {
        //�ϐ�������
        VariableInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        //�k��
        if (parentScript.reducationFlag == true)
        {
            if (changeScaleFlag == true)
            {
                //�k��
                ReducationScale();
            }
        }
    }

    //�ϐ�������
    private void VariableInitialize()
    {
        //�T�C�Y��ς��邽�߂̃t���O
        changeScaleFlag = false;

        //�e�I�u�W�F�N�g�ƃX�N���v�g�擾
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneV2>();

        //�ڕW�T�C�Y��ݒ�
        SetPostScale();
    }

    //�ڕW�T�C�Y��ݒ�
    private void SetPostScale()
    {
        //�e�I�u�W�F�N�g�̏k���񐔂ɂ����
        //�T�C�Y��ύX������T�C�Y�ύX�p�t���O��False
        changeScaleFlag = false;

    }

    //�k��
    private void ReducationScale()
    {
        //�e�I�u�W�F�N�g�̏k���񐔂ɂ����
        switch (parentScript.maxReduStage)
        {

        }
    }

    //�g��
    private void MagnificationScale()
    {

    }
}
