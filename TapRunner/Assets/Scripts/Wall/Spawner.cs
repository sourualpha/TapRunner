using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    bool useObjectPool = true; // オブジェクトプールを使用するかどうかのフラグ

    [SerializeField]
    GameManager poolManager; // オブジェクトプールの管理マネージャー

    [SerializeField]
    List<GameObject> gameObjects; // スポーンするゲームオブジェクトのリスト

    [SerializeField]
    int spawnCount = Common.GrovalConst.DEFAULT_SPAWN_COUNT; // 一度にスポーンするオブジェクトの数

    [SerializeField]
    float spawnInterval = Common.GrovalConst.DEFAULT_SPAWN_INTERVAL; // スポーン間隔

    [SerializeField]
    Vector3 minSpawnPosition = Common.GrovalConst.DEFAULT_MIN_SPAWN_POSITION; // スポーン位置の最小値

    [SerializeField]
    Vector3 maxSpawnPosition = Common.GrovalConst.DEFAULT_MAX_SPAWN_POSITION; // スポーン位置の最大値

    [SerializeField]
    float destroyWaitTime = Common.GrovalConst.DEFAULT_DESTROY_WAIT_TIME; // オブジェクトが破壊されるまでの時間

    [SerializeField]
    GameObject player; // プレイヤーオブジェクト

    WaitForSeconds spawnIntervalWait; // スポーン間隔のWaitForSecondsオブジェクト

    void Start()
    {
        spawnIntervalWait = new WaitForSeconds(spawnInterval);

        // スポーンタイマーのコルーチンを開始
        StartCoroutine(nameof(SpawnTimer));
    }

    // スポーンタイマーのコルーチン
    IEnumerator SpawnTimer()
    {
        int i;
        int random;
        while (true)
        {
            // 指定された回数だけオブジェクトをスポーン
            for (i = 0; i < spawnCount; i++)
            {
                random = Random.Range(0, gameObjects.Count); // ランダムなゲームオブジェクトを選択
                Spawn(gameObjects[random]);
            }

            // 指定された時間待機
            yield return spawnIntervalWait;
        }
    }

    // オブジェクトをスポーン
    void Spawn(GameObject prefab)
    {
        Destroyer destroyer;
        Vector3 pos = new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
        );

        if (useObjectPool)
        {
            // オブジェクトプールから取得してスポーン
            destroyer = poolManager.GetGameObject(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
            destroyer.PoolManager = poolManager;
        }
        else
        {
            // 新しくインスタンス化してスポーン
            destroyer = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
        }

        if (destroyer != null)
        {
            // 破壊タイマーを開始
            destroyer.StartDestroyTimer(destroyWaitTime);
        }

        // プレイヤーが存在しない場合、スポーンを停止
        if (player == null)
        {
            spawnCount = 0;
        }
    }
}
