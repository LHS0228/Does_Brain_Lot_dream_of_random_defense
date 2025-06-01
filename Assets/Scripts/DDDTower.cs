using UnityEngine;

public class DDDTower : Tower
{
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerName = "DDD";
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
    }

    public override void AttackSingleTarget()
    {
        Debug.Log("DDD АјАн");
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
