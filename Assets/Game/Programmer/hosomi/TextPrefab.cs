using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrefab : MonoBehaviour
{
    private RectTransform rectTransform;
    public float speed;

    public Image parentImage;  // �e�I�u�W�F�N�g��Image�R���|�[�l���g

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        float textHeight = rectTransform.sizeDelta.y;

        // �ŏ��̈ʒu��e�I�u�W�F�N�g��Image�̉E�[�ɂ���
        float parentImageWidth = parentImage.rectTransform.sizeDelta.x;
        float textWidth = rectTransform.sizeDelta.x;
        float startPosX = parentImageWidth / 2 + textWidth / 2;
        float startPosY = Random.Range(-parentImage.rectTransform.sizeDelta.y / 2 + textHeight / 2, parentImage.rectTransform.sizeDelta.y / 2 - textHeight / 2);

        rectTransform.localPosition = new Vector2(startPosX, startPosY);
    }

    // Update is called once per frame
    void Update()
    {
        // speed�ɉ����ĉE���獶�ֈړ�
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        // ��ʊO�ɏo���ꍇ�͎��g���폜����
        if (rectTransform.localPosition.x + rectTransform.sizeDelta.x / 2 < -parentImage.rectTransform.sizeDelta.x / 2)
        {
            Destroy(this.gameObject);
        }
    }

    // �e�L�X�g��ݒ肷��
    public void SetText(string text)
    {
        // Unity��UI Text�R���|�[�l���g���擾���A�e�L�X�g��ݒ肷��
        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}
