using UnityEngine;
using UnityEngine.TextCore;

public class MoneyManager : MonoBehaviour
{
    static MoneyManager instance;
    public static MoneyManager Instance => instance;

    int gold;

    public int gem;

    public int Gold => gold; 

    public int Gem => gem;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        gold = 100;
        gem = 0;
    }

    private void Start()
    {
        UIManager.Instance.GoldUIUpdate();
        UIManager.Instance.GemUIUpdate();
    }

    public void UpdateGold(int amount)
    {
        gold += amount;
        UIManager.Instance.GoldUIUpdate();
    }

    public void UpdateGem(int amount)
    {
        gem += amount;
        UIManager.Instance.GemUIUpdate();
    }
}
