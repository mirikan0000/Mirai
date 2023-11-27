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
    AudioClip BigCanonSoundA;
    [SerializeField]
    AudioClip BigCanonSoundB;
    [SerializeField]
    AudioClip SmallCanonSoundA;
    [SerializeField]
    AudioClip SmallCanonSoundB;
    [SerializeField]
    AudioClip hitSE;

    public void PlayFootstepSound()
    {
        audioSourceA.PlayOneShot(footstepSound);
    }
    public void PlayBigCanonSoundA()
    {
        audioSourceB.PlayOneShot(BigCanonSoundA);
    }
    public void PlayBigCanonSoundB()
    {
        audioSourceB.PlayOneShot(BigCanonSoundB);
    }
    public void PlaySmallCanonSoundA()
    {
        audioSourceB.PlayOneShot(SmallCanonSoundA);
    }
    public void PlaySmallCanonSoundB()
    {
        audioSourceB.PlayOneShot(SmallCanonSoundB);
    }
    public void PlayHitSE()
    {
        audioSourceB.PlayOneShot(hitSE);
    }

}
