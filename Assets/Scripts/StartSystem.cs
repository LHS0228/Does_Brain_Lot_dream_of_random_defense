using System.Collections.Generic;
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
        int maxCount = 5;

        // 중복 제거: 이름+점수가 같은 항목은 하나만 유지
        List<string> seenEntries = new List<string>();
        List<RankingEntry> uniqueRankings = new List<RankingEntry>();

        foreach (var entry in ranking.rankings)
        {
            string key = entry.player_name + "_" + entry.score;
            if (!seenEntries.Contains(key))
            {
                seenEntries.Add(key);
                uniqueRankings.Add(entry);
            }

            if (uniqueRankings.Count >= maxCount)
                break;
        }

        // 출력
        for (int i = 0; i < maxCount; i++)
        {
            if (i < uniqueRankings.Count)
            {
                var entry = uniqueRankings[i];
                text += $"{i + 1}위  {entry.player_name}  -  {entry.score}점\n\n";
            }
            else
            {
                text += $"{i + 1}위  -----  -  ---------\n\n";
            }
        }

        ranking_In_Text.text = text;
    }
}
