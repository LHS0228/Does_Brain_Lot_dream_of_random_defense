using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField]
    GameObject[] units = new GameObject[5];

    public void CreateRandomUnit()
    {
        Tile spawnTile = TileManager.Instance.GetRandomSpawnableTile();
        // Ÿ���� ���� á�� ��!
        if (spawnTile == null) return;

        int randomTypeUnit = Random.Range(0, units.Length);
        
        GameObject spawnedUnit = Instantiate(units[randomTypeUnit]);
        Unit unit = spawnedUnit.GetComponent<Unit>();


        unit.Init(spawnTile);
    }
}
