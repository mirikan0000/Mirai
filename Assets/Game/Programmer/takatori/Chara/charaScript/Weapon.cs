using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// ベースのWeaponクラス
public class Weapon:MonoBehaviour
{
    public enum weaponcase{
        Normal,
        penetrait,
        Missile
    }
   public weaponcase Weaponcase;
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;
    //予測線の描画モード
    public bool predictionLine_RayMode = true;
    [SerializeField] private ItemSlotUI itemSlotUI;
    [SerializeField] private Weapon weapon=null;
    [SerializeField] private Fragreceiver fragreceiver;
    [SerializeField]private Bullet bullet1;
    [SerializeField] private ThroughManager penetoraitManager;
    [SerializeField] private MissileManager missileManager;
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
    private bool canSwitchWeapon = true;
    // 各武器の残弾数
    [Header("通常弾初期弾数")] [SerializeField] private int normalBulletsMax = 5;
    [Header("通常弾現在の弾数")] [SerializeField] public int normalBulletsRemaining = 5;

    [Header("貫通弾初期弾数")] [SerializeField] private int penetratingBulletsMax = 1;
    [Header("貫通弾現在の弾数")] [SerializeField] public int penetratingBulletsRemaining = 1;

    [Header("ミサイル初期弾数")] [SerializeField] private int missileBulletsMax = 1;
    [Header("ミサイル現在の弾数")] [SerializeField] public int missileBulletsRemaining = 1;

    public bool isReloading = false;
    [Header("再装填時間")] public float reloadTime = 9.0f; // 再装填にかかる時間
    [Header("通常弾再装填時間")] public float normalReloadTime = 9.0f;
    [Header("貫通弾再装填時間")] public float penetratingReloadTime = 9.0f;
    [Header("ミサイル再装填時間")] public float missileReloadTime = 9.0f;
    public float elapsedTime;
    private bool isActive;
    public float shotCooldown = 3.0f;
    [SerializeField] private float lastShotTime = 0f;

