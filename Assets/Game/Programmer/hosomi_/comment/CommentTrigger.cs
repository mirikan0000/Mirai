using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommentTrigger : MonoBehaviour
{
    public Camera PlayerCamera;
    private Vector3 lastPosition;       // ���O�̍��W
    private bool positionStable;        // ���W����莞�ԕω����Ă��Ȃ����̃t���O
    private PlayerManager_ playermanager;
    private bool isMovingValue;
    private DropPlayer dropplayer;
    private bool dropplayerFlag;
    private PlayerHealth playerhealth;
    private bool playerhealthFlag;
    private Weapon weapon;
    private bool reloadflag;

    public CommentManager commentManager;

    public float timeThreshold = 5.0f; // ���b�ԍ��W���ω����Ȃ�����臒l
    private float timer;               // �o�ߎ���

    public GameObject targetObject;
    private Vector3 targetlastPosition;
    private bool targetpositionStable;
    private PlayerManager_ targetplayermanager;
    private bool targetIsMovingValue;
    private DropPlayer targetdropplayer;
    private bool targetdropplayerFlag;
    private PlayerHealth targetplayerhealth;
    private bool targetplayerhealthFlag;

    private bool dropable = false;

    private static readonly string[] FIND = new string[] { "����G����Ȃ��H", "�G��������", "�����Ɖ�ʌ��Ă�H" };
    private static readonly string[] SEARCH = new string[] { "�G�ǂ��H", "���������猩���Ȃ���" };
    private static readonly string[] STAY = new string[] { "�G���X�g�H", "�������", "����̕��s����[", "�헪����", "�u���b�N����", "��������ŋ�!!" };
    private static readonly string[] ENEMYSTAY = new string[] { "���蓮���ĂȂ���", "�G�r�r�b�ē�������w" };
    private static readonly string[] SUPPLIES = new string[] { "�P�A�p�P�����I", "�P�A�p�P�ǂ��H", "�����T���ɍs���I" };
    private static readonly string[] NOTMOVE = new string[] { "�Q�[�W����", "����Ɉʒu�`�����w", "�K�x�Ɏ~�܂�Ƃ����ł���" };
    private static readonly string[] ENEMTNOTMOVE = new string[] { "����K�X�����Ă��", "�K�X���Ƃ��M���O���w", "�`�����X��ׂ�w" };
    private static readonly string[] UPGRADE = new string[] { "���g����", "��������" };
    private static readonly string[] ENEMYUPGRADE = new string[] { "�����Ƃ�ꂽ�H", "���苭���Ȃ���" };
    private static readonly string[] HIT = new string[] { "���̔������", "�ɂ��ˁH", "�܂����Ă�", "�I�����" };
    private static readonly string[] ENEMYHIT = new string[] { "������", "����͂���Ă�", "�i�C�X�I", "������", "�����", "����������������" };
    private static readonly string[] SPAWNER = new string[] { "���̈��u�́H", "�G���A����" };
    private static readonly string[] RELOAD = new string[] { "�e���߂āI", "�i�C�X�����[�h", "�e�厖�ɂ���" };

    void Start()
    {
        InitializePositionTracking();
        playermanager = GetComponent<PlayerManager_>();
        dropplayer = GetComponent<DropPlayer>();
        playerhealth = GetComponent<PlayerHealth>();
        weapon = GetComponent<Weapon>();

        targetplayermanager = targetObject.GetComponent<PlayerManager_>();
        targetdropplayer = targetObject.GetComponent<DropPlayer>();
        targetplayerhealth = targetObject.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        isMovingValue = playermanager.GetIsMoving();
        dropplayerFlag = dropplayer.GetOpenBoxFlag();
        playerhealthFlag = playerhealth.GetHitFlag();
        reloadflag = weapon.GetReloadFlag();

        targetIsMovingValue = targetplayermanager.GetIsMoving();
        targetdropplayerFlag = targetdropplayer.GetOpenBoxFlag();
        targetplayerhealthFlag = targetplayerhealth.GetHitFlag();

        UpdatePosition();

        if (PlayerCamera != null && targetObject != null)
        {
            // �J�������I�u�W�F�N�g���f���Ă��邩�ǂ������m�F
            if (IsObjectVisibleInCamera())
            {
                string find = FIND.ElementAt(Random.Range(0, FIND.Count()));
                commentManager.SetCommentTextWithWeight(find, 2);
            }
            else
            {
                string search = SEARCH.ElementAt(Random.Range(0, SEARCH.Count()));
                commentManager.SetCommentTextWithWeight(search, 1);
            }
        }
        //���v���C���[�������ĂȂ��ꍇ
        if (positionStable)
        {
            string stay = STAY.ElementAt(Random.Range(0, STAY.Count()));
            commentManager.SetCommentTextWithWeight(stay, 1);
        }
        //�G�v���C���[�������ĂȂ��ꍇ
        if (targetpositionStable)
        {
            string enemystay = ENEMYSTAY.ElementAt(Random.Range(0, ENEMYSTAY.Count()));
            commentManager.SetCommentTextWithWeight(enemystay, 2);
        }
        //�������o�������ꍇ
        if (dropable)
        {
            string supplies = SUPPLIES.ElementAt(Random.Range(0, SUPPLIES.Count()));
            commentManager.SetCommentTextWithWeight(supplies, 2);
        }
        //�������擾����
        if (dropplayerFlag)
        {
            string upgrade = UPGRADE.ElementAt(Random.Range(0, UPGRADE.Count()));
            commentManager.SetCommentTextWithWeight(upgrade, 2);
        }

        //�G���������擾����
        if (targetdropplayerFlag)
        {
            string enemyupgrade = ENEMYUPGRADE.ElementAt(Random.Range(0, ENEMYUPGRADE.Count()));
            commentManager.SetCommentTextWithWeight(enemyupgrade, 2);
        }

        //��e
        if (playerhealth)
        {
            string hit = HIT.ElementAt(Random.Range(0, HIT.Count()));
            commentManager.SetCommentTextWithWeight(hit, 2);
        }

        //�G����e
        if (targetplayerhealthFlag)
        {
            string enemyhit = ENEMYHIT.ElementAt(Random.Range(0, ENEMYHIT.Count()));
            commentManager.SetCommentTextWithWeight(enemyhit, 2);
        }

        //�ړ��G�l���M�[���؂ꂽ�ꍇ
        if (!isMovingValue)
        {
            string notmove = NOTMOVE.ElementAt(Random.Range(0, NOTMOVE.Count()));
            commentManager.SetCommentTextWithWeight(notmove, 2);
        }
        //�G�̈ړ��G�l���M�[���؂ꂽ�ꍇ
        if (!targetIsMovingValue)
        {
            string enemynotmove = ENEMTNOTMOVE.ElementAt(Random.Range(0, ENEMTNOTMOVE.Count()));
            commentManager.SetCommentTextWithWeight(enemynotmove, 2);
        }

        //�����[�h��
        if (reloadflag)
        {
            string reload = RELOAD.ElementAt(Random.Range(0, RELOAD.Count()));
            commentManager.SetCommentTextWithWeight(reload, 2);
        }
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