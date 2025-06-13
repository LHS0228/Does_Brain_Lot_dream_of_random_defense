using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    [SerializeField]
    GameObject[] units = new GameObject[5];
    private int spawnGold = 10; //�׽�Ʈ�� ��Ȱ�ϵ��� �ϴ� 0

    public void CreateRandomUnit()
    {
        if (MoneyManager.Instance.Gold >= spawnGold)
        {
            MoneyManager.Instance.UpdateGold(-spawnGold);
            Tile spawnTile = TileManager.Instance.GetRandomSpawnableTile();
            // Ÿ���� ���� á�� ��!
            if (spawnTile == null) return;

            int randomTypeUnit = Random.Range(0, units.Length);

            GameObject spawnedUnit = Instantiate(units[randomTypeUnit]);
            Tower tower = spawnedUnit.GetComponent<Tower>();


            tower.Init(spawnTile);
            spawnGold += 10;
        }
    }
}
