using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : UnitState
{
    private Tile curTile = null;

    public virtual void Init(Tile tile)
    {
        curTile = tile;
        Move(curTile);
    }

    public void Move(Tile targetTile)
    {
        if (targetTile == null) return;

        if (targetTile == curTile)
        {
            transform.position = targetTile.transform.position;
        }

        if (curTile != null)
        {
            curTile.OutPlace();
        }

        curTile = targetTile;
        curTile.InPlace(this);


        transform.position = targetTile.transform.position;
    }

    public void ClearTile()
    {
        curTile.OutPlace();
    }   
}
