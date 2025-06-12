using Unity.VisualScripting;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;

public enum AttackType { SingleTarget, MultiTarget, WideAreaTarget }
public enum TowerType { TungTungSahur, Tralarare, Larila, Bombardiro, Patapim }

public class Tower : MonoBehaviour
{
    private Tile curTile = null;

    public TowerType towerType;
    public float attackDamage;
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
        attackDamage = 180f;
        attackRange = 3f;
        attackCooltime = 1f;
        towerStar = 1;
        sellGold = 0;

        //���⿡ �ʱ�ȭ�ϴ� �ڵ� �־���
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

    public void StarUpgrade()
    {
        towerStar++;
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

    public void StateSetting(float _attackDamage, float _attackRange, float _attackCooltime, int _towerStar)
    {
        attackDamage = _attackDamage;
        attackRange = _attackRange;
        attackCooltime = _attackCooltime;
        towerStar = _towerStar;
    }

    public void AttackReloding()
    {
        if (attackDamage <= 0)
        {
            switch (towerType)
            {
                case TowerType.TungTungSahur:
                    attackDamage = 1 * UpgradeManager.Instance.up_Level_TungTungSahur;
                    break;
                case TowerType.Tralarare:
                    attackDamage = 1 * UpgradeManager.Instance.up_Level_Tralarare;
                    break;
                case TowerType.Larila:
                    attackDamage = 1 * UpgradeManager.Instance.up_Level_Larila;
                    break;
                case TowerType.Bombardiro:
                    attackDamage = 1 * UpgradeManager.Instance.up_Level_Bombardiro;
                    break;
                case TowerType.Patapim:
                    attackDamage = 1 * UpgradeManager.Instance.up_Level_Patapim;
                    break;
            }
        }

        /*switch (towerStar)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 94, 0);
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
                break;
            case 6:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 94, 255);
                break;
            default:
                Debug.Log("���� �ڵ� ���׳�");
                break;
        }*/
    }
}
