using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region
    [SerializeField]
    Text textscore;

    [SerializeField]
    GameObject player;

    [SerializeField]
    private Fade fade; //FadeCanvas取得

    [SerializeField]
    private float fadeTime;  //フェード時間（秒）
    #endregion
    ObjectPool<GameObject> pool;

    public GameObject Prefab { get; private set; }

    private float time;
    public int score;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);


        score = 0;
    }

    private void Start()
    {
        fade.FadeOut(fadeTime);
    }
    private void Update()
    {
        
        score = (int)time;
        textscore.text = score.ToString();
        if(player !=  null)
        {
            time += Time.deltaTime *10;
        }
    }

    

    GameObject OnCreatePooledObject()
    {
        return Instantiate(Prefab);
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Prefab = prefab;
        GameObject obj = pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;

        return obj;
    }

    public void ReleaseGameObject(GameObject obj)
    {
        pool.Release(obj);
    }

    public void RestartButton()
    {

        //フェードを掛けてからシーン遷移する
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
}
