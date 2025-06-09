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
    public float attackSpeed;
    public float attackCooltime;
    public AttackType attackType;
    public int towerStar = 0;
    public int sellGold = 0;

    private void Update()
    {
        AttackReloding();
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

    public void StateSetting(float _attackDamage, float _attackRange, float _attackSpeed, float _attackCooltime, int _towerStar)
    {
        attackDamage = _attackDamage;
        attackRange = _attackRange;
        attackSpeed = _attackSpeed;
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
    }
}
