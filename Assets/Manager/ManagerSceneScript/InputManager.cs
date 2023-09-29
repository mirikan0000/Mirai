
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

        // カーソル非表示
        Cursor.visible = false;
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
                SetStopActionMap("Cursor", false);
            }
        }

        //bool isStageTransition = false;
        //GameObject[] stageTransitionMaskObjArray = GameObject.FindGameObjectsWithTag("StageTransitionMask");
        //GameObject[] transitionMaskObjArray = GameObject.FindGameObjectsWithTag("transitionMask");
        //GameObject[] outgameTransitionMaskObjArray = GameObject.FindGameObjectsWithTag("OutGameTransitionMask");
        //GameObject[] spriteTransitionMaskObjArray = GameObject.FindGameObjectsWithTag("spriteTransitionMask");
        //GameObject[] goalObjArray = GameObject.FindGameObjectsWithTag("Goal");
        //GameObject[] playerObjArray = GameObject.FindGameObjectsWithTag("Player");
        //UnityEngine.SceneManagement.Scene[] nowSceneArray = UnityEngine.SceneManagement.SceneManager.GetAllScenes();
        //bool isSceneChangeEnd = false;
        //bool isStartLogo = false;
        //for (int i = 0; i < nowSceneArray.Length; i++)
        //{
        //    if (nowSceneArray[i].name == "s_StartLogo" || nowSceneArray[i].name == "s_OpMV" || nowSceneArray[i].name == "s_EndMV")
        //    {
        //        isStartLogo = true;
        //    }

        //    if (nowSceneArray[i].name == "s_Start" || nowSceneArray[i].name == "s_Title" || nowSceneArray[i].name == "s_StageSelect")
        //    {
        //        foreach (GameObject stageTransitionMaskObj in stageTransitionMaskObjArray)
        //        {
        //            if (!stageTransitionMaskObj.GetComponent<StageTransitionMask>().maskDogAnimator.GetBool("isEnd"))
        //            {
        //                isStageTransition = true;
        //            }
        //        }
        //    }

        //    foreach (string lightSceneName in LightSceneChange.Instance.lightSceneNameArray)
        //    {
        //        if (nowSceneArray[i].name == lightSceneName)
        //        {
        //            if (!isSceneChangeEnd)
        //            {
        //                isSceneChangeEnd = true;
        //            }
        //            else
        //            {
        //                isStageTransition = true;
        //            }
        //        }
        //    }
        //}
        //foreach (GameObject transitionMaskObj in transitionMaskObjArray)
        //{
        //    if (transitionMaskObj.transform.Find("MaskDog").GetComponent<Animator>().GetBool("isMask"))
        //    {
        //        isStageTransition = true;
        //    }
        //}
        //foreach (GameObject outgameTransitionMaskObj in outgameTransitionMaskObjArray)
        //{
        //    if (outgameTransitionMaskObj.GetComponent<Animator>().GetBool("isMask"))
        //    {
        //        isStageTransition = true;
        //    }
        //}
        //foreach (GameObject spriteTransitionMaskObj in spriteTransitionMaskObjArray)
        //{
        //    if (spriteTransitionMaskObj.GetComponent<Animator>().GetBool("isMask"))
        //    {
        //        isStageTransition = true;
        //    }
        //}
        //foreach (GameObject goalObj in goalObjArray)
        //{
        //    if (goalObj.name != "NextSceneBoss")
        //    {
        //        if (goalObj.GetComponent<ParentScript>().isGoal)
        //        {
        //            isStageTransition = true;
        //        }
        //    }
        //}
        //foreach (GameObject playerObj in playerObjArray)
        //{
        //    if (playerObj.GetComponent<PlayerMover6>().gameStartTime <= 100)
        //    {
        //        isStageTransition = true;
        //    }
        //}

        // トランジションしてない場合
        //if (!isStageTransition && !isStartLogo)
        //{
            // コントローラーを使ってる時,キーボード入力があった場合
            if (isUseGamepad)
            {
                if (GetButtonDown("Judge", "AnyKeyboard") || MathF.Abs(GetStick("Judge", "MoveMouse").x) >= 20.0f || MathF.Abs(GetStick("Judge", "MoveMouse").y) >= 20.0f)
                {
                    UnityEngine.SceneManagement.Scene[] sceneArray = UnityEngine.SceneManagement.SceneManager.GetAllScenes();
                    bool isSceneChange = true;
                    foreach (UnityEngine.SceneManagement.Scene scene in sceneArray)
                    {
                        if (scene.name == "s_Operate")
                        {
                            isSceneChange = false;
                        }
                    }
                    if (isSceneChange)
                    {
                        SceneManager.Instance.LoadScene("s_Operate");
                        SceneManager.Instance.ChangeScene();
                    }
                }
            }
            // キーボードを使ってる時,コントローラー入力があった場合
            else
            {
                if (GetButtonDown("Judge", "AnyGamepad") || MathF.Abs(GetStick("Judge", "MoveGamepad").x) >= 0.5f || MathF.Abs(GetStick("Judge", "MoveGamepad").y) >= 0.5f)
                {
                    UnityEngine.SceneManagement.Scene[] sceneArray = UnityEngine.SceneManagement.SceneManager.GetAllScenes();
                    bool isSceneChange = true;
                    foreach (UnityEngine.SceneManagement.Scene scene in sceneArray)
                    {
                        if (scene.name == "s_Operate")
                        {
                            isSceneChange = false;
                        }
                    }
                    if (isSceneChange)
                    {
                        SceneManager.Instance.LoadScene("s_Operate");
                        SceneManager.Instance.ChangeScene();
                    }
                }
            }
       // }

        Debug.Log("isVibrationCannot" + isVibrationCannot);

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
