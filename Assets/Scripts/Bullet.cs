using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Monster targetEnemy;
    public float bulletSpeed = 5f;
    public float bulletDamage;

    public bool isBleeding = false;
    public float totalBleedDamage;
    public float bleedDuration;


    // Ÿ�ٰ� ���� ��������
    public void SetTarget(Transform enemy)
    {
        target = enemy;
        targetEnemy = enemy.GetComponent<Monster>();
    }

    void Update()
    {
        // Ÿ�� ������ �굵 ����
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // ���� �� ������ ũ��[�Ÿ�]��� 
        Vector3 direction = target.position - transform.position;
        // �ʴ� �̵��Ÿ� x �� �������� �ð��� ���ؼ� �� �����ӿ����� �̵� �Ÿ� ���
        float moveFrame = bulletSpeed * Time.deltaTime;

        // magnitude -> ���� ũ��(����)[���������� ����] ��ȯ ���⼭�� ���� �Ÿ� ��꿡 ������
        // if�� ���� >> ���� �Ÿ� <= �̹� �� �����ӿ� �̵� �� �Ÿ��� �� ���� 
        if (direction.magnitude <= moveFrame)
        {
            HitTarget();
            return;
        }

        // Translate() >> ������Ʈ�� �ش� �������� �̵���Ű�� �Լ�
        // direction�� ����ȭ�� ���Ⱚ ���� x �� ������ �̵� �Ÿ�
        // Space.World�� ���� ���� ������ ���� �׷��� ������
        transform.Translate(direction.normalized * moveFrame, Space.World);
    }

    void HitTarget()
    {
        // �� ���� �����ͼ� �������ְ�, �ҷ� ������Ŵ
        Monster enemy = target.GetComponent<Monster>();
        if(isBleeding == true)
        {
            Bleed bleed = new Bleed(MonsterDebuffT.Bleed, bleedDuration, enemy, totalBleedDamage);
            enemy.AddMonsterDebuff(bleed);
            Debug.Log($"���� ���ݷ� {totalBleedDamage}");

            Destroy(gameObject);
        }
        else
        {
            if (enemy != null)
            {
                enemy.Damaged(bulletDamage);
            }
            Destroy(gameObject);
        }
        
    }
}
