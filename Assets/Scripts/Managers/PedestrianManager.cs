using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PedestrianManager : MonoBehaviour
{

    private List<Vector3Int> spawnPoints;
    private List<Pedestrian> pedestrians;
    [SerializeField] private Pedestrian pedestrianGO;
    [SerializeField] private float spawnDelay = 3;
    private float timer = 0;
    // ------------------------------------
    public Grid grid;
    public Tilemap tilemap;
    public TileBase spawnTile;
    public TileBase walkTile;
    public TileBase decisionTile;
    public TileBase crossTile;

    [HideInInspector] private static Dictionary<Vector3Int, TileBase> dataFromTiles;

    public static Dictionary<Vector3Int, TileBase> get_neighbor_tiles(Vector3Int cell_position)
    {
        Dictionary<Vector3Int, TileBase> neighbor = new Dictionary<Vector3Int, TileBase>();

        var cell_up = cell_position + Vector3Int.up;

        var cell_right = cell_position + Vector3Int.right;
        var cell_down = cell_position + Vector3Int.down;
        var cell_left = cell_position + Vector3Int.left;

        neighbor.Add(cell_up, dataFromTiles[cell_up]);
        neighbor.Add(cell_right, dataFromTiles[cell_right]);
        neighbor.Add(cell_down, dataFromTiles[cell_down]);
        neighbor.Add(cell_left, dataFromTiles[cell_left]);

        return neighbor;
    }
    public Dictionary<Vector3Int, TileBase> get_neighbor_tiles_world(Vector3 position)
    {
        var gridPosition = grid.WorldToCell(position);
        // Debug.Log("Capsule Grid Position: " + gridPosition.x + " " + gridPosition.y);
        return get_neighbor_tiles(gridPosition);
    }

    public List<Vector3Int> get_all_position_for_tile(TileBase tile)
    {
        return dataFromTiles.Where(pair => pair.Value == tile)
                            .Select(pair => pair.Key)
                            .ToList();
    }

    // ------------------------------------
    [ContextMenu("spawn")]
    public void spawnRandomPedestrian()
    {
        int spawnPosition = Random.Range(0, spawnPoints.Count);
        Pedestrian ped = Instantiate(pedestrianGO, spawnPoints[spawnPosition], transform.rotation);

        int destinationPosition;
        do
        {
            destinationPosition = Random.Range(0, spawnPoints.Count);
        } while (destinationPosition == spawnPosition);
        ped.setDestination(spawnPoints[destinationPosition]);
        // Debug.Log("Spawn Capsule: " + spawnPoints[spawnPosition].x + " " + spawnPoints[spawnPosition].y);
        // Debug.Log("Destination Capsule: " + spawnPoints[destinationPosition].x + " " + spawnPoints[destinationPosition].y);

        ped.transform.parent = gameObject.transform;
        pedestrians.Add(ped);
    }
    private void Awake()
    {
        pedestrians = new List<Pedestrian>();
        dataFromTiles = new Dictionary<Vector3Int, TileBase>();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] Tiles = tilemap.GetTilesBlock(bounds);

        // filling Dictionary
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = Tiles[x + y * bounds.size.x];
                var vec = new Vector3Int(x - bounds.size.x / 2, y - bounds.size.y / 2);
                var vecgrid = grid.WorldToCell(vec + transform.position);

                dataFromTiles.Add(grid.WorldToCell(vecgrid), tile);
            }
        }

        spawnPoints = get_all_position_for_tile(spawnTile);
    }

    private void Start()
    {
        spawnRandomPedestrian();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnDelay)
        {
            timer = 0;
            spawnRandomPedestrian();
        }
    }
}
