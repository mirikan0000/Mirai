
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

        // �J�[�\����\��
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

        // �g�����W�V�������ĂȂ��ꍇ
        //if (!isStageTransition && !isStartLogo)
        //{
            // �R���g���[���[���g���Ă鎞,�L�[�{�[�h���͂��������ꍇ
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
            // �L�[�{�[�h���g���Ă鎞,�R���g���[���[���͂��������ꍇ
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
