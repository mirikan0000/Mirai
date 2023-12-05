using UnityEngine;

public class ClockController : MonoBehaviour
{
    public bool sec;   // �b�j�̗L��
    public bool secTick;   // �b�j��b���Ƃɓ�������

    public GameObject second;

   [SerializeField] private float currentSeconds; // ���݂̌o�ߕb��
    private float degreesPerSecond = 360f / (5 * 60); // ���b���x�i�ނ�

    void Update()
    {
        if (secTick)
        {
            // ���t���[���b�j��i�߂�
            currentSeconds += Time.deltaTime;
            float rotationAngle = currentSeconds * degreesPerSecond;
            second.transform.rotation = Quaternion.Euler(0, 0, -rotationAngle);
        }
        // �X�J�C�{�b�N�X���X�V
        SkyboxManager.Instance.UpdateSkybox(currentSeconds);
    }

    // �b�j�̊p�x��Ԃ��ÓI���\�b�h
    public static float GetSecondHandAngle(float seconds)
    {
        // 0�b����n�܂��Čܕ��ň�����鎞�v�Ȃ̂ŁA360�x��5���i300�b�j�ň��
        float degreesPerSecond = 360f / (5 * 60);
        return seconds * degreesPerSecond;
    }

    // ���݂̕b����Ԃ��ÓI���\�b�h
    public static float GetSeconds(float time)
    {
        return time % (5 * 60); // 5�����ƂɃ��Z�b�g
    }
}
