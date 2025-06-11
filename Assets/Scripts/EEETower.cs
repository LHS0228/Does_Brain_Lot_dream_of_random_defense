using UnityEngine;

public class EEETower : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private int attackCount = 0;
    private void Start()
    {
        attackType = AttackType.SingleTarget;
        towerType = TowerType.Larila;
        attackDamage = 180f;
        attackRange = 3f;
        attackCooltime = 1f;
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
        Debug.Log("BBB ����");

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
                // �Ʒ��� Ȯ�ο� �α� ��� ���ֵ���
                Debug.Log("Ÿ�� ���� ����");
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
        // Ÿ�� ������ bullet ��ȯ �� ���� ���ݷ� ���� �� Ÿ�� ���� ����
        if (target != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            SplashBullet bullet = bulletObj.GetComponent<SplashBullet>();
            bullet.bulletDamage = attackDamage;
            bullet.SetTarget(target);
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
