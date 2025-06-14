using Unity.VisualScripting;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;

public enum AttackType { SingleTarget, MultiTarget, WideAreaTarget }
public enum TowerType { TungTungSahur, Tralarare, Larila, Bombardiro, Patapim }

public class Tower : MonoBehaviour
{
    private Tile curTile = null;

    public TowerType towerType;
    public float baseAttackDamage; //초기뎀
    public float attackDamage; //최종뎀
    public float attackRange;
    public float attackCooltime;
    public AttackType attackType;
    public int towerStar = 0;
    public int sellGold = 0;

    private void Update()
    {
        AttackReloding();
    }

    public virtual void Init()
    {
        attackType = AttackType.SingleTarget;
        towerType = TowerType.Patapim;
        baseAttackDamage = 180f;
        attackRange = 3f;
        attackCooltime = 1f;
        towerStar = 1;
        sellGold = 30;
    }

    public virtual void AttackSingleTarget()
    {

    }
    public virtual void AttackMultiTarget()
    {

    }
    public virtual void AttackWideAreaTarget()
    {

    }

    public virtual void OnDrawGizmos()
    {
        
    }

    public int StarUpgrade()
    {
        towerStar++;
        AttackReloding();

        return towerStar;
    }

    public virtual void Init(Tile tile)
    {
        curTile = tile;
        Move(curTile);
    }

    public void Move(Tile targetTile)
    {
        if (targetTile == null) return;

        if (targetTile == curTile)
        {
            transform.position = targetTile.transform.position;
        }

        if (curTile != null)
        {
            curTile.OutPlace();
        }

        curTile = targetTile;
        curTile.InPlace(this);


        transform.position = targetTile.transform.position;
    }

    public void ClearTile()
    {
        curTile.OutPlace();
    }

    public void StateSetting(float _baseAttackDamage, float _attackRange, float _attackCooltime, int _towerStar)
    {
        baseAttackDamage = _baseAttackDamage;
        attackRange = _attackRange;
        attackCooltime = _attackCooltime;
        towerStar = _towerStar;

        AttackReloding();
    }

    public void AttackReloding()
    {
        int level = 0;

        if(baseAttackDamage > 0f)
        {
            switch (towerType)
            {
                case TowerType.TungTungSahur:
                    level = UpgradeManager.Instance.up_Level_TungTungSahur;
                    break;
                case TowerType.Tralarare:
                    level = UpgradeManager.Instance.up_Level_Tralarare;
                    break;
                case TowerType.Larila:
                    level = UpgradeManager.Instance.up_Level_Larila;
                    break;
                case TowerType.Bombardiro:
                    level = UpgradeManager.Instance.up_Level_Bombardiro;
                    break;
                case TowerType.Patapim:
                    level = UpgradeManager.Instance.up_Level_Patapim;
                    break;
            }

            attackDamage = (200f * Mathf.Pow(level, 0.7f) + baseAttackDamage) * towerStar;
        }

        switch (towerStar)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // 흰색
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 94f / 255f, 0f, 1f); // 주황
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f); // 노랑
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f); // 초록
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 1f); // 파랑
                break;
            case 6:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 94f / 255f, 1f, 1f); // 분홍
                break;
            default:
                Debug.Log("별 등급 잘못됨");
                break;
        }
    }
}
