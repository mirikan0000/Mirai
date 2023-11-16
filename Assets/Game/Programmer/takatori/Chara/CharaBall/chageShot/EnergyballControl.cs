using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class EnergyballControl : MonoBehaviour
{
    public bool shot = false; //射出判定用フラグ
    //Prefab化した弾オブジェクト
    [SerializeField]
    private GameObject energyballPrefab_S; //Prefab化したSサイズのエネルギー弾
    [SerializeField]
    private GameObject energyballPrefab_M; //Prefab化したMサイズのエネルギー弾
    [SerializeField]
    private GameObject energyballPrefab_L; //Prefab化したLサイズのエネルギー弾

    //複製して生成したオブジェクト
    private GameObject enemyball_S; //生成したSサイズのエネルギー弾
    private GameObject enemyball_M; //生成したMサイズのエネルギー弾
    private GameObject enemyball_L; //生成したLサイズのエネルギー弾

    //チャージ状態の判定用フラグ
    private bool sState = false; //S弾用
    private bool mState = false; //M弾用
    private bool lState = false; //L弾用

    //パラメータ
    public float shotSpeed; //発射スピード
    private float time = 0.0f; //経過時間
    [SerializeField]
    private float chargetime = 3.0f; //チャージ時間

    //エフェクト
    [SerializeField]
    private GameObject chargeEB; //チャージエフェクト
    private VisualEffect chargeEB_VFX; //チャージVFX
    private AudioSource chargeEB_SE; //チャージSE
    [SerializeField]
    private GameObject hazeEB; //モヤエフェクト
    private VisualEffect hazeEB_VFX; //モヤVFX
    private AudioSource hazeEB_SE; //モヤSE
    [SerializeField]
    private GameObject fireEB; //火花エフェクト
    private VisualEffect fireEB_VFX; //火花VFX
    private AudioSource fireEB_SE; //火花SE
    PlayerManager_ Player;
    //その他
    Rigidbody rb;
    private PlayerController playerController; //プレイヤーコントロール用スクリプト
    int count_s = 0; //生成したSサイズ弾の数
    int count_m = 0; //生成したMサイズ弾の数
    int count_l = 0; //生成したLサイズ弾の数
    int max = 1; //生成できる弾の数

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>(); //PlayerControllerのコンポーネント情報を取得
        chargeEB_VFX = chargeEB.transform.GetComponent<VisualEffect>();
        chargeEB_SE = chargeEB.transform.GetComponent<AudioSource>();
        hazeEB_VFX = hazeEB.transform.GetComponent<VisualEffect>();
        hazeEB_SE = hazeEB.transform.GetComponent<AudioSource>();
        fireEB_VFX = fireEB.transform.GetComponent<VisualEffect>();
        fireEB_SE = fireEB.transform.GetComponent<AudioSource>();

        //エフェクト停止
        chargeEB_VFX.SendEvent("StopPlay");
        hazeEB_VFX.SendEvent("StopPlay");
        fireEB_VFX.SendEvent("StopPlay");
        chargeEB_SE.Stop();
        hazeEB_SE.Stop();
        fireEB_SE.Stop();
    }
    void Update()
    {
        //ボタンを入力しているとき
      //  if (Player.energyballFlug) Generate(); 
      //エネルギー弾生成用メソッド

        //ボタンを離したとき
       // else Shot(); //エネルギー弾発射用メソッド
    }

    //エネルギー弾の生成処理
    void Generate()
    {
        time += Time.deltaTime; //チャージ時間の加算

        if (time >= 0.5f && time < chargetime) //チャージ時間が0.5秒以上3秒未満のとき
        {
            if (count_s == max) return; //上限に達したときは、何もしない
            else
            {
                OnChargeEffect();
                enemyball_S = Instantiate(energyballPrefab_S, transform.position, Quaternion.identity); //S弾を生成
                enemyball_S.transform.parent = this.transform; //S弾の親を発射ポイントに設定→キャラの移動に追尾させるため
                rb = enemyball_S.GetComponent<Rigidbody>(); //S弾のrigidbodyを取得
                rb.isKinematic = true; //力の影響の無効化
                sState = true; //S弾の準備完了状態
                count_s++; //S弾の生成数を加算
            }
        }
        else if (time >= chargetime && time < chargetime * 2) //チャージ時間が3秒以上6秒未満のとき
        {
            if (count_m == max) return;
            else
            {
                Destroy(enemyball_S); //S弾の削除
                sState = false; //S弾の準備解除状態

                //以下はS弾と同様
                enemyball_M = Instantiate(energyballPrefab_M, transform.position, Quaternion.identity);
                enemyball_M.transform.parent = this.transform;
                rb = enemyball_M.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                mState = true;
                count_m++;
            }
        }
        else if (time >= chargetime * 2) //チャージ時間が6秒以上のとき
        {
            if (count_l == max) return;
            else
            {
                Destroy(enemyball_M);
                mState = false;

                enemyball_L = Instantiate(energyballPrefab_L, transform.position, Quaternion.identity);
                enemyball_L.transform.parent = this.transform;
                rb = enemyball_L.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                lState = true;
                count_l++;
            }
        }
    }
    //エネルギー弾の発射処理
    void Shot()
    {
        OnShotEffect();
        this.transform.DetachChildren(); //親子関係の切り離し→キャラの移動に追尾しないため

        if (sState)
        {
            OnShotEffect(); //S弾の発射エフェクト
            rb.isKinematic = false; //力の影響の有効化
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse); //力の付与（向き、速さ、力の種類の設定）
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up); //発射方向を取得
            enemyball_L.transform.rotation = rotation; //弾を発射方向に設定
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>(); //生成した弾のEnergyballControlを取得
            energyballControl.shot = true; //EnergyballControlのshotフラグをtrueに変更
            Destroy(enemyball_S, 2.0f); //一定時間の経過で消滅
            sState = false; //S弾の準備解除状態
        }
        else if (mState) //M弾、L弾も上記と同様
        {
            OnShotEffect();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            enemyball_L.transform.rotation = rotation;
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>();
            energyballControl.shot = true;
            Destroy(enemyball_M, 2.0f);
            mState = false;
        }
        else if (lState)
        {
            OnShotEffect();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            enemyball_L.transform.rotation = rotation;
            EnergyballControl energyballControl = enemyball_L.GetComponent<EnergyballControl>();
            energyballControl.shot = true;
            Destroy(enemyball_L, 2.0f);
            lState = false;
        }
        //弾数とチャージ経過時間のリセット
        count_s = 0;
        count_m = 0;
        count_l = 0;
        time = 0.0f;
    }
    private void OnChargeEffect()
    {
        //チャージ中のエフェクト開始
        chargeEB_VFX.SendEvent("OnPlay");
        hazeEB_VFX.SendEvent("OnPlay");
        chargeEB_SE.Play();
        hazeEB_SE.Play();
    }
    private void OnShotEffect() //発射エフェクト
    {
        //チャージ中のエフェクト終了
        chargeEB_VFX.SendEvent("StopPlay");
        hazeEB_VFX.SendEvent("StopPlay");
        chargeEB_SE.Stop();
        hazeEB_SE.Stop();

        //発射時のエフェクト開始
        fireEB_VFX.Play();
        fireEB_SE.Play();
    }
}
