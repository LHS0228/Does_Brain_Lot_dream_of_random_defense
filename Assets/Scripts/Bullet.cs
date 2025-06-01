using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Monster targetEnemy;
    public float bulletSpeed = 5f;
    public float bulletDamage;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
        targetEnemy = enemy.GetComponent<Monster>();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        // Ÿ�ٿ� ����
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // �̵�
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {

        Monster enemy = target.GetComponent<Monster>();
        if (enemy != null)
        {
            enemy.Damaged(bulletDamage);
        }
        Destroy(gameObject);
    }
}
