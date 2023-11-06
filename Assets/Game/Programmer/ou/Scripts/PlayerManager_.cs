using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager_ : MonoBehaviour
{
    //�Q�[���͍X�V���Ă��邩(�Ⴆ�΁F�J�n��ʁA�N���A��ʂȂǂł͂Ȃ����)
    bool is_start;

    //�Ə������킹�Ă���(�\����)
    bool is_aiming;
    public bool energyballFlug;
    //���ˁA�����[�h�͈ړ��ł��Ȃ�����
    bool is_moveable = true;
    //�J�����̃A���O��
    public float x_angle=1000;
    [Header("�v���C���[�̈ړ����x")]
    public float move_speed = 5.0f;
    //�v���C���[�̗��}�b�v�ԍ�
    public int CurrentMap;
  [SerializeField] float rot_angle = 0.1f;

    //���ˊp�x�̒������x(��])
    float gunBarrel_rotSpeed = 0.5f;

    //���̔��ˊp�x(�C���A�Z�b�g���Ȃ����߁A��U�L�^����)
    public float gun_rotAngle = 0.0f;

    //���ˋ����̒����ϐ�(Public�^)
    public float Bullet_RangeOffset = 0;

    //���ˈʒu�𒲐�����ϐ�(�v���C���[�̒��ł͂Ȃ��āA�O�Ŕ��˂��邽��)
    //�e�ۂƃv���C���[��������(����������)�A�e�ۂ͕ςȕ����ɂȂ邽�߁B
    public float bulletCreatePosOffset = 1.0f;

    //�e��(Public�^)
    public GameObject Buttet;
    //�e�ۗ\����(Public�^)
    public GameObject PredictionLine;

    //�e�ۗ\�������\�����邽�߂̕`�搔
    public int PredictionLineNumber = 66;

    //�e�ۗ\�����̌v�Z���ʃ��X�g(�`��ʒu��ۑ�����)
    List<GameObject> PredictionLine_List = new List<GameObject>();
    
    //���[�J���}���`�ݒ�
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    //
    public PlayerInput[] playerInputArray;
    // �U��////////////////////////////////////
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
    // �U������
    private float vibrationTime;

    public bool isVibrationCannot;

    //animation
    public Animator animator;  // �A�j���[�^�[�R���|�[�l���g�擾�p
   public FootSteps footSteps;
    private Rigidbody rb; // Rigidbody�R���|�[�l���g

    // �T�E���h���Đ������ǂ����������t���O
    private bool isSoundPlaying = false;

// �O��̑����Đ�����
private float lastFootstepTime = 0f;
    private float footstepInterval=0.5f;

    //Player�̎��_�J����
    public GameObject Playercamera;

    //Player�̈ړ��Q�[�W
    public float moveGauge = 100.0f;  // �ړ��Q�[�W�̏����l
    public float moveGaugeRate = 10.0f;  // �ړ��Q�[�W�̏[�d���x
    public float moveGaugeThreshold = 20.0f;  // �ړ��Q�[�W���[���ɋ߂Â����Ƃ��ɒ�~����臒l
    private float moveGaugeConsumptionRate = 10.0f;  // �ړ����ɃQ�[�W�������鑬�x
    private bool isMoving = false;  // �v���C���[���ړ������ǂ����������t���O


    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���J�n(�X�V��������)
        is_start = true;

        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        if (animator) animator.SetBool("Walk", false);
        
        //�U������
        vibrationTime = 99;
        rb = GetComponent<Rigidbody>(); // Rigidbody�R���|�[�l���g���擾
    }
  
    /// <summary>
    /// �{�^�����������u��
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
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
    /// <summary>
    /// �{�^���𗣂����u�� 
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
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
            // �v���C���[���ړ����łȂ��ꍇ�A�ړ��Q�[�W�����ʈȉ��ɂȂ�ƒ�~����
            if (!isMoving && moveGauge <= moveGaugeThreshold)
            {
                rb.velocity = Vector3.zero;  // �v���C���[�̑��x���[���ɐݒ肵�Ē�~
            }

            // �ړ��{�^����������Ă��邩�`�F�b�N
            bool isMoveButtonDown = GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                                   GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack") ||
                                   GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft") ||
                                   GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight");

            // �ړ����̏ꍇ�A�Q�[�W�������
            if (isMoving)
            {
                moveGauge -= moveGaugeConsumptionRate * Time.deltaTime;
                moveGauge = Mathf.Max(moveGauge, moveGaugeThreshold);  // �Œ�l��1�ɐ���
            }
            else if (isMoveButtonDown && moveGauge > moveGaugeThreshold)  // �ړ��{�^����������Ă��ăQ�[�W���Œ�l�ȏ�̏ꍇ
            {
                isMoving = true;  // �v���C���[���ړ���
            }
            else
            {
                isMoving = false;  // �v���C���[���ړ����łȂ�
            }

            // �ړ��Q�[�W�̏[�d
            if (!isMoving && moveGauge < 100.0f)  // �ő�l��30
            {
                moveGauge += moveGaugeRate * Time.deltaTime;
                moveGauge = Mathf.Clamp(moveGauge, moveGaugeThreshold, 100.0f);  // �Œ�l��1�ɐ���
            }

            // �f�o�b�O���O�ňړ��Q�[�W�̒l��\��
            Debug.Log("Move Gauge: " + moveGauge);

            // ���̃X�e�b�v�i�_���[�W�A�ړ��A�U���j�̏����͂��̂܂܂Ŏc���Ă����܂��B
            DamageStep();
            MoveStep();
            ShotStep();
        }
    }

    //�e�ۗ\�����̌v�Z�ƕ`��
    void DrewPredictionLine()
    {
        //���ˊp�x�����W�A���ɂ���
        float angle_y = gun_rotAngle * Mathf.Deg2Rad;
        //�v���C���[��]�p�x�����W�A���ɂ���
        float angle_xz = transform.eulerAngles.y * Mathf.Deg2Rad;

        //�e�ۂ̔��ˑ��x���Q�b�g����(Script�uBullet�v�ŕۑ����Ă���)
        float Bullet_Speed = PlayerPrefs.GetFloat("Bullet_Speed");

        //�e�ۗ\�������v�Z����
        for (int i = 0; i < PredictionLineNumber; i++)
        {
            //���ԊԊu
            float t = i * 0.05f;

            //�����ʂ̈ʒu(XZ���W)���v�Z����
            float X = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Cos(angle_y);
            float x = X * Mathf.Sin(angle_xz) + transform.position.x;
            float z = X * Mathf.Cos(angle_xz) + transform.position.z;

            //�d�͂Ɣ��ˋ��������킹�邽�߁A�v�Z����
            float Bullet_Gravity = Physics2D.gravity.y;
            //�d�� < = 0 
            if ((Physics2D.gravity.y + Bullet_RangeOffset) <= 0)
            {
                //���ˋ��������킹��"�d��"���v�Z����
                Bullet_Gravity += Bullet_RangeOffset;
                //���ˋ�����ۑ�����(Script�uBullet�v�Ōv�Z���鎞�Ɏg������)
                PlayerPrefs.SetFloat("Bullet_RangeOffset", Bullet_RangeOffset);
            }

            //�c�����̂x���W���v�Z����
            float y = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //�e�ۗ\������`�悷��
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);
            //�e�ۗ\�����̌v�Z���ʃ��X�g�ɕۑ�����
            PredictionLine_List.Add(gb);
        }
    }

    void MoveStep()
    {
        if (is_moveable)
        {
            // �ړ��Q�[�W�̏���
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack") ||
                GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft") ||
                GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight"))
            {
                isMoving = true;
                moveGauge -= Time.deltaTime;  // �ړ��Q�[�W������
                moveGauge = Mathf.Clamp(moveGauge, 0.0f, 100.0f);
               
                if (moveGauge == 0.0f)
                {
                    // �ړ��Q�[�W���[���ɂȂ�����ړ����֎~
                    is_moveable = false;
                    isMoving = false;
                }
            }
            else
            {
                isMoving = false;
            }

            // �ړ������x�N�g����������
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

            // �ړ��Q�[�W�����ʈȏ゠��ꍇ�݈̂ړ�������
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

    bool CanMove(Vector3 moveDirection)
    {
        // �v���C���[�̌��݈ʒu����ړ�����v�Z
        Vector3 newPosition = transform.position + moveDirection;

        // Ray���������邽�߂ɁARay�̊J�n�_�ƏI�_���v�Z
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = newPosition;

        // Raycast���g�p���Ĉړ���ɕǂ����邩�ǂ������`�F�b�N
        Ray ray = new Ray(rayStart, moveDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDirection.magnitude))
        {
            // �ړ���ɕǂ�����ꍇ�A�ړ��������Ȃ�
            Debug.DrawLine(rayStart, hit.point, Color.red); // Ray���Փ˂���������Ԃ����ŕ\��
            return false;
        }

        // Ray���Փ˂��Ȃ��ꍇ�A�ړ�������
        Debug.DrawLine(rayStart, rayEnd, Color.green); // Ray�̕�����΂̐��ŕ\��
        return true;
    }
    void ShotStep()
    {
        //�e�ۗ\����
        //Space�L�[������������ƒe�ۗ\������`�悷��
        if (GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire"))
        {
            //�\����
            //����@(�d�́A�O�p�֐��Ŗ͋[������)
            //�d��
            //�ړ��֎~
            is_moveable = false;
            //�Ə���(�\������`�悷�邽��)
            is_aiming = true;
            energyballFlug = true;
        }

        //�e�۔���
        if (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire"))
        {
            //����
            //����@(�d�́A�O�p�֐��Ŗ͋[������)
            //�d��
            //���˂�����ňړ���������
            is_moveable = true;
            //�Ə��ς�
            is_aiming = false;

            //�e�ۗ\�����̌v�Z���ʃ��X�g���N���A
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //�e�ې���
            GameObject buttle = Instantiate(Buttet, transform.position, transform.rotation);
            //�e�ۂ̊p�x���v���C���[�ƈ�v����
            buttle.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
            //�e�ۈʒu�̓v���C���[�̑O�ɂ���
            buttle.transform.Translate(new Vector3(0, 0, bulletCreatePosOffset));
        }
        //�Ə����̂��߁A�e�ۗ\������`�悷��
        //���ˊp�x�������ł���
        if (is_aiming)
        {
            //���ˊp�x�𒲐�����
            //���ݒ� ���ˊp�x�͈̔�:0��~90��
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                  if(90.0f> gun_rotAngle)Playercamera.transform.Rotate(new Vector3(-gun_rotAngle/x_angle, 0, 0));
            }
            else if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
            {
                gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
                if (0f < gun_rotAngle) Playercamera.transform.Rotate(new Vector3(gun_rotAngle / x_angle, 0, 0));
            }
           
            //���ˊp�x�𒲐��������߁A
            //�e�ۗ\�����̌v�Z���ʃ��X�g���N���A���čČv�Z����K�v
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //�e�ۗ\�����̌v�Z�ƕ`�悷��
            DrewPredictionLine();
        }
    }

    void DamageStep()
    {
       
       
    }
    int AreaNumber;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Area"))
        {
            AreaNumber = collision.gameObject.GetComponent<EreaNumbers>().AreaNumber;

            CurrentMap = AreaNumber;
        }
        
    }
}