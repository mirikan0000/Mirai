using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommentTrigger : MonoBehaviour
{
    public Camera PlayerCamera;
    private Vector3 lastPosition;       // 直前の座標
    private bool positionStable;        // 座標が一定時間変化していないかのフラグ
    private PlayerManager_ playermanager;
    private bool isMovingValue;
    private DropPlayer dropplayer;
    private bool dropplayerFlag;
    private PlayerHealth playerhealth;
    private bool playerhealthFlag;
    private Weapon weapon;
    private bool reloadflag;

    public CommentManager commentManager;

    public float timeThreshold = 5.0f; // 何秒間座標が変化しないかの閾値
    private float timer;               // 経過時間

    public GameObject targetObject;
    private Vector3 targetlastPosition;
    private bool targetpositionStable;
    private PlayerManager_ targetplayermanager;
    private bool targetIsMovingValue;
    private DropPlayer targetdropplayer;
    private bool targetdropplayerFlag;
    private PlayerHealth targetplayerhealth;
    private bool targetplayerhealthFlag;

    private bool dropable = false;

    private static readonly string[] FIND = new string[] { "あれ敵じゃない？", "敵おったな", "ちゃんと画面見てる？" };
    private static readonly string[] SEARCH = new string[] { "敵どこ？", "こっちから見えないな" };
    private static readonly string[] STAY = new string[] { "エンスト？", "おもんな", "相手の方行くわー", "戦略だろ", "ブロックしろ", "加藤純二最強!!" };
    private static readonly string[] ENEMYSTAY = new string[] { "相手動いてないな", "敵ビビッて動かんやんw" };
    private static readonly string[] SUPPLIES = new string[] { "ケアパケきた！", "ケアパケどこ？", "物資探しに行こ！" };
    private static readonly string[] NOTMOVE = new string[] { "ゲージ見ろ", "相手に位置伝えるわw", "適度に止まるといいですよ" };
    private static readonly string[] ENEMTNOTMOVE = new string[] { "相手ガス欠してるよ", "ガス欠とかギャグやろw", "チャンスや潰せw" };
    private static readonly string[] UPGRADE = new string[] { "中身えぐ", "勝ったな" };
    private static readonly string[] ENEMYUPGRADE = new string[] { "物資とられた？", "相手強くなった" };
    private static readonly string[] HIT = new string[] { "今の避けろよ", "痛くね？", "まだ勝てる", "終わった" };
    private static readonly string[] ENEMYHIT = new string[] { "うっま", "これはやってる", "ナイス！", "いいね", "やるやん", "おおおおおおおお" };
    private static readonly string[] SPAWNER = new string[] { "次の安置は？", "エリア見よ" };
    private static readonly string[] RELOAD = new string[] { "弾込めて！", "ナイスリロード", "弾大事にしよ" };

    void Start()
    {
        InitializePositionTracking();
        playermanager = GetComponent<PlayerManager_>();
        dropplayer = GetComponent<DropPlayer>();
        playerhealth = GetComponent<PlayerHealth>();
        weapon = GetComponent<Weapon>();

        targetplayermanager = targetObject.GetComponent<PlayerManager_>();
        targetdropplayer = targetObject.GetComponent<DropPlayer>();
        targetplayerhealth = targetObject.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        isMovingValue = playermanager.GetIsMoving();
        dropplayerFlag = dropplayer.GetOpenBoxFlag();
        playerhealthFlag = playerhealth.GetHitFlag();
        reloadflag = weapon.GetReloadFlag();

        targetIsMovingValue = targetplayermanager.GetIsMoving();
        targetdropplayerFlag = targetdropplayer.GetOpenBoxFlag();
        targetplayerhealthFlag = targetplayerhealth.GetHitFlag();

        UpdatePosition();

        if (PlayerCamera != null && targetObject != null)
        {
            // カメラがオブジェクトを映しているかどうかを確認
            if (IsObjectVisibleInCamera())
            {
                string find = FIND.ElementAt(Random.Range(0, FIND.Count()));
                commentManager.SetCommentTextWithWeight(find, 2);
            }
            else
            {
                string search = SEARCH.ElementAt(Random.Range(0, SEARCH.Count()));
                commentManager.SetCommentTextWithWeight(search, 1);
            }
        }
        //自プレイヤーが動いてない場合
        if (positionStable)
        {
            string stay = STAY.ElementAt(Random.Range(0, STAY.Count()));
            commentManager.SetCommentTextWithWeight(stay, 1);
        }
        //敵プレイヤーが動いてない場合
        if (targetpositionStable)
        {
            string enemystay = ENEMYSTAY.ElementAt(Random.Range(0, ENEMYSTAY.Count()));
            commentManager.SetCommentTextWithWeight(enemystay, 2);
        }
        //物資が出現した場合
        if (dropable)
        {
            string supplies = SUPPLIES.ElementAt(Random.Range(0, SUPPLIES.Count()));
            commentManager.SetCommentTextWithWeight(supplies, 2);
        }
        //物資を取得した
        if (dropplayerFlag)
        {
            string upgrade = UPGRADE.ElementAt(Random.Range(0, UPGRADE.Count()));
            commentManager.SetCommentTextWithWeight(upgrade, 2);
        }

        //敵が物資を取得した
        if (targetdropplayerFlag)
        {
            string enemyupgrade = ENEMYUPGRADE.ElementAt(Random.Range(0, ENEMYUPGRADE.Count()));
            commentManager.SetCommentTextWithWeight(enemyupgrade, 2);
        }

        //被弾
        if (playerhealth)
        {
            string hit = HIT.ElementAt(Random.Range(0, HIT.Count()));
            commentManager.SetCommentTextWithWeight(hit, 2);
        }

        //敵が被弾
        if (targetplayerhealthFlag)
        {
            string enemyhit = ENEMYHIT.ElementAt(Random.Range(0, ENEMYHIT.Count()));
            commentManager.SetCommentTextWithWeight(enemyhit, 2);
        }

        //移動エネルギーが切れた場合
        if (!isMovingValue)
        {
            string notmove = NOTMOVE.ElementAt(Random.Range(0, NOTMOVE.Count()));
            commentManager.SetCommentTextWithWeight(notmove, 2);
        }
        //敵の移動エネルギーが切れた場合
        if (!targetIsMovingValue)
        {
            string enemynotmove = ENEMTNOTMOVE.ElementAt(Random.Range(0, ENEMTNOTMOVE.Count()));
            commentManager.SetCommentTextWithWeight(enemynotmove, 2);
        }

        //リロード中
        if (reloadflag)
        {
            string reload = RELOAD.ElementAt(Random.Range(0, RELOAD.Count()));
            commentManager.SetCommentTextWithWeight(reload, 2);
        }
    }

    private bool IsObjectVisibleInCamera()
    {
        // オブジェクトがカメラのビューフラストに含まれているかどうかをチェック
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        Collider objectCollider = targetObject.GetComponent<Collider>();

        if (objectCollider != null)
        {
            return GeometryUtility.TestPlanesAABB(planes, objectCollider.bounds);
        }

        return false;
    }

    void InitializePositionTracking()
    {
        // 初期化時に座標とタイマーを設定
        lastPosition = transform.position;
        targetlastPosition = targetObject != null ? targetObject.transform.position : Vector3.zero;
        timer = 0f;
        positionStable = false;
        targetpositionStable = false;
    }

    void UpdatePosition()
    {
        if (IsPositionStable())
        {
            // 一定時間座標が変化していない場合の処理
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !positionStable)
            {
                // 一定時間経過したらフラグを立てる
                positionStable = true;
            }
        }
        else
        {
            // 座標が変化した場合はフラグをリセット
            timer = 0f;
            positionStable = false;

            //物資が生成された場合のフラグ
            GameObject dropper = GameObject.FindWithTag("Item");
            if (dropper != null)
            {
                dropable = true;
            }
            dropable = false;
        }

        if (IsTargetPositionStable())
        {
            // 一定時間座標が変化していない場合の処理
            timer += Time.deltaTime;

            if (timer >= timeThreshold && !targetpositionStable)
            {
                // 一定時間経過したらフラグを立てる
                targetpositionStable = true;
            }
        }
        else
        {
            // 座標が変化した場合はフラグをリセット
            timer = 0f;
            targetpositionStable = false;
        }

        // 現在の座標を直前の座標として保存
        lastPosition = transform.position;
        targetlastPosition = targetObject.transform.position;
    }

    bool IsPositionStable()
    {
        // 現在の座標と直前の座標を比較し、一致しているか確認
        return transform.position == lastPosition;
    }

    bool IsTargetPositionStable()
    {
        // targetObjectの座標が一定時間変化していないかを判定
        return targetObject != null && targetObject.transform.position == targetlastPosition;
    }
}