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
    [SerializeField]private bool is_moveable = true;

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
    public bool isVibrationCannot;
    int AreaNumber;

    // �A�j���[�V����
    public Animator animator;
    public FootSteps footSteps;
    private Rigidbody rb;
    // Player�̎��_�J����
  //  public GameObject Playercamera;
    [SerializeField] private float recoveryDelay=5.0f;
    [SerializeField] private float MaxDelay =5.0f;
   // Player�̈ړ��Q�[�W

   [SerializeField] private static float MaxFuel = 50f; // �ő�R���e��
    [SerializeField] private float currentFuel = 0.0f; // ���݂̔R����
    public float fuelRechargeRate = 5.0f; // �R���̍ď[�d���x
    private float emptyFuel = 0.0f; // ��̔R����
    private float fuelConsumptionRate = 5.0f; // �R������x
    [SerializeField] private bool isMoving = false; // �ړ������ǂ����̃t���O
    [SerializeField] private bool OverHeat;

    [Header("�A�C�e��UI")] [SerializeField] private ItemSlotUI itemSlotUI;
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
        currentFuel=MaxFuel;
        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        if (animator) animator.SetBool("Walk", false);

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
            itemStep();
            MoveStep();
            
        }
    }
    void itemStep()
    {
       if( GetButtonDown("Player", "RightChange")|| GetButtonDown("Player1", "RightChange"))
        {
            itemSlotUI.ChangeRightItem();
        }
        if (GetButtonDown("Player", "LeftChange")|| GetButtonDown("Player1", "LeftChange"))
        {
            itemSlotUI.ChangeLeftItem();
        }
    }
    void MoveStep()
    {
        bool isMoveButtonDown = GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack");

        // �ړ��J�n����
        isMoving = !OverHeat && isMoveButtonDown && currentFuel > emptyFuel;

        // �Q�[�W��
        if (!isMoving && currentFuel <= MaxFuel)
        {
            // �Q�[�W��0�ɂȂ������莞�Ԉړ����������ɂȂ�
            if (currentFuel <= emptyFuel)
            {
                OverHeat = true;
            }
            if (OverHeat)
            {
                if (recoveryDelay > 0.0f)
                {
                    currentFuel += fuelRechargeRate * Time.deltaTime;
                    currentFuel = Mathf.Clamp(currentFuel, emptyFuel, MaxFuel);
                    recoveryDelay -= Time.deltaTime;
                   // rb.velocity = Vector3.zero;
                }
                else
                {
                    is_moveable = true;
                    isMoving = true;
                    recoveryDelay = MaxDelay;
                    OverHeat = false;
                }
            }
            if (OverHeat == false)
            {
                currentFuel += fuelRechargeRate * Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, emptyFuel, MaxFuel);
            }
        }

        // �ړ����̏���
        if (isMoving && !OverHeat)
        {
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Max(currentFuel, emptyFuel);
        }

        // �Q�[�W��0�ɂȂ�����ړ��s��
        if (currentFuel == 0.0f)
        {
            is_moveable = false;
            isMoving = false;
        }

        // �ړ��\�ȏ�Ԃ��ǂ���
    
            // �ړ����̏���
            if (isMoveButtonDown && !OverHeat)
            {
                currentFuel -= Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, 0.0f, MaxFuel);
            }
            else
            {
                isMoving = false;
            }

            Vector3 moveDirection = Vector3.zero;

            // �ړ������̎擾
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                moveDirection = transform.forward * move_speed;
            }

            if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
            {
                moveDirection = -transform.forward * move_speed;
            }

            // �Q�[�W�����ȏ�Ȃ瑬�x��ݒ�
            if (currentFuel > emptyFuel)
            {
                rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            }
        if (OverHeat)
        {
            // �R�����Ȃ��ꍇ�A���x�𔼕��ɂ���
            rb.velocity = new Vector3(moveDirection.x * 0.5f, rb.velocity.y, moveDirection.z * 0.5f);

        }


        // �A�j���[�V�����̐ݒ�
        if (animator)
            {
                animator.SetBool("Walk", moveDirection != Vector3.zero);
            }
        

        // ���E�̉�]
        if (GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft"))
        {
            transform.Rotate(new Vector3(0, -rot_angle, 0));
        }

        if (GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight"))
        {
            transform.Rotate(new Vector3(0, rot_angle, 0));
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
