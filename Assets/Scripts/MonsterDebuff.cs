using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * 상태이상
 * 1. 출혈 : 4초 동안 N의 피해를 입는다.                                         | 트랄랄렐로 트랄랄라
 * 2. 둔화 : 현재 속도의 % 연산(현재 가장 높은 둔화율이 적용, 중첩X)             | 리릴리 라릴라, 봄바르딜로 크로코딜로
 * 3. 속박 : n초 동안 이동 불가                                                  | 브르르 브르르 파타핌
 */
public enum MonsterDebuffT { Bleed, Slow, Bind}
public interface MonsterDebuff
{
    public abstract bool Execute();
}
public class Bleed : MonsterDebuff
{
    MonsterDebuffT type;
    float durationTime; 
    Monster target;
    
    float tickDamage;
    float damageInflictTime;
    float tickTime = 0.5f;

    public Bleed(MonsterDebuffT type, float durationTime, Monster target, float totalDamage)
    {
        this.type = type;
        this.durationTime = Time.time + durationTime + 0.1f;
        this.damageInflictTime = Time.time + tickTime;
        this.target = target;

        float tickCount = durationTime / tickTime;
        tickDamage = totalDamage / tickCount;
    }

    public bool Execute()
    {
        if (Time.time > durationTime) return true;
        if(Time.time > damageInflictTime)
        {
            target.Damaged(tickDamage);
            damageInflictTime += tickTime;
        }
        return false;
    }
}
public class Slow : MonsterDebuff
{
    MonsterDebuffT type;
    float durationTime;
    Monster target;

    float slowRatio;

    public Slow(MonsterDebuffT type, float durationTime, Monster target, float slowRatio)
    {
        this.type = type;
        this.durationTime = Time.time + durationTime + 0.1f;
        this.target = target;
        this.slowRatio = slowRatio;
    }

    public bool Execute()
    {
        if (Time.time > durationTime) return true;
        target.Slow(slowRatio);
        return false;
    }
}
public class Bind : MonsterDebuff
{
    MonsterDebuffT type;
    float durationTime;
    Monster target;


    public Bind(MonsterDebuffT type, float durationTime, Monster target)
    {
        this.type = type;
        this.durationTime = Time.time + durationTime + 0.1f;
        this.target = target;
    }

    public bool Execute()
    {
        if (Time.time > durationTime) return true;
        target.Bind();
        return false;
    }
}