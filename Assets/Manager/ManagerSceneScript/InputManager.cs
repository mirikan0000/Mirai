
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public PlayerInput[] playerInputArray;
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();

    [Header("�ݒ�m�F�p")]
    [SerializeField]
    private InputActionAsset inputActionControls;

    private List<string> stopActionMapNameList = new List<string>();

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

    // true(�R���g���[���[���g���Ă�),false(�}�E�X���g���Ă�)
    public bool isUseGamepad;

    private float endOperateInputTime;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        foreach (PlayerInput playerInput in playerInputArray)
        {
            playerInputDictionary[playerInput.currentActionMap.name] = playerInput;
        }

        vibrationTime = 99;

    }

    // Update is called once per frame
    void Update()
    {
        if (endOperateInputTime > 0)
        {
            endOperateInputTime--;
            if (endOperateInputTime <= 0)
            {
                SetStopActionMap("UI", false);

            }
        }
        // �U������
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }
        if ((vibrationTime <= 1.0f && !isVibrationCannot) && !PauseManager.Instance.isPause)
        {
            gamepad.SetMotorSpeeds(vibrationStructNow.valueLeft.Evaluate(vibrationTime), vibrationStructNow.valueRight.Evaluate(vibrationTime));
            vibrationTime += Time.deltaTime * vibrationStructNow.speed;
        }
        else
        {
            StopVibration();
        }
    }

    /// <summary>
    /// �{�^�����������u��
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
    public bool GetButtonDown(string actionMapsName, string actionsName)
    {
        foreach (string stopActionMap in stopActionMapNameList)
        {
            if (stopActionMap == actionMapsName)
            {
                return false;
            }
        }

        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasPressedThisFrame();
        }

        Debug.Log("���͎�t���s" + actionMapsName + actionsName);
        return false;
    }
    /// <summary>
    /// �{�^���������Ă���� 
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
    public bool GetButton(string actionMapsName, string actionsName)
    {
        foreach (string stopActionMap in stopActionMapNameList)
        {
            if (stopActionMap == actionMapsName)
            {
                return false;
            }
        }

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

        foreach (string stopActionMap in stopActionMapNameList)
        {
            if (stopActionMap == actionMapsName)
            {
                return false;
            }
        }

        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return false;
    }
    /// <summary>
    /// �X�e�B�b�N��XY���W(0.0f�`1.0f)
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
    public Vector2 GetStick(string actionMapsName, string actionsName)
    {

        foreach (string stopActionMap in stopActionMapNameList)
        {
            if (stopActionMap == actionMapsName)
            {
                return Vector2.zero;
            }
        }

        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].ReadValue<Vector2>();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return Vector2.zero;
    }
    /// <summary>
    /// �g���K�[�̉������݋
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
    public float GetTrigger(string actionMapsName, string actionsName)
    {

        foreach (string stopActionMap in stopActionMapNameList)
        {
            if (stopActionMap == actionMapsName)
            {
                return 0;
            }
        }

        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].ReadValue<float>();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return 0;
    }
    /// <summary>
    /// �U���J�n
    /// </summary>
    public float SetVibration(string _vibrationName)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return 0;
        }

        VibrationStruct vibrationStruct = Array.Find(vibrationStructArray, a => a.name == _vibrationName);
        if (vibrationStruct.name != "")
        {
            vibrationStructNow = Array.Find(vibrationStructArray, a => a.name == _vibrationName);
        }
        vibrationTime = 0;

        return 0;
    }
    /// <summary>
    /// �U����~
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
    /// <summary>
    /// ���͒�~
    /// ��ActionMaps��,Actions���́uInputActionControls�v���m�F
    /// </summary>
    public void SetStopActionMap(string actionMapName, bool isStop)
    {
        if (isStop)
        {
            if (!stopActionMapNameList.Contains(actionMapName))
            {
                stopActionMapNameList.Add(actionMapName);
            }
        }
        else
        {
            if (stopActionMapNameList.Contains(actionMapName))
            {
                stopActionMapNameList.Remove(actionMapName);
            }
        }
    }
    public void EndOperateInput(float time)
    {
        endOperateInputTime = time;
    }

    /// <summary>
    /// �R���g���[���[���ڑ�����Ă��邩
    /// </summary>
    /// <returns></returns>
    public bool IsGamepad()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// �}�E�X���ڑ�����Ă��邩
    /// </summary>
    /// <returns></returns>
    public bool IsMouse()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
