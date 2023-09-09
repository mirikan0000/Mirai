//=============================================================================
//
// �T�E���h�}�l�[�W���[����(BGM,SE�𗬂�����)

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
        // �t�F�[�h����
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
    /// BGM�Đ�
    /// </summary>
    public bool PlayBGM(string clipName, bool isLoop = true)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (!audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // �V�[���ύX
                string sceneNowName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                foreach(UnityEngine.SceneManagement.Scene scene in UnityEngine.SceneManagement.SceneManager.GetAllScenes())
                {
                    if(scene.name== "ManagerScene")
                    {
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
                    }
                }
                // AudioSource�����݂��ĂȂ���ΐV��������
                GameObject audioSourceObj = new GameObject();
                // �V�[���߂�
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

            // �Đ�
            audioSource.loop = isLoop;
            audioSource.volume = 1;
            audioSource.Play();
            return true;
        }

        Debug.Log("BGM�̃f�[�^�����݂��Ȃ�" + clipName);
        return false;
    }

    /// <summary>
    /// BGM��~
    /// </summary>
    public bool StopBGM(string clipName)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // ��~
                audioSource.Stop();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGM�ꎞ��~
    /// </summary>
    public bool PauseBGM(string clipName)
    {
        if (audioStructDictionary.TryGetValue(clipName, out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (audioSourceBGMDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // �ꎞ��~
                audioSource.Pause();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGM�t�F�[�h�Đ�
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
    /// BGM�t�F�[�h��~
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
    /// BGM�t�F�[�h�ꎞ��~
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
    /// SE�Đ�
    /// </summary>
    public bool PlaySe(string clipName, bool isOverlap = true)
    {
        if(audioStructDictionary.TryGetValue(clipName,out AudioStruct audioStruct))
        {
            AudioSource audioSource;
            if (!audioSourceSEDictionary.TryGetValue(audioStruct.clip.name, out audioSource))
            {
                // AudioSource�����݂��ĂȂ���ΐV��������
                GameObject audioSourceObj = new GameObject();
                audioSource = audioSourceObj.AddComponent<AudioSource>();
                audioSource.clip = audioStruct.clip;
                audioSource.loop = false;
                audioSource.outputAudioMixerGroup = audioStruct.audioMixerGroup;
                audioSourceSEDictionary[audioStruct.clip.name] = audioSource;
            }

            // �Đ�
            if (audioSource != null)
            {
                if (isOverlap)
                {
                    // �d�˂čĐ�
                    audioSource.PlayOneShot(audioStruct.clip);
                }
                else
                {
                    // 1��1�Đ�
                    audioSource.Play();
                }
            }
            return true;
        }

        Debug.Log("SE�̃f�[�^�����݂��Ȃ�" + clipName);
        return false;
    }

    /// <summary>
    /// ������SE�͍Đ����Ă邩
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
