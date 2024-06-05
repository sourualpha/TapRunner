using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Fade fade; //FadeCanvas取得

    [SerializeField]
    private float fadeTime;  //フェード時間（秒）
    // Start is called before the first frame update
    void Start()
    {
        fade.FadeOut(fadeTime);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButton()
    {
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }
}
