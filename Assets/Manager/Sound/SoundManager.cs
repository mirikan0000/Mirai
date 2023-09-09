//=============================================================================
//
// サウンドマネージャー処理(BGM,SEを流す処理)

//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{

    [System.Serializable]
    public struct AudioStruct
    {
        public AudioClip clip;
        public AudioMixerGroup audioMixerGroup;
    }
    public AudioStruct[] audioStructArray;
    private Dictionary<string, AudioStruct> audioStructDictionary = new Dictionary<string, AudioStruct>();
    private Dictionary<string, AudioSource> audioSourceBGMDictionary = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> audioSourceSEDictionary = new Dictionary<string, AudioSource>();

    private bool isFade = false;
    private string fadeClipName;
    private float fadeSpeed = 0;
    private float fadeValue = 0;
    private bool isFadeEndStop = false;
    private bool isFadeEndPause = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioStruct audioStruct in audioStructArray)
        {
            audioStructDictionary[audioStruct.clip.name] = audioStruct;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // フェード処理
        if (isFade)
        {
            if (audioStructDictionary.TryGetValue(fadeClipName, out AudioStruct audioStruct))
            {
                AudioSource audioSource;
                if (audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
                {
                    fadeValue += fadeSpeed * Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(0.0f, 1.0f, fadeValue);
                    if (fadeSpeed > 0)
                    {
                        if (fadeValue >= 1.0f)
                        {
                            isFade = false;
                        }
                    }
                    else
                    {
                        if (fadeValue <= 0)
                        {
                            isFade = false;
                            if (isFadeEndStop)
                            {
                                StopBGM(fadeClipName);
                                isFadeEndStop = false;
                            }
                            if (isFadeEndPause)
                            {
                                PauseBGM(fadeClipName);
                                isFadeEndPause = false;
                            }
                        }
                    }
                }
                else
                {
                    isFade = false;
                }
            }
            else
            {
                isFade = false;
            }

        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    public bool PlayBGM(string clipName, bool isLoop = true)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (!audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // シーン変更
                string sceneNowName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                foreach(UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                {
                    if(scene.name== "ManagerScene")
                    {
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
                    }
                }
                // AudioSourceが存在してなければ新しく生成
                GameObject audioSourceObj = new GameObject();
                // シーン戻す
                foreach (UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                {
                    if(scene.name == sceneNowName)
                    {
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
                    }
                }
                audioSource = audioSourceObj.AddComponent<AudioSource>();
                audioSource.clip = audioStruct.clip;
                audioSource.outputAudioMixerGroup = audioStruct.audioMixerGroup;
                audioSourceBGMDictionary[audioStruct.clip.name] = audioSource;
            }

            // 再生
            audioSource.loop = isLoop;
            audioSource.volume = 1;
            audioSource.Play();
            return true;
        }

        Debug.Log("BGMのデータが存在しない" + clipName);
        return false;
    }

    /// <summary>
    /// BGM停止
    /// </summary>
    public bool StopBGM(string clipName)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // 停止
                audioSource.Stop();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGM一時停止
    /// </summary>
    public bool PauseBGM(string clipName)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // 一時停止
                audioSource.Pause();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGMフェード再生
    /// </summary>
    public bool PlayFadeBGM(string clipName, float _fadeSpeed, bool isLoop = true)
    {
        if (PlayBGM(clipName, isLoop))
        {
            
            isFade = true;
            fadeClipName = clipName;
            fadeSpeed = Mathf.Abs(_fadeSpeed);
            audioSourceBGMDictionary.TryGetValue(clipName, out AudioSource audioSource);
            audioSource.volume = 0;
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGMフェード停止
    /// </summary>
    public bool StopFadeBGM(string clipName, float _fadeSpeed)
    {
        if(audioSourceBGMDictionary.TryGetValue(clipName, out AudioSource audioSource))
        {
            isFade = true;
            fadeClipName = clipName;
            fadeSpeed = -Mathf.Abs(_fadeSpeed);
            isFadeEndStop = true;
            audioSource.volume = 1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// BGMフェード一時停止
    /// </summary>
    public bool PauseFadeBGM(string clipName, float _fadeSpeed)
    {
        if (audioSourceBGMDictionary.TryGetValue(clipName, out AudioSource audioSource))
        {
            isFade = true;
            fadeClipName = clipName;
            fadeSpeed = -Mathf.Abs(_fadeSpeed);
            isFadeEndPause = true;
            audioSource.volume = 1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// SE再生
    /// </summary>
    public bool PlaySe(string clipName, bool isOverlap = true)
    {
        if(audioStructDictionary.TryGetValue(clipName,out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (!audioSourceSEDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // AudioSourceが存在してなければ新しく生成
                GameObject audioSourceObj = new GameObject();
                audioSource = audioSourceObj.AddComponent<AudioSource>();
                audioSource.clip = audioStruct.clip;
                audioSource.loop = false;
                audioSource.outputAudioMixerGroup = audioStruct.audioMixerGroup;
                audioSourceSEDictionary[audioStruct.clip.name] = audioSource;
            }

            // 再生
            if (audioSource != null)
            {
                if (isOverlap)
                {
                    // 重ねて再生
                    audioSource.PlayOneShot(audioStruct.clip);
                }
                else
                {
                    // 1つ1つ再生
                    audioSource.Play();
                }
            }
            return true;
        }

        Debug.Log("SEのデータが存在しない" + clipName);
        return false;
    }

    /// <summary>
    /// 引数のSEは再生してるか
    /// </summary>
    public bool isPlaySe(string clipName) {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (audioSourceSEDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                return audioSourceSEDictionary[audioStruct.clip.name].isPlaying;
            }
        }
        return false;
    }

    public void SoundDataClear()
    {
        audioSourceSEDictionary.Clear();
    }
}
