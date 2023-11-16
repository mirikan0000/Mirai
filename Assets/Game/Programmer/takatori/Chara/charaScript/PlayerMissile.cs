using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMissile : Weapon
{

    private Dictionary<string, PlayerInput> playerInputDictionary = new Dictionary<string, PlayerInput>();//プレイヤーインプット
    public GameObject Missile;//ミサイル
    Fragreceiver fragreceiver;
    public bool GetButtonDown(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasPressedThisFrame();
        }

        Debug.Log("入力受付失敗" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButton(string actionMapsName, string actionsName)
    {


        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].IsPressed();
        }

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;
    }
    public bool GetButtonUp(string actionMapsName, string actionsName)
    {
        if (playerInputDictionary.TryGetValue(actionMapsName, out PlayerInput playerInput))
        {
            return playerInput.actions[actionsName].WasReleasedThisFrame();
        }

        Debug.Log("ActionMapが存在しない" + actionMapsName + actionsName);
        return false;

    }

    void ShotStep()
    {       //弾丸予測線
            //Spaceキーを押し続けると弾丸予測線を描画する
            if (fragreceiver.misileflog&&(GetButtonDown("Player", "Fire") || GetButtonDown("Player1", "Fire")))
            {

            }

            //弾丸発射
            if (fragreceiver.misileflog && (GetButtonUp("Player", "Fire") || GetButtonUp("Player1", "Fire")))
            {
                //弾丸生成
                GameObject buttle = Instantiate(Missile, transform.position, transform.rotation);

            }
        
       
    }
}