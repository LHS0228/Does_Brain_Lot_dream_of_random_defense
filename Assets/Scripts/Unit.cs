using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UnitType { TTTTTTTTT}
public class Unit : MonoBehaviour
{
    UnitType type;

    private Tile curTile = null;
    private float range = 1f;
    public UnitType Type => type;
    public virtual void Init(Tile tile)
    {
        curTile = tile;
        Move(curTile);
    }
    public void Move(Tile targetTile)
    {
        if (targetTile == null) return;
        if(targetTile == curTile) transform.position = targetTile.transform.position;
        if (curTile != null) curTile.OutPlace();
        curTile = targetTile;
        curTile.InPlace(this);


        transform.position = targetTile.transform.position;
    }
}
