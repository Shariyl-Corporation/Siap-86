using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapData", menuName = "ScriptableObjects/TileMapData")]
public class TileMapData : ScriptableObject
{
    // public Grid grid;
    public Tilemap tilemap;
    public TileBase spawnTile;
    public TileBase walkTile;
    public TileBase decisionTile;
    public TileBase crossTile;

    [HideInInspector] public Dictionary<Vector3Int, TileBase> dataFromTiles = new Dictionary<Vector3Int, TileBase>();

    public Dictionary<Vector3Int, TileBase> get_neighbor_tiles(Vector3Int cell_position)
    {
        Dictionary<Vector3Int, TileBase> neighbor = new Dictionary<Vector3Int, TileBase>();
        neighbor.Add(cell_position + Vector3Int.up, dataFromTiles[cell_position + Vector3Int.up]);
        neighbor.Add(cell_position + Vector3Int.right, dataFromTiles[cell_position + Vector3Int.right]);
        neighbor.Add(cell_position + Vector3Int.down, dataFromTiles[cell_position + Vector3Int.down]);
        neighbor.Add(cell_position + Vector3Int.left, dataFromTiles[cell_position + Vector3Int.left]);
        return neighbor;
    }

    public List<Vector3Int> get_all_position_for_tile(TileBase tile)
    {
        return dataFromTiles.Where(pair => pair.Value == tile)
                            .Select(pair => pair.Key)
                            .ToList();
    }

}
