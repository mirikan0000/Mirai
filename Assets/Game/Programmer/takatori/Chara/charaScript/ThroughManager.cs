using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughManager : MonoBehaviour
{
    public PenetratingBullet pb;
    // Start is called before the first frame update
    // ミサイル生成メソッド
    public PenetratingBullet CreatePb(Vector3 position, Quaternion rotation)
    {
        PenetratingBullet newpb = Instantiate(pb, position, rotation);
        // ここで必要な初期化処理を行う
        return newpb;
    }

    // ミサイル再利用メソッド
    public void ReactivateMissile(PenetratingBullet pb)
    {
        // ここで必要な初期化処理を行う
        pb.gameObject.SetActive(true);
    }
}
