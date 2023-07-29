using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CarManager : MonoBehaviour
{

    private List<Vector3Int> spawnPoints;
    private List<Car> cars;
    [SerializeField] private Car carGO;
    [SerializeField] private float spawnDelay = 3;
    private float timer = 0;
    // ------------------------------------
    public Grid grid;
    public Tilemap tilemap;
    public TileBase spawnTile;
    public TileBase leftTile;
    public TileBase rightTile;
    public TileBase crossTile;

    [HideInInspector] private static Dictionary<Vector3Int, TileBase> dataFromTiles;


    private void Awake()
    {
        cars = new List<Car>();
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
        spawnRandomCar();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnDelay)
        {
            timer = 0;
        }
    }

    public TileBase tileAt(Vector3 position){
        var gridPosition = grid.WorldToCell(position);
        return dataFromTiles[gridPosition];
    }
    public static Dictionary<Vector3Int, TileBase> get_turn_end_tiles(Vector3Int cell_position, Quaternion direction)
    {
        Dictionary<Vector3Int, TileBase> neighbor = new Dictionary<Vector3Int, TileBase>();
        const int straight_gap   = 12;
        const int turn_right_gap = 8;
        const int turn_left_gap  = 4;

        Vector3Int straight_vector = Vector3Int.RoundToInt(direction * Vector3.right);
        Vector3Int right_vector    = Vector3Int.RoundToInt(direction * Quaternion.AngleAxis(270, Vector3.forward) * Vector3.right);
        Vector3Int left_vector     = Vector3Int.RoundToInt(direction * Quaternion.AngleAxis(90, Vector3.forward) * Vector3.right);

        Debug.Log("direction eu: " + direction.eulerAngles.x + " " + direction.eulerAngles.y + " " + direction.eulerAngles.z);
        Debug.Log("Straight: " + straight_vector.x + " " + straight_vector.y + " " + straight_vector.z);
        Debug.Log("right: " + right_vector.x + " " + right_vector.y + " " + right_vector.z);
        Debug.Log("left: " + left_vector.x + " " + left_vector.y + " " + left_vector.z);

        var straight_cell   = cell_position + straight_vector * straight_gap;
        var turn_right_cell = cell_position + (straight_vector + right_vector) * turn_right_gap;
        var turn_left_cell  = cell_position + (straight_vector + left_vector) * turn_left_gap;

        Debug.Log("Straight: " + straight_cell.x + " " + straight_cell.y + " " + straight_cell.z);
        Debug.Log("right: " + turn_right_cell.x + " " + turn_right_cell.y + " " + turn_right_cell.z);
        Debug.Log("left: " + turn_left_cell.x + " " + turn_left_cell.y + " " + turn_left_cell.z);

        neighbor.Add(straight_cell,   dataFromTiles[straight_cell]);
        neighbor.Add(turn_right_cell, dataFromTiles[turn_right_cell]);
        neighbor.Add(turn_left_cell,  dataFromTiles[turn_left_cell]);

        return neighbor;
    }
    public Dictionary<Vector3Int, TileBase> get_turn_end_tiles(Vector3 position, Quaternion direction)
    {
        var gridPosition = grid.WorldToCell(position);
        return get_turn_end_tiles(gridPosition, direction);
    }

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
    public void spawnRandomCar()
    {
        int spawnPosition = Random.Range(0, spawnPoints.Count);
        Car ped = Instantiate(carGO, spawnPoints[spawnPosition], transform.rotation);

        int destinationPosition;
        do
        {
            destinationPosition = Random.Range(0, spawnPoints.Count);
        } while (destinationPosition == spawnPosition);
        ped.setDestination(spawnPoints[destinationPosition]);
        Debug.Log("Spawn Capsule: " + spawnPoints[spawnPosition].x + " " + spawnPoints[spawnPosition].y);
        Debug.Log("Destination Capsule: " + spawnPoints[destinationPosition].x + " " + spawnPoints[destinationPosition].y);

        ped.transform.parent = gameObject.transform;
        cars.Add(ped);
    }
}
