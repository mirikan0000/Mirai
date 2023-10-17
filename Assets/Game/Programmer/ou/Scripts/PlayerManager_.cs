using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager_ : MonoBehaviour
{
    //ゲームは更新しているか(例えば：開始画面、クリア画面などではない状態)
    bool is_start;

    //照準を合わせている(予測線)
    bool is_aiming;

    //発射、リロードは移動できないため
    bool is_moveable = true;

  
    [Header("プレイヤーの移動速度")]
    public float move_speed = 5.0f;

  [SerializeField] float rot_angle = 0.1f;

    //発射角度の調整速度(回転)
    float gunBarrel_rotSpeed = 0.5f;

    //今の発射角度(砲塔アセットがないため、一旦記録する)
    public float gun_rotAngle = 0.0f;

    //発射距離の調整変数(Public型)
    public float Bullet_RangeOffset = 0;

    //発射位置を調整する変数(プレイヤーの中ではなくて、前で発射するため)
    //弾丸とプレイヤーを被ったら(当たったら)、弾丸は変な方向になるため。
    public float bulletCreatePosOffset = 1.0f;

    //弾丸(Public型)
    public GameObject Buttet;
    //弾丸予測線(Public型)
    public GameObject PredictionLine;

    //弾丸予測線を構成するための描画数
    public int PredictionLineNumber = 66;

    //弾丸予測線の計算結果リスト(描画位置を保存する)
    List<GameObject> PredictionLine_List = new List<GameObject>();
    
    //ローカルマルチ設定
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    //
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

    //animation
    public Animator animator;  // アニメーターコンポーネント取得用
   public FootSteps footSteps;
    private Rigidbody rb; // Rigidbodyコンポーネント

    // サウンドが再生中かどうかを示すフラグ
    private bool isSoundPlaying = false;

// 前回の足音再生時刻
private float lastFootstepTime = 0f;
    private float footstepInterval=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始(更新を許可する)
        is_start = true;

        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        if (animator) animator.SetBool("Walk", false);
        
        //振動時間
        vibrationTime = 99;
        rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得
    }
  
    /// <summary>
    /// ボタンを押した瞬間
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
    /// </summary>
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
    /// <summary>
    /// ボタンを離した瞬間 
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
    /// </summary>
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
            //ダメージ
            DamageStep();
            //移動
            MoveStep();
            //攻撃
            ShotStep();


        }
    }

    //弾丸予測線の計算と描画
    void DrewPredictionLine()
    {
        //発射角度をラジアンにする
        float angle_y = gun_rotAngle * Mathf.Deg2Rad;
        //プレイヤー回転角度をラジアンにする
        float angle_xz = transform.eulerAngles.y * Mathf.Deg2Rad;

        //弾丸の発射速度をゲットする(Script「Bullet」で保存している)
        float Bullet_Speed = PlayerPrefs.GetFloat("Bullet_Speed");

        //弾丸予測線を計算する
        for (int i = 0; i < PredictionLineNumber; i++)
        {
            //時間間隔
            float t = i * 0.05f;

            //横平面の位置(XZ座標)を計算する
            float X = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Cos(angle_y);
            float x = X * Mathf.Sin(angle_xz) + transform.position.x;
            float z = X * Mathf.Cos(angle_xz) + transform.position.z;

            //重力と発射距離を合わせるため、計算する
            float Bullet_Gravity = Physics2D.gravity.y;
            //重力 < = 0 
            if ((Physics2D.gravity.y + Bullet_RangeOffset) <= 0)
            {
                //発射距離を合わせた"重力"を計算する
                Bullet_Gravity += Bullet_RangeOffset;
                //発射距離を保存する(Script「Bullet」で計算する時に使うため)
                PlayerPrefs.SetFloat("Bullet_RangeOffset", Bullet_RangeOffset);
            }

            //縦方向のＹ座標を計算する
            float y = (bulletCreatePosOffset + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //弾丸予測線を描画する
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);
            //弾丸予測線の計算結果リストに保存する
            PredictionLine_List.Add(gb);
        }
    }

    void MoveStep()
    {
        if (is_moveable)
        {
            // 移動方向ベクトルを初期化
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

            // Rigidbodyのvelocityプロパティを設定して移動
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

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
        // プレイヤーの現在位置から移動先を計算
        Vector3 newPosition = transform.position + moveDirection;

        // Rayを可視化するために、Rayの開始点と終点を計算
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = newPosition;

        // Raycastを使用して移動先に壁があるかどうかをチェック
        Ray ray = new Ray(rayStart, moveDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDirection.magnitude))
        {
            // 移動先に壁がある場合、移動を許可しない
            Debug.DrawLine(rayStart, hit.point, Color.red); // Rayが衝突した部分を赤い線で表示
            return false;
        }

        // Rayが衝突しない場合、移動を許可
        Debug.DrawLine(rayStart, rayEnd, Color.green); // Rayの方向を緑の線で表示
        return true;
    }
    void ShotStep()
    {
        //弾丸予測線
        //Spaceキーを押し続けると弾丸予測線を描画する
        if (GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire"))
        {
            //予測線
            //二つ方法(重力、三角関数で模擬放物線)
            //重力
            //移動禁止
            is_moveable = false;
            //照準中(予測線を描画するため)
            is_aiming = true;
        }

        //弾丸発射
        if (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire"))
        {
            //発射
            //二つ方法(重力、三角関数で模擬放物線)
            //重力
            //発射した後で移動を許可する
            is_moveable = true;
            //照準済み
            is_aiming = false;

            //弾丸予測線の計算結果リストをクリア
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //弾丸生成
            GameObject buttle = Instantiate(Buttet, transform.position, transform.rotation);
            //弾丸の角度をプレイヤーと一致する
            buttle.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
            //弾丸位置はプレイヤーの前にする
            buttle.transform.Translate(new Vector3(0, 0, bulletCreatePosOffset));
        }
        //照準中のため、弾丸予測線を描画する
        //発射角度も調整できる
        if (is_aiming)
        {
            //発射角度を調整する
            //仮設定 発射角度の範囲:0°~90°
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
            }
            else if (GetButton("Player", "MoveBack") || GetButton("Player1", "MoveBack"))
            {
                gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
            }

            //発射角度を調整したため、
            //弾丸予測線の計算結果リストをクリアして再計算する必要
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //弾丸予測線の計算と描画する
            DrewPredictionLine();
        }
    }

    void DamageStep()
    {
       
       
    }
 
}