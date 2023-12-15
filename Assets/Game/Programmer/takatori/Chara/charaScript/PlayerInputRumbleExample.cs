using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputRumbleExample : MonoBehaviour
{
    private IEnumerator Start()
    {
        // PlayerInputインスタンスを取得
        var playerInput = GetComponent<PlayerInput>();

        // PlayerInputから振動可能なデバイス取得
        // playerInput.devicesは現在選択されているスキームのデバイス一覧であることに注意
        if (playerInput.devices.FirstOrDefault(x => x is IDualMotorRumble) is not IDualMotorRumble gamepad)
        {
           
            yield break;
        }
        
        // 振動
   

        gamepad.SetMotorSpeeds(1.0f, 0.0f);
        yield return new WaitForSeconds(10.0f);

        gamepad.SetMotorSpeeds(0.0f, 1.0f);
        yield return new WaitForSeconds(10.0f);

        gamepad.SetMotorSpeeds(0.0f, 0.0f);

  
    }
}