using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rankingMenu;
    [SerializeField]
    private TMP_Text ranking_In_Text; // 유니티 인스펙터에서 연결

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void RankingOpen()
    {
        if (!rankingMenu.activeSelf)
        {
            rankingMenu.SetActive(true);
            ShowTop5Ranking();
        }
    }

    public void RankingClose()
    {
        if (rankingMenu.activeSelf)
        {
            rankingMenu.SetActive(false);
        }
    }

    void ShowTop5Ranking()
    {
        var ranking = ScoreManager.Instance.rankingList;
        if (ranking == null || ranking.rankings == null)
        {
            ranking_In_Text.text = "랭킹 데이터를 불러오지 못했습니다.";
            return;
        }

        string text = "<size=150%><b>★ 랭킹 TOP 5 ★</b></size>\n\n";
        int count = Mathf.Min(5, ranking.rankings.Length);

        for (int i = 0; i < count; i++)
        {
            var entry = ranking.rankings[i];
            text += $"{i + 1}위  {entry.player_name}  -  {entry.score}점\n";
        }

        ranking_In_Text.text = text;
    }
}
