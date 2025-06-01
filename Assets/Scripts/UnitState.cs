using UnityEngine;

public enum ObjectType { Enemy, PlayerMob }
public enum UnitType { TungTungSahur, Tralarare, Larila, Bombardiro, Patapim, Boneca, Zippitini }

public class UnitState : MonoBehaviour
{
    public ObjectType objectType;
    public UnitType unitType;

    [SerializeField] private int attack;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private int maxMana;
    [SerializeField] private int currentMana;
    [SerializeField] private float attackRange;

    //이 캐릭터가 지금 몇 성임?
    [SerializeField] private int unitStar = 0;

    public int UnitStar => unitStar;

    private void Update()
    {
        AttackReloding();
    }

    public void StateSetting(int _attack, int _maxHp, int _maxMana, int _attackRange, int _unitStar)
    {
        attack = _attack;
        
        maxHp = _maxHp;
        currentHp = _maxHp;

        maxMana = _maxMana;
        currentMana = _maxMana;

        attackRange = _attackRange;

        unitStar = _unitStar;
    }

    //업그레이드 확인.
    public void AttackReloding()
    {
        if (objectType == ObjectType.PlayerMob)
        {
            switch (unitType)
            {
                case UnitType.TungTungSahur:
                    attack = 1 * UpgradeManager.Instance.up_Level_TungTungSahur;
                    break;
                case UnitType.Tralarare:
                    attack = 1 * UpgradeManager.Instance.up_Level_Tralarare;
                    break;
                case UnitType.Larila:
                    attack = 1 * UpgradeManager.Instance.up_Level_Larila;
                    break;
                case UnitType.Bombardiro:
                    attack = 1 * UpgradeManager.Instance.up_Level_Bombardiro;
                    break;
                case UnitType.Patapim:
                    attack = 1 * UpgradeManager.Instance.up_Level_Patapim;
                    break;
            }
        }
    }
}
