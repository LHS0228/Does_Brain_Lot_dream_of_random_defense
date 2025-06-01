using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance;
    public GameObject previewPrefab;   // 투명 유닛 프리팹
    private GameObject previewInstance;

    private void Awake()
    {
        Instance = this;
    }

    //마우스 유닛 드래그 할 때 보이게 하기
    public void ShowPreview_Unit(GameObject unit)
    {
        if (previewInstance == null)
        {
            previewInstance = Instantiate(previewPrefab);
            previewInstance.GetComponent<SpriteRenderer>().sprite = unit.GetComponent<SpriteRenderer>().sprite;
            previewInstance.transform.localScale = unit.transform.localScale;
        }
    }

    //마우스 유닛 드래그 할 때 안보이게 하기
    public void HidePreview_Unit()
    {
        Destroy(previewInstance);
        previewInstance = null;
    }

    // 마우스 움직이는 동안 → 마우스 위치 따라감
    public void UpdatePreviewPostion()
    {
        if (previewInstance != null)
        {
            previewInstance.transform.position = GetMouseWorldPosition();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // 카메라에서 거리 설정
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}