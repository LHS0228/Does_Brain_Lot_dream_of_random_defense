using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rankingMenu;
    [SerializeField]
    private TMP_Text ranking_In_Text; // ����Ƽ �ν����Ϳ��� ����

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
            ranking_In_Text.text = "��ŷ �����͸� �ҷ����� ���߽��ϴ�.";
            return;
        }

        string text = "<size=150%><b>�� ��ŷ TOP 5 ��</b></size>\n\n";
        int count = Mathf.Min(5, ranking.rankings.Length);

        for (int i = 0; i < count; i++)
        {
            var entry = ranking.rankings[i];
            text += $"{i + 1}��  {entry.player_name}  -  {entry.score}��\n";
        }

        ranking_In_Text.text = text;
    }
}
