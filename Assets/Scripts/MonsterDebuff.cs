using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * �����̻�
 * 1. ���� : 4�� ���� N�� ���ظ� �Դ´�.                                         | Ʈ�������� Ʈ������
 * 2. ��ȭ : ���� �ӵ��� % ����(���� ���� ���� ��ȭ���� ����, ��øX)             | ������ �󸱶�, ���ٸ����� ũ���ڵ���
 * 3. �ӹ� : n�� ���� �̵� �Ұ�                                                  | �긣�� �긣�� ��Ÿ��
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