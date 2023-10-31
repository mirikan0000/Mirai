using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class option : MonoBehaviour
{
    [Header("前シーン遷移時_削除シーンリスト")]
    public List<string> unLoadSceneNameList_Before;

    [Header("次シーン遷移時_削除シーンリスト")]
    public List<string> unLoadSceneNameList;
    [Header("次シーン遷移時_読み込みシーンリスト")]
    public List<string> loadSceneNameList;

    public AudioMixer[] audioMixerArray;
    public AudioMixer mainAudioMixer;

    [Header("BGMスライダー")]
    public Slider sliderBGM;
    [Header("SEスライダー")]
    public Slider sliderSE;
    [Header("カーソルの速度スライダー")]
    public Slider sliderCursorSpeed;
    [Header("スライダー速度")]
    public float sliderSpeed;


    [Header("カーソル")]
    public GameObject cursorData;
    private Transform cursor;
    public float moveSpeed = 15.0f;
    //public float moveSpeedMouse = 1.0f;

    [Header("セレクトリスト")]
    public List<GameObject> selectList = new List<GameObject>();
    public Material normalMaterial;
    public Material outlineMaterial;
    public Material outlineMaterialSlider;
    public Material outlineMaterialButton;
    //private List<Outline> OutLineList = new List<Outline>();

    public List<SpriteRenderer> vibrationSelectList = new List<SpriteRenderer>();

    [Header("項目リスト")]
    public List<GameObject> sectionList;
    //private List<Outline> vibrationOutLineList = new List<Outline>();

    [Header("セレクト番号")]
    public int selectNo;
    public int selectNo_2;
    public int selectNo_GamePad;

    public AudioSource audioSouceSE;
    public AudioSource se_select;
    public AudioSource se_back;

    public float delayTime;
    private float preTime;
    private float deltaTime;

    public bool isTitleBack;
    public bool isSelectBack;

    public Sprite spriteSliderPlusDefault;
    public Sprite spriteSliderMinusDefault;
    public Sprite spriteSliderPlusHold;
    public Sprite spriteSliderMinusHold;

    [Header("コントローラー振動UI")]
    public GameObject gamepadVibrationUIObj;
    public GameObject gamepadBackUIObj;
    [Header("カーソル速度UI")]
    public GameObject cursorSpeedUIObj;
    public GameObject keyboardBackUIObj;

    //各パラメータ時の実数値
    enum VolumeParamete
    {
        Vol_0 = -80,
        Vol_1 = -35,
        Vol_2 = -30,
        Vol_3 = -25,
        Vol_4 = -20,
        Vol_5 = -15,
        Vol_6 = -10,
        Vol_7 = -5,
        Vol_8 = 0,
        Vol_9 = 5,
        Vol_10 = 10,
    }
    VolumeParamete bgmVolume;
    VolumeParamete seVolume;
    // カーソルの速度*100
    enum CursorSpeedParamete
    {
        Speed_0 = 10,
        Speed_1 = 25,
        Speed_2 = 40,
        Speed_3 = 55,
        Speed_4 = 70,
        Speed_5 = 85,
        Speed_6 = 100,
        Speed_7 = 115,
        Speed_8 = 130,
        Speed_9 = 145,
        Speed_10 = 160,
    }

    enum SliderValue
    {
        Vol_0 = 0,
        Vol_1 = 1,
        Vol_2 = 2,
        Vol_3 = 3,
        Vol_4 = 4,
        Vol_5 = 5,
        Vol_6 = 6,
        Vol_7 = 7,
        Vol_8 = 8,
        Vol_9 = 9,
        Vol_10 = 10,
    }
    SliderValue sliderBGM_val;
    SliderValue sliderSE_val;
    SliderValue sliderCursor_val;

    private bool isLoad;

    private GameObject postProcessObj;
    private bool isPostProcess;


    // Start is called before the first frame update
    void Start()
    {
        //SetOutLine();
        //RegulationSelect();
        UpdateSelectSprite();
        foreach (AudioMixer audioMixer in audioMixerArray)
        {
            audioMixer.SetFloat("BGM_Vol", (float)VolumeParamete.Vol_6);
            audioMixer.SetFloat("SE_Vol", (float)VolumeParamete.Vol_6);
        }
    //    CursorSpeedManager.Instance.cursorSpeed = (float)CursorSpeedParamete.Speed_6 * 0.01f;
     
        if (cursorData != null)
        {
            cursor = cursorData.GetComponent<Transform>();
        }
        if (LoadSceneSetManager.Instance != null && !string.IsNullOrEmpty(LoadSceneSetManager.Instance.loadSceneName))
        {
            loadSceneNameList.Add(LoadSceneSetManager.Instance.loadSceneName);
            LoadSceneSetManager.Instance.loadSceneName = "";
        }

        if (postProcessObj != null && !isPostProcess)
        {
            postProcessObj.GetComponent<Volume>().enabled = false;
        }
    }

    private void Awake()
    {
        //Debug.Log(bgm);
        //呼び出したときの設定音量を入れる
        //現在の音量取得
        mainAudioMixer.GetFloat("BGM_Vol", out float bgmVol);
        mainAudioMixer.GetFloat("SE_Vol", out float seVol);
        //スライダー用に変換
        sliderBGM_val = ConvertSliderFromVolume((VolumeParamete)bgmVol);
        sliderSE_val = ConvertSliderFromVolume((VolumeParamete)seVol);
      //  sliderCursor_val = ConvertSliderFromCursorSpeed((CursorSpeedParamete)(CursorSpeedManager.Instance.cursorSpeed * 100.0f));
        //スライダーに反映
        sliderBGM.value = (float)sliderBGM_val;
        sliderSE.value = (float)sliderSE_val;
        sliderCursorSpeed.value = (float)sliderCursor_val;

        bool isVibration = InputManager.Instance.isVibrationCannot;
        if (!isVibration)
        {
            selectNo_2 = 0;
        }
        else
        {
            selectNo_2 = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (postProcessObj != null && !isPostProcess)
        {
            postProcessObj.GetComponent<Volume>().enabled = false;
        }

        deltaTime += 0.015f;
        if (deltaTime > preTime + delayTime)
        {
            if (!isLoad)
            {
                // キーボード入力の場合
                if (!InputManager.Instance.isUseGamepad)
                {
                 //   GameObject selectObj = cursor.GetComponent<CursorCollider>().selectObj;
                    //if (selectObj != null)
                    //{
                    //    selectNo = selectList.IndexOf(selectObj);
                    //    //selectNo = selectList.IndexOf(selectObj.GetComponent<SpriteRenderer>());
                    //}
                    //else
                    //{
                    //    selectNo = -1;
                    //}
                    gamepadVibrationUIObj.SetActive(false);
                    gamepadBackUIObj.SetActive(false);
                    cursorSpeedUIObj.SetActive(true);
                    keyboardBackUIObj.SetActive(true);
                }
                // コントローラー入力の場合
                else
                {
                    if (selectNo == -1)
                    {
                        selectNo = 0;
                    }
                    cursorSpeedUIObj.SetActive(false);
                    keyboardBackUIObj.SetActive(false);
                    gamepadVibrationUIObj.SetActive(true);
                    gamepadBackUIObj.SetActive(true);
                }
            }

            // 次の項目(下)に移動
            if (InputManager.Instance.GetButtonDown("UI", "Down"))
            {
                selectNo_GamePad++;
                RegulationSelect();
                //UpdateSelectSprite();
                //se_select.Play();
            }
            // 前の項目(上)に移動
            if (InputManager.Instance.GetButtonDown("UI", "Up"))
            {
                selectNo_GamePad--;
                RegulationSelect();
                //UpdateSelectSprite();
                //se_select.Play();

            }
            if (InputManager.Instance.GetButton("UI", "Left"))
            {
                if (selectNo_GamePad == 0)
                {
                    sliderBGM.value -= 1;
                    selectNo = 5;
                }
                if (selectNo_GamePad == 1)
                {
                    sliderSE.value -= 1;
                    //audioSouceSE.Play();
                    SoundManager.Instance.PlaySe("決定ボタンを押す3");
                    selectNo = 3;
                }
                RegulationSelect();
                //UpdateSelectSprite();
                preTime = deltaTime;
            }
            if (InputManager.Instance.GetButtonDown("UI", "Left"))
            {
                if (selectNo_GamePad == 2)
                {
                    selectNo_2--;
                    //se_select.Play();
                }
                RegulationSelect();
                //UpdateSelectSprite();
                //preTime = deltaTime;
            }

            if (InputManager.Instance.GetButton("UI", "Right"))
            {
                if (selectNo_GamePad == 0)
                {
                    sliderBGM.value += 1;
                    selectNo = 4;
                }
                if (selectNo_GamePad == 1)
                {
                    sliderSE.value += 1;
                    selectNo = 2;
                    if (sliderSE_val != SliderValue.Vol_10)
                    {
                        //audioSouceSE.Play();
                        SoundManager.Instance.PlaySe("決定ボタンを押す3");
                    }

                }
                RegulationSelect();
                //UpdateSelectSprite();

                preTime = deltaTime;
            }
            if (InputManager.Instance.GetButtonDown("UI", "Right"))
            {
                if (selectNo_GamePad == 2)
                {
                    selectNo_2--;
                    //se_select.Play();
                }
                RegulationSelect();
                //UpdateSelectSprite();
                //preTime = deltaTime;
            }

            UpdateSelectSprite();

            if (InputManager.Instance.GetButton("UI", "Decision"))
            {
                switch (selectNo)
                {
                    // SEプラス
                    case 2:
                        sliderSE.value += 0.015f * sliderSpeed;
                        break;
                    // SEマイナス
                    case 3:
                        sliderSE.value -= 0.015f * sliderSpeed;
                        break;
                    // BGMプラス
                    case 4:
                        sliderBGM.value += 0.015f * sliderSpeed;
                        break;
                    // BGMマイナス
                    case 5:
                        sliderBGM.value -= 0.015f * sliderSpeed;
                        break;
                    // 振動プラス
                    case 6:
                        sliderCursorSpeed.value += 0.015f * sliderSpeed;
                        break;
                    // 振動マイナス
                    case 7:
                        sliderCursorSpeed.value -= 0.015f * sliderSpeed;
                        break;
                }
            }
            // セレクト項目決定
            if (InputManager.Instance.GetButtonDown("UI", "Decision"))
            {
                if (!isLoad)
                {
                    //if (isTitleBack)
                    //{
                    //    TitleBack();
                    //    SceneManager.Instance.DestroyScene("s_Option");
                    //}

                    switch (selectNo)
                    {
                        // ボタンオン
                        case 0:
                            selectNo_2 = 0;
                            break;
                        // ボタンオフ
                        case 1:
                            selectNo_2 = 1;
                            break;
                        // キャンセル
                        case 8:
                        case 9:
                            if (postProcessObj != null && !isPostProcess)
                            {
                                postProcessObj.GetComponent<Volume>().enabled = true;
                                isPostProcess = true;
                            }

                            if (isTitleBack)
                            {
                                TitleBack();
                                SceneManager.Instance.DestroyScene("s_Option");
                            }
                            else if (isSelectBack)
                            {
                                SelectBack();
                                SceneManager.Instance.DestroyScene("s_Option");
                            }
                            else
                            {
                                foreach (string loadSceneName in loadSceneNameList)
                                {
                                    // シーン読み込み
                                    SceneManager.Instance.LoadScene(loadSceneName);
                                    // シーン変更
                                    SceneManager.Instance.ChangeScene();
                                }
                                // シーン削除
                                foreach (string unLoadSceneName in unLoadSceneNameList_Before)
                                {
                                    SceneManager.Instance.DestroyScene(unLoadSceneName);
                                }
                                //PauseManager.Instance.PauseOff();

                            }
                            isLoad = true;
                            break;
                    }

                    //else
                    //{
                    //    // シーン読み込み
                    //    string loadSceneName = loadSceneNameList[selectNo];
                    //    SceneManager.Instance.LoadScene(loadSceneName);


                    //    // シーン変更
                    //    SceneManager.Instance.ChangeScene();

                    //    // シーン削除
                    //    foreach (string unLoadSceneName in unLoadSceneNameList)
                    //    {
                    //        if (unLoadSceneName != loadSceneName)
                    //        {
                    //            SceneManager.Instance.DestroyScene(unLoadSceneName);
                    //        }
                    //    }

                    //}
                    //isLoad = true;
                    //se_back.Play();
                    SoundManager.Instance.PlaySe("SE_UIBack");
                }
            }

            if (InputManager.Instance.GetButtonDown("UI", "Cancel") || InputManager.Instance.GetButtonDown("UI", "Operation"))
            {
                if (!isLoad)
                {
                    if (postProcessObj != null && !isPostProcess)
                    {
                        postProcessObj.GetComponent<Volume>().enabled = true;
                        isPostProcess = true;
                    }

                    if (isTitleBack)
                    {
                        TitleBack();
                        SceneManager.Instance.DestroyScene("s_Option");
                    }
                    else if (isSelectBack)
                    {
                        SelectBack();
                        SceneManager.Instance.DestroyScene("s_Option");
                    }
                    else
                    {
                        foreach (string loadSceneName in loadSceneNameList)
                        {
                            // シーン読み込み
                            SceneManager.Instance.LoadScene(loadSceneName);
                            // シーン変更
                            SceneManager.Instance.ChangeScene();
                        }
                        // シーン削除
                        foreach (string unLoadSceneName in unLoadSceneNameList_Before)
                        {
                            SceneManager.Instance.DestroyScene(unLoadSceneName);
                        }
                        //PauseManager.Instance.PauseOff();

                    }
                    isLoad = true;
                    //se_back.Play();
                    SoundManager.Instance.PlaySe("SE_UIBack");
                }
            }
        }

        if (cursorData != null)
        {
            MoveCursor();
        }

    }

    private void LateUpdate()
    {
        //スライダーの値更新
        SetValueFromSlider();
        if (audioMixerArray != null)
        {
            foreach (AudioMixer audioMixer in audioMixerArray)
            {
                if (audioMixer != null)
                {
                    audioMixer.SetFloat("BGM_Vol", (float)ConvertVolumeFromSlider(sliderBGM_val));
                    audioMixer.SetFloat("SE_Vol", (float)ConvertVolumeFromSlider(sliderSE_val));
                }
            }
        }

        // CursorSpeedManager.Instance.cursorSpeed = (float)ConvertCursorSpeedFromSlider(sliderCursor_val) * 0.01f;
        SetVibration();
    }

    // 選択項目の整理
    void RegulationSelect()
    {
        if (selectNo_GamePad < 0)
        {
            selectNo_GamePad = sectionList.Count - 1;
        }
        if (selectNo_GamePad > sectionList.Count - 1)
        {
            selectNo_GamePad = 0;
        }

        if (selectNo_2 < 0)
        {
            selectNo_2 = vibrationSelectList.Count - 1;
        }
        if (selectNo_2 > vibrationSelectList.Count - 1)
        {
            selectNo_2 = 0;
        }


    }

    // 選択項目のスプライト更新
    void UpdateSelectSprite()
    {

        //  選択項目のスプライト更新
        for (int i = 0; i < selectList.Count; i++)
        {
            if (i == selectNo)
            {
                // アウトラインを表示
                if (i >= 2)
                {
                    if (i >= 8)
                    {
                        selectList[i].GetComponent<SpriteRenderer>().material = outlineMaterial;
                    }
                    else
                    {
                        selectList[i].GetComponent<SpriteRenderer>().material = outlineMaterialSlider;

                    }
                    //OutLineList[i].enabled = true;
                    // プラス
                    if (i == 2 || i == 4 || i == 6)
                    {
                        selectList[i].GetComponent<SpriteRenderer>().sprite = spriteSliderPlusHold;
                    }
                    // マイナス
                    if (i == 3 || i == 5 || i == 7)
                    {
                        selectList[i].GetComponent<SpriteRenderer>().sprite = spriteSliderMinusHold;
                    }
                }
                else
                {
                    selectList[i].GetComponent<SpriteRenderer>().material = outlineMaterialButton;
                }
            }
            else
            {
                selectList[i].GetComponent<SpriteRenderer>().material = normalMaterial;
                //OutLineList[i].enabled = false;
                // プラス
                if (i == 2 || i == 4 || i == 6)
                {
                    selectList[i].GetComponent<SpriteRenderer>().sprite = spriteSliderPlusDefault;
                }
                // マイナス
                if (i == 3 || i == 5 || i == 7)
                {
                    selectList[i].GetComponent<SpriteRenderer>().sprite = spriteSliderMinusDefault;
                }
            }
        }
        for (int i = 0; i < vibrationSelectList.Count; i++)
        {
            if (i == selectNo_2)
            {
                // アウトラインを表示
                //vibrationOutLineList[i].enabled = true;
              //  vibrationSelectList[i].GetComponent<changeSprite>().SetState("Default");
             //   sectionList[2].GetComponent<changeSprite>().SetState("Hover");
            }
            else
            {
                //vibrationOutLineList[i].enabled = false;
           //     vibrationSelectList[i].GetComponent<changeSprite>().SetState("Hover");
           //     sectionList[2].GetComponent<changeSprite>().SetState("Default");
            }
        }

    }

    //アウトラインのセット
    //void SetOutLine()
    //{
    //    for (int n = 0; n < selectList.Count; n++)
    //    {
    //        OutLineList.Add(selectList[n].GetComponent<Outline>());
    //    }

    //    for(int n = 0; n < vibrationSelectList.Count; n++)
    //    {
    //        vibrationOutLineList.Add(vibrationSelectList[n].GetComponent<Outline>());
    //    }
    //}

    void SetValueFromSlider()
    {
        sliderBGM_val = (SliderValue)sliderBGM.value;
        sliderSE_val = (SliderValue)sliderSE.value;
        sliderCursor_val = (SliderValue)sliderCursorSpeed.value;

        if (sliderBGM_val == SliderValue.Vol_0)
        {
        //    sectionList[0].GetComponent<changeSprite>().SetState("Hover");
        }
        else
        {
       //     sectionList[0].GetComponent<changeSprite>().SetState("Default");
        }

        if (sliderSE_val == SliderValue.Vol_0)
        {
        //    sectionList[1].GetComponent<changeSprite>().SetState("Hover");
        }
        else
        {
        //    sectionList[1].GetComponent<changeSprite>().SetState("Default");
        }
    }

    VolumeParamete ConvertVolumeFromSlider(SliderValue sliderValue)
    {
        VolumeParamete vol_P = VolumeParamete.Vol_0;
        switch (sliderValue)
        {
            case SliderValue.Vol_0:
                vol_P = VolumeParamete.Vol_0;
                break;
            case SliderValue.Vol_1:
                vol_P = VolumeParamete.Vol_1;
                break;
            case SliderValue.Vol_2:
                vol_P = VolumeParamete.Vol_2;
                break;
            case SliderValue.Vol_3:
                vol_P = VolumeParamete.Vol_3;
                break;
            case SliderValue.Vol_4:
                vol_P = VolumeParamete.Vol_4;
                break;
            case SliderValue.Vol_5:
                vol_P = VolumeParamete.Vol_5;
                break;
            case SliderValue.Vol_6:
                vol_P = VolumeParamete.Vol_6;
                break;
            case SliderValue.Vol_7:
                vol_P = VolumeParamete.Vol_7;
                break;
            case SliderValue.Vol_8:
                vol_P = VolumeParamete.Vol_8;
                break;
            case SliderValue.Vol_9:
                vol_P = VolumeParamete.Vol_9;
                break;
            case SliderValue.Vol_10:
                vol_P = VolumeParamete.Vol_10;
                break;
        }
        return vol_P;
    }
    CursorSpeedParamete ConvertCursorSpeedFromSlider(SliderValue sliderValue)
    {
        CursorSpeedParamete vol_P = CursorSpeedParamete.Speed_0;
        switch (sliderValue)
        {
            case SliderValue.Vol_0:
                vol_P = CursorSpeedParamete.Speed_0;
                break;
            case SliderValue.Vol_1:
                vol_P = CursorSpeedParamete.Speed_1;
                break;
            case SliderValue.Vol_2:
                vol_P = CursorSpeedParamete.Speed_2;
                break;
            case SliderValue.Vol_3:
                vol_P = CursorSpeedParamete.Speed_3;
                break;
            case SliderValue.Vol_4:
                vol_P = CursorSpeedParamete.Speed_4;
                break;
            case SliderValue.Vol_5:
                vol_P = CursorSpeedParamete.Speed_5;
                break;
            case SliderValue.Vol_6:
                vol_P = CursorSpeedParamete.Speed_6;
                break;
            case SliderValue.Vol_7:
                vol_P = CursorSpeedParamete.Speed_7;
                break;
            case SliderValue.Vol_8:
                vol_P = CursorSpeedParamete.Speed_8;
                break;
            case SliderValue.Vol_9:
                vol_P = CursorSpeedParamete.Speed_9;
                break;
            case SliderValue.Vol_10:
                vol_P = CursorSpeedParamete.Speed_10;
                break;
        }
        return vol_P;
    }

    SliderValue ConvertSliderFromVolume(VolumeParamete volume)
    {
        SliderValue sliderVal = SliderValue.Vol_0;
        switch (volume)
        {
            case VolumeParamete.Vol_0:
                sliderVal = SliderValue.Vol_0;
                break;
            case VolumeParamete.Vol_1:
                sliderVal = SliderValue.Vol_1;
                break;
            case VolumeParamete.Vol_2:
                sliderVal = SliderValue.Vol_2;
                break;
            case VolumeParamete.Vol_3:
                sliderVal = SliderValue.Vol_3;
                break;
            case VolumeParamete.Vol_4:
                sliderVal = SliderValue.Vol_4;
                break;
            case VolumeParamete.Vol_5:
                sliderVal = SliderValue.Vol_5;
                break;
            case VolumeParamete.Vol_6:
                sliderVal = SliderValue.Vol_6;
                break;
            case VolumeParamete.Vol_7:
                sliderVal = SliderValue.Vol_7;
                break;
            case VolumeParamete.Vol_8:
                sliderVal = SliderValue.Vol_8;
                break;
            case VolumeParamete.Vol_9:
                sliderVal = SliderValue.Vol_9;
                break;
            case VolumeParamete.Vol_10:
                sliderVal = SliderValue.Vol_10;
                break;
        }
        return sliderVal;
    }
    SliderValue ConvertSliderFromCursorSpeed(CursorSpeedParamete volume)
    {
        SliderValue sliderVal = SliderValue.Vol_0;
        bool isLoop;
        do
        {
            isLoop = false;
            switch (volume)
            {
                case CursorSpeedParamete.Speed_0:
                    sliderVal = SliderValue.Vol_0;
                    break;
                case CursorSpeedParamete.Speed_1:
                    sliderVal = SliderValue.Vol_1;
                    break;
                case CursorSpeedParamete.Speed_2:
                    sliderVal = SliderValue.Vol_2;
                    break;
                case CursorSpeedParamete.Speed_3:
                    sliderVal = SliderValue.Vol_3;
                    break;
                case CursorSpeedParamete.Speed_4:
                    sliderVal = SliderValue.Vol_4;
                    break;
                case CursorSpeedParamete.Speed_5:
                    sliderVal = SliderValue.Vol_5;
                    break;
                case CursorSpeedParamete.Speed_6:
                    sliderVal = SliderValue.Vol_6;
                    break;
                case CursorSpeedParamete.Speed_7:
                    sliderVal = SliderValue.Vol_7;
                    break;
                case CursorSpeedParamete.Speed_8:
                    sliderVal = SliderValue.Vol_8;
                    break;
                case CursorSpeedParamete.Speed_9:
                    sliderVal = SliderValue.Vol_9;
                    break;
                case CursorSpeedParamete.Speed_10:
                    sliderVal = SliderValue.Vol_10;
                    break;
                default:
                    if (!isLoop)
                    {
                        volume--;
                    }
                    else
                    {
                        volume++;
                    }
                    isLoop = true;
                    break;
            }
        } while (isLoop);
        return sliderVal;
    }

    void SetVibration()
    {
        if (selectNo_2 == 0)
        {
            //On
            InputManager.Instance.isVibrationCannot = false;
            selectNo = 0;
        }
        else
        {
            //Off
            InputManager.Instance.isVibrationCannot = true;
            selectNo = 1;
        }
    }

    void TitleBack()
    {
        GameObject titleObj = GameObject.Find("TitleSceneManager");
        TitleSceneSelect titlescene = titleObj.GetComponent<TitleSceneSelect>();
        titlescene.SetLoadFlag(false);
    }
    void SelectBack()
    {

        //GameObject selectObj = GameObject.Find("MouceCursol");
        //MouseControllers selectscene = selectObj.GetComponent<MouseControllers>();
        //selectscene.SetLoadFlag(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("s_StageSelect");
        //SceneManager.Instance.LoadScene("s_StageSelect");
        //SceneManager.Instance.ChangeScene();
    }
    void MoveCursor()
    {
        //Transform trans = obj.GetComponent<Transform>();
        //cursor.position = new Vector3(cursor.position.x, trans.position.y, cursor.position.z);
        if (!isLoad)
        {
            if (InputManager.Instance != null)
            {
                if (!InputManager.Instance.isUseGamepad)
                {
                    float positionX_Mouse = InputManager.Instance.GetStick("Cursor", "MoveMouse").x;
                    float positionY_Mouse = InputManager.Instance.GetStick("Cursor", "MoveMouse").y;
                    //float positionX_Gamepad = InputManager.Instance.GetStick("Cursor", "MoveGamepad").x;
                    //float positionY_Gamepad = InputManager.Instance.GetStick("Cursor", "MoveGamepad").y;
                    //Vector3 movement = new Vector2(positionX_Mouse, positionY_Mouse) * moveSpeedMouse + new Vector2(positionX_Gamepad, positionY_Gamepad) * moveSpeed;
                    // Vector3 movement = new Vector2(positionX_Mouse, positionY_Mouse) * CursorSpeedManager.Instance.cursorSpeed;
                    //  cursor.position += (movement / 10);
                    cursorData.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    cursor.position = new Vector3(selectList[selectNo].transform.position.x, selectList[selectNo].transform.position.y, selectList[selectNo].transform.position.z);
                    cursorData.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        else
        {
            Vector2 movement = new Vector2(0, 0) * moveSpeed;
            cursor.position = movement;
        }
        Debug.Log("cursor" + cursor.localPosition);
        if (cursor.localPosition.x >= 935)
        {
            cursor.localPosition = new Vector3(935, cursor.localPosition.y, cursor.localPosition.z);
        }
        if (cursor.localPosition.x <= -935)
        {
            cursor.localPosition = new Vector3(-935, cursor.localPosition.y, cursor.localPosition.z);
        }
        if (cursor.localPosition.y >= 495)
        {
            cursor.localPosition = new Vector3(cursor.localPosition.x, 495, cursor.localPosition.z);
        }
        if (cursor.localPosition.y <= -495)
        {
            cursor.localPosition = new Vector3(cursor.localPosition.x, -495, cursor.localPosition.z);
        }
    }
}