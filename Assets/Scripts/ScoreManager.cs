using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class RankingEntry
{
    public string player_name;
    public int score;
    public string created_at;
}

[System.Serializable]
public class RankingList
{
    public RankingEntry[] rankings;
}

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private int _score;
    public int Score => _score;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        InitScore();
    }

    private void Start()
    {
        UploadScore("��μ�", 20);
        GetRanking(10);
    }

    public void InitScore()
    { _score = 0; }
    public void IncreaseScore(int score)
    { _score += score; }
    public void DecreaseScore(int score) 
    { _score -= score; }

    public void UploadScore(string name, int score = -1)
    {
        if (score == -1) score = _score;
        StartCoroutine(IUploadScore(name, score));
    }
    IEnumerator IUploadScore(string name, int score)
    {
        _loadT = LoadT.OnUpLoading;

        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post("https://heominseok.duckdns.org/save_score.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("Error: " + www.error);
            else
                Debug.Log("Success: " + www.downloadHandler.text);
        }
        _loadT = LoadT.OnSuccess;
    }

    public enum LoadT { Init, OnLoading, OnSuccess, OnFailure, OnUpLoading }
    private LoadT _loadT;
    // �� ������ ���� ����� ��! ->(������ �ε� ��Ȳ, ������ �����̳�)
    public LoadT LoadType => _loadT;
    public RankingList rankingList;
    public void InitLoadT()=>_loadT=LoadT.Init;
    public void GetRanking(int count)
    {
        StartCoroutine(IGetRanking(count));
    }
    IEnumerator IGetRanking(int count)
    {
        while(LoadType == LoadT.OnUpLoading) yield return null;
        _loadT = LoadT.OnLoading;
        rankingList = null;

        WWWForm form = new WWWForm();
        form.AddField("rowCount", count);

        using (UnityWebRequest www = UnityWebRequest.Post("https://heominseok.duckdns.org/get_ranking.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                _loadT = LoadT.OnFailure;
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log("���� ������: " + json);

                rankingList = JsonUtility.FromJson<RankingList>(json);

                //���� ��� �����
                foreach (RankingEntry entry in rankingList.rankings)
                {
                    Debug.Log(entry.player_name);
                    Debug.Log(entry.score);
                }

                _loadT = LoadT.OnSuccess;
            }
        }
    }


}
