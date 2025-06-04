using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance;
    public GameObject previewPrefab;   // ���� ���� ������
    private GameObject previewInstance;

    private void Awake()
    {
        Instance = this;
    }

    //���콺 ���� �巡�� �� �� ���̰� �ϱ�
    public void ShowPreview_Tower(GameObject tower)
    {
        if (previewInstance == null)
        {
            previewInstance = Instantiate(previewPrefab);
            previewInstance.GetComponent<SpriteRenderer>().sprite = tower.GetComponent<SpriteRenderer>().sprite;
            previewInstance.transform.localScale = tower.transform.localScale;
        }
    }

    //���콺 ���� �巡�� �� �� �Ⱥ��̰� �ϱ�
    public void HidePreview_Tower()
    {
        Destroy(previewInstance);
        previewInstance = null;
    }

    // ���콺 �����̴� ���� �� ���콺 ��ġ ����
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
        mousePos.z = 10f; // ī�޶󿡼� �Ÿ� ����
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}