using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// ベースのWeaponクラス
public class Weapon:MonoBehaviour
{
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;
    //予測線の描画モード
    public bool predictionLine_RayMode = true;
    [SerializeField] private ItemSlotUI itemSlotUI;
    [SerializeField] private Weapon weapon=null;
    [SerializeField] private Fragreceiver fragreceiver;
    [SerializeField]private Bullet bullet1;
    [SerializeField] private Missile missileBullet;
    [SerializeField] private PenetratingBullet penetratingBullet;
    private List<Bullet> bulletPool = new List<Bullet>();
    private List<Missile> missileBulletPool = new List<Missile>();
    private List<PenetratingBullet> penetratingBulletPool = new List<PenetratingBullet>();
    [SerializeField] private PlayerManager_ PlayerManager;
    [SerializeField]
    private PlayerSound BulletSE;//弾のSE
    //発射角度の調整速度(回転)
    float gunBarrel_rotSpeed = 0.5f;
    //今の発射角度(砲塔アセットがないため、一旦記録する)
    [SerializeField] protected float gun_rotAngle = 0.0f;
    [Range(0f, 100f)] // 0から100の範囲で変更可能
    public float Bullet_RangeOffset = 0;
    // 発射位置を調整する変数(プレイヤーの中ではなくて、前で発射するため)
    // 弾丸とプレイヤーを被ったら(当たったら)、弾丸は変な方向になるため。
    [Range(-50f, 50f)] // 0から10の範囲で変更可能
    public float bulletCreatePosOffsetZ = 1.0f;
    [Range(0f, 50f)] // 0から50の範囲で変更可能
    public float bulletCreatePosOffsetY = 25.0f;
    //チャージショット(Public)
    public GameObject ChageShot;
    //弾丸予測線(Public型)
   public GameObject Playercamera;
    //弾丸予測線(重力を使う)
    public GameObject PredictionLine;
    //弾丸予測線(重力を使わず)
    public GameObject PredictionRay;
    //カメラのアングル
    public float x_angle = 1000;
    //照準を合わせている(予測線)
   public bool is_aiming;
    public bool energyballFlug;
    private bool canShoot = true;
    //予測線GameObjectを保存する
    GameObject pRay;
    //弾丸予測線を構成するための描画数
    public int PredictionLineNumber = 66;
    //弾丸予測線の計算結果リスト(描画位置を保存する)
    List<GameObject> PredictionLine_List = new List<GameObject>();
    bool flog_Missile;
    bool flog_penetoratBullet;
    bool flog_normalBullet;
    [Header("初期弾数")] [SerializeField] private int bulletsMax = 5; 
    [Header("現在の弾数")] [SerializeField] private int bulletsRemaining = 5; 
    private bool isReloading = false;
    [Header("再装填時間")] [SerializeField] private float reloadTime = 2.0f; // 再装填にかかる時間

   
    private bool isActive;
    public float shotCooldown = 3.0f;
    [SerializeField] private float lastShotTime = 0f;
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

      //  Debug.Log("入力受付失敗" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButton(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

       // Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
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

