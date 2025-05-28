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
    float RemainWaveTime => remainWaveTime;
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
        isWaveOngoing = true;
        StartCoroutine(MonsterManager.Instance.MonsterSpawnRoutine());
    }

    void Update()
    {
        if (isWaveOngoing == false) return;

        remainWaveTime -= Time.deltaTime;
        if(remainWaveTime <= 0)
        {
            waveCnt++;
            if (waveCnt % 5 == 0)
                MonsterManager.Instance.SpawnBoss();
            else
                StartCoroutine(MonsterManager.Instance.MonsterSpawnRoutine());
                remainWaveTime = oneWaveTime;
        }
        
    }
}
