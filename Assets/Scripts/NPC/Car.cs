using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Car : MonoBehaviour {
    /*
    Post-fix -Loc means it use the local grid coordinate instead of the world coordinate
    */
    [SerializeField] private CarManager carManager;
    [SerializeField] private float speed = 1;
    [SerializeField] private LayerMask carLayerMask;
    [SerializeField] private LayerMask trafficLayerMask;

    public Driver driver;

    private HashSet<TileBase> visited2;
    private Vector3 targetPosition;
    private Vector3 destinationCell;
    private Vector3 prevCell;

    private bool isInTurnTile = false;
    private TrafficLightController trafficLight;
    private Vector3Int dir_vector;

    private bool isTilang;

    void Start() {
        targetPosition = transform.position;

        driver = generateRandomDriver();
    }
    void Update(){
        if (isTilang) return;
        if (transform.position != targetPosition) {
            if (isInTurnTile) {
                var state = trafficLight.traffic_light[TrafficLightController.VectorToDirection[dir_vector]];
                if (state == TrafficLightController.State.red || state == TrafficLightController.State.yellow){
                    return;
                }
                isInTurnTile = false;
            }
            MoveTowardsTargetPosition();
        }
        else {
            do_astar_step();
        }

    }

    void OnDestroy(){
        CarManager.RemoveCar(this);
    }

    Driver generateRandomDriver() {
        Driver d = gameObject.AddComponent(typeof(Driver)) as Driver;
        d.hasKTP = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["KTP"];
        d.hasSIM = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["SIM"];
        d.hasSTNK = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["STNK"];

        if (StateManager.Instance.DayCount >= 2){
            d.isDrunk = Random.Range(0.0f, 1.0f) < StateManager.Instance.SpawnRate["DR"];
        } else {
            d.isDrunk = true;
        }

        if (StateManager.Instance.DayCount >= 3){
            if (Random.Range(0.0f, 1.0f) < StateManager.Instance.SpawnRate["UA"]){
                d.Age = Random.Range(14, 17);
            } else {
                d.Age = Random.Range(17, 53);
            }
        }
            
        return d;
    }

    Driver generateGuiltyDriver() {
        Driver d = gameObject.AddComponent(typeof(Driver)) as Driver;
        d.hasKTP = false;
        d.hasSIM = false;
        d.hasSTNK = false;
        return d;
    }

    public void Minggir() {
        // jika posisi dekat turn tile
        
        // posisi aman

    }

    public void Kembali() {

    }

    // calculating cell to the destination, BOTH using the local coordinate (grid)
    private float calculate_heuristic_at(Vector3Int cell) {
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
            isInTurnTile = true;
            trafficLight = Percept()[0].GetComponent<TrafficLightController>();
            dir_vector = GetForwardVector();
        }
        else {
            dict = carManager.get_neighbor_tiles_world(transform.position);
        }

        foreach (var kp in dict) {
            if (kp.Value != null && (Vector3)kp.Key != prevCell) {
                float heuristic = calculate_heuristic_at(kp.Key);
                if (heuristic < min_heuristic) {
                    decision = kp.Key;
                    min_heuristic = heuristic;
                }
            }
        }

        // cant go anywhere
        if (min_heuristic == float.PositiveInfinity) {
            // Debug.Log("Cant go anywhere! " + gameObject.transform.position);
            Destroy(gameObject);
        }

        prevCell = transform.position;
        targetPosition = decision;
        // Debug.Log("Move Towards: " + targetPosition.x + " " + targetPosition.y);
        // Debug.Log("From: " + transform.position.x + " " + transform.position.y);
    }
    
    private void MoveTowardsTargetPosition() {
        var angle = angle_towards(targetPosition);
        var straight_vector = Vector3Int.RoundToInt(transform.rotation * Vector3.right);
        var collision = Percept();

        foreach (var c in collision){
            var go = c.gameObject;
            if (go.CompareTag("car"))
                return;
        }
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private Collider2D[] Percept() {
        var angle = angle_towards(targetPosition);
        var forwardVector = GetForwardVector();
        var check_collision = transform.position + forwardVector*2;
        var collision = Physics2D.OverlapBoxAll(check_collision, new Vector2(2, 2), angle, carLayerMask);

        return collision;
    }

    private Vector3Int GetForwardVector(){
        return Vector3Int.RoundToInt(transform.rotation * Vector3.right);
    }

    private float angle_towards(Vector3 dir) {
        var targetDirection = dir - transform.position;
        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        return angle;
    }

    private void step(Vector3Int dir) {
        targetPosition = transform.position + dir;
    }

    public void setDestination(Vector3Int destinationCell)
    {
        this.destinationCell = destinationCell;
    }
}
