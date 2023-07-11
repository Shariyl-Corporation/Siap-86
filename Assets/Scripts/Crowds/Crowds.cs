using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crowds : MonoBehaviour
{
    [SerializeField] private Tilemap walkTM;
    [SerializeField] private string spriteSpawnerName;
    [SerializeField] private GameObject pedestrian;

    private List<GameObject> pedestrians;
    private List<Vector3> spawnPoints;

    [ContextMenu("spawn")]
    public void spawnRandomPedestrian()
    {
        int randomPosition = Random.Range(0, spawnPoints.Count);
        GameObject ped = Instantiate(pedestrian, spawnPoints[randomPosition] + gameObject.transform.position, transform.rotation);
        ped.transform.parent = gameObject.transform;
        pedestrians.Add(ped);
        Debug.Log("Spawn Capsule: " + spawnPoints[randomPosition].x + " " + spawnPoints[randomPosition].y);
    }
    void Start()
    {
        pedestrians = new List<GameObject>();

        spawnPoints = new List<Vector3>();
        BoundsInt bounds = walkTM.cellBounds;
        TileBase[] allTiles = walkTM.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (tile.name == spriteSpawnerName)
                        spawnPoints.Add(new Vector3(x - bounds.size.x / 2, y - bounds.size.y / 2, 0));
                }
            }
        }

        foreach (Vector3 p in spawnPoints)
        {
            Debug.Log("Spawn point: " + p.x + " " + p.y);
        }
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
        spawnRandomPedestrian();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
