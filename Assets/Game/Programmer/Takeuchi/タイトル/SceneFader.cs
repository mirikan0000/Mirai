using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    [Header("�t�F�[�h�A�E�g�֌W")]
    public Image fadePanel;
    public float fadeDeration = 1.0f;
    public bool fadein;

    [Header("Manager�֌W")]
    private GameObject managerObj;
    private Tittle_Manager managerScript;

    void Start()
    {
        //�t�F�[�h�Ǘ��t���O������
        fadein = false;

        //Manager�̃X�N���v�g�擾
        managerObj = GameObject.Find("Manager");
        managerScript = managerObj.GetComponent<Tittle_Manager>();
    }

    void Update()
    {
        if (fadein == true)
        {
            CallCoroutine();
        }
    }

    //��ʂ��t�F�[�h�A�E�g
    public void CallCoroutine()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        //�p�l����L����
        fadePanel.enabled = true;

        //�o�ߎ��ԏ�����
        float elapsedTime = 0.0f;

        //�t�F�[�h�p�l���̊J�n�F���擾
        Color startColor = fadePanel.color;

        //�t�F�[�h�p�l���̍ŏI�F��ݒ�
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        //�t�F�[�h�A�E�g�A�j���[�V���������s
        while (elapsedTime < fadeDeration)
        {
            //�o�ߎ��Ԍv��
            elapsedTime += Time.deltaTime;

            //�t�F�[�h�̐i�s�x���v�Z
            float t = Mathf.Clamp01(elapsedTime / fadeDeration);

            //�p�l���̐F��ύX���ăt�F�[�h�A�E�g
            fadePanel.color = Color.Lerp(startColor, endColor, t);

            //�P�t���[���ҋ@
            yield return null;
        }

        //�t�F�[�h������������ŏI�F�ɐݒ�
        fadePanel.color = endColor;

        //�t�F�[�h�A�E�g�t���O�Ǘ�
        fadein = false;
        managerScript.fadeEnd = true;
    }
}
