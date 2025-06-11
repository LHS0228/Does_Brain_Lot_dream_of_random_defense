using UnityEngine;

public class Larila : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private int attackCount = 0;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        attackType = AttackType.MultiTarget;
        towerType = TowerType.Larila;
        attackDamage = 300f;
        attackRange = 2.5f;
        attackCooltime = 2;
        towerStar = 1;
        sellGold = 0;
    }

    private void FixedUpdate()
    {
        // ���� �ӵ� ���� �ϴ� �س�
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooltime)
        {
            attackTimer = 0f;
            AttackMultiTarget();
        }
    }

    public override void AttackMultiTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Transform target = null;
        // ���� ���Ѵ븦 ��Ÿ���� �ڵ� �λߴ϶� ���� ������ ������ �̱� �� ����
        float mindist = Mathf.Infinity;

        // ���� ���� ���� ���͸� �˻��غ����� 
        foreach (var hit in hits)
        {
            // �±� Ȯ��
            if (hit.CompareTag("Enemy"))
            {
                // Ÿ���� Ÿ����ġ ������ �Ÿ�
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                // Ÿ�ٰ��� �Ÿ� < ���Ѵ� ������ mindist���� ���� ����� �� Ȯ���ϴ°���
                if (dist < mindist)
                {
                    mindist = dist;
                    target = hit.transform;
                }
            }
        }

        attackCount++;

        if (attackCount == 3) // ���ο� ����
        {
            if (target != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                SplashBullet bullet = bulletObj.GetComponent<SplashBullet>();
                bullet.bulletDamage = attackDamage * 3;

                // ���ο� ���� ����
                bullet.isSlowing = true;
                bullet.slowRatio = 0.5f;
                bullet.slowDuration = 3f;

                bullet.SetTarget(target);

                Debug.Log($"�󸱶� 3ȸ ���ݷ� {bullet.bulletDamage}");
                attackCount = 0;
            }
        }
        else
        {
            // Ÿ�� ������ bullet ��ȯ �� ���� ���ݷ� ���� �� Ÿ�� ���� ����
            if (target != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                SplashBullet bullet = bulletObj.GetComponent<SplashBullet>();
                bullet.bulletDamage = attackDamage;
                bullet.SetTarget(target);
            }
        }
    }


    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

