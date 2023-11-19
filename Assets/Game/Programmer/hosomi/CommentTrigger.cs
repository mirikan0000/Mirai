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

    public float timeThreshold = 5.0f; // ���b�ԍ��W���ω����Ȃ�����臒l
    private Vector3 lastPosition;      // ���O�̍��W
    private float timer;               // �o�ߎ���
    private bool positionStable;       // ���W����莞�ԕω����Ă��Ȃ����̃t���O

    private Vector3 targetlastPosition;
    private bool targetpositionStable;
    private PlayerManager_ targetplayermanager;
    private bool targetIsMovingValue;
    [SerializeField] private DropPlayer DP;
    [SerializeField] private PlayerHealth PH;
    private bool dropable = false;

    private string[] comments = new string[]
    {
        "�R�����g1",
        "�R�����g2",
        "�R�����g3",
    // �ǉ��̃R�����g�������ɋL�q
    };

    void Start()
    {
        InitializePositionTracking();
        playermanager = GetComponent<PlayerManager_>();
        isMovingValue = playermanager.GetIsMoving();
        targetplayermanager = targetObject.GetComponent<PlayerManager_>();
        targetIsMovingValue = targetplayermanager.GetIsMoving();
    }

    void Update()
    {
        //RandomComment();

        UpdatePosition();

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
        //���v���C���[�������ĂȂ��ꍇ
        if (positionStable)
        {
            commentManager.SetCommentText("�����ĂȂ��I");
        }
        //�G�v���C���[�������ĂȂ��ꍇ
        if (targetpositionStable)
        {
            commentManager.SetCommentText("�G�����ĂȂ��I");
        }
        //�������o�������ꍇ
        if (dropable)
        {
            commentManager.SetCommentText("�������Ă�I");
            Debug.Log("�������Ă�I");
        }
        //�������擾����
        if (DP.speedFlag == true)
        {
            commentManager.SetCommentText("�����Ȃ����I");
        }
        if (DP.powerFlag == true)
        {
            commentManager.SetCommentText("�����Ȃ����I");
        }
        //�G���������擾����
       
        // DropPlayer�R���|�[�l���g���A�^�b�`����Ă���ꍇ�A����Flag�ɃA�N�Z�X
        if (DP != null &&DP.speedFlag)
        {
            commentManager.SetCommentText("�G�������Ȃ����I");
        }
        if (DP != null && DP.powerFlag)
        {
            commentManager.SetCommentText("�G�������Ȃ����I");
        }
        //��e
        if (PH.hitflog)
        {
            commentManager.SetCommentText("�_���[�W���󂯂��I");
        }
        
        // DropPlayer�R���|�[�l���g���A�^�b�`����Ă���ꍇ�A����Flag�ɃA�N�Z�X
        if (PH != null && PH.hitflog)
        {
            commentManager.SetCommentText("�G�Ƀ_���[�W��^�����I");
        }

        //�ړ��G�l���M�[���؂ꂽ�ꍇ
        if (!isMovingValue)
        {
            commentManager.SetCommentText("�ړ��G�l���M�[���؂ꂽ�I");
        }
        //�G�̈ړ��G�l���M�[���؂ꂽ�ꍇ
        if (!targetIsMovingValue)
        {
            commentManager.SetCommentText("�G�̈ړ��G�l���M�[���؂ꂽ�I");
        }

        //test
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

    void InitializePositionTracking()
    {
        // ���������ɍ��W�ƃ^�C�}�[��ݒ�
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
            // ��莞�ԍ��W���ω����Ă��Ȃ��ꍇ�̏���
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !positionStable)
            {
                // ��莞�Ԍo�߂�����t���O�𗧂Ă�
                positionStable = true;
            }
        }
        else
        {
            // ���W���ω������ꍇ�̓t���O�����Z�b�g
            timer = 0f;
            positionStable = false;

            //�������������ꂽ�ꍇ�̃t���O
            GameObject dropper = GameObject.FindWithTag("Item");
            if (dropper != null)
            {
                Debug.Log("�I");
                dropable = true;
            }
            dropable = false;
        }

        if (IsTargetPositionStable())
        {
            // ��莞�ԍ��W���ω����Ă��Ȃ��ꍇ�̏���
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !targetpositionStable)
            {
                // ��莞�Ԍo�߂�����t���O�𗧂Ă�
                targetpositionStable = true;
            }
        }
        else
        {
            // ���W���ω������ꍇ�̓t���O�����Z�b�g
            timer = 0f;
            targetpositionStable = false;
        }

        // ���݂̍��W�𒼑O�̍��W�Ƃ��ĕۑ�
        lastPosition = transform.position;
        targetlastPosition = targetObject.transform.position;
    }

    bool IsPositionStable()
    {
        // ���݂̍��W�ƒ��O�̍��W���r���A��v���Ă��邩�m�F
        return transform.position == lastPosition;
    }

    bool IsTargetPositionStable()
    {
        // targetObject�̍��W����莞�ԕω����Ă��Ȃ����𔻒�
        return targetObject != null && targetObject.transform.position == targetlastPosition;
    }
}