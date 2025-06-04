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

            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Unit 레이어*/);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                isGrabUnit = true;
                grabbingUnit = hit.collider.gameObject.GetComponent<Unit>();

                DragManager.Instance.ShowPreview_Unit(hit.collider.gameObject); //마우스 보임
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //!! 그냥 유닛을 선택하려고 클릭했을 수도 있음.

            // 유닛을 쥐고 있었다면..
            if (isGrabUnit)
            {
                // 타일에 놓았으면 타일에 이미 배치 된 유닛이 있는지 확인하고 3가지 경우의 로직 진행
                // 1. 같은 타입 + 같은 등급의 유닛이 있는 경우 = Merge
                // 2. 1.번을 제외한 경우의 유닛이 배치 되어 있는 경우 = 원위치로 복귀
                // 3. 유닛이 배치되지 않은 경우 = MoveUnit(CollisionTile);

                // 
                Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 3/*Tile 레이어*/);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Tile hitTile = hit.collider.GetComponent<Tile>();

                    if (hitTile.IsUnitLocate && hitTile.GetUnit().gameObject != grabbingUnit.gameObject)
                    {
                        //여기가 합치는 부분인 듯.
                        if (hitTile.GetUnit().unitType == grabbingUnit.unitType && hitTile.GetUnit().UnitStar == grabbingUnit.UnitStar)
                        {
                            Debug.Log("합쳐");

                            //합치는 것, 끌어온 유닛 삭제하고 타일 초기화
                            grabbingUnit.ClearTile();
                            Destroy(grabbingUnit.gameObject);

                            //소리 입혀
                            EffectManager.Instance.SpawnEffect(EffectName.dust, hit.collider.gameObject.transform.position);

                            //업그레이드 부분
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
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Unit 레이어*/);

            if(hit.collider != null)
            {
                //여기에 돈 얻는 코드도 작성 부탁 (완성 된다면)
                hit.collider.GetComponent<Unit>().ClearTile();
                Destroy(hit.collider.gameObject);

                Debug.Log("판매");
            }
        }
    }
}
