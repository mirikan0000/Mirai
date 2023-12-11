using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSourceA;
    [SerializeField]
    AudioSource audioSourceB;
    [SerializeField]
    AudioClip footstepSound;
    [SerializeField]
    AudioClip CanonSound;
    [SerializeField]
    AudioClip hitSE;

    public void PlayFootstepSound()
    {
        audioSourceA.PlayOneShot(footstepSound);
    }
    public void PlayCanonSound()
    {
        audioSourceB.PlayOneShot(CanonSound);
    }   
    public void PlayHitSE()
    {
        audioSourceB.PlayOneShot(hitSE);
    }

}
