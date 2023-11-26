
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public PlayerInput[] playerInputArray;
    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();

    [Header("設定確認用")]
    [SerializeField]
    private InputActionAsset inputActionControls;

    private List<string> stopActionMapNameList = new List<string>();

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

    // true(コントローラーを使ってる),false(マウスを使ってる)
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
        // 振動処理
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
    /// ボタンを押した瞬間
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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

        Debug.Log("入力受付失敗" + actionMapsName + actionsName);
        return false;
    }
    /// <summary>
    /// ボタンを押している間 
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;
    }
    /// <summary>
    /// ボタンを離した瞬間 
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;
    }
    /// <summary>
    /// スティックのXY座標(0.0f～1.0f)
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return Vector2.zero;
    }
    /// <summary>
    /// トリガーの押し込み具合
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return 0;
    }
    /// <summary>
    /// 振動開始
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
    /// <summary>
    /// 入力停止
    /// ※ActionMaps名,Actions名は「InputActionControls」を確認
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
    /// コントローラーが接続されているか
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
    /// マウスが接続されているか
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
