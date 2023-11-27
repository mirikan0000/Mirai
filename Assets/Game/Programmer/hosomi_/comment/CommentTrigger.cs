using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentTrigger : MonoBehaviour
{
    private PlayerManager_ playermanager;
    private bool isMovingValue;
    public CommentManager commentManager;

    public Camera PlayerCamera;
    public GameObject targetObject;

    public float timeThreshold = 5.0f; // 何秒間座標が変化しないかの閾値
    private Vector3 lastPosition;      // 直前の座標
    private float timer;               // 経過時間
    private bool positionStable;       // 座標が一定時間変化していないかのフラグ

    private Vector3 targetlastPosition;
    private bool targetpositionStable;
    private PlayerManager_ targetplayermanager;
    private bool targetIsMovingValue;
    [SerializeField] private DropPlayer DP;
    [SerializeField] private PlayerHealth PH;
    private bool dropable = false;

    void Start()
    {
        InitializePositionTracking();
        //playermanager = GetComponent<PlayerManager_>();
        //isMovingValue = playermanager.GetIsMoving();
        //targetplayermanager = targetObject.GetComponent<PlayerManager_>();
        //targetIsMovingValue = targetplayermanager.GetIsMoving();
    }

    void Update()
    {
        UpdatePosition();

        if (PlayerCamera != null && targetObject != null)
        {
            // カメラがオブジェクトを映しているかどうかを確認
            if (IsObjectVisibleInCamera())
            {
                commentManager.SetCommentTextWithWeight("エネミーを発見しました！", 9);
            }
            else
            {
                commentManager.SetCommentTextWithWeight("エネミーを探して！", 1);
            }
        }
        //自プレイヤーが動いてない場合
        if (positionStable)
        {
            commentManager.SetCommentTextWithWeight("動いてない！", 1);
        }
        //敵プレイヤーが動いてない場合
        if (targetpositionStable)
        {
            commentManager.SetCommentTextWithWeight("敵動いてない！", 2);
        }
        //物資が出現した場合
        //if (dropable)
        //{
        //    commentManager.SetCommentText("物資来てる！");
        //}
        ////物資を取得した
        //if (DP.speedFlag == true)
        //{
        //    commentManager.SetCommentText("速くなった！");
        //}
        //if (DP.powerFlag == true)
        //{
        //    commentManager.SetCommentText("強くなった！");
        //}
        ////敵が物資を取得した

        //// DropPlayerコンポーネントがアタッチされている場合、そのFlagにアクセス
        //if (DP != null && DP.speedFlag)
        //{
        //    commentManager.SetCommentText("敵が速くなった！");
        //}
        //if (DP != null && DP.powerFlag)
        //{
        //    commentManager.SetCommentText("敵が強くなった！");
        //}
        ////被弾
        //if (PH.hitflog)
        //{
        //    commentManager.SetCommentText("ダメージを受けた！");
        //}

        //// DropPlayerコンポーネントがアタッチされている場合、そのFlagにアクセス
        //if (PH != null && PH.hitflog)
        //{
        //    commentManager.SetCommentText("敵にダメージを与えた！");
        //}

        //移動エネルギーが切れた場合
        //if (!isMovingValue)
        //{
        //    commentManager.SetCommentText("移動エネルギーが切れた！");
        //}
        ////敵の移動エネルギーが切れた場合
        //if (!targetIsMovingValue)
        //{
        //    commentManager.SetCommentText("敵の移動エネルギーが切れた！");
        //}
    }

    private bool IsObjectVisibleInCamera()
    {
        // オブジェクトがカメラのビューフラストに含まれているかどうかをチェック
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        Collider objectCollider = targetObject.GetComponent<Collider>();

        if (objectCollider != null)
        {
            return GeometryUtility.TestPlanesAABB(planes, objectCollider.bounds);
        }

        return false;
    }

    void InitializePositionTracking()
    {
        // 初期化時に座標とタイマーを設定
        lastPosition = transform.position;
        targetlastPosition = targetObject != null ? targetObject.transform.position : Vector3.zero;
        timer = 0f;
        positionStable = false;
        targetpositionStable = false;
    }

    void UpdatePosition()
    {
        if (IsPositionStable())
        {
            // 一定時間座標が変化していない場合の処理
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !positionStable)
            {
                // 一定時間経過したらフラグを立てる
                positionStable = true;
            }
        }
        else
        {
            // 座標が変化した場合はフラグをリセット
            timer = 0f;
            positionStable = false;

            //物資が生成された場合のフラグ
            GameObject dropper = GameObject.FindWithTag("Item");
            if (dropper != null)
            {
                dropable = true;
            }
            dropable = false;
        }

        if (IsTargetPositionStable())
        {
            // 一定時間座標が変化していない場合の処理
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !targetpositionStable)
            {
                // 一定時間経過したらフラグを立てる
                targetpositionStable = true;
            }
        }
        else
        {
            // 座標が変化した場合はフラグをリセット
            timer = 0f;
            targetpositionStable = false;
        }

        // 現在の座標を直前の座標として保存
        lastPosition = transform.position;
        targetlastPosition = targetObject.transform.position;
    }

    bool IsPositionStable()
    {
        // 現在の座標と直前の座標を比較し、一致しているか確認
        return transform.position == lastPosition;
    }

    bool IsTargetPositionStable()
    {
        // targetObjectの座標が一定時間変化していないかを判定
        return targetObject != null && targetObject.transform.position == targetlastPosition;
    }
}