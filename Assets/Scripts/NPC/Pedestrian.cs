using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private PedestrianManager pedestrianManager;
    [SerializeField] private float speed = 1;

    private HashSet<TileBase> visited2;
    private Vector3 targetPosition;
    private Vector3 destinationCell;
    private Vector3 prevCell;
    private SpriteRenderer npcSpriteRenderer;

    private float calculate_heuristic_at(Vector3Int cell)
    {
        return Vector3.Distance(cell, destinationCell);
    }

    private void do_astar_step()
    {
        Vector3Int decision = new Vector3Int();
        float min_heuristic = float.PositiveInfinity;
        var dict = pedestrianManager.get_neighbor_tiles_world(transform.position);
        foreach (var kp in dict)
        {
            if (kp.Value != null && (Vector3)kp.Key != prevCell)
            {
                float heuristic = calculate_heuristic_at(kp.Key);
                if (heuristic < min_heuristic)
                {
                    decision = kp.Key;
                    min_heuristic = heuristic;
                }
            }
        }

        if (min_heuristic == float.PositiveInfinity)
        {
            Destroy(gameObject);
        }

        prevCell = transform.position;
        targetPosition = decision;

        if (targetPosition.x > transform.position.x)
            npcSpriteRenderer.flipX = false; 

        else if (targetPosition.x < transform.position.x)
            npcSpriteRenderer.flipX = true; 

        Debug.Log("Move Towards: " + targetPosition.x + " " + targetPosition.y);
        Debug.Log("From: " + transform.position.x + " " + transform.position.y);
    }

    void Awake()
    {
    }

    void Start()
    {
        targetPosition = transform.position;
        npcSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position != targetPosition)
        {
            MoveTowardsTargetPosition();
        }
        else
        {
            do_astar_step();
        }
    }

    private void MoveTowardsTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (targetPosition.x > transform.position.x)
            npcSpriteRenderer.flipX = false; 

        else if (targetPosition.x < transform.position.x)
            npcSpriteRenderer.flipX = true; 
    }

    private void step(Vector3Int dir)
    {
        targetPosition = transform.position + dir;
    }

    public void setDestination(Vector3Int destinationCell)
    {
        this.destinationCell = destinationCell;
    }
}
