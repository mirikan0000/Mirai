using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMissile : Weapon
{

    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();//�v���C���[�C���v�b�g
    public GameObject Missile;//�~�T�C��
    Fragreceiver fragreceiver;
    public bool GetButtonDown(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasPressedThisFrame();
        }

        Debug.Log("���͎�t���s" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButton(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButtonUp(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }

        Debug.Log("ActionMap�����݂��Ȃ�" + actionMapsName + actionsName);
        return false;

    }

    void ShotStep()
    {       //�e�ۗ\����
            //Space�L�[������������ƒe�ۗ\������`�悷��
            if (fragreceiver.misileflog&&(GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire")))
            {

            }

            //�e�۔���
            if (fragreceiver.misileflog && (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire")))
            {
                //�e�ې���
                GameObject buttle = Instantiate(Missile, transform.position, transform.rotation);

            }
        
       
    }
}