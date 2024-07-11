using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    bool useObjectPool = true; // �I�u�W�F�N�g�v�[�����g�p���邩�ǂ����̃t���O

    [SerializeField]
    GameManager poolManager; // �I�u�W�F�N�g�v�[���̊Ǘ��}�l�[�W���[

    [SerializeField]
    List<GameObject> gameObjects; // �X�|�[������Q�[���I�u�W�F�N�g�̃��X�g

    [SerializeField]
    int spawnCount = Common.GrovalConst.DEFAULT_SPAWN_COUNT; // ��x�ɃX�|�[������I�u�W�F�N�g�̐�

    [SerializeField]
    float spawnInterval = Common.GrovalConst.DEFAULT_SPAWN_INTERVAL; // �X�|�[���Ԋu

    [SerializeField]
    Vector3 minSpawnPosition = Common.GrovalConst.DEFAULT_MIN_SPAWN_POSITION; // �X�|�[���ʒu�̍ŏ��l

    [SerializeField]
    Vector3 maxSpawnPosition = Common.GrovalConst.DEFAULT_MAX_SPAWN_POSITION; // �X�|�[���ʒu�̍ő�l

    [SerializeField]
    float destroyWaitTime = Common.GrovalConst.DEFAULT_DESTROY_WAIT_TIME; // �I�u�W�F�N�g���j�󂳂��܂ł̎���

    [SerializeField]
    GameObject player; // �v���C���[�I�u�W�F�N�g

    WaitForSeconds spawnIntervalWait; // �X�|�[���Ԋu��WaitForSeconds�I�u�W�F�N�g

    void Start()
    {
        spawnIntervalWait = new WaitForSeconds(spawnInterval);

        // �X�|�[���^�C�}�[�̃R���[�`�����J�n
        StartCoroutine(nameof(SpawnTimer));
    }

    // �X�|�[���^�C�}�[�̃R���[�`��
    IEnumerator SpawnTimer()
    {
        int i;
        int random;
        while (true)
        {
            // �w�肳�ꂽ�񐔂����I�u�W�F�N�g���X�|�[��
            for (i = 0; i < spawnCount; i++)
            {
                random = Random.Range(0, gameObjects.Count); // �����_���ȃQ�[���I�u�W�F�N�g��I��
                Spawn(gameObjects[random]);
            }

            // �w�肳�ꂽ���ԑҋ@
            yield return spawnIntervalWait;
        }
    }

    // �I�u�W�F�N�g���X�|�[��
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
            // �I�u�W�F�N�g�v�[������擾���ăX�|�[��
            destroyer = poolManager.GetGameObject(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
            destroyer.PoolManager = poolManager;
        }
        else
        {
            // �V�����C���X�^���X�����ăX�|�[��
            destroyer = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Destroyer>();
        }

        if (destroyer != null)
        {
            // �j��^�C�}�[���J�n
            destroyer.StartDestroyTimer(destroyWaitTime);
        }

        // �v���C���[�����݂��Ȃ��ꍇ�A�X�|�[�����~
        if (player == null)
        {
            spawnCount = 0;
        }
    }
}
