using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private Tilemap pathTilemap;
    [SerializeField] private Grid pathGrid;
    [SerializeField] private float speed = 1;
    [SerializeField] private string spritePathName;


    private HashSet<Vector3> visited;
    private Vector3 targetPosition;

    // a lil bit janky but idk
    [ContextMenu("Do Random Walk")]
    public void randomWalk()
    {
        Vector3 p = transform.position;
        List<Vector2Int> directions = new List<Vector2Int>();
        directions.Add(Vector2Int.right);
        directions.Add(Vector2Int.down);
        directions.Add(Vector2Int.left);
        directions.Add(Vector2Int.up);

        Debug.Log("Position: " + p.x + " " + p.y);
        for (int i = 4; i > 0; i--)
        {
            var randomInt = Random.Range(0, i);
            Vector3 test = p + ((Vector3Int)directions[randomInt]);
            Vector3Int w2c = pathTilemap.WorldToCell(test);
            TileBase tile = pathTilemap.GetTile(w2c);
            if (tile != null)
            {
                Debug.Log("Tile not null name: " + tile.name);
                if (tile.name == spritePathName && !visited.Contains(test))
                {
                    // move
                    Debug.Log("MOVING TO:" + test.x + " " + test.y);
                    targetPosition = test;
                    visited.Add(test);
                    return;
                }
                Debug.Log("Tile is visited");
            }
            directions.RemoveAt(randomInt);
        }
        Debug.Log("CANT MOVE");

        // reset visited cuz the pedestrian just making a loop
        visited = new HashSet<Vector3>();
    }

    [ContextMenu("Let the AI choose")]
    public void doRandomActivity()
    {
        float randomNumber = Random.Range(0, 1);

        if (randomNumber < 0.01)
        {
            // do idle animation
        }
        else
        {
            // do random walk
            randomWalk();
        }

    }

    void Awake()
    {
        visited = new HashSet<Vector3>();
        targetPosition = transform.position;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var moving = (Vector3)transform.position != targetPosition;

        if (moving)
        {
            MoveTowardsTargetPosition();
        }
        else
        {
            randomWalk();
        }
    }

    private void MoveTowardsTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
