using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameManager PoolManager { get; set; }

    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void StartDestroyTimer(float time)
    {
        //StartCoroutine(DestroyTimer(time));
        StartCoroutine(DestroyTransform());
    }

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

    private IEnumerator DestroyTransform()
    {
        while (true)
        {
            yield return null;
            if (transform.position.x < -30 && PoolManager != null)
            {
                if(timer <6)
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
