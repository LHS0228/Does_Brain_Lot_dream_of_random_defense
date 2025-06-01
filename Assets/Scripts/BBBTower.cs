using UnityEngine;

public class BBBTower : Tower
{
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerName = "BBB";
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
    }

    public override void AttackSingleTarget()
    {
        Debug.Log("BBB АјАн");
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
