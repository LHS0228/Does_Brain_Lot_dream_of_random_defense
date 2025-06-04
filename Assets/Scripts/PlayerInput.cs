using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    TileManager tileManager;

    bool isGrabTower = false;
    Tower grabbingTower = null;

    void Update()
    {
        TowerDrag();
        TowerSellClick();
    }

    private void TowerDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Tower ���̾�*/);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                isGrabTower = true;
                grabbingTower = hit.collider.gameObject.GetComponent<Tower>();

                DragManager.Instance.ShowPreview_Tower(hit.collider.gameObject); //���콺 ����
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //!! �׳� ������ �����Ϸ��� Ŭ������ ���� ����.

            // ������ ��� �־��ٸ�..
            if (isGrabTower)
            {
                // Ÿ�Ͽ� �������� Ÿ�Ͽ� �̹� ��ġ �� ������ �ִ��� Ȯ���ϰ� 3���� ����� ���� ����
                // 1. ���� Ÿ�� + ���� ����� ������ �ִ� ��� = Merge
                // 2. 1.���� ������ ����� ������ ��ġ �Ǿ� �ִ� ��� = ����ġ�� ����
                // 3. ������ ��ġ���� ���� ��� = MoveTower(CollisionTile);

                // 
                Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 3/*Tile ���̾�*/);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Tile hitTile = hit.collider.GetComponent<Tile>();

                    if (hitTile.IsTowerLocate && hitTile.GetTower().gameObject != grabbingTower.gameObject)
                    {
                        //���Ⱑ ��ġ�� �κ��� ��.
                        if (hitTile.GetTower().towerType == grabbingTower.towerType && hitTile.GetTower().towerStar == grabbingTower.towerStar)
                        {
                            Debug.Log("����");

                            //��ġ�� ��, ����� ���� �����ϰ� Ÿ�� �ʱ�ȭ
                            grabbingTower.ClearTile();
                            Destroy(grabbingTower.gameObject);

                            //�Ҹ� ����
                            EffectManager.Instance.SpawnEffect(EffectName.dust, hit.collider.gameObject.transform.position);

                            //���׷��̵� �κ�
                            hitTile.GetTower().StateSetting(0, 1, 1, 1, hitTile.GetTower().towerStar + 1);
                        }
                    }
                    else
                    {
                        grabbingTower.Move(hitTile);
                    }
                }

                DragManager.Instance.HidePreview_Tower();

                isGrabTower = false;
                grabbingTower = null;
            }
        }

        if (isGrabTower)
        {
            DragManager.Instance.UpdatePreviewPostion();
        }
    }
    
    private void TowerSellClick()
    {   
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, 1 << 6/*Tower ���̾�*/);

            if(hit.collider != null)
            {
                //���⿡ �� ��� �ڵ嵵 �ۼ� ��Ź (�ϼ� �ȴٸ�)
                hit.collider.GetComponent<Tower>().ClearTile();
                Destroy(hit.collider.gameObject);

                Debug.Log("�Ǹ�");
            }
        }
    }
}