      //  Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;

    }
    public float GetRotAngle()
    {
        return gun_rotAngle;
    }
    public int GetbulletsMax()
    {
        return bulletsMax;
    }
    public int GetbulletsRemaining()
    {
        return bulletsRemaining;
    }

 
    private void Start()
    {
        weapon = bullet1;
        flog_Missile =fragreceiver.misileflog;
        flog_penetoratBullet = fragreceiver.penetratBulletflog;
        flog_normalBullet = fragreceiver.normalBullet;
        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

 
    }
    void Update()
    {
        // アイテムスロットから現在選択されているアイテムを取得
        int selectedSlotIndex = itemSlotUI.GetCurrentSlotIndex();
        Debug.Log("選ばれている番号"+selectedSlotIndex);
        // 武器の切り替え
        SwitchWeapon(selectedSlotIndex);
        if (canShoot)
        {
            Shot();
        }
    


    }

    void SwitchWeapon(int weaponNumber)
    {
        // 武器の切り替え処理
        switch (weaponNumber)
        {
            case 0:
                weapon = bullet1;
                flog_penetoratBullet = false;
                flog_Missile = false;
                flog_normalBullet = true;
                break;
            case 1:
                weapon = missileBullet;
                flog_penetoratBullet = false;
                flog_Missile = true;
                flog_normalBullet = false;
                break;
            case 2:
                weapon = penetratingBullet;
                flog_penetoratBullet = true;
                flog_Missile = false;
                flog_normalBullet = false;
                break;
            default:
                break;
        }
    }
    void Shot()
    {
       
            //弾丸予測線
            //Spaceキーを押し続けると弾丸予測線を描画する
            if (GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire"))
            {

                PlayerManager.SetisMoving(false);
                //照準中(予測線を描画するため)
                is_aiming = true;
                energyballFlug = true;

                if (PlayerManager.animator != null)
                {
                    PlayerManager.animator.SetBool("ShotStanby", true);
                }
            }

            //弾丸発射
            if (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire"))
            {
                
                PlayerManager.SetisMoving(false);
                //照準済み
                is_aiming = false;
                if (PlayerManager.animator != null)
                {
                    PlayerManager.animator.SetBool("ShotStanby", false);
                    PlayerManager.animator.SetBool("Shot", true);
                }
                // 弾数がある場合のみ発射できるようにする
                if (canShoot&&bulletsRemaining > 0)
                {
                    StartCoroutine(ShootCooldown());
                    // 弾数を減らす
                    bulletsRemaining--;


                    //  弾丸生成
                    weapon = Instantiate(weapon, transform.position, transform.rotation);
                    //  親子関係を設定する
                    weapon.transform.parent = this.transform;
                    //   1フレーム後に親子関係を解除するコルーチンを呼び出す
                    StartCoroutine(UnparentAfterOneFrame(weapon.transform));
                    //  弾丸の角度をプレイヤーと一致する
                    weapon.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                    //   弾丸位置はプレイヤーの前にする
                    weapon.transform.Translate(new Vector3(0, bulletCreatePosOffsetY, bulletCreatePosOffsetZ));
                    //SEを鳴らす
                    BulletSE.PlayCanonSound();
                    //レイを消す
                    if (pRay != null)
                    {
                        Destroy(pRay);
                        pRay = null;
                    }
                    // 弾数がなくなったら再装填を始める
                    if (bulletsRemaining == 0 && !isReloading)
                    {
                        StartCoroutine(Reload());
                    }
                }
            }
        
       
        // 照準中のため、弾丸予測線を描画する
        //発射角度も調整できる
        if (is_aiming)
        {


            //発射角度を調整する
            //仮設定 発射角度の範囲:0°~90°
            if (GetButton("Player", "AngleUp") || GetButton("Player1", "AngleUp"))
            {
                gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                if (90.0f > gun_rotAngle) Playercamera.transform.Rotate(new Vector3(-gun_rotAngle / x_angle, 0, 0));
            }
            else if (GetButton("Player", "AngleDown") || GetButton("Player1", "AngleDown"))
            {
                gun_rotAngle = (gun_rotAngle - gunBarrel_rotSpeed) > 0 ? (gun_rotAngle - gunBarrel_rotSpeed) : 0.0f;
                if (0f < gun_rotAngle) Playercamera.transform.Rotate(new Vector3(gun_rotAngle / x_angle, 0, 0));
            }

            //発射角度を調整したため、
            //弾丸予測線の計算結果リストをクリアして再計算する必要
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //弾丸予測線の計算と描画する
            //LineRenderモード(重力使わず)
            if (predictionLine_RayMode)
            {
                if (pRay == null)
                {
                    DrewPredictionRay();
                }
            }
            else
            {
                //(重力を使う)
                DrewPredictionLine();
            }
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = 5; // 初期弾数に再設定
                              // 弾をプールに戻す
        foreach (Bullet bullet in bulletPool)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(false);
                bulletPool.Add(bullet);
            }
        }

        // ミサイル弾をプールに戻す
        foreach (Missile missileBullet in missileBulletPool)
        {
            missileBullet.gameObject.SetActive(false);
            if (missileBullet.gameObject.activeSelf)
            {
                missileBullet.gameObject.SetActive(false);
                missileBulletPool.Add(missileBullet);
            }
        }

        // 貫通弾をプールに戻す
        foreach (PenetratingBullet penetratingBullet in penetratingBulletPool)
        {
            if (penetratingBullet.gameObject.activeSelf)
            {
                penetratingBullet.gameObject.SetActive(false);
                penetratingBulletPool.Add(penetratingBullet);
            }
        }
        isReloading = false;
    }

    public bool GetReloadFlag()
    {
        return isReloading;
    }

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
            float X = (bulletCreatePosOffsetZ + Bullet_Speed * t) * Mathf.Cos(angle_y);
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
            float y = (bulletCreatePosOffsetZ + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //弾丸予測線を描画する
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);

            //弾丸予測線の計算結果リストに保存する
            PredictionLine_List.Add(gb);
        }
    }

    //弾丸予測線の計算と描画(Ray型)
    void DrewPredictionRay()
    {
        pRay = Instantiate(PredictionRay, transform.position, transform.rotation);
        pRay.transform.parent = this.transform;
    }

    IEnumerator UnparentAfterOneFrame(Transform child)
    {
        // 1フレーム待つ
        yield return null;

        // child が null でなく、かつ未破棄の場合にのみ親子関係を解除する
        if (child != null && !child.gameObject.Equals(null))
        {
            child.parent = null;
        }
    }


    //弾の管理
    Bullet GetInactiveBullet()
    {
        foreach (Bullet bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
        }

        // プール内に使用可能な弾がない場合は新しく生成してプールに追加する
        Bullet newBullet = InstantiateBullet();
        bulletPool.Add(newBullet);

        return newBullet;
    }

    Missile GetInactiveMissileBullet()
    {
        foreach (Missile missileBullet in missileBulletPool)
        {
            if (!missileBullet.gameObject.activeInHierarchy)
            {
                return missileBullet;
            }
        }

        // プール内に使用可能な弾がない場合は新しく生成してプールに追加する
        Missile newMissileBullet = InstantiateMissileBullet();
        missileBulletPool.Add(newMissileBullet);

        return newMissileBullet;
    }

    PenetratingBullet GetInactivePenetratingBullet()
    {
        foreach (PenetratingBullet penetratingBullet in penetratingBulletPool)
        {
            if (!penetratingBullet.gameObject.activeInHierarchy)
            {
                return penetratingBullet;
            }
        }

        // プール内に使用可能な弾がない場合は新しく生成してプールに追加する
        PenetratingBullet newPenetratingBullet = InstantiatePenetratingBullet();
        penetratingBulletPool.Add(newPenetratingBullet);

        return newPenetratingBullet;
    }

    Bullet InstantiateBullet()
    {
        // 弾の新しいインスタンスを生成する
        return Instantiate(bullet1);
    }

    Missile InstantiateMissileBullet()
    {
        // 弾の新しいインスタンスを生成する
        return Instantiate(missileBullet);
    }

    PenetratingBullet InstantiatePenetratingBullet()
    {
        // 弾の新しいインスタンスを生成する
        return Instantiate(penetratingBullet);
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

}


