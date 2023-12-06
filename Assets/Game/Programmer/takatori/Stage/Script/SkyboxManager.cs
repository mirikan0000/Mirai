using UnityEngine;

public class SkyboxManager : SingletonMonoBehaviour<SkyboxManager>
{
    public Material morningSkybox;
    public Material daySkybox;
    public Material nightSkybox;

    private const float secondsInDay = 300f; // 1���̕b��

    public void UpdateSkybox(float seconds)
    {
        // �����Ŏ��Ԃɉ����ăX�J�C�{�b�N�X��؂�ւ��郍�W�b�N������
        float t = seconds / secondsInDay; // 0����1�͈̔͂ɐ��K��

        // �摜�̑J�ڂ����炩�ɂ���
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        if (t < 0.25f)
        {
            // �����璋�ւ̑J��
            RenderSettings.skybox = morningSkybox;
        }
        else if (t < 0.75f)
        {
            // �������ւ̑J��
            RenderSettings.skybox = daySkybox;
        }
        else
        {
            // �邩�璩�ւ̑J��
            RenderSettings.skybox = nightSkybox;
        }

        // �摜�̑J�ڂ����炩�ɂ���
        RenderSettings.skybox.SetFloat("_Blend", smoothT);
    }
}
