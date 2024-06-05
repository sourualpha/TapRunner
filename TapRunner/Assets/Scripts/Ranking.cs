using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{

    #region SerializeField
    [SerializeField] private string accessKey; //�f�v���C��URL

    [SerializeField] GameManager gameManager; //�Q�[���}�l�[�W���[

    [SerializeField] GameObject rankingPanel; //�����L���O�̃p�l��

    [SerializeField] GameObject SavePanel; //�ۑ��̃p�l��

    [SerializeField] GameObject rankingPrefab; //�����L���O

    [SerializeField] GameObject imageScroll; //�����L���O�̃v���n�u��\�����鏊

    [SerializeField] InputField nameInput; //�v���C���[�̖��O�̃C���v�b�g�t�B�[���h

    [SerializeField] GameObject loading;
    #endregion
    int score = 0;
    int rank = 0;
    bool ranking = false;
    private void Update()
    {

        if(ranking == true)
        {
            ranking = false;
        }
    }

    private IEnumerator GetData()
    {
        Debug.Log("�f�[�^��M�J�n�E�E�E");
        var request = UnityWebRequest.Get("https://script.google.com/macros/s/" + accessKey + "/exec");
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {   

            if (request.responseCode == 200)
            {

                var records = JsonUtility.FromJson<Records>(request.downloadHandler.text).records;
                Debug.Log("�f�[�^��M�����I");
                Debug.Log(records);
                foreach (var record in records)
                {
                    rank++;
                    //�e���v���[�g�̍쐬
                    var template = Instantiate(rankingPrefab,imageScroll.transform);

                    // ���O�̃Z�b�g
                    template.transform.GetChild(0).GetComponent<Text>().text = rank.ToString();

                    // �X�R�A�̃Z�b�g
                    template.transform.GetChild(1).GetComponent<Text>().text = record.name + " " + record.score;
                    Debug.Log("Name�F" + record.name + "�AScore�F" + record.score);

                    loading.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("�f�[�^��M���s�F" + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("�f�[�^��M���s1" + request.result);
        }
    }

    private IEnumerator PostData(string username, int score)
    {
        Debug.Log("�f�[�^���M�J�n�E�E�E");
        var form = new WWWForm();
        form.AddField("name", username);
        form.AddField("score", score);

        var request = UnityWebRequest.Post("https://script.google.com/macros/s/" + accessKey + "/exec", form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                var records = JsonUtility.FromJson<Records>(request.downloadHandler.text).records;
                Debug.Log("�f�[�^���M�����I");
                foreach (var record in records)
                {
                    Debug.Log("Name�F" + record.name + "�AScore�F" + record.score);

                }   
                StartCoroutine(GetData());
            }
            else
            {
                Debug.LogError("�f�[�^���M���s" + request.responseCode);
            }
        }
        else
        {
            Debug.Log("�f�[�^���M���s1" + request.result);
        }
    }

    public void button()
    {
            StartCoroutine(GetData());
    }

    public void SaveButton()
    {
        score = gameManager.score;
        StartCoroutine(PostData(nameInput.text, score));
        SavePanel.SetActive(false);
        rankingPanel.SetActive(true);
        loading.SetActive(true);
        ranking = true;
    }
}