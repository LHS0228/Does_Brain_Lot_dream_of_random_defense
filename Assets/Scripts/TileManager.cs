using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance=>instance;
    Tile[] tiles;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        tiles = GetComponentsInChildren<Tile>();
    }
    public Tile GetRandomSpawnableTile()
    {
        List<Tile> list = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile.IsTowerLocate == false) list.Add(tile);
        }
        if (list.Count == 0)
            return null;
        
        int randomIdx = Random.Range(0, list.Count);
        return list[randomIdx];
    }
}
