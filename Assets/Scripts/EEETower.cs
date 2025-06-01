using UnityEngine;

public class EEETower : Tower
{
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerName = "EEE";
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
    }

    public override void AttackSingleTarget()
    {
        Debug.Log("EEE АјАн");
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
