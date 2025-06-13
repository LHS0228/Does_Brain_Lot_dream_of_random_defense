using UnityEngine;

public class Tralarare : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private int attackCount = 0;

    private void Start()
    {
        AudioManager.instance.PlaySound("Character", "Ʈ��������Ʈ������");
        Init();
    }

    private void FixedUpdate()
    {
        // ���� �ӵ� ���� �ϴ� �س�
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackCooltime)
        {
            attackTimer = 0f;
            AttackSingleTarget();
        }
    }

    public override void Init()
    {
        attackType = AttackType.SingleTarget;
        towerType = TowerType.Tralarare;
        attackDamage = 200f;
        attackRange = 1.5f;
        attackCooltime = 0.6666f;
        towerStar = 1;
        sellGold = 0;
    }

    // ���� Ÿ�� �Լ�
    public override void AttackSingleTarget()
    {
        Debug.Log("AAA ����");
        // �������� ������
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Transform target = null;
        // ���� ���Ѵ븦 ��Ÿ���� �ڵ� �λߴ϶� ���� ������ ������ �̱� �� ����
        float mindist = Mathf.Infinity;

        // ���� ���� ���� ���͸� �˻��غ����� 
        foreach(var hit in hits)
        {
            // �±� Ȯ��
            if (hit.CompareTag("Enemy"))
            {
                // Ÿ���� Ÿ����ġ ������ �Ÿ�
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                // Ÿ�ٰ��� �Ÿ� < ���Ѵ� ������ mindist���� ���� ����� �� Ȯ���ϴ°���
                if(dist < mindist) 
                { 
                    mindist = dist;
                    target = hit.transform;
                }
            }
        }

        attackCount++;

        if( attackCount == 3)
        {
            if (target != null)
            {
                

                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bullet = bulletObj.GetComponent<Bullet>();

                //���� ���� ����
                bullet.isBleeding = true;
                bullet.totalBleedDamage = attackDamage * 2.5f;
                bullet.bleedDuration = 4f;

                bullet.SetTarget(target);

                Debug.Log($"���� ���ݷ� {bullet.totalBleedDamage}");

                attackCount = 0;
            }
        }
        else
        {
            // Ÿ�� ������ bullet ��ȯ �� ���� ���ݷ� ���� �� Ÿ�� ���� ����
            if (target != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.bulletDamage = attackDamage;
                bullet.SetTarget(target);
            }
        }

    }
    // �̰Ŵ� ��Ÿ� Ȯ���� ���� ��
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
