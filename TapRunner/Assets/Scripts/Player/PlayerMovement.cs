using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    GameObject PlayerTurbo; // プレイヤーの後ろに表示するアニメーション

    [SerializeField]
    GameObject SavePanel; // 保存のパネル
    #endregion

    public float speed; // プレイヤーの速度
    public float gravity; // 重力
    float playerRotate; // プレイヤーの回転角度
    Rigidbody2D rb; // Rigidbody2D コンポーネント
    Animator animator; // アニメーター

    // 初期化処理
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRotate = Common.GrovalConst.PLAYER_ROTATE_INITIAL;
        SavePanel.SetActive(false); // 保存パネルを非表示
    }

    void Update()
    {
        Vector2 force = new Vector2(speed * Time.deltaTime, 0);
        rb.AddForce(force);

        // プレイヤーの位置を更新する
        transform.position += new Vector3(0, gravity * Time.deltaTime, 0);

        // プレイヤーの回転を更新する
        transform.rotation = Quaternion.Euler(0, 0, playerRotate);
    }

    // プレイヤーがタップされたときの処理
    public void Tap()
    {
        gravity *= Common.GrovalConst.INVERSION; // 重力の方向を反転する
        playerRotate *= Common.GrovalConst.INVERSION; // 回転角度を反転する
    }

    // 衝突時の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // プレイヤーのターボを破壊する
            Destroy(PlayerTurbo);

            // 爆発アニメーションを再生する
            animator.Play("Explosions");

            // 保存パネルを表示する
            SavePanel.SetActive(true);
        }
    }
}
