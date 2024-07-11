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

    [SerializeField]
    private GameObject optionpanel; //オプションパネル

    private bool isOption = false; //オプションパネルを開いているかどうかのフラグ

    void Start()
    {
        fade.FadeOut(fadeTime);        
        optionpanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isOption)
            {
                optionpanel.SetActive(true);
                isOption = true;
            }
            else
            {
                optionpanel.SetActive(false);
                isOption=false;
            }
        }
    }

    //フェードインが終わった際にシーン遷移をする
    public void StartButton()
    {
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    //ゲーム終了処理
    public void ExitButton()
    {
        Application.Quit();
    }
}
