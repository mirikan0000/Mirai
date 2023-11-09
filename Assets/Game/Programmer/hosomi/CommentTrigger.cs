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
        "�R�����g1",
        "�R�����g2",
        "�R�����g3",
    // �ǉ��̃R�����g�������ɋL�q
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
            // �J�������I�u�W�F�N�g���f���Ă��邩�ǂ������m�F
            if (IsObjectVisibleInCamera())
            {
                commentManager.SetCommentText("�G�l�~�[�𔭌����܂����I");
                Debug.Log("�G�l�~�[�𔭌����܂����I");
            }
            else
            {
                commentManager.SetCommentText("�G�l�~�[��T���āI");
                Debug.Log("�G�l�~�[��T���āI");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            commentManager.SetCommentText("Q��������");
            Debug.Log("ono");
        }
    }

    void RandomComment()
    {
        // �����_���ȃR�����g�̑I��
        int randomIndex = Random.Range(0, comments.Length);
        string selectedComment = comments[randomIndex];

        commentManager.SetCommentText(selectedComment);
    }

    private bool IsObjectVisibleInCamera()
    {
        // �I�u�W�F�N�g���J�����̃r���[�t���X�g�Ɋ܂܂�Ă��邩�ǂ������`�F�b�N
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        Collider objectCollider = targetObject.GetComponent<Collider>();

        if (objectCollider != null)
        {
            return GeometryUtility.TestPlanesAABB(planes, objectCollider.bounds);
        }

        return false;
    }
}