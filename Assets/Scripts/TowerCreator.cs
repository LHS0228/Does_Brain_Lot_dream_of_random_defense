using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    [SerializeField]
    GameObject[] units = new GameObject[5];
    private int spawnGold = 10; //테스트가 원활하도록 일단 0

    public void CreateRandomUnit()
    {
        if (MoneyManager.Instance.Gold >= spawnGold)
        {
            MoneyManager.Instance.UpdateGold(-spawnGold);
            Tile spawnTile = TileManager.Instance.GetRandomSpawnableTile();
            // 타일이 가득 찼을 때!
            if (spawnTile == null) return;

            int randomTypeUnit = Random.Range(0, units.Length);

            GameObject spawnedUnit = Instantiate(units[randomTypeUnit]);
            Tower tower = spawnedUnit.GetComponent<Tower>();


            tower.Init(spawnTile);
            spawnGold += 10;
        }
    }
}
