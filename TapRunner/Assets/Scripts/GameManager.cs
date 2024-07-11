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
    Text textscore; // �X�R�A�\���p�̃e�L�X�g

    [SerializeField]
    GameObject player; // �v���C���[�I�u�W�F�N�g

    [SerializeField]
    private Fade fade; // �t�F�[�h�p�̃L�����o�X�擾

    [SerializeField]
    private float fadeTime;  // �t�F�[�h���ԁi�b�j
    #endregion

    ObjectPool<GameObject> pool; // �I�u�W�F�N�g�v�[��

    public GameObject Prefab { get; private set; } // �v�[������v���n�u

    private float time; // �Q�[���v���C����
    public int score; // �X�R�A


    void Awake()
    {
        pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
        score = 0;
    }

    private void Start()
    {
        fade.FadeOut(fadeTime); //�t�F�[�h�A�E�g����
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

    // �v�[������I�u�W�F�N�g�̍쐬
    GameObject OnCreatePooledObject()
    {
        return Instantiate(Prefab);
    }

    // �v�[������I�u�W�F�N�g���擾
    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    // �v�[���ɃI�u�W�F�N�g�����
    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // �v�[������I�u�W�F�N�g�̔j��
    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    // �v�[������I�u�W�F�N�g���擾���ď�����
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Prefab = prefab;
        GameObject obj = pool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;
        return obj;
    }

    // �I�u�W�F�N�g���v�[���ɉ��
    public void ReleaseGameObject(GameObject obj)
    {
        pool.Release(obj);
    }

    // ���X�^�[�g�{�^���̏���
    public void RestartButton()
    {
        // �t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene(Common.GrovalConst.TITLE_SCENE);
        });
    }
}
