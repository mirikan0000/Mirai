using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip footstepSound;

    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footstepSound);
    }
}