    [SerializeField] private Image reloadFillImageNormal;
    [SerializeField] private Image reloadFillImageMissile;
    [SerializeField] private Image reloadFillImagepenetrait;
    private float currentReloadTime;
    //リロードカウントの左と右

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
        return normalBulletsMax;
    }
    public int GetbulletsRemaining()
    {
        return normalBulletsRemaining;
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
        int selectedSlotIndex = itemSlotUI.GetCurrentSlotIndex();
        if (isReloading)
        {
            canSwitchWeapon = false;
        }
        else
        {
            canSwitchWeapon = true;
            // アイテムスロットから現在選択されているアイテムを取得
            SwitchWeapon(selectedSlotIndex);
            Weaponcase = (weaponcase)selectedSlotIndex;
            if (canShoot)
            {
                Shot();
            }
        }
   
        // リロード中は武器の切り替えを禁止
        
    }

    void SwitchWeapon(int weaponNumber)
    {
        // 武器の切り替え処理
        if (canSwitchWeapon)
        {
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
                if (canShoot)
                {
                    StartCoroutine(ShootCooldown());
                int selectedSlotIndex = itemSlotUI.GetCurrentSlotIndex();
                
                switch (Weaponcase)
                {
                    case weaponcase.Normal:
                        ShootNormalBullet();
                        break;
                    case weaponcase.penetrait:
                        ShootPenetratingBullet();
                        break;
                    case weaponcase.Missile:
                        ShootMissileBullet();
                        break;
                }                  //レイを消す
                if (pRay != null)
                    {
                        Destroy(pRay);
                        pRay = null;
                    }
                    // 弾数がなくなったら再装填を始める
                 if ((penetratingBulletsRemaining == 0|| missileBulletsRemaining == 0 || normalBulletsRemaining ==0)&&!isReloading)
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
       
      //  reloadFillImageNormal.fillAmount = 1.0f;
        float reloadTime = 0.0f;
   
        switch (Weaponcase)
        {
            case weaponcase.Normal:
                reloadTime = normalReloadTime;
                reloadFillImageNormal.fillAmount = 1.0f;
                break;
            case weaponcase.Missile:
                reloadTime = missileReloadTime;
                reloadFillImageMissile.fillAmount = 1.0f;
                break;
            case weaponcase.penetrait:
                reloadTime = penetratingReloadTime;
                reloadFillImagepenetrait.fillAmount = 1.0f;
                break;
            default:
                break;
        }
        // リロード時間までの経過時間をカウント
        elapsedTime = 0.0f;
        while (elapsedTime < reloadTime)
        {
            // リロード進捗に合わせて FillAmount を更新
            float fillAmount = 1.0f - (elapsedTime / reloadTime);
            switch (Weaponcase)
            {
                case weaponcase.Normal:
                    reloadFillImageNormal.fillAmount = fillAmount;
                    break;
                case weaponcase.Missile:
                    reloadFillImageMissile.fillAmount = fillAmount;
                    break;
                case weaponcase.penetrait:
                    reloadFillImagepenetrait.fillAmount = fillAmount;
                    break;
                default:
                    break;
            }
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

            yield return null;
        }

        // リロードが完了したときに reloadFillImageNormal を 0% にリセット
        reloadFillImageNormal.fillAmount = 0.0f;
        reloadFillImageMissile.fillAmount = 0.0f;
        reloadFillImagepenetrait.fillAmount = 0.0f;

        // 弾薬を補充
        switch (Weaponcase)
        {
            case weaponcase.Normal:
                normalBulletsRemaining = normalBulletsMax;
                break;
            case weaponcase.Missile:
                missileBulletsRemaining = missileBulletsMax;
                break;
            case weaponcase.penetrait:
                penetratingBulletsRemaining = penetratingBulletsMax;
                break;
            default:
                break;
        }

        List<Bullet> bulletsToDisable = new List<Bullet>();

        foreach (Bullet bullet in bulletPool)
        {
            if (bullet != null && bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(false);
                bulletsToDisable.Add(bullet);
            }
        }

        foreach (Bullet bullet in bulletsToDisable)
        {
            bulletPool.Remove(bullet);
        }
      
        
        List<Missile> bulletsToDisable2 = new List<Missile>();
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
        foreach (Missile bullet in bulletsToDisable2)
        {
            missileBulletPool.Remove(bullet);
        }

        List<PenetratingBullet> bulletsToDisable3 = new List<PenetratingBullet>();
        // 貫通弾をプールに戻す
        foreach (PenetratingBullet penetratingBullet in penetratingBulletPool)
        {
            if (penetratingBullet.gameObject.activeSelf)
            {
                penetratingBullet.gameObject.SetActive(false);
                penetratingBulletPool.Add(penetratingBullet);
            }
        }
        foreach (PenetratingBullet bullet in bulletsToDisable3)
        {
            penetratingBulletPool.Remove(bullet);
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
            if (bullet != null && !bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
        }

        // プール内に使用可能な弾がない場合は新しく生成してプールに追加する
        Bullet newBullet = InstantiateBullet();
        bulletPool.Add(newBullet);

        return newBullet;
    }

   

    Bullet InstantiateBullet()
    {
        // 弾の新しいインスタンスを生成する
        return Instantiate(bullet1);
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }
    void ShootNormalBullet()
    {
        if (normalBulletsRemaining > 0)
        {
            Bullet bullet = GetInactiveBullet();
            ShootBullet(bullet);
            normalBulletsRemaining--;
        }
    }

    void ShootPenetratingBullet()
    {
        if (penetratingBulletsRemaining > 0)
        {
            PenetratingBullet penetratingBullet = penetoraitManager.CreatePb(this.transform.position, this.transform.rotation);
            ShootBullet(penetratingBullet);
            penetratingBulletsRemaining--;
        }
    }

    void ShootMissileBullet()
    {
        if (missileBulletsRemaining > 0)
        {
            // ミサイルマネージャーからミサイルを取得
            Missile missile = missileManager.CreateMissile(this.transform.position,this.transform.rotation);

            if (missile != null)
            {
                // ミサイルを発射
                missile.gameObject.SetActive(true);
                missile.transform.parent = this.transform;
                StartCoroutine(UnparentAfterOneFrame(missile.transform));
                missile.transform.position = transform.position;
                missile.transform.rotation = transform.rotation;
                missile.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                missile.transform.Translate(new Vector3(0, bulletCreatePosOffsetY, bulletCreatePosOffsetZ));

                missileBulletsRemaining--;
            }
        }
    }
    void ShootBullet(Weapon bullet)
    {
        bullet.gameObject.SetActive(true);
        //  親子関係を設定する
        bullet.transform.parent = this.transform;
        //   1フレーム後に親子関係を解除するコルーチンを呼び出す
        StartCoroutine(UnparentAfterOneFrame(bullet.transform));
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
        bullet.transform.Translate(new Vector3(0, bulletCreatePosOffsetY, bulletCreatePosOffsetZ));
      
    }

}
