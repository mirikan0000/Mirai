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
        string randomText = GenerateRandomString(10); // 10は生成するランダムな文字列の長さです。必要に応じて変更してください.
        GameObject newTextObj = Instantiate(textPrefab, transform); // 親をコンポーネントしているオブジェクトに設定
        newTextObj.GetComponent<Text>().text = randomText; // テキストコンポーネントにテキストを設定
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
