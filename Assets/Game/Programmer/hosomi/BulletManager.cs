using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    // 弾の情報を格納する構造体
    public struct BulletInfo
    {
        public Vector2 position;
        //public string bulletType;
    }

    // フィールドにあるすべての弾のリスト
    private List<GameObject> bullets = new List<GameObject>();

    // シーン内の2Dマップに渡すための弾の情報配列
    private List<BulletInfo> bulletInfoList = new List<BulletInfo>();
    int bulletCount;

    // 弾の数を表示するUI TextをInspectorウィンドウから設定
    public Text bulletCountText;

    private void Start()
    {
        // シーン内のすべての弾を取得し、bulletCountに設定
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
        bulletCount = allBullets.Length;

        // シーン内のすべての弾の数を更新
        UpdateBulletCountText();
    }

    // 弾を生成する関数が外部から呼ばれたと仮定
    // 弾を生成したら、これを呼んで弾を管理リストに追加
    public void AddBullet(GameObject bullet, Vector2 position/*, string bulletType*/)
    {
        bullets.Add(bullet);

        // 弾の情報をリストに追加
        BulletInfo bulletInfo;
        bulletInfo.position = position;
        //bulletInfo.bulletType = bulletType;
        bulletInfoList.Add(bulletInfo);

        // シーン内のすべての弾の数を更新
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
    }

    // 弾を削除する関数が外部から呼ばれたと仮定
    public void RemoveBullet(GameObject bullet)
    {
        bullets.Remove(bullet);

        // 弾の情報をリストから削除
        int indexToRemove = -1;
        for (int i = 0; i < bulletInfoList.Count; i++)
        {
            // Vector2とVector3を比較するためにVector2に変換
            Vector2 bulletPosition = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            if (bulletInfoList[i].position == bulletPosition)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove >= 0)
        {
            bulletInfoList.RemoveAt(indexToRemove);
        }

        // シーン内のすべての弾の数を更新
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
    }

    // 弾の情報を取得する関数
    public List<BulletInfo> GetBulletInfoList()
    {
        return bulletInfoList;
    }

    // フィールドにある弾の数を取得する関数
    public int GetBulletCount()
    {
        //Debug.Log("弾の数: " + bulletCount);
        return bulletCount;
    }

    // 弾の情報を取得し、ログに表示する関数
    public void LogBulletInfo()
    {
        //Debug.Log("弾の情報:");
        foreach (var bulletInfo in bulletInfoList)
        {
            //Debug.Log("位置: " + bulletInfo.position/* + ", 種類: " + bulletInfo.bulletType*/);
        }
    }

    // 弾の数をUI Textに表示するメソッド
    private void UpdateBulletCountText()
    {
        // 弾の数をUI Textに反映
        if (bulletCountText != null)
        {
            bulletCountText.text = "弾の数: " + GetBulletCount().ToString();
        }
    }

    void Update()
    {
        // シーン内のすべての弾の数を更新
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        // シーン内のすべての弾の数を更新
        UpdateBulletCountText();
        LogBulletInfo();
    }
}
