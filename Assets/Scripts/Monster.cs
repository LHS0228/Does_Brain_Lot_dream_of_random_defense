using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    float speed = 0;
    Rigidbody2D rb;
    MovementTarget movementTarget;
    float hp = 0;
    float MaxHp = 0;
    int earnGold = 10;
    float gemRatio = 0.01f;
    bool isBoss = false;
    float Hp { get { return hp; } set { hp = Mathf.Max(0, value); } }
    
    List<MonsterDebuff> debuffs = new List<MonsterDebuff>();
    bool isBinding = false;
    float slowRatio = 0;
    public virtual void Init(float speed, float MaxHp, MovementTarget movementTarget, bool isboss)
    {
        this.isBoss = isboss;
        this.speed = speed;
        this.hp = MaxHp;
        this.movementTarget = movementTarget;
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddMonsterDebuff(MonsterDebuff debuff)
    {
        debuffs.Add(debuff);
    }
    void Update()
    {
        isBinding = false;

        debuffs.RemoveAll(x => x.Execute());

        float finalMoveSpeed = isBinding ? 0 : (speed * (1-slowRatio));

        Vector2 movDIrRaw = new Vector2(movementTarget.transform.position.x, movementTarget.transform.position.y)
            - new Vector2(transform.position.x, transform.position.y);
        Vector2 movDirNom = movDIrRaw.normalized;

        // 최종 속도를 방향과 곱해서 RigibBody Velocity에 입력
        rb.linearVelocity =  movDirNom * finalMoveSpeed;

        // 목적지 도착 시 다음 목표 설정
        if (movDIrRaw.magnitude < 0.2f)
        {
            movementTarget = movementTarget.NextTarget;
        }
    }

    public void Bind()
    {
        isBinding = true;
    }
    public void Slow(float slowRatio)
    {
        if(this.slowRatio < slowRatio)
            this.slowRatio = slowRatio;
    }
    public void Damaged(float damage)
    {
        Hp -= damage;
        if(Hp <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        MoneyManager.Instance.UpdateGold(earnGold);
        if(Random.value <= gemRatio)
        {
            MoneyManager.Instance.UpdateGem(1);
        }

        if(isBoss)
        {
            MoneyManager.Instance.UpdateGem(3);
        }
        MonsterManager.Instance.RemoveMonster(this);
    }

    public IEnumerator PrintHello()
    {
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("Hello" + i);
            yield return new WaitForSeconds(1);
        }
    }
}
