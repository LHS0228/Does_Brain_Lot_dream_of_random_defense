using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    static MonsterManager instance;
    public static MonsterManager Instance=>instance;


    [SerializeField]
    GameObject monster_normal;
    [SerializeField]
    GameObject monster_boss;

    List<Monster> monsters = new List<Monster>();
    public int currentMonsterCnt => monsters.Count;

    public WaveManager waveManager;
    private float increaseHp = 0.1f;
    private float hp;

    [SerializeField]
    Transform spawnPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

    }

    private void Update()
    {
        if (currentMonsterCnt > 20 && !GameOverManager.Instance.isGameOver)
        {
            if (!GameOverManager.Instance.isCountCheck) GameOverManager.Instance.isCountCheck = true;
            else
            {
                Debug.Log("게임 오버카운팅");
                GameOverManager.Instance.DeadCounting();
            }
        }
        else if(currentMonsterCnt <= 20 && GameOverManager.Instance.isCountCheck)
        {
            GameOverManager.Instance.isCountCheck = false;
            GameOverManager.Instance.timeCount = 0;
            GameOverManager.Instance.countText_Obj.SetActive(false);
        }
    }

    public void SpawnMonster()
    {
        if (GameOverManager.Instance.isGameOver) return;

        GameObject spawnedM = Instantiate(monster_normal, spawnPos.position, spawnPos.rotation);
        Monster m = spawnedM.GetComponent<Monster>();
        UpdateMonsterHp();
        m.Init(3, hp, spawnPos.GetComponent<MovementTarget>(),false);
        monsters.Add(m);
        UIManager.Instance.MonsterCounting(currentMonsterCnt);

        Debug.Log($"몬스터 체력 {hp}");
    }

    public void SpawnBoss()
    {
        if (GameOverManager.Instance.isGameOver) return;

        GameObject spawnedM = Instantiate(monster_boss, spawnPos.position, spawnPos.rotation);
        Monster m = spawnedM.GetComponent<Monster>();
        UpdateMonsterHp();
        m.Init(3, hp*10, spawnPos.GetComponent<MovementTarget>(),true);
        monsters.Add(m);
        UIManager.Instance.MonsterCounting(currentMonsterCnt);
    }

    // 몬스터가 피해 받아서 사라질 때
    public void RemoveMonster(Monster target)
    {
        monsters.Remove(target);
        Destroy(target.gameObject, 0.1f);
        UIManager.Instance.MonsterCounting(currentMonsterCnt);
    }

    public IEnumerator MonsterSpawnRoutine()
    {
        for(int spawnCnt = 0; spawnCnt < 20; ++spawnCnt)
        {
            SpawnMonster();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void UpdateMonsterHp()
    {
        int curWave = waveManager.WaveCnt;
        hp = 500 * MathF.Pow(1 + increaseHp, curWave - 1);
    }
}
