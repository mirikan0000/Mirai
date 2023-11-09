using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentTrigger : MonoBehaviour
{
    public CommentManager commentManager;

    public Camera PlayerCamera;
    public GameObject targetObject;

    private string[] comments = new string[]
    {
        "コメント1",
        "コメント2",
        "コメント3",
    // 追加のコメントをここに記述
    };

    void Start()
    {
        //renderer = targetObject.GetComponent<Renderer>();
    }

    void Update()
    {
        //RandomComment();

        if (PlayerCamera != null && targetObject != null)
        {
            // カメラがオブジェクトを映しているかどうかを確認
            if (IsObjectVisibleInCamera())
            {
                commentManager.SetCommentText("エネミーを発見しました！");
                Debug.Log("エネミーを発見しました！");
            }
            else
            {
                commentManager.SetCommentText("エネミーを探して！");
                Debug.Log("エネミーを探して！");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            commentManager.SetCommentText("Qをおした");
            Debug.Log("ono");
        }
    }

    void RandomComment()
    {
        // ランダムなコメントの選択
        int randomIndex = Random.Range(0, comments.Length);
        string selectedComment = comments[randomIndex];

        commentManager.SetCommentText(selectedComment);
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
}