using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FakeSaftyZone : MonoBehaviour
{
    [SerializeField]
    [Header("拡縮用")]
    public bool setScaleFlag;    //拡縮後の目標サイズを設定するためのフラグ
    public bool moveFlag;        //拡縮用フラグ

    [Header("目標サイズ")]
    //縮小時
    public Vector3 postReduScale1;  //第一安置のサイズ
    public Vector3 postReduScale2;  //第二安置のサイズ
    public Vector3 postReduScale3;  //第三安置のサイズ
    public Vector3 postReduScale4;  //第四安置のサイズ
    public Vector3 postReduScale5;  //第五安置のサイズ
    public Vector3 postReduScale6;  //第六安置のサイズ
    //拡大時
    public Vector3 postMagScale1;   //第一安置のサイズ
    public Vector3 postMagScale2;   //第二安置のサイズ
    public Vector3 postMagScale3;   //第三安置のサイズ
    public Vector3 postMagScale4;   //第四安置のサイズ
    public Vector3 postMagScale5;   //第五安置のサイズ
    public Vector3 postMagScale6;   //第六安置のサイズ

    public Vector3 preScale;        //安置の初期サイズ
    private float preScalex, preScaley, preScalez;

    [Header("オブジェクト取得用")]
    private GameObject parentObj;      //親オブジェクト
    private SaftyZoneV2 parentScript;  //親オブジェクトのスクリプト

    void Start()
    {
        //親オブジェクト取得
        GetParent();  //親オブジェクト、親オブジェクトのスクリプト、親オブジェクトのサイズ

        //初期サイズ取得
        GetPreScale();

        //目標サイズ初期化
        PostScaleInitialize();

        //フラグ初期化
        setScaleFlag = true;
        moveFlag = true;

        //目標サイズを設定
        if (setScaleFlag == true)
        {
            SetPostReduScale();
        }

    }


    void Update()
    {
        //サイズ変更
        if (moveFlag == true)
        {
            ChangeScale();
        }

        //拡大段階の時に削除
        if (parentScript.magnificationFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    //目標サイズ初期化
    private void PostScaleInitialize()
    {
        //縮小後の目標サイズ
        postReduScale1 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale4 = new Vector3(0.0f, 0.0f, 0.0f);
        postReduScale2 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale5 = new Vector3(0.0f, 0.0f, 0.0f);
        postReduScale3 = new Vector3(0.0f, 0.0f, 0.0f); postReduScale6 = new Vector3(0.0f, 0.0f, 0.0f);

        //拡大後の目標サイス
        postMagScale1 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale4 = new Vector3(0.0f, 0.0f, 0.0f);
        postMagScale2 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale5 = new Vector3(0.0f, 0.0f, 0.0f);
        postMagScale3 = new Vector3(0.0f, 0.0f, 0.0f); postMagScale6 = new Vector3(0.0f, 0.0f, 0.0f);
    }

    //親オブジェクト取得
    private void GetParent()
    {
        //親オブジェクト取得
        parentObj = transform.parent.gameObject;
        parentScript = parentObj.GetComponent<SaftyZoneV2>();
    }

    //初期サイズ取得
    private void GetPreScale()
    {
        preScalex = this.transform.localScale.x;
        preScaley = this.transform.localScale.y;
        preScalez = this.transform.localScale.z;
        preScale = new Vector3(preScalex, preScaley, preScalez);
    }

    //目標サイズを設定
    private void SetPostReduScale()
    {
        //縮小回数によって目標位置を設定
        switch (parentScript.maxReduStage)
        {
            case 1.0f:  //一回の時
                //縮小目標位置
                postReduScale1 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
                postMagScale1 = preScale;
                //設定用フラグ管理
                setScaleFlag = false;
                break;
            case 2.0f:  //二回の時
                //縮小目標位置
                postReduScale1 = new Vector3(5.0f, preScaley, 5.0f);
                postReduScale2 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
                postMagScale1 = postReduScale1;
                postMagScale2 = preScale;

                setScaleFlag = false;
                break;
            case 3.0f:  //三回の時
                //縮小目標位置
                postReduScale1 = new Vector3(6.4f, preScaley, 6.4f);
                postReduScale2 = new Vector3(3.8f, preScaley, 3.8f);
                postReduScale3 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
                postMagScale1 = postReduScale2;
                postMagScale2 = postReduScale1;
                postMagScale3 = preScale;

                setScaleFlag = false;
                break;
            case 4.0f:  //四回の時
                //縮小目標位置
                postReduScale1 = new Vector3(7.0f, preScaley, 7.0f);
                postReduScale2 = new Vector3(5.0f, preScaley, 5.0f);
                postReduScale3 = new Vector3(3.0f, preScaley, 3.0f);
                postReduScale4 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
                postMagScale1 = postReduScale3;
                postMagScale2 = postReduScale2;
                postMagScale3 = postReduScale1;
                postMagScale4 = preScale;

                setScaleFlag = false;
                break;
            case 5.0f:  //五回の時
                //縮小目標位置
                postReduScale1 = new Vector3(7.4f, preScaley, 7.4f);
                postReduScale2 = new Vector3(5.8f, preScaley, 5.8f);
                postReduScale3 = new Vector3(4.2f, preScaley, 4.2f);
                postReduScale4 = new Vector3(2.6f, preScaley, 2.6f);
                postReduScale5 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
                postMagScale1 = postReduScale4;
                postMagScale2 = postReduScale3;
                postMagScale3 = postReduScale2;
                postMagScale4 = postReduScale1;
                postMagScale5 = preScale;

                setScaleFlag = false;
                break;
            case 6.0f:  //六回の時
                //縮小目標位置
                postReduScale1 = new Vector3(7.7f, preScaley, 7.7f);
                postReduScale2 = new Vector3(6.4f, preScaley, 6.4f);
                postReduScale3 = new Vector3(5.1f, preScaley, 5.1f);
                postReduScale4 = new Vector3(3.8f, preScaley, 3.8f);
                postReduScale5 = new Vector3(2.5f, preScaley, 2.5f);
                postReduScale6 = new Vector3(1.0f, preScaley, 1.0f);

                //拡大目標位置
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

    //サイズ変更
    private void ChangeScale()
    {
        //親オブジェクトの最大拡縮段階数に応じて処理
        switch (parentScript.maxReduStage)
        {
            case 1.0f:  //縮小回数が一回の時
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
                break;
            case 2.0f:  //二回の時
                if (parentScript.nextReduStage == 1.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale1;
                }
               else if (parentScript.nextReduStage == 2.0f && parentScript.reducationFlag == true)
                {
                    this.transform.localScale = postReduScale2;
                }
                break;
            case 3.0f:  //三回の時
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
            case 4.0f:  //四回の時
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
            case 5.0f:  //五回の時
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
            case 6.0f:  //六回の時
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
