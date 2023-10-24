using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio2 : MonoBehaviour
{
    // 音データの再生装置を格納する変数
    private AudioSource audio;

    // 音データを格納する変数（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時にAudioSource（音再生装置）のコンポーネントを加える
        audio = gameObject.AddComponent<AudioSource>();

    }

    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手にPlayer1タグが付いているとき
        if (collision.gameObject.tag == "Player2")
        {
            // 音（sound）を一度だけ（PlayOneShot）再生する
            audio.PlayOneShot(sound);
        }
    }
}
