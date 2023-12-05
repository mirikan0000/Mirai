using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingEye : MonoBehaviour
{
    public Sprite[] images; // �؂�ւ���摜�̔z��

    private Image imageComponent;
    private bool isBlinking = false;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        // ����̏u�����J�n
        StartBlink();
    }

    private void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            // �摜��؂�ւ���
            imageComponent.sprite = images[0];
            yield return new WaitForSeconds(0.3f);

            imageComponent.sprite = images[1];
            yield return new WaitForSeconds(0.3f);

            // �����_���ȊԊu�Ŏ��̏u���܂őҋ@
            float randomInterval = Random.Range(0.5f, 4f);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
