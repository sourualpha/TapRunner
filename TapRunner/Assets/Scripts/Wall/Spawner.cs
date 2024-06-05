using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    bool useObjectPool = true;
    [SerializeField]
    GameManager poolManager;
    [SerializeField]
    List<GameObject> gameObjects;
    [SerializeField]
    int spawnCount = 1;
    [SerializeField]
    float spawnInterval = 0.1f;
    [SerializeField]
    Vector3 minSpawnPosition = Vector3.zero;
    [SerializeField]
    Vector3 maxSpawnPosition = Vector3.zero;
    [SerializeField]
    float destroyWaitTime = 3;
    [SerializeField]
    GameObject player;

    WaitForSeconds spawnIntervalWait;

    void Start()
    {
        spawnIntervalWait = new WaitForSeconds(spawnInterval);

        StartCoroutine(nameof(SpawnTimer));
    }

    IEnumerator SpawnTimer()
    {
        int i;
        int random;
        while (true)
        {
            for (i = 0; i < spawnCount; i++)
            {
                random = Random.Range(0, 3);
                Spawn(gameObjects[random]);
            }

            yield return spawnIntervalWait;
        }
    }

    void Spawn(GameObject prefab)
    {
        Destroyer destroyer;
        Vector3 pos = new Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y), Random.Range(minSpawnPosition.z, maxSpawnPosition.z));

        if (useObjectPool)
        {
            destroyer = poolManager.GetGameObject(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
            destroyer.PoolManager = poolManager;
        }
        else
        {
            destroyer = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
        }

        if (destroyer != null)
        {
            destroyer.StartDestroyTimer(destroyWaitTime);
        }

        if (player == null)
        {
            spawnCount = 0;
        }
    }

}