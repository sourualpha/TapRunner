using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{

    #region SerializeField
    [SerializeField] private string accessKey; //デプロイのURL

    [SerializeField] GameManager gameManager; //ゲームマネージャー

    [SerializeField] GameObject rankingPanel; //ランキングのパネル

    [SerializeField] GameObject SavePanel; //保存のパネル

    [SerializeField] GameObject rankingPrefab; //ランキング

    [SerializeField] GameObject imageScroll; //ランキングのプレハブを表示する所

    [SerializeField] InputField nameInput; //プレイヤーの名前のインプットフィールド

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
        Debug.Log("データ受信開始・・・");
        var request = UnityWebRequest.Get("https://script.google.com/macros/s/" + accessKey + "/exec");
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {   

            if (request.responseCode == 200)
            {

                var records = JsonUtility.FromJson<Records>(request.downloadHandler.text).records;
                Debug.Log("データ受信成功！");
                Debug.Log(records);
                foreach (var record in records)
                {
                    rank++;
                    //テンプレートの作成
                    var template = Instantiate(rankingPrefab,imageScroll.transform);

                    // 名前のセット
                    template.transform.GetChild(0).GetComponent<Text>().text = rank.ToString();

                    // スコアのセット
                    template.transform.GetChild(1).GetComponent<Text>().text = record.name + " " + record.score;
                    Debug.Log("Name：" + record.name + "、Score：" + record.score);

                    loading.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("データ受信失敗：" + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("データ受信失敗1" + request.result);
        }
    }

    private IEnumerator PostData(string username, int score)
    {
        Debug.Log("データ送信開始・・・");
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
                Debug.Log("データ送信成功！");
                foreach (var record in records)
                {
                    Debug.Log("Name：" + record.name + "、Score：" + record.score);

                }   
                StartCoroutine(GetData());
            }
            else
            {
                Debug.LogError("データ送信失敗" + request.responseCode);
            }
        }
        else
        {
            Debug.Log("データ送信失敗1" + request.result);
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