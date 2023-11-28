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
    [SerializeField] private Weapon weapon;
    [SerializeField] private Fragreceiver fragreceiver;
    [SerializeField]private Bullet bullet1;
    [SerializeField] private Missile missileBullet;
    [SerializeField] private PenetratingBullet penetratingBullet;
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
    bool is_aiming;
    public bool energyballFlug;
    
    //�\����GameObject��ۑ�����
    GameObject pRay;
    //�e�ۗ\�������\�����邽�߂̕`�搔
    public int PredictionLineNumber = 66;
    //�e�ۗ\�����̌v�Z���ʃ��X�g(�`��ʒu��ۑ�����)
    List<GameObject> PredictionLine_List = new List<GameObject>();
    bool flog_Missile;
    bool penetoratBullet;
    [Header("�����e��")] [SerializeField] private int bulletsMax = 5; 
    [Header("���݂̒e��")] [SerializeField] private int bulletsRemaining = 5; 
    private bool isReloading = false;
    [Header("�đ��U����")] [SerializeField] private float reloadTime = 2.0f; // �đ��U�ɂ����鎞��
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
       flog_Missile=fragreceiver.misileflog;
       penetoratBullet = fragreceiver.penetratBulletflog;
        
        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

    }
    void Update()
    {
        if (fragreceiver.pierceBulletItemFlag == true)
        {
           weapon= penetratingBullet;
        }
        else if (flog_Missile)
        {
            weapon = missileBullet;
        }
        else
        {
            weapon = bullet1;
        }
        Shot();
    }
   

    void Shot()
    {

        //�e�ۗ\����
        //Space�L�[������������ƒe�ۗ\������`�悷��
        if (GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire"))
        {
            //�\����
            //����@(�d�́A�O�p�֐��Ŗ͋[������)
            //�d��
            //�ړ��֎~
            PlayerManager.SetisMoving(false);
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
            PlayerManager.SetisMoving(false);
            //�Ə��ς�
            is_aiming = false;

            // �e��������ꍇ�̂ݔ��˂ł���悤�ɂ���
            if (bulletsRemaining > 0)
            {
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
                BulletSE.PlaySmallCanonSoundB();
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
            if (GetButton("Player", "MoveForward") || GetButton("Player1", "MoveForward"))
            {
                gun_rotAngle = (gun_rotAngle + gunBarrel_rotSpeed) <= 90.0f ? (gun_rotAngle + gunBarrel_rotSpeed) : 90.0f;
                if (90.0f > gun_rotAngle) Playercamera.transform.Rotate(new Vector3(-gun_rotAngle / x_angle, 0, 0));
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
        isReloading = false;
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
}


