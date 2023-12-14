using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughManager : MonoBehaviour
{
    public PenetratingBullet pb;
    // Start is called before the first frame update
    // �~�T�C���������\�b�h
    public PenetratingBullet CreatePb(Vector3 position, Quaternion rotation)
    {
        PenetratingBullet newpb = Instantiate(pb, position, rotation);
        // �����ŕK�v�ȏ������������s��
        return newpb;
    }

    // �~�T�C���ė��p���\�b�h
    public void ReactivateMissile(PenetratingBullet pb)
    {
        // �����ŕK�v�ȏ������������s��
        pb.gameObject.SetActive(true);
    }
}
