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
    public float move_speed = 5.0f;
    public int CurrentMap;
    [SerializeField] float rot_angle = 0.1f;
    public Camera camera;
    [SerializeField]private bool is_moveable = true;

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
    private Rigidbody rb;
    // Playerの視点カメラ
  //  public GameObject Playercamera;
    [SerializeField] private float recoveryDelay=5.0f;
    [SerializeField] private float MaxDelay =5.0f;
   // Playerの移動ゲージ

    [SerializeField] private static float MaxFuel = 50f; // 最大燃料容量
    [SerializeField] private float currentFuel = 0.0f; // 現在の燃料量
    [SerializeField] private Transform targettransform;
    public float fuelRechargeRate = 5.0f; // 燃料の再充電速度
    private float emptyFuel = 0.0f; // 空の燃料量
    private float fuelConsumptionRate = 5.0f; // 燃料消費速度
    [SerializeField] private bool isMoving = false; // 移動中かどうかのフラグ
    [SerializeField] private bool OverHeat;

    [Header("アイテムUI")] [SerializeField] private ItemSlotUI itemSlotUI;
    [SerializeField] private Weapon weapon;
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
        if (is_start)
        {
            itemStep();
            if (!weapon.is_aiming)
            {
                MoveStep();
            }

        }
    }
    void itemStep()
    {
       if( GetButtonDown("Player", "RightChange")|| GetButtonDown("Player1", "RightChange"))
        {
            itemSlotUI.ChangeRightItem();
            SetVibration(vibrationStructArray[0].name);
        }
        if (GetButtonUp("Player", "RightChange") || GetButtonUp("Player1", "RightChange"))
        {
          //  StopVibration();
        }
        if (GetButtonDown("Player", "LeftChange")|| GetButtonDown("Player1", "LeftChange"))
        {
            itemSlotUI.ChangeLeftItem();
            SetVibration(vibrationStructArray[0].name);
        }
        if (GetButtonUp("Player", "LeftChange") || GetButtonUp("Player1", "LeftChange"))
        {
        //    StopVibration();
        }
    }
    void MoveStep()
    {
        //targettransform.localRotation=new Vector3(2f,2f,2f) ;
        bool isMoveButtonDown = GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward") ||
                                GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack");

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
            isMoving = false;
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
                isMoving = false;
            }

            Vector3 moveDirection = Vector3.zero;

            // 移動方向の取得
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                moveDirection = transform.forward * move_speed;
            }

            if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
            {
                moveDirection = -transform.forward * move_speed;
            }

            // ゲージが一定以上なら速度を設定
            if (currentFuel > emptyFuel)
            {
                rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            }
        if (OverHeat)
        {
            // 燃料がない場合、速度を半分にする
            rb.velocity = new Vector3(moveDirection.x * 0.5f, rb.velocity.y, moveDirection.z * 0.5f);

        }


        // アニメーションの設定
        if (animator)
            {
                animator.SetBool("Walk", moveDirection != Vector3.zero);
            }
        

        // 左右の回転
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
