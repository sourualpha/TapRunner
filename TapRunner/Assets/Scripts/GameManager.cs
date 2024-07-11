using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Common;

public class GameManager : MonoBehaviour
{
    #region
    [SerializeField]
    Text textscore; // スコア表示用のテキスト

    [SerializeField]
    GameObject player; // プレイヤーオブジェクト

    [SerializeField]
    private Fade fade; // フェード用のキャンバス取得

    [SerializeField]
    private float fadeTime;  // フェード時間（秒）
    #endregion

    ObjectPool<GameObject> pool; // オブジェクトプール

    public GameObject Prefab { get; private set; } // プールするプレハブ

    private float time; // ゲームプレイ時間
    public int score; // スコア


    void Awake()
    {
        pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
        score = 0;
    }

    private void Start()
    {
        fade.FadeOut(fadeTime); //フェードアウト処理
    }

    private void Update()
    {
        score = (int)time;
        textscore.text = score.ToString();
        if (player != null)
        {
            time += Time.deltaTime * 10;
        }
    }

    // プールするオブジェクトの作成
    GameObject OnCreatePooledObject()
    {
        return Instantiate(Prefab);
    }

    // プールからオブジェクトを取得
    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    // プールにオブジェクトを解放
    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // プールするオブジェクトの破棄
    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    // プールからオブジェクトを取得して初期化
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Prefab = prefab;
        GameObject obj = pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj;
    }

    // オブジェクトをプールに解放
    public void ReleaseGameObject(GameObject obj)
    {
        pool.Release(obj);
    }

    // リスタートボタンの処理
    public void RestartButton()
    {
        // フェードを掛けてからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene(Common.GrovalConst.TITLE_SCENE);
        });
    }
}
