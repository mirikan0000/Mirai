using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerManager_ : MonoBehaviour
{

    // ゲームは更新しているか（例：開始画面、クリア画面などではない状態）
    bool is_start;

    [Header("プレイヤーの移動速度")]
    public float move_speed = 1.0f;
    public int CurrentMap;
    [SerializeField] float rot_angle = 0.1f;
    public Camera camera;
    [SerializeField] private bool is_moveable = true;
    float horizontalFuelConsumptionRate = 5.0f;
    // ローカルマルチ設定
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;

    // 振動////////////////////////////////////
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
    // 振動時間
    private float vibrationTime;

    public bool isVibrationCannot;
    int AreaNumber;
    // アニメーション
    public Animator animator;
    [Header("アニメーション速度の同期")]
    [SerializeField] private float animationSpeedMultiplier = 1.0f;
    private Rigidbody rb;
    // Playerの視点カメラ
    //  public GameObject Playercamera;
    [SerializeField] private float recoveryDelay = 5.0f;
    [SerializeField] private float MaxDelay = 5.0f;
    // Playerの移動ゲージ
    [SerializeField] private static float MaxFuel = 50f; // 最大燃料容量
    [SerializeField] private float currentFuel = 0.0f; // 現在の燃料量
    [SerializeField] private Transform targettransform;
    public float fuelRechargeRate = 10.0f; // 燃料の再充電速度
    private float emptyFuel = 0.0f; // 空の燃料量
    private float fuelConsumptionRate = 5.0f; // 燃料消費速度
    [SerializeField] private bool isMoving = false; // 移動中かどうかのフラグ
    [SerializeField] private bool OverHeat;
    [Header("アイテムUI")] [SerializeField] private ItemSlotUI itemSlotUI;
    [SerializeField] private Weapon weapon;
    [Header("アイテム取得時用")]
    private Fragreceiver fragreceiver;  //フラグ管理用のスクリプト
    private float speedUpTimerLimit;    //スピードアップしていられる時間
    private float speedTimer;           //スピードアップ時間計測用
    [Header("右スティックの回転速度")]
    public float rotationSpeed = 1.0f;
    public void SetisMoving(bool ismoveing)
    {
        isMoving = ismoveing;
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public float GetcurrentFuel()
    {
        return currentFuel;
    }
    public float GetMaxFuel()
    {
        return MaxFuel;
    }
    void Start()
    {
        is_start = true;
        currentFuel = MaxFuel;

        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        if (animator) animator.SetBool("Walk", false);

        rb = GetComponent<Rigidbody>();

        fragreceiver = GetComponent<Fragreceiver>();
        speedTimer = 0.0f;
        if (animator)
        {
            animator.SetBool("Walk", false);
            // アニメーターの速度を同期させるためのパラメータを設定
            animator.SetFloat("Speed", 0.0f);
        }
    }

    public bool GetButtonDown(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasPressedThisFrame();
        }

        return false;
    }

    public bool GetButton(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }


        return false;
    }

    public bool GetButtonUp(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }


        return false;
    }
    /// <summary>
    /// 振動開始
    /// </summary>
    public float SetVibration(string _vibrationName)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("NotGamePad");
            return 0;
        }

        VibrationStruct vibrationStruct = Array.Find(vibrationStructArray, a => a.name == _vibrationName);
        if (!string.IsNullOrEmpty(vibrationStruct.name)) // 修正: 空でないことを確認
        {
            vibrationStructNow = vibrationStruct; // 修正: Array.Find を使わずに直接代入
        }
        else
        {
            Debug.LogError("NotvibrationStruct!!");
        }
        vibrationTime = 100;

        return 0;
    }
    /// <summary>
    /// 振動停止
    /// </summary>
    public void StopVibration()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }
        gamepad.SetMotorSpeeds(0, 0);
        vibrationTime = 99;
    }
    void Update()
    {
        if (gameObject.transform.position.y < -256)
        {
            Vector3 newPosition = new Vector3(gameObject.transform.position.x, -256f, gameObject.transform.position.z);
            gameObject.transform.position = newPosition;
        }
        if (is_start)
        {
            if (!weapon.isReloading)
            {
                itemStep();
            }
            //右スティックの回転
            float rotateInput = GetRotateInput();
            RotatePlayer(rotateInput);
            if (!weapon.is_aiming)
            {
                MoveStep();
            }
        }

        //スピードアップ
        SpeedUP();
    }
    void itemStep()
    {
        if (GetButtonDown("Player", "RightChange") || GetButtonDown("Player1", "RightChange"))
        {

            SetVibration(vibrationStructArray[0].name);
        }
        if (GetButtonUp("Player", "RightChange") || GetButtonUp("Player1", "RightChange"))
        {
            itemSlotUI.ChangeRightItem();
            //  StopVibration();
        }
        if (GetButtonDown("Player", "LeftChange") || GetButtonDown("Player1", "LeftChange"))
        {

            SetVibration(vibrationStructArray[0].name);
        }
        if (GetButtonUp("Player", "LeftChange") || GetButtonUp("Player1", "LeftChange"))
        {
            itemSlotUI.ChangeRightItem();
            //    StopVibration();
        }
    }
    void MoveStep()
    {
     
        //targettransform.localRotation=new Vector3(2f,2f,2f) ;
        bool isMoveButtonDown = GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack")|| 
                                (GetButton("Player", "MoveLeft") || (GetButton("Player", "MoveRight") ||
                                GetButton("Player1", "MoveRight")) ||GetButton("Player1", "MoveLeft"));

        // 移動開始条件
        isMoving = !OverHeat && isMoveButtonDown && currentFuel > emptyFuel;

        // ゲージ回復
        if (!isMoving && currentFuel <= MaxFuel)
        {
            // ゲージが0になったら一定時間移動がゆっくりになる
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

        // 移動中の処理
        if (isMoving && !OverHeat)
        {
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Max(currentFuel, emptyFuel);
        }

        // ゲージが0になったら移動不可
        if (currentFuel == 0.0f)
        {
            is_moveable = false;
            //isMoving = false;
        }

        // 移動可能な状態かどうか

        // 移動中の処理
        if (isMoveButtonDown && !OverHeat)
        {
            currentFuel -= Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0.0f, MaxFuel);
        }
        else
        {
            //isMoving = false;
        }

        Vector3 moveDirection = Vector3.zero;
        Vector3 horizontalVelocity = Vector3.zero;
        float horizontalMovement = 0f;
        // 移動方向の取得
        if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
        {
            moveDirection = transform.forward * move_speed;
        }

        if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
        {
            moveDirection = -transform.forward * move_speed;
        }
        
        // 左右の移動
        if (GetButton("Player", "MoveLeft") || GetButton("Player1", "MoveLeft"))
        {
            horizontalMovement = -1f;
        }
        else if (GetButton("Player", "MoveRight") || GetButton("Player1", "MoveRight"))
        {
            horizontalMovement = 1f;
        }
        // 左右の回転

        
        // 横方向の速度を計算
        Vector3 moveDirection_horizontal = transform.TransformDirection(new Vector3(horizontalMovement, 0f, 0f));
        horizontalVelocity = moveDirection_horizontal * move_speed;

     
        // ゲージが一定以上なら速度を設定
        if (currentFuel > emptyFuel)
        {
            rb.velocity = new Vector3(moveDirection.x + horizontalVelocity.x, rb.velocity.y, moveDirection.z + horizontalVelocity.z);
        }
        // アニメーション速度を同期
        if (animator)
        {
            float animationSpeed = move_speed * animationSpeedMultiplier;
            animator.SetFloat("Speed", isMoving ? animationSpeed : animationSpeed);
        }
        // アニメーションの設定
        if (animator)
        {
            animator.SetBool("Walk", moveDirection != Vector3.zero);
        }

        if (OverHeat)
        {
            // 燃料がない場合、速度を半分にする
            rb.velocity = new Vector3(moveDirection.x * 0.5f, rb.velocity.y, moveDirection.z * 0.5f);
         if (animator)
         {
            animator.SetBool("Walk", moveDirection != Vector3.zero);
         }
        }
 
        RaycastHit hit;
        float raycastLength = 5f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLength))
        {
            // 地面との当たり判定があれば、高さを調整
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
    //右スティックの回転の入力を取得
    private float GetRotateInput()
    {
        Vector2 rightStickInput = GetRightStickInput();
        float rotateInput = rightStickInput.x;

        return rotateInput;
    }

    //右スティックの入力を取得
    private Vector2 GetRightStickInput()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>(); // プレイヤーごとに異なるPlayerInputを取得

        if (playerInput != null)
        {
            return playerInput.actions["Look"].ReadValue<Vector2>(); // アクション名は実際のプロジェクトに合わせて変更
        }

        return Vector2.zero;
    }

    //プレイヤーを回転させる
    private void RotatePlayer(float rotateInput)
    {
        if (weapon.is_aiming)
        {
            float rotationAmount = rotateInput * rotationSpeed/2.5f;
            transform.Rotate(Vector3.up, rotationAmount);
        }
        else
        {
            float rotationAmount = rotateInput * rotationSpeed;
            transform.Rotate(Vector3.up, rotationAmount);
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

    //スピ―ドアップ処理
    private void SpeedUP()
    {
        if (fragreceiver.speedUpItemFlag == true)
        {
            move_speed = 10.0f;

            speedTimer += Time.deltaTime;

            if (speedTimer > speedUpTimerLimit)
            {
                move_speed = 5.0f;
                fragreceiver.speedUpItemFlag = false;
            }
        }
    }
}

