using UnityEngine;

public class AAATower : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerName = "AAA";
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
    }

    private void FixedUpdate()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackCooltime)
        {
            attackTimer = 0f;
            AttackSingleTarget();
        }
    }

    public override void AttackSingleTarget()
    {
        Debug.Log("AAA 공격");
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Transform target = null;
        float mindist = Mathf.Infinity;

        foreach(var hit in hits)
        {

            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("타겟 범위 내에");
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if(dist < mindist) 
                { 
                    mindist = dist;
                    target = hit.transform;
                }
            }
        }

        if (target != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.bulletDamage = attackDamage;
            bullet.SetTarget(target);
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
