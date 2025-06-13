using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("강화 버튼 넣어줘")]
    [SerializeField] private Button button_Upgrade_TungTungSahur;
    [SerializeField] private Button button_Upgrade_Tralarare;
    [SerializeField] private Button button_Upgrade_Larila;
    [SerializeField] private Button button_Upgrade_Bombardiro;
    [SerializeField] private Button button_Upgrade_Patapim;
    [SerializeField] private TextMeshProUGUI text_level_TungTungSahur;
    [SerializeField] private TextMeshProUGUI text_level_Tralarare;
    [SerializeField] private TextMeshProUGUI text_level_Larila;
    [SerializeField] private TextMeshProUGUI text_level_Bombardiro;
    [SerializeField] private TextMeshProUGUI text_level_Patapim;

    public int up_Level_TungTungSahur = 0;
    public int up_Level_Tralarare = 0;
    public int up_Level_Larila = 0;
    public int up_Level_Bombardiro = 0;
    public int up_Level_Patapim = 0;

    public int up_Money_TungTungSahur = 0;
    public int up_Money_Tralarare = 0;
    public int up_Money_Larila = 0;
    public int up_Money_Bombardiro = 0;
    public int up_Money_Patapim = 0;

    //강화 시 들어가는 스텟 계산식

    private void Awake()
    {
        up_Level_TungTungSahur = 0;
        up_Level_Tralarare = 0;
        up_Level_Larila = 0;
        up_Level_Bombardiro = 0;
        up_Level_Patapim = 0;

        up_Money_TungTungSahur = 1;
        up_Money_Tralarare = 1;
        up_Money_Larila = 1;
        up_Money_Bombardiro = 1;
        up_Money_Patapim = 1;
        text_level_TungTungSahur.text = $"Lv.{up_Level_TungTungSahur}";
        text_level_Tralarare.text = $"Lv.{up_Level_Tralarare}";
        text_level_Larila.text = $"Lv.{up_Level_Larila}";
        text_level_Bombardiro.text = $"Lv.{up_Level_Bombardiro}";
        text_level_Patapim.text = $"Lv.{up_Level_Patapim}";

        Instance = this;
    }

    public void Update()
    {
        UpgradeButtonSystem();
    }

    private void UpgradeButtonSystem()
    {
        if (MoneyManager.Instance.Gem >= up_Money_TungTungSahur) button_Upgrade_TungTungSahur.interactable = true;
        else button_Upgrade_TungTungSahur.interactable = false;

        if (MoneyManager.Instance.Gem >= up_Money_Tralarare) button_Upgrade_Tralarare.interactable = true;
        else button_Upgrade_Tralarare.interactable = false;

        if (MoneyManager.Instance.Gem >= up_Money_Larila) button_Upgrade_Larila.interactable = true;
        else button_Upgrade_Larila.interactable = false;

        if(MoneyManager.Instance.Gem >= up_Money_Bombardiro) button_Upgrade_Bombardiro.interactable = true;
        else button_Upgrade_Bombardiro.interactable = false;

        if(MoneyManager.Instance.Gem >= up_Money_Patapim) button_Upgrade_Patapim.interactable = true;
        else button_Upgrade_Patapim.interactable = false;
    }

    public void UnitUpgrade(int unitType)
    {
        //0번 퉁퉁퉁 사후르, 1번 트랄라레오 트랄랄라, 2번 리릴리 라릴라, 3번 봄바르딜로, 4번 파르핌
        switch (unitType)
        {
            case 0:
                up_Level_TungTungSahur++;
                MoneyManager.Instance.UpdateGem(-up_Money_TungTungSahur);
                //다음 업그레이드 계산식 (아직 아무것도 없음)
                up_Money_TungTungSahur += 1;
                text_level_TungTungSahur.text = $"Lv.{up_Level_TungTungSahur}";
                Debug.Log("퉁사 업글");
                break;
            case 1:
                up_Level_Tralarare++;
                MoneyManager.Instance.UpdateGem(-up_Money_Tralarare);

                //다음 업그레이드 계산식 (아직 아무것도 없음)
                up_Money_Tralarare += 1;
                text_level_Tralarare.text = $"Lv.{up_Level_Tralarare}";
                Debug.Log("트랄 업글");
                break;
            case 2:
                up_Level_Larila++;
                MoneyManager.Instance.UpdateGem(-up_Money_Larila);

                //다음 업그레이드 계산식 (아직 아무것도 없음)
                up_Money_Larila += 1;
                text_level_Larila.text = $"Lv.{up_Level_Larila}";
                Debug.Log("라릴라 업글");
                break;
            case 3:
                up_Level_Bombardiro++;
                MoneyManager.Instance.UpdateGem(-up_Money_Bombardiro);

                //다음 업그레이드 계산식 (아직 아무것도 없음)
                up_Money_Bombardiro += 1;
                text_level_Bombardiro.text = $"Lv.{up_Level_Bombardiro}";
                Debug.Log("봄바르 업글");
                break;
            case 4:
                up_Level_Patapim++;
                MoneyManager.Instance.UpdateGem(-up_Money_Patapim);

                //다음 업그레이드 계산식 (아직 아무것도 없음)
                up_Money_Patapim += 1;
                text_level_Patapim.text = $"Lv.{up_Level_Patapim}";
                Debug.Log("파타빔 업글");
                break;
        }
    }
}
