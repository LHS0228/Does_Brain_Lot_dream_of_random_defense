using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour 
{
    bool isTowerLocate = false;
    public bool IsTowerLocate => isTowerLocate;
    Tower placedTower = null;
    Tower PlacedTower => placedTower;

    public void InPlace(Tower tower)
    {
        placedTower = tower;
        isTowerLocate = true;
    }

    public void OutPlace()
    {
        placedTower = null;
        isTowerLocate = false;
    }

    public Tower GetTower()
    {
        return placedTower;
    }
}
