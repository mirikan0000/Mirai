using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクト拡縮用")]
    public Vector3 preScale;  //初期サイズ
    public float scaleDistance;
    public bool changeScaleFlag;

    //目標サイズ
    public Vector3 postReduScale1;
    public Vector3 postReduScale2;
    public Vector3 postReduScale3;
    public Vector3 postReduScale4;
    public Vector3 postReduScale5;
    public Vector3 postReduScale6;

    private float preScalex, preScaley, preScalez;
    [Header("オブジェクト取得用")]
    GameObject parentObj;        //親オブジェクト
    SaftyZoneV2 parentScript;  //親オブジェクトのスクリプト

    void Start()
    {
        //変数初期化
        VariableInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        //縮小
        if (parentScript.reducationFlag == true)
        {
            if (changeScaleFlag == true)
            {
                //縮小
                ReducationScale();
            }
        }
    }

    //変数初期化
    private void VariableInitialize()
    {
        //サイズを変えるためのフラグ
        changeScaleFlag = false;

        //親オブジェクトとスクリプト取得
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneV2>();

        //目標サイズを設定
        SetPostScale();
    }

    //目標サイズを設定
    private void SetPostScale()
    {
        //親オブジェクトの縮小回数によって
        //サイズを変更したらサイズ変更用フラグをFalse
        changeScaleFlag = false;

    }

    //縮小
    private void ReducationScale()
    {
        //親オブジェクトの縮小回数によって
        switch (parentScript.maxReduStage)
        {

        }
    }

    //拡大
    private void MagnificationScale()
    {

    }
}
