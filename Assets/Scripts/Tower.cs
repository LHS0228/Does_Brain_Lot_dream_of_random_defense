using Unity.VisualScripting;
using UnityEngine;

public enum AttackType { SingleTarget, MultiTarget, WideAreaTarget }
public class Tower : MonoBehaviour
{
    public string towerName;
    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
    public float attackCooltime;
    public AttackType attackType;

    public virtual void AttackSingleTarget()
    {

    }
    public virtual void AttackMultiTarget()
    {

    }
    public virtual void AttackWideAreaTarget()
    {

    }

    public virtual void OnDrawGizmos()
    {
        
    }
}
