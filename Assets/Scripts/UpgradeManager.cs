using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("��ȭ ��ư �־���")]
    [SerializeField] private Button button_Upgrade_TungTungSahur;
    [SerializeField] private Button button_Upgrade_Tralarare;
    [SerializeField] private Button button_Upgrade_Larila;
    [SerializeField] private Button button_Upgrade_Bombardiro;
    [SerializeField] private Button button_Upgrade_Patapim;

    public int up_Level_TungTungSahur;
    public int up_Level_Tralarare;
    public int up_Level_Larila;
    public int up_Level_Bombardiro;
    public int up_Level_Patapim;

    public int up_Money_TungTungSahur;
    public int up_Money_Tralarare;
    public int up_Money_Larila;
    public int up_Money_Bombardiro;
    public int up_Money_Patapim;

    
    /*�̰� �� ���� ���� �ý��� ����� ���ְ� �ٸ��ɷ� ��ü*/public int money;
    //��ȭ �� ���� �� ����
    //��ȭ �� ���� ���� ����

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        UpgradeButtonSystem();
    }

    private void UpgradeButtonSystem()
    {
        if (money >= up_Money_TungTungSahur) button_Upgrade_TungTungSahur.interactable = true;
        else button_Upgrade_TungTungSahur.interactable = false;

        if (money >= up_Money_Tralarare) button_Upgrade_Tralarare.interactable = true;
        else button_Upgrade_Tralarare.interactable = false;

        if (money >= up_Money_Larila) button_Upgrade_Larila.interactable = true;
        else button_Upgrade_Larila.interactable = false;

        if(money >= up_Money_Bombardiro) button_Upgrade_Bombardiro.interactable = true;
        else button_Upgrade_Bombardiro.interactable = false;

        if(money >= up_Money_Patapim) button_Upgrade_Patapim.interactable = true;
        else button_Upgrade_Patapim.interactable = false;
    }

    public void UnitUpgrade(int unitType)
    {
        //0�� ������ ���ĸ�, 1�� Ʈ���󷹿� Ʈ������, 2�� ������ �󸱶�, 3�� ���ٸ�����, 4�� �ĸ���
        switch (unitType)
        {
            case 0:
                up_Level_TungTungSahur++;
                money -= up_Money_TungTungSahur;

                //���� ���׷��̵� ���� (���� �ƹ��͵� ����)
                up_Money_TungTungSahur += up_Level_TungTungSahur;

                Debug.Log("���� ����");
                break;
            case 1:
                up_Level_Tralarare++;
                money -= up_Money_Tralarare;

                //���� ���׷��̵� ���� (���� �ƹ��͵� ����)
                up_Money_Tralarare += up_Level_Tralarare;

                Debug.Log("Ʈ�� ����");
                break;
            case 2:
                up_Level_Larila++;
                money -= up_Money_Larila;

                //���� ���׷��̵� ���� (���� �ƹ��͵� ����)
                up_Money_Larila += up_Level_Larila;

                Debug.Log("�󸱶� ����");
                break;
            case 3:
                up_Level_Bombardiro++;
                money -= up_Money_Bombardiro;

                //���� ���׷��̵� ���� (���� �ƹ��͵� ����)
                up_Money_Bombardiro += up_Level_Bombardiro;

                Debug.Log("���ٸ� ����");
                break;
            case 4:
                up_Level_Patapim++;
                money -= up_Money_Patapim;

                //���� ���׷��̵� ���� (���� �ƹ��͵� ����)
                up_Money_Patapim += up_Level_Patapim;

                Debug.Log("��Ÿ�� ����");
                break;
        }
    }
}
