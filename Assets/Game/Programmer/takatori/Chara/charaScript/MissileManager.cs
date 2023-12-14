using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public Missile missilePrefab;  // プレハブ

    // ミサイル生成メソッド
    public Missile CreateMissile(Vector3 position, Quaternion rotation)
    {
        Missile newMissile = Instantiate(missilePrefab, position, rotation);
        // ここで必要な初期化処理を行う
        return newMissile;
    }

    // ミサイル再利用メソッド
    public void ReactivateMissile(Missile missile)
    {
        // ここで必要な初期化処理を行う
        missile.gameObject.SetActive(true);
    }
}
