using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// �x�[�X��Weapon�N���X
public class Weapon:MonoBehaviour
{
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();
    public PlayerInput[] playerInputArray;
    //�\�����̕`�惂�[�h
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
    [Header("�����e��")] [SerializeField] private int bulletsMax = 5; 
    [Header("���݂̒e��")] [SerializeField] private int bulletsRemaining = 5; 
    private bool isReloading = false;
    [Header("�đ��U����")] [SerializeField] private float reloadTime = 2.0f; // �đ��U�ɂ����鎞��

   
    private bool isActive;
    public float shotCooldown = 3.0f;
    [SerializeField] private float lastShotTime = 0f;
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
        // �A�C�e���X���b�g���猻�ݑI������Ă���A�C�e�����擾
        int selectedSlotIndex = itemSlotUI.GetCurrentSlotIndex();
        Debug.Log("�I�΂�Ă���ԍ�"+selectedSlotIndex);
        // ����̐؂�ւ�
        SwitchWeapon(selectedSlotIndex);
        if (canShoot)
        {
            Shot();
        }
    


    }

    void SwitchWeapon(int weaponNumber)
    {
        // ����̐؂�ւ�����
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
                if (canShoot&&bulletsRemaining > 0)
                {
                    StartCoroutine(ShootCooldown());
                    // �e�������炷
                    bulletsRemaining--;


                    //  �e�ې���
                    weapon = Instantiate(weapon, transform.position, transform.rotation);
                    //  �e�q�֌W��ݒ肷��
                    weapon.transform.parent = this.transform;
                    //   1�t���[����ɐe�q�֌W����������R���[�`�����Ăяo��
                    StartCoroutine(UnparentAfterOneFrame(weapon.transform));
                    //  �e�ۂ̊p�x���v���C���[�ƈ�v����
                    weapon.transform.Rotate(new Vector3(-gun_rotAngle, 0, 0));
                    //   �e�ۈʒu�̓v���C���[�̑O�ɂ���
                    weapon.transform.Translate(new Vector3(0, bulletCreatePosOffsetY, bulletCreatePosOffsetZ));
                    //SE��炷
                    BulletSE.PlayCanonSound();
                    //���C������
                    if (pRay != null)
                    {
                        Destroy(pRay);
                        pRay = null;
                    }
                    // �e�����Ȃ��Ȃ�����đ��U���n�߂�
                    if (bulletsRemaining == 0 && !isReloading)
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
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = 5; // �����e���ɍĐݒ�
                              // �e���v�[���ɖ߂�
        foreach (Bullet bullet in bulletPool)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(false);
                bulletPool.Add(bullet);
            }
        }

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

        // �ђʒe���v�[���ɖ߂�
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
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
        }

        // �v�[�����Ɏg�p�\�Ȓe���Ȃ��ꍇ�͐V�����������ăv�[���ɒǉ�����
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

        // �v�[�����Ɏg�p�\�Ȓe���Ȃ��ꍇ�͐V�����������ăv�[���ɒǉ�����
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

        // �v�[�����Ɏg�p�\�Ȓe���Ȃ��ꍇ�͐V�����������ăv�[���ɒǉ�����
        PenetratingBullet newPenetratingBullet = InstantiatePenetratingBullet();
        penetratingBulletPool.Add(newPenetratingBullet);

        return newPenetratingBullet;
    }

    Bullet InstantiateBullet()
    {
        // �e�̐V�����C���X�^���X�𐶐�����
        return Instantiate(bullet1);
    }

    Missile InstantiateMissileBullet()
    {
        // �e�̐V�����C���X�^���X�𐶐�����
        return Instantiate(missileBullet);
    }

    PenetratingBullet InstantiatePenetratingBullet()
    {
        // �e�̐V�����C���X�^���X�𐶐�����
        return Instantiate(penetratingBullet);
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

}


