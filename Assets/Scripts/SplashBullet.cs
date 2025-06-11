using UnityEngine;

public class SplashBullet : MonoBehaviour
{
    private Transform target;
    private Monster targetEnemy;
    public float bulletSpeed = 5f;
    public float bulletDamage;
    public float radius = 1.3f; // �������� �� ���� (�ݰ�)

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
        if (enemy != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < 1.5f)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.3f);

                // ã�� �ֵ鿡�� ���� 1��
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.tag == "Enemy")
                    {
                        hit.gameObject.GetComponent<Monster>().Damaged(bulletDamage);
                    }
                }
                //Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
                //enemy.Damaged(bulletDamage);
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.3f);
    }
}
