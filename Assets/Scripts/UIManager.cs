using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance => instance;

    public TextMeshProUGUI waveCountUI;
    public TextMeshProUGUI waveTimer;
    public TextMeshProUGUI MonsterCount;
    public TextMeshProUGUI GoldUI;
    public TextMeshProUGUI GemUI;
    public TextMeshProUGUI SpawnGoldUI;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        WaveTimerUpdate();
    }

    public void WaveCounting(int wave)
    {
        waveCountUI.text = $"WAVE {wave}";
    }

    public void MonsterCounting(int monsterCount)
    {
        MonsterCount.text = $"{monsterCount} / 20";
    }

    private void WaveTimerUpdate()
    {
        waveTimer.text = $"Next Wave.. {WaveManager.Instance.RemainWaveTime :F0}";
    }

    public void GoldUIUpdate()
    {
        GoldUI.text = $"{MoneyManager.Instance.Gold} Gold";
    }

    public void SpawnGoldUpdate(int amount)
    {
        SpawnGoldUI.text = $"{amount} Gold";
    }

    public void GemUIUpdate()
    {
        GemUI.text = $"{MoneyManager.Instance.Gem} Gem";
    }
}
