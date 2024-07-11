using System.Collections;
using UnityEngine;
using Common;

public class Destroyer : MonoBehaviour
{
    public GameManager PoolManager { get; set; } // �I�u�W�F�N�g�v�[���̊Ǘ��}�l�[�W���[

    float timer = 0; // �^�C�}�[


    private void Update()
    {
        timer += Time.deltaTime;
    }

    // �j��^�C�}�[���J�n���郁�\�b�h
    public void StartDestroyTimer(float time)
    {
        StartCoroutine(DestroyTransform());
    }

    // ��莞�Ԍ�ɃI�u�W�F�N�g��j�󂷂�R���[�`��
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

    // �I�u�W�F�N�g�̈ʒu�Ɋ�Â��Ĕj�󂷂�R���[�`��
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
