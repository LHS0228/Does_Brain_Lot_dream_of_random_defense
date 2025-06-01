using UnityEngine;

public enum ObjectType { Enemy, PlayerMob }
public enum UnitType { TungTungSahur, Tralarare, Larila, Bombardiro, Patapim, Boneca, Zippitini}

public class UnitState : MonoBehaviour
{
    public ObjectType type;
    public UnitType UnitType;

    public int attack;
    public int maxHp;
    public int currentHp;
    public int maxMana;
    public int currentMana;
    public float attackRange;

    public void Setting(int _attack, int _maxHp, int _maxMana, int _attackRange)
    {
        attack = _attack;
        
        maxHp = _maxHp;
        currentHp = _maxHp;

        maxMana = _maxMana;
        currentMana = _maxMana;

        attackRange = _attackRange;
    }
}
