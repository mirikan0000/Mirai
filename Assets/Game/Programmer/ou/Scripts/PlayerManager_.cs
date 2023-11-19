using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager_ : MonoBehaviour
{
    // �Q�[���͍X�V���Ă��邩�i��F�J�n��ʁA�N���A��ʂȂǂł͂Ȃ���ԁj
    bool is_start;

    [Header("�v���C���[�̈ړ����x")]
    public float move_speed = 5.0f;
    public int CurrentMap;
    [SerializeField] float rot_angle = 0.1f;
    public Camera camera;
    bool is_moveable = true;

    // ���[�J���}���`�ݒ�
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;

    // �U��
    [System.Serializable]
    public struct VibrationStruct
    {
        public string name;
        public AnimationCurve valueLeft;
        public AnimationCurve valueRight;
        public float speed;
    }
    [Header("�U���\���̔z��")]
    public VibrationStruct[] vibrationStructArray;
    private VibrationStruct vibrationStructNow;
    private float vibrationTime;
    public bool isVibrationCannot;
    int AreaNumber;

    // �A�j���[�V����
    public Animator animator;
    public FootSteps footSteps;
    private Rigidbody rb;
    private bool isSoundPlaying = false;
    private float lastFootstepTime = 0f;
    private float footstepInterval = 0.5f;

    // Player�̎��_�J����
    public GameObject Playercamera;

    // Player�̈ړ��Q�[�W
    public float moveGauge = 100.0f;
    public float moveGaugeRate = 10.0f;
    public float moveGaugeThreshold = 20.0f;
    private float moveGaugeConsumptionRate = 10.0f;
    private bool isMoving = false;
    public void SetisMoving(bool ismoveing)
    {
        isMoving = ismoveing;
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    void Start()
    {
        is_start = true;

        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        if (animator) animator.SetBool("Walk", false);

        vibrationTime = 99;
        rb = GetComponent<Rigidbody>();
    }

    public bool GetButtonDown(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasPressedThisFrame();
        }

        Debug.Log("���͎�t���s" + actionMapsName + actionsName);
        return false;
    }

    public bool GetButton(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return false;
    }

    public bool GetButtonUp(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return false;
    }

    void Update()
    {
        if (is_start)
        {
            MoveStep();
        }
    }

    void MoveStep()
    {
        if (!isMoving && moveGauge <= moveGaugeThreshold)
        {
            rb.velocity = Vector3.zero;
        }

        bool isMoveButtonDown = GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack") ||
                                GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft") ||
                                GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight");

        if (isMoving)
        {
            moveGauge -= moveGaugeConsumptionRate * Time.deltaTime;
            moveGauge = Mathf.Max(moveGauge, moveGaugeThreshold);
        }
        else if (isMoveButtonDown && moveGauge > moveGaugeThreshold)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving && moveGauge < 100.0f)
        {
            moveGauge += moveGaugeRate * Time.deltaTime;
            moveGauge = Mathf.Clamp(moveGauge, moveGaugeThreshold, 100.0f);
        }

        if (is_moveable)
        {
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack") ||
                GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft") ||
                GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight"))
            {
                isMoving = true;
                moveGauge -= Time.deltaTime;
                moveGauge = Mathf.Clamp(moveGauge, 0.0f, 100.0f);

                if (moveGauge == 0.0f)
                {
                    is_moveable = false;
                    isMoving = false;
                }
            }
            else
            {
                isMoving = false;
            }

            Vector3 moveDirection = Vector3.zero;

            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                moveDirection = transform.forward * move_speed * Time.deltaTime;
            }

            if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
            {
                moveDirection = -transform.forward * move_speed * Time.deltaTime;
            }

            if (GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft"))
            {
                transform.Rotate(new Vector3(0, -rot_angle, 0));
            }

            if (GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight"))
            {
                transform.Rotate(new Vector3(0, rot_angle, 0));
            }

            if (moveGauge > moveGaugeThreshold)
            {
                rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            }

            if (animator)
            {
                if (moveDirection != Vector3.zero)
                {
                    animator.SetBool("Walk", true);
                }
                else
                {
                    animator.SetBool("Walk", false);
                }
            }
        }
    }

    public Camera GetMainCamera()
    {
        return camera;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Area"))
        {
            AreaNumber = collision.gameObject.GetComponent<EreaNumbers>().AreaNumber;
            CurrentMap = AreaNumber;
        }
    }
}
