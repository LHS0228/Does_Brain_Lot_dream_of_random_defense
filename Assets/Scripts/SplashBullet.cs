using UnityEngine;

public class SplashBullet : MonoBehaviour
{
    private Transform target;
    private Monster targetEnemy;
    public float bulletSpeed = 5f;
    public float bulletDamage;
    public float radius = 1.3f; // 데미지를 줄 범위 (반경)

    // 타겟과 정보 가져오기
    public void SetTarget(Transform enemy)
    {
        target = enemy;
        targetEnemy = enemy.GetComponent<Monster>();
    }

    void Update()
    {
        // 타겟 없으면 얘도 삭제
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // 방향 및 벡터의 크기[거리]계산 
        Vector3 direction = target.position - transform.position;
        // 초당 이동거리 x 한 프레임의 시간을 곱해서 한 프레임에서의 이동 거리 계산
        float moveFrame = bulletSpeed * Time.deltaTime;

        // magnitude -> 벡터 크기(길이)[시작점에서 끝점] 반환 여기서는 남은 거리 계산에 쓰였음
        // if문 설명 >> 남은 거리 <= 이번 한 프레임에 이동 할 거리일 때 뜻함 
        if (direction.magnitude <= moveFrame)
        {
            HitTarget();
            return;
        }

        // Translate() >> 오브젝트를 해당 방향으로 이동시키는 함수
        // direction을 정규화해 방향값 추출 x 한 프레임 이동 거리
        // Space.World는 월드 기준 방향을 뜻함 그래서 고정값
        transform.Translate(direction.normalized * moveFrame, Space.World);
    }

    void HitTarget()
    {
        // 적 정보 가져와서 데미지주고, 불렛 삭제시킴
        Monster enemy = target.GetComponent<Monster>();
        if (enemy != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < 1.5f)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.3f);

                // 찾은 애들에게 전부 1뎀
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
