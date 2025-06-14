using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    static WaveManager instance;
    public static WaveManager Instance => instance;

    bool isWaveOngoing = false;

    float oneWaveTime = 30f;
    float remainWaveTime = 30f;
    int waveCnt = 1;

    float OneWaveTime => oneWaveTime;
    public float RemainWaveTime => remainWaveTime;
    public int WaveCnt => waveCnt;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        StartWave();
    }
    public void StartWave()
    {
        UIManager.Instance.WaveCounting(WaveCnt);
        isWaveOngoing = true;
        StartCoroutine(MonsterManager.Instance.MonsterSpawnRoutine());
    }

    public void NextWave()
    {
        remainWaveTime = 0f;
    }

    void Update()
    {
        if (GameOverManager.Instance.isGameOver) return;
        if (isWaveOngoing == false) return;

        remainWaveTime -= Time.deltaTime;
        if (remainWaveTime <= 0)
        {
            waveCnt++;
            UIManager.Instance.WaveCounting(WaveCnt);
            if (waveCnt % 5 == 0)
            {
                StartCoroutine(MonsterManager.Instance.MonsterSpawnRoutine());
                MonsterManager.Instance.SpawnBoss();
            }
            else
            {
                StartCoroutine(MonsterManager.Instance.MonsterSpawnRoutine());
            }
            remainWaveTime = oneWaveTime;
        }
        
    }
}
