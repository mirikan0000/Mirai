using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public Missile missilePrefab;  // �v���n�u

    // �~�T�C���������\�b�h
    public Missile CreateMissile(Vector3 position, Quaternion rotation)
    {
        Missile newMissile = Instantiate(missilePrefab, position, rotation);
        // �����ŕK�v�ȏ������������s��
        return newMissile;
    }

    // �~�T�C���ė��p���\�b�h
    public void ReactivateMissile(Missile missile)
    {
        // �����ŕK�v�ȏ������������s��
        missile.gameObject.SetActive(true);
    }
}
