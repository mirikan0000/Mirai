using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager_ : MonoBehaviour
{
    // ゲームは更新しているか（例：開始画面、クリア画面などではない状態）
    bool is_start;

    [Header("プレイヤーの移動速度")]
    public float move_speed = 5.0f;
    public int CurrentMap;
    [SerializeField] float rot_angle = 0.1f;
    public Camera camera;
    bool is_moveable = true;

    // ローカルマルチ設定
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;

    // 振動
    [System.Serializable]
    public struct VibrationStruct
    {
        public string name;
        public AnimationCurve valueLeft;
        public AnimationCurve valueRight;
        public float speed;
    }
    [Header("振動構造体配列")]
    public VibrationStruct[] vibrationStructArray;
    private VibrationStruct vibrationStructNow;
    private float vibrationTime;
    public bool isVibrationCannot;
    int AreaNumber;

    // アニメーション
    public Animator animator;
    public FootSteps footSteps;
    private Rigidbody rb;
    private bool isSoundPlaying = false;
    private float lastFootstepTime = 0f;
    private float footstepInterval = 0.5f;

    // Playerの視点カメラ
    public GameObject Playercamera;

    // Playerの移動ゲージ
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

        Debug.Log("入力受付失敗" + actionMapsName + actionsName);
        return false;
    }

    public bool GetButton(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;
    }

    public bool GetButtonUp(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
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
