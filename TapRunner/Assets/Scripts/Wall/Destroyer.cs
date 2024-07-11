using System.Collections;
using UnityEngine;
using Common;

public class Destroyer : MonoBehaviour
{
    public GameManager PoolManager { get; set; } // オブジェクトプールの管理マネージャー

    float timer = 0; // タイマー


    private void Update()
    {
        timer += Time.deltaTime;
    }

    // 破壊タイマーを開始するメソッド
    public void StartDestroyTimer(float time)
    {
        StartCoroutine(DestroyTransform());
    }

    // 一定時間後にオブジェクトを破壊するコルーチン
    private IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds(time);

        if (PoolManager != null)
        {
            PoolManager.ReleaseGameObject(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // オブジェクトの位置に基づいて破壊するコルーチン
    private IEnumerator DestroyTransform()
    {
        while (true)
        {
            yield return null;
            if (transform.position.x < Common.GrovalConst.POSITION_THRESHOLD && PoolManager != null)
            {
                if (timer < Common.GrovalConst.TIMER_THRESHOLD)
                {
                    PoolManager.ReleaseGameObject(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                    timer = 0;
                }
                yield break;
            }
        }
    }
}
