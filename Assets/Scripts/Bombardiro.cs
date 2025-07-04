using UnityEngine;

public class Bombardiro : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;
    private int attackCount = 0;
    private void Start()
    {
        AudioManager.instance.PlaySound("Character", "봄바르딜로크로코딜로");
        Init();
    }
    public override void Init()
    {
        attackType = AttackType.MultiTarget;
        towerType = TowerType.Bombardiro;
        baseAttackDamage = 250f;
        attackRange = 3.5f;
        attackCooltime = 1;
        towerStar = 1;
        sellGold = 0;
    }

    private void FixedUpdate()
    {
        // 공격 속도 때메 일단 해놈
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
        // 양의 무한대를 나타내는 코드 인삐니띠 뭐가 나오든 무한을 이길 수 없다
        float mindist = Mathf.Infinity;

        // 범위 내에 들어온 몬스터를 검사해보리기 
        foreach (var hit in hits)
        {
            // 태그 확인
            if (hit.CompareTag("Enemy"))
            {
                // 타워와 타겟위치 사이의 거리
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                // 타겟과의 거리 < 무한대 가까우면 mindist설정 대충 가까운 애 확인하는거임
                if (dist < mindist)
                {
                    mindist = dist;
                    target = hit.transform;
                }
            }
        }

        attackCount++;

        if (attackCount == 5) // 5회 공격
        {
            if (target != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                SplashBullet bullet = bulletObj.GetComponent<SplashBullet>();
                bullet.bulletDamage = attackDamage * 5;
                bullet.SetTarget(target);

                Debug.Log($"봄바 5회 공격력 {bullet.bulletDamage}");
                attackCount = 0;
            }
        }
        else
        {
            // 타겟 있으면 bullet 소환 그 다음 공격력 설정 및 타겟 설정 ㅇㅇ
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
