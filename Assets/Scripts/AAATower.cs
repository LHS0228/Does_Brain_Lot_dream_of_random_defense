using UnityEngine;

public class AAATower : Tower
{
    public GameObject bulletPrefab;
    private float attackTimer = 0f;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        // 공격 속도 때메 일단 해놈
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
        towerType = TowerType.TungTungSahur;
        attackDamage = 180f;
        attackRange = 3f;
        attackSpeed = 1f;
        attackCooltime = 1f;
        towerStar = 1;
        sellGold = 0;
    }

    // 단일 타격 함수
    public override void AttackSingleTarget()
    {
        Debug.Log("AAA 공격");
        // 범위내로 들어오면
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Transform target = null;
        // 양의 무한대를 나타내는 코드 인삐니띠 뭐가 나오든 무한을 이길 수 없다
        float mindist = Mathf.Infinity;

        // 범위 내에 들어온 몬스터를 검사해보리기 
        foreach(var hit in hits)
        {
            // 태그 확인
            if (hit.CompareTag("Enemy"))
            {
                // 아래는 확인용 로그 출력 없애도댐
                Debug.Log("타겟 범위 내에");
                // 타워와 타겟위치 사이의 거리
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                // 타겟과의 거리 < 무한대 가까우면 mindist설정 대충 가까운 애 확인하는거임
                if(dist < mindist) 
                { 
                    mindist = dist;
                    target = hit.transform;
                }
            }
        }
        // 타겟 있으면 bullet 소환 그 다음 공격력 설정 및 타겟 설정 ㅇㅇ
        if (target != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.bulletDamage = attackDamage;
            bullet.SetTarget(target);
        }
    }
    // 이거는 사거리 확인차 만든 거
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
