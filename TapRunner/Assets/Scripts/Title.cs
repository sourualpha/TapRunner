using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Fade fade; //FadeCanvas�擾

    [SerializeField]
    private float fadeTime;  //�t�F�[�h���ԁi�b�j

    [SerializeField]
    private GameObject optionpanel; //�I�v�V�����p�l��

    private bool isOption = false; //�I�v�V�����p�l�����J���Ă��邩�ǂ����̃t���O

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

    //�t�F�[�h�C�����I������ۂɃV�[���J�ڂ�����
    public void StartButton()
    {
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    //�Q�[���I������
    public void ExitButton()
    {
        Application.Quit();
    }
}
