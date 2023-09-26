using System.Collections;
using UnityEngine;

public class HitStopController : SingletonMonoBehaviour<HitStopController>
{
    [Tooltip("�X���[�ɂ��鎞�Ԃ̒����i�b�j")]
    public float SlowTimer = 0.1f;

    [Tooltip("�ˌ��������Ƀq�b�g�������̉�")]
    public float HitTimer = 2f;

    /// <summary>
    /// �q�b�g�X�g�b�v���ʂ��A�N�e�B�u�����A�w�肳�ꂽ���Ԃ������s������B
    /// </summary>
    /// <param name="_duration">�q�b�g�X�g�b�v���ʂ̎���</param>
    /// <param name="_isPlayer">�{�X�Ƀq�b�g�X�g�b�v�𔭐������邩</param>
    public void ActivateHitStop(float _duration, bool _isPlayer)
    {
        StartCoroutine(HitStopAction(_duration, _isPlayer));
    }

    private IEnumerator HitStopAction(float _duration, bool _isPlayer)
    {
        if (!PauseManager.Instance.isPause)
        {
            // ���Ԃ̒�~
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(_duration);

            if (_isPlayer)
            {
                // �X���[���[�V�����ɐ؂�ւ���
                Time.timeScale = HitTimer;
                yield return new WaitForSecondsRealtime(HitTimer);
            }

            // ���Ԃ̍ĊJ

            Time.timeScale = 1f;
        }
        Debug.Log("�q�b�g�X�g�b�v");
    }
}