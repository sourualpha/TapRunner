using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;

public class Ranking : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private string accessKey; // デプロイのURL

    [SerializeField] GameManager gameManager; 

    [SerializeField] GameObject rankingPanel; // ランキングのパネル

    [SerializeField] GameObject SavePanel; // 保存のパネル

    [SerializeField] GameObject rankingPrefab; 

    [SerializeField] GameObject imageScroll; // ランキングのプレハブを表示する所

    [SerializeField] InputField nameInput; // プレイヤーの名前のインプットフィールド

    [SerializeField] GameObject loading; // ローディング表示
    #endregion

    int score = 0; // 現在のスコア
    int rank = 0; // 現在のランク
    bool ranking = false; // ランキングが表示されているか

    private void Update()
    {
        if (ranking)
        {
            ranking = false;
        }
    }

    // データ取得処理
    private IEnumerator GetData()
    {
        Debug.Log("データ受信開始・・・");
        var request = UnityWebRequest.Get(Common.GrovalConst.BASE_URL + accessKey + "/exec");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                var records = JsonUtility.FromJson<Records>(request.downloadHandler.text).records;
                Debug.Log("データ受信成功！");
                foreach (var record in records)
                {
                    rank++;
                    // テンプレートの作成
                    var template = Instantiate(rankingPrefab, imageScroll.transform);

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
            Debug.LogError("データ受信失敗1：" + request.result);
        }
    }

    // データ送信処理
    private IEnumerator PostData(string username, int score)
    {
        Debug.Log("データ送信開始・・・");
        var form = new WWWForm();
        form.AddField("name", username);
        form.AddField("score", score);

        var request = UnityWebRequest.Post(Common.GrovalConst.BASE_URL + accessKey + "/exec", form);

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
                Debug.LogError("データ送信失敗：" + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("データ送信失敗1：" + request.result);
        }
    }

    // データ取得ボタンの処理
    public void Button()
    {
        StartCoroutine(GetData());
    }

    // 保存ボタンの処理
    public void SaveButton()
    {
        score = gameManager.score;
        StartCoroutine(PostData(nameInput.text, score));//データ送信

        //パネルの整理
        SavePanel.SetActive(false);
        rankingPanel.SetActive(true);
        loading.SetActive(true);

        ranking = true;
    }
}
