using System;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public bool sec;   // •bj‚Ì—L–³
    public bool secTick;   // •bj‚ğ•b‚²‚Æ‚É“®‚©‚·‚©

   
    public GameObject second;

    void Update()
    {
        DateTime dt = DateTime.Now;
                second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360);
    }
}
