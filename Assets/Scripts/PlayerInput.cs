using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    TileManager tileManager;

    bool isGrabUnit = false;
    Unit grabbingUnit = null;

    void Update()
    {
        UnitDrag();
        UnitSellClick();
    }

    private void UnitDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Unit ���̾�*/);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                isGrabUnit = true;
                grabbingUnit = hit.collider.gameObject.GetComponent<Unit>();

                DragManager.Instance.ShowPreview_Unit(hit.collider.gameObject); //���콺 ����
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //!! �׳� ������ �����Ϸ��� Ŭ������ ���� ����.

            // ������ ��� �־��ٸ�..
            if (isGrabUnit)
            {
                // Ÿ�Ͽ� �������� Ÿ�Ͽ� �̹� ��ġ �� ������ �ִ��� Ȯ���ϰ� 3���� ����� ���� ����
                // 1. ���� Ÿ�� + ���� ����� ������ �ִ� ��� = Merge
                // 2. 1.���� ������ ����� ������ ��ġ �Ǿ� �ִ� ��� = ����ġ�� ����
                // 3. ������ ��ġ���� ���� ��� = MoveUnit(CollisionTile);

                // 
                Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 3/*Tile ���̾�*/);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Tile hitTile = hit.collider.GetComponent<Tile>();

                    if (hitTile.IsUnitLocate && hitTile.GetUnit().gameObject != grabbingUnit.gameObject)
                    {
                        //���Ⱑ ��ġ�� �κ��� ��.
                        if (hitTile.GetUnit().unitType == grabbingUnit.unitType && hitTile.GetUnit().UnitStar == grabbingUnit.UnitStar)
                        {
                            Debug.Log("����");

                            //��ġ�� ��, ����� ���� �����ϰ� Ÿ�� �ʱ�ȭ
                            grabbingUnit.ClearTile();
                            Destroy(grabbingUnit.gameObject);

                            //�Ҹ� ����
                            EffectManager.Instance.SpawnEffect(EffectName.dust, hit.collider.gameObject.transform.position);

                            //���׷��̵� �κ�
                            hitTile.GetUnit().StateSetting(0, 1, 1, 1, hitTile.GetUnit().UnitStar + 1);
                        }
                    }
                    else
                    {
                        grabbingUnit.Move(hitTile);
                    }
                }

                DragManager.Instance.HidePreview_Unit();

                isGrabUnit = false;
                grabbingUnit = null;
            }
        }

        if (isGrabUnit)
        {
            DragManager.Instance.UpdatePreviewPostion();
        }
    }
    
    private void UnitSellClick()
    {   
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Unit ���̾�*/);

            if(hit.collider != null)
            {
                //���⿡ �� ��� �ڵ嵵 �ۼ� ��Ź (�ϼ� �ȴٸ�)
                hit.collider.GetComponent<Unit>().ClearTile();
                Destroy(hit.collider.gameObject);

                Debug.Log("�Ǹ�");
            }
        }
    }
}
