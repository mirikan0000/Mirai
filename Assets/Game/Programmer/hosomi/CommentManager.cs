using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommentManager : MonoBehaviour
{
    public GameObject textPrefab;
    private List<string> situationQueue = new List<string>();
    public float speed = 200;
    public float commentInterval = 5.0f;
    private float lastCommentTime;

    void Start()
    {
        lastCommentTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastCommentTime >= commentInterval)
        {
            if (situationQueue.Count > 0)
            {
                GenerateTextPrefab(situationQueue[0]);
                situationQueue.RemoveAt(0);
                lastCommentTime = Time.time;
            }
        }
    }

    public void SetSituationFlag(bool flag)
    {
        // ���葤����̃t���O�ݒ�i�����Ńt���O���󂯎��A�󋵂��L���[�ɒǉ�����Ȃǂ̏������s���j
        if (flag)
        {
            situationQueue.Add("Your situation description here");
        }
    }

    private void GenerateTextPrefab(string text)
    {
        GameObject newTextObj = Instantiate(textPrefab, transform.parent);
        TextPrefab textComponent = newTextObj.GetComponent<TextPrefab>();
        textComponent.speed = speed;
        textComponent.SetText(text);
    }
}