using UnityEngine;

public class Patapim : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private int attackCount = 0;
    private void Start()
    {
        AudioManager.instance.PlaySound("Character", "�긣���긣����Ÿ��");
        Init();
    }
    public override void Init()
    {
        attackType = AttackType.MultiTarget;
        towerType = TowerType.Patapim;
        attackDamage = 50f;
        attackRange = 3.5f;
        attackCooltime = 0.6666f;
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

        if (attackCount == 3) // 5ȸ Ȯ�� �ӹ� ����
        {
            if (target != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                SplashBullet bullet = bulletObj.GetComponent<SplashBullet>();
                bullet.bulletDamage = attackDamage * 2.5f;

                bullet.isBinding = true;
                bullet.bindDuration = 2f;

                bullet.SetTarget(target);

                Debug.Log($"��Ÿ�� 5ȸ ���ݷ� {bullet.bulletDamage}");
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
