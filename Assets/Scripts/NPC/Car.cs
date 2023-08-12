using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;



public class Car : MonoBehaviour
{
    /*
    Post-fix -Loc means it use the local grid coordinate instead of the world coordinate
    */
    [SerializeField] private CarManager carManager;
    [SerializeField] private float speed = 1;

    public Driver driver;

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
        Dictionary<Vector3Int, TileBase> dict;
        if (carManager.tileAt(transform.position) == carManager.crossTile) {
            dict = carManager.get_turn_end_tiles(transform.position, transform.rotation);
        }
        else
        {
            dict = carManager.get_neighbor_tiles_world(transform.position);
        }
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
        // Debug.Log("Move Towards: " + targetPosition.x + " " + targetPosition.y);
        // Debug.Log("From: " + transform.position.x + " " + transform.position.y);
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
    // void OnMouseDown()
    // {
    //     Debug.Log("Clicked");
    //     SceneManager.LoadScene("Interact", LoadSceneMode.Additive);
    // }

    private void MoveTowardsTargetPosition()
    {

        var targetDirection = targetPosition - transform.position;
        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // transform.position += transform.forward * speed * Time.deltaTime;
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
