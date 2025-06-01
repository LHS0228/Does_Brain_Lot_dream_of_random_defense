using UnityEngine;

public enum ObjectType { Enemy, PlayerMob }
public enum UnitType { TungTungSahur, Tralarare, Larila, Bombardiro, Patapim, Boneca, Zippitini}

public class UnitState : MonoBehaviour
{
    public ObjectType type;
    public UnitType unitType;

    [SerializeField] private int attack;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private int maxMana;
    [SerializeField] private int currentMana;
    [SerializeField] private float attackRange;
    [SerializeField] private int unitStar = 0;

    public int UnitStar => unitStar;

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
}
