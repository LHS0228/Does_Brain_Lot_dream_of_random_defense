using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour 
{
    bool isUnitLocate = false;
    public bool IsUnitLocate => isUnitLocate;
    Unit placedUnit = null;
    Unit PlacedUnit => placedUnit;

    public void InPlace(Unit unit)
    {
        placedUnit = unit;
        isUnitLocate = true;
    }
    public void OutPlace()
    {
        placedUnit = null;
        isUnitLocate = false;
    }
}
