using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGage : MonoBehaviour
{
   public PlayerManager_ player;
    public Image image;
   
    private void Update()
    {
        // ”R—¿‚Ì—Ê‚É‰‚¶‚Ärotation‚ğŒvZ‚µA0‚©‚ç90‚ÉƒNƒ‰ƒ“ƒv
        float fuelRatio = Mathf.Clamp01(player.GetcurrentFuel() / player.GetMaxFuel()); // ”R—¿‚ÌŠ„‡i0‚©‚ç1j
        float rotationZ = Mathf.Lerp(-90, 0, fuelRatio); // 0‚©‚ç90‚Ì”ÍˆÍ‚Å”R—¿‚ÌŠ„‡‚É‰‚¶‚Ä‰ñ“]
        image.rectTransform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
