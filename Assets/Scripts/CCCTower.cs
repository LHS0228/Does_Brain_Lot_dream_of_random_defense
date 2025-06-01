using UnityEngine;

public class CCCTower : Tower
{
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerName = "CCC";
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
    }

    public override void AttackSingleTarget()
    {
        Debug.Log("CCC АјАн");
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
