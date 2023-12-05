using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : SingletonMonoBehaviour<SkyboxManager>
{
    public Material morningSkybox;
    public Material nightSkybox;

    private const float secondsInDay = 24 * 60 * 60; // 1���̕b��

    public void UpdateSkybox(float seconds)
    {
        // �����Ŏ��Ԃɉ����ăX�J�C�{�b�N�X��؂�ւ��郍�W�b�N������
        float t = seconds / secondsInDay; // 0����1�͈̔͂ɐ��K��
        RenderSettings.skybox = (t < 0.5f) ? morningSkybox : nightSkybox;
    }
}
