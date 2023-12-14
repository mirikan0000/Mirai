using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// �x�[�X��Weapon�N���X
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
    //�\�����̕`�惂�[�h
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
    private PlayerSound BulletSE;//�e��SE
    //���ˊp�x�̒������x(��])
    float gunBarrel_rotSpeed = 0.5f;
    //���̔��ˊp�x(�C���A�Z�b�g���Ȃ����߁A��U�L�^����)
    [SerializeField] protected float gun_rotAngle = 0.0f;
    [Range(0f, 100f)] // 0����100�͈̔͂ŕύX�\
    public float Bullet_RangeOffset = 0;
    // ���ˈʒu�𒲐�����ϐ�(�v���C���[�̒��ł͂Ȃ��āA�O�Ŕ��˂��邽��)
    // �e�ۂƃv���C���[��������(����������)�A�e�ۂ͕ςȕ����ɂȂ邽�߁B
    [Range(-50f, 50f)] // 0����10�͈̔͂ŕύX�\
    public float bulletCreatePosOffsetZ = 1.0f;
    [Range(0f, 50f)] // 0����50�͈̔͂ŕύX�\
    public float bulletCreatePosOffsetY = 25.0f;
    //�`���[�W�V���b�g(Public)
    public GameObject ChageShot;
    //�e�ۗ\����(Public�^)
   public GameObject Playercamera;
    //�e�ۗ\����(�d�͂��g��)
    public GameObject PredictionLine;
    //�e�ۗ\����(�d�͂��g�킸)
    public GameObject PredictionRay;
    //�J�����̃A���O��
    public float x_angle = 1000;
    //�Ə������킹�Ă���(�\����)
   public bool is_aiming;
    public bool energyballFlug;
    private bool canShoot = true;
    //�\����GameObject��ۑ�����
    GameObject pRay;
    //�e�ۗ\�������\�����邽�߂̕`�搔
    public int PredictionLineNumber = 66;
    //�e�ۗ\�����̌v�Z���ʃ��X�g(�`��ʒu��ۑ�����)
    List<GameObject> PredictionLine_List = new List<GameObject>();
    bool flog_Missile;
    bool flog_penetoratBullet;
    bool flog_normalBullet;
    private bool canSwitchWeapon = true;
    // �e����̎c�e��
    [Header("�ʏ�e�����e��")] [SerializeField] private int normalBulletsMax = 5;
    [Header("�ʏ�e���݂̒e��")] [SerializeField] public int normalBulletsRemaining = 5;

    [Header("�ђʒe�����e��")] [SerializeField] private int penetratingBulletsMax = 1;
    [Header("�ђʒe���݂̒e��")] [SerializeField] public int penetratingBulletsRemaining = 1;

    [Header("�~�T�C�������e��")] [SerializeField] private int missileBulletsMax = 1;
    [Header("�~�T�C�����݂̒e��")] [SerializeField] public int missileBulletsRemaining = 1;

    public bool isReloading = false;
    [Header("�đ��U����")] public float reloadTime = 9.0f; // �đ��U�ɂ����鎞��
    [Header("�ʏ�e�đ��U����")] public float normalReloadTime = 9.0f;
    [Header("�ђʒe�đ��U����")] public float penetratingReloadTime = 9.0f;
    [Header("�~�T�C���đ��U����")] public float missileReloadTime = 9.0f;
    public float elapsedTime;
    private bool isActive;
    public float shotCooldown = 3.0f;
    [SerializeField] private float lastShotTime = 0f;

    [SerializeField] private Image reloadFillImageNormal;
    [SerializeField] private Image reloadFillImageMissile;
    [SerializeField] private Image reloadFillImagepenetrait;
    private float currentReloadTime;
    //�����[�h�J�E���g�̍��ƉE

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

      //  Debug.Log("���͎�t���s" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButton(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

       // Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
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

      //  Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
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
            // �A�C�e���X���b�g���猻�ݑI������Ă���A�C�e�����擾
            SwitchWeapon(selectedSlotIndex);
            Weaponcase = (weaponcase)selectedSlotIndex;
            if (canShoot)
            {
                Shot();
            }
        }
   
        // �����[�h���͕���̐؂�ւ����֎~
        
    }

    void SwitchWeapon(int weaponNumber)
    {
        // ����̐؂�ւ�����
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
       
            //�e�ۗ\����
            //Space�L�[������������ƒe�ۗ\������`�悷��
            if (GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire"))
            {

                PlayerManager.SetisMoving(false);
                //�Ə���(�\������`�悷�邽��)
                is_aiming = true;
                energyballFlug = true;

                if (PlayerManager.animator != null)
                {
                    PlayerManager.animator.SetBool("ShotStanby", true);
                }
            }

            //�e�۔���
            if (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire"))
            {
                
                PlayerManager.SetisMoving(false);
                //�Ə��ς�
                is_aiming = false;

                if (PlayerManager.animator != null)
                {
                    PlayerManager.animator.SetBool("ShotStanby", false);
                    PlayerManager.animator.SetBool("Shot", true);
                }
                // �e��������ꍇ�̂ݔ��˂ł���悤�ɂ���
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
                }                  //���C������
                if (pRay != null)
                    {
                        Destroy(pRay);
                        pRay = null;
                    }
                    // �e�����Ȃ��Ȃ�����đ��U���n�߂�
                 if ((penetratingBulletsRemaining == 0|| missileBulletsRemaining == 0 || normalBulletsRemaining ==0)&&!isReloading)
                 {
                    StartCoroutine(Reload());
                 }
            }
            }
        
       
        // �Ə����̂��߁A�e�ۗ\������`�悷��
        //���ˊp�x�������ł���
        if (is_aiming)
        {


            //���ˊp�x�𒲐�����
            //���ݒ� ���ˊp�x�͈̔�:0��~90��
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

            //���ˊp�x�𒲐��������߁A
            //�e�ۗ\�����̌v�Z���ʃ��X�g���N���A���čČv�Z����K�v
            for (int i = PredictionLine_List.Count - 1; i >= 0; i--)
            {
                Destroy(PredictionLine_List[i]);
            }
            PredictionLine_List = new List<GameObject>();

            //�e�ۗ\�����̌v�Z�ƕ`�悷��
            //LineRender���[�h(�d�͎g�킸)
            if (predictionLine_RayMode)
            {
                if (pRay == null)
                {
                    DrewPredictionRay();
                }
            }
            else
            {
                //(�d�͂��g��)
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
        // �����[�h���Ԃ܂ł̌o�ߎ��Ԃ��J�E���g
        elapsedTime = 0.0f;
        while (elapsedTime < reloadTime)
        {
            // �����[�h�i���ɍ��킹�� FillAmount ���X�V
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
        // �o�ߎ��Ԃ��X�V
        elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �����[�h�����������Ƃ��� reloadFillImageNormal �� 0% �Ƀ��Z�b�g
        reloadFillImageNormal.fillAmount = 0.0f;
        reloadFillImageMissile.fillAmount = 0.0f;
        reloadFillImagepenetrait.fillAmount = 0.0f;

        // �e����[
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
        // �~�T�C���e���v�[���ɖ߂�
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
        // �ђʒe���v�[���ɖ߂�
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
            float X = (bulletCreatePosOffsetZ + Bullet_Speed * t) * Mathf.Cos(angle_y);
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
            float y = (bulletCreatePosOffsetZ + Bullet_Speed * t) * Mathf.Sin(angle_y) + 0.5f * Bullet_Gravity * t * t + transform.position.y;

            //�e�ۗ\������`�悷��
            GameObject gb = Instantiate(PredictionLine, new Vector3(x, y, z), transform.rotation);

            //�e�ۗ\�����̌v�Z���ʃ��X�g�ɕۑ�����
            PredictionLine_List.Add(gb);
        }
    }

    //�e�ۗ\�����̌v�Z�ƕ`��(Ray�^)
    void DrewPredictionRay()
    {
        pRay = Instantiate(PredictionRay, transform.position, transform.rotation);
        pRay.transform.parent = this.transform;
    }

    IEnumerator UnparentAfterOneFrame(Transform child)
    {
        // 1�t���[���҂�
        yield return null;

        // child �� null �łȂ��A�����j���̏ꍇ�ɂ̂ݐe�q�֌W����������
        if (child != null && !child.gameObject.Equals(null))
        {
            child.parent = null;
        }
    }


    //�e�̊Ǘ�
    Bullet GetInactiveBullet()
    {
        foreach (Bullet bullet in bulletPool)
        {
            if (bullet != null && !bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
        }

        // �v�[�����Ɏg�p�\�Ȓe���Ȃ��ꍇ�͐V�����������ăv�[���ɒǉ�����
        Bullet newBullet = InstantiateBullet();
        bulletPool.Add(newBullet);

        return newBullet;
    }

   

    Bullet InstantiateBullet()
    {
        // �e�̐V�����C���X�^���X�𐶐�����
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
            // �~�T�C���}�l�[�W���[����~�T�C�����擾
            Missile missile = missileManager.CreateMissile(this.transform.position,this.transform.rotation);

            if (missile != null)
            {
                // �~�T�C���𔭎�
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
        //  �e�q�֌W��ݒ肷��
        bullet.transform.parent = this.transform;
        //   1�t���[����ɐe�q�֌W����������R���[�`�����Ăяo��
        StartCoroutine(UnparentAfterOneFrame(bullet.transform));
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
        bullet.transform.Translate(new Vector3(0, bulletCreatePosOffsetY, bulletCreatePosOffsetZ));
      
    }

}
