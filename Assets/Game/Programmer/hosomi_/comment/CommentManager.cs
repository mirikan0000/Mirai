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

            // 表示されたコメントに対応するWeightedCommentをスタックから削除
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
            Debug.Log("コメント: " + weightedComment.Comment);
        }
    }

    private bool IsCommentAlreadyInStack(WeightedComment weightedComment)
    {
        // スタック内のすべての要素と新しいコメントを比較
        foreach (WeightedComment comment in situationStack)
        {
            if (comment.Comment == weightedComment.Comment)
            {
                // 重複が見つかった場合
                return true;
            }
        }
        return false; // 重複が見つからなかった場合
    }

    // 重複を確認してからスタックにコメントを追加する
    //public void SetCommentText(string text)
    //{
    //    if (!IsCommentAlreadyInStack(text))
    //    {
    //        situationStack.Push(text);
    //    }
    //}

    // コメントとその重みをスタックに追加するメソッド
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

        // スタックからランダムに要素を取り出す
        WeightedComment[] commentsArray = situationStack.ToArray();
        for (int i = 0; i < Mathf.Min(count, commentsArray.Length); i++)
        {
            int totalWeight = commentsArray.Sum(c => c.Weight);
            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            // 重みに基づいてランダムなコメントを選択
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