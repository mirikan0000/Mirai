using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommentManager : MonoBehaviour
{
    public GameObject textPrefab;
    private Stack<string> situationStack = new Stack<string>();
    public float speed = 200;
    public float commentInterval = 5.0f;
    private float lastCommentTime;

    void Start()
    {
        lastCommentTime = Time.time;
    }

    void Update()
    {
        if (situationStack.Count > 0 && Time.time - lastCommentTime >= commentInterval)
        {
            string topComment = situationStack.Peek();
            GenerateTextPrefab(topComment);
            situationStack.Pop();
            lastCommentTime = Time.time;
        }
    }

    public void SetCommentText(string text)
    {
        situationStack.Push(text);
    }

    private void GenerateTextPrefab(string text)
    {
        GameObject newTextObj = Instantiate(textPrefab, transform);
        TextPrefab textComponent = newTextObj.GetComponent<TextPrefab>();
        textComponent.speed = speed;
        textComponent.SetText(text);
    }
}