using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateText : MonoBehaviour
{
    public GameObject textPrefab;
    public float speed = 200;

    public void GenerateRandomText()
    {
        string randomText = GenerateRandomString(10); // 10�͐������郉���_���ȕ�����̒����ł��B�K�v�ɉ����ĕύX���Ă�������.
        GameObject newTextObj = Instantiate(textPrefab, transform); // �e���R���|�[�l���g���Ă���I�u�W�F�N�g�ɐݒ�
        newTextObj.GetComponent<Text>().text = randomText; // �e�L�X�g�R���|�[�l���g�Ƀe�L�X�g��ݒ�
        newTextObj.GetComponent<TextPrefab>().speed = speed;
    }

    private string GenerateRandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] randomString = new char[length];
        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {
            randomString[i] = characters[random.Next(characters.Length)];
        }

        return new string(randomString);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GenerateRandomText();
        }
    }
}
