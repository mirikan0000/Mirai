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
        // PlayerInput�C���X�^���X���擾
        var playerInput = GetComponent<PlayerInput>();

        // PlayerInput����U���\�ȃf�o�C�X�擾
        // playerInput.devices�͌��ݑI������Ă���X�L�[���̃f�o�C�X�ꗗ�ł��邱�Ƃɒ���
        if (playerInput.devices.FirstOrDefault(x => x is IDualMotorRumble) is not IDualMotorRumble gamepad)
        {
            Debug.Log("�f�o�C�X���ڑ�");
            yield break;
        }
        
        // �U��
        Debug.Log("�R���g���[���U���J�n");

        gamepad.SetMotorSpeeds(1.0f, 0.0f);
        yield return new WaitForSeconds(10.0f);

        gamepad.SetMotorSpeeds(0.0f, 1.0f);
        yield return new WaitForSeconds(10.0f);

        gamepad.SetMotorSpeeds(0.0f, 0.0f);

        Debug.Log("�R���g���[���U����~");
    }
}