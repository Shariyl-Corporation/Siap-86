using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class Pedestrian : MonoBehaviour
{
    /*
    Post-fix -Loc means it use the local grid coordinate instead of the world coordinate
    */
    [SerializeField] private PedestrianManager pedestrianManager;
    [SerializeField] private float speed = 1;

    private HashSet<TileBase> visited2;
    private Vector3 targetPosition;
    private Vector3 destinationCell;
    private Vector3 prevCell;

    // calculating cell to the destination, BOTH using the local coordinate (grid)
    private float calculate_heuristic_at(Vector3Int cell)
    {
        return Vector3.Distance(cell, destinationCell);
    }

    // it is actually not an a star, cuz it cant backtrack, maybe hill climbing?
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

        // cant go anywhere
        if (min_heuristic == float.PositiveInfinity)
        {
            Destroy(gameObject);
        }

        prevCell = transform.position;
        targetPosition = decision;
        Debug.Log("Move Towards: " + targetPosition.x + " " + targetPosition.y);
        Debug.Log("From: " + transform.position.x + " " + transform.position.y);
    }
    void Awake()
    {
    }
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
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
