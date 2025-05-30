using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    Transform spawnPos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    public void SpawnMonster()
    {
        GameObject spawnedM = Instantiate(monster_normal, spawnPos.position, spawnPos.rotation);
        Monster m = spawnedM.GetComponent<Monster>();
        m.Init(3, 500, spawnPos.GetComponent<MovementTarget>());
        monsters.Add(m);
    }
    public void SpawnBoss()
    {

    }

    // 몬스터가 피해 받아서 사라질 때
    public void RemoveMonster(Monster target)
    {
        monsters.Remove(target);
        Destroy(target, 0.1f);
    }

    public IEnumerator MonsterSpawnRoutine()
    {
        for(int spawnCnt = 0; spawnCnt < 30; ++spawnCnt)
        {
            SpawnMonster();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
