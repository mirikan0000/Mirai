using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeOut : MonoBehaviour
{

    public GameObject Panelfade;   //�t�F�[�h�p�l���̎擾

    Image fadealpha;               //�t�F�[�h�p�l���̃C���[�W�擾�ϐ�

    private float alpha;           //�p�l����alpha�l�擾�ϐ�
    [SerializeField, Range(0.0f, 0.01f)]
    public float alphaSpeed;
     public bool fadeout;          //�t�F�[�h�A�E�g�̃t���O�ϐ�
     public bool fadein;

    // Use this for initialization
    void Start()
    {
        
    }
    private void Update()
    {
        if (fadein)
        {
            FadeIn();
        }
    }

    public void Fadeout()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //�p�l���̃C���[�W�擾
        alpha = fadealpha.color.a;                 //�p�l����alpha�l���擾
        fadeout = true;                             //�V�[���ǂݍ��ݎ��Ƀt�F�[�h�C��������
        alpha += alphaSpeed;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            fadeout = false;
        }
    }
    public void FadeIn()
    {
        fadealpha = Panelfade.GetComponent<Image>(); //�p�l���̃C���[�W�擾
        alpha = fadealpha.color.a;                 //�p�l����alpha�l���擾
        fadeout = true;                             //�V�[���ǂݍ��ݎ��Ƀt�F�[�h�C��������
   
            alpha -= alphaSpeed;
            fadealpha.color = new Color(0, 0, 0, alpha);
        
       
        if (alpha <= 0)
        {
            fadein = false;
        }
    }

}
