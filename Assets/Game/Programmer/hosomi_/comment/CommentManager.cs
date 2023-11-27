using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeightedComment
{
    public string Comment { get; set; }
    public int Weight { get; set; }

    public WeightedComment(string comment, int weight)
    {
        Comment = comment;
        Weight = weight;
    }
}

public class CommentManager : MonoBehaviour
{
    public GameObject textPrefab;
    //private Stack<string> situationStack = new Stack<string>();
    public float speed = 200;
    public float commentInterval = 5.0f;
    private float lastCommentTime;
    public int RandCom = 2;
    private Stack<WeightedComment> situationStack = new Stack<WeightedComment>();
    //private List<WeightedComment> weightedComments = new List<WeightedComment>();

    void Start()
    {
        lastCommentTime = Time.time;
    }

    void Update()
    {
        if (situationStack.Count > 0 && Time.time - lastCommentTime >= commentInterval)
        {
            List<WeightedComment> randomComments = GetRandomComments(RandCom);
            foreach (WeightedComment weightedComment in randomComments)
            {
                GenerateTextPrefab(weightedComment.Comment);
            }

            lastCommentTime = Time.time;

            // �\�����ꂽ�R�����g�ɑΉ�����WeightedComment���X�^�b�N����폜
            foreach (WeightedComment weightedComment in randomComments)
            {
                situationStack = new Stack<WeightedComment>(situationStack.Where(comment => comment != weightedComment));
            }
        }

        List<WeightedComment> commentsToRemove = new List<WeightedComment>();

        foreach (WeightedComment commentToRemove in commentsToRemove)
        {
            situationStack = new Stack<WeightedComment>(situationStack.Where(comment => comment != commentToRemove));
        }

        foreach (var weightedComment in situationStack)
        {
            Debug.Log("�R�����g: " + weightedComment.Comment);
        }
    }

    private bool IsCommentAlreadyInStack(WeightedComment weightedComment)
    {
        // �X�^�b�N���̂��ׂĂ̗v�f�ƐV�����R�����g���r
        foreach (WeightedComment comment in situationStack)
        {
            if (comment.Comment == weightedComment.Comment)
            {
                // �d�������������ꍇ
                return true;
            }
        }
        return false; // �d����������Ȃ������ꍇ
    }

    // �d�����m�F���Ă���X�^�b�N�ɃR�����g��ǉ�����
    //public void SetCommentText(string text)
    //{
    //    if (!IsCommentAlreadyInStack(text))
    //    {
    //        situationStack.Push(text);
    //    }
    //}

    // �R�����g�Ƃ��̏d�݂��X�^�b�N�ɒǉ����郁�\�b�h
    public void SetCommentTextWithWeight(string text, int weight)
    {
        WeightedComment weightedComment = new WeightedComment(text, weight);
        if (!IsCommentAlreadyInStack(weightedComment))
        {
            situationStack.Push(weightedComment);
        }
    }

    private List<WeightedComment> GetRandomComments(int count)
    {
        List<WeightedComment> randomComments = new List<WeightedComment>();

        // �X�^�b�N���烉���_���ɗv�f�����o��
        WeightedComment[] commentsArray = situationStack.ToArray();
        for (int i = 0; i < Mathf.Min(count, commentsArray.Length); i++)
        {
            int totalWeight = commentsArray.Sum(c => c.Weight);
            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            // �d�݂Ɋ�Â��ă����_���ȃR�����g��I��
            foreach (WeightedComment weightedComment in commentsArray)
            {
                cumulativeWeight += weightedComment.Weight;
                if (randomValue < cumulativeWeight)
                {
                    randomComments.Add(weightedComment);
                    break;
                }
            }
        }

        return randomComments;
    }


    private void GenerateTextPrefab(string text)
    {
        GameObject newTextObj = Instantiate(textPrefab, transform);
        TextPrefab textComponent = newTextObj.GetComponent<TextPrefab>();
        textComponent.speed = speed;
        textComponent.SetText(text);
    }
}