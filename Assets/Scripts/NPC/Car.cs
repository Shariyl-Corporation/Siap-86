using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

[Serializable]
public class SpriteSet {
    public List<Sprite> spriteList;
}

public class Car : MonoBehaviour {
    /*
    Post-fix -Loc means it use the local grid coordinate instead of the world coordinate
    */
    [SerializeField] private CarManager carManager;
    [SerializeField] private float speed = 1;
    [SerializeField] private LayerMask carLayerMask;
    [SerializeField] private ParticleSystem smokeParticleSystem;
    public Driver driver;

    private HashSet<TileBase> visited2;
    private Vector3 targetPosition;
    private Vector3 destinationCell;
    private Vector3 prevCell;

    public bool isInTurnTile = false;
    public bool isWantToStraight = false;
    public bool isTilang;
    public bool isMundur;
    private TrafficLightController trafficLight;
    public Vector3Int dir_vector;

    [SerializeField] private List<SpriteSet> SpriteSelection;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public enum ArahMataAngin {
        Timur, 
        Tenggara, 
        Selatan, 
        BaratDaya, 
        Barat, 
        BaratLaut, 
        Utara, 
        TimurLaut
    }

    private int SpriteSelect;


    void Start() {
        smokeParticleSystem.Play();
        carManager = FindObjectOfType<CarManager>();
        targetPosition = transform.position;

        driver = generateRandomDriver();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SpriteSelect = UnityEngine.Random.Range(0, 3);
    }

    void Update(){
        EvalRotation(transform.rotation.eulerAngles.z);
        if (isTilang) return;

        if (transform.position != targetPosition) {
            if (isInTurnTile) {
                // Debug.Log(isInTurnTile);
                // Debug.Log(trafficLight.CheckAllowStraightThrough(dir_vector));
                // Debug.Log(trafficLight.CheckAllowTurn(dir_vector));
                if ((isWantToStraight && trafficLight.CheckAllowStraightThrough(dir_vector)) ||
                    trafficLight.CheckAllowTurn(dir_vector)) {
                    isInTurnTile = false;
                }
                if (isInTurnTile) return;
            }
            MoveTowardsTargetPosition();
        }
        else {
            do_astar_step();
        }
    }

    void LateUpdate() {
        spriteRenderer.gameObject.transform.localRotation =  Quaternion.Inverse(transform.rotation);
        // smokeParticleSystem.gameObject.transform.localRotation = Quaternion.Inverse(transform.rotation);
    }

    void OnDestroy(){
        CarManager.RemoveCar(gameObject);
    }

    Driver generateRandomDriver() {
        Driver d = gameObject.AddComponent(typeof(Driver)) as Driver;
        // d.hasKTP = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["KTP"];
        // d.hasSIM = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["SIM"];
        // d.hasSTNK = Random.Range(0.0f, 1.0f) > StateManager.Instance.SpawnRate["STNK"];

        // if (StateManager.Instance.DayCount >= 2){
        //     d.isDrunk = Random.Range(0.0f, 1.0f) < StateManager.Instance.SpawnRate["DR"];
        // } else {
        //     d.isDrunk = true;
        // }

        // if (StateManager.Instance.DayCount >= 3){
        //     if (Random.Range(0.0f, 1.0f) < StateManager.Instance.SpawnRate["UA"]){
        //         d.Age = Random.Range(14, 17);
        //     } else {
        //         d.Age = Random.Range(17, 53);
        //     }
        // }
            
        return d;
    }

    Driver generateGuiltyDriver() {
        Driver d = gameObject.AddComponent(typeof(Driver)) as Driver;
        d.hasKTP = false;
        d.hasSIM = false;
        d.hasSTNK = false;
        return d;
    }

    public void PrepareInterrogate() {
        isTilang = true;
        StartCoroutine(Minggir());
    }

    public void PrepareKembali() {
        isTilang = false;
        StartCoroutine(Kembali());
    }

    public void PrepareDisintegrate() {
        StartCoroutine(Disintegrate());
    }

    public IEnumerator Minggir() {
        dir_vector = GetForwardVector();
        if (isInTurnTile) {
            isMundur = true;
            // menuju kanan
            if (dir_vector == new Vector3Int(1, 0, 0)) {
                spriteRenderer.sortingOrder = 0;
            } else if (dir_vector == new Vector3Int(-1, 0, 0)) { // menuju kiri
                spriteRenderer.sortingOrder = 2;
            }

            yield return MinggirMundur();
        } else {
            isMundur = false;
            spriteRenderer.sortingOrder = 1;
            yield return MinggirMaju();
        }

        var collider = GetComponent<Collider2D>();
        collider.enabled = false;

        yield return null;
    }

    private IEnumerator MinggirMaju() {
        float angle = Mathf.Atan2(dir_vector.y, dir_vector.x) * Mathf.Rad2Deg;
        angle += 45;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position + QuaternionToVectorZ(rotation)*1.5f);

        angle -= 45;
        rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position + QuaternionToVectorZ(rotation)*1.5f);
    }

    private IEnumerator MinggirMundur() {
        float angle = Mathf.Atan2(dir_vector.y, dir_vector.x) * Mathf.Rad2Deg;
        angle -= 45;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position - QuaternionToVectorZ(rotation)*1.5f);

        angle += 45;
        rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position - QuaternionToVectorZ(rotation)*1.5f);
    }

    public IEnumerator Kembali() {
        var collider = GetComponent<Collider2D>();
        collider.enabled = true;

        if (isMundur) {
            yield return KembaliMaju();
        } else {
            yield return KembaliMundur();
        }
    }

    private IEnumerator KembaliMaju() {
        float angle = Mathf.Atan2(dir_vector.y, dir_vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position + QuaternionToVectorZ(rotation));

        angle -= 45;
        angle += 45;
        rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position + QuaternionToVectorZ(rotation));
    }

    private IEnumerator KembaliMundur() {
        float angle = Mathf.Atan2(dir_vector.y, dir_vector.x) * Mathf.Rad2Deg;
        angle -= 45;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position - QuaternionToVectorZ(rotation));

        angle += 45;
        rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        yield return Move(1, transform.position, transform.position - QuaternionToVectorZ(rotation));
    }

    private IEnumerator Disintegrate() {
        Color color = spriteRenderer.color;

        color.a = 1;
        while (color.a > 0) {
            spriteRenderer.color = color;
            color.a -= .5f * Time.deltaTime;
            yield return null;
        }
        CarManager.RemoveCar(gameObject);
    }



    IEnumerator Move(float duration, Vector3 startPosition, Vector3 endPosition){
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration; // Calculate the interpolation parameter

            // Interpolate the position linearly
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exactly the target position
        transform.position = endPosition;
    }

    Vector3 QuaternionToVectorZ(Quaternion quaternion) {
        // Extract the Z-axis rotation angle from the quaternion
        float angle = quaternion.eulerAngles.z;

        // Convert the angle to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate the vector using trigonometry
        Vector3 vector = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);

        return vector;
    }

    // calculating cell to the destination, BOTH using the local coordinate (grid)
    private float calculate_heuristic_at(Vector3Int cell) {
        return Vector3.Distance(cell, destinationCell);
    }

    // it is actually not an a star, cuz it cant backtrack, maybe hill climbing?
    private void do_astar_step() {
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

        if (isInTurnTile) {
            // Debug.Log((targetPosition - transform.position).normalized);
            // Debug.Log(dir_vector);

            isWantToStraight = Vector3Int.RoundToInt((targetPosition - transform.position).normalized) == dir_vector;
        }
        // Debug.Log("Move Towards: " + targetPosition.x + " " + targetPosition.y);
        // Debug.Log("From: " + transform.position.x + " " + transform.position.y);
    }
    
    private void MoveTowardsTargetPosition() {
        var angle = angle_towards(targetPosition);
        var straight_vector = Vector3Int.RoundToInt(transform.rotation * Vector3.right);
        var collision = Percept();

        foreach (var c in collision){
            var go = c.gameObject;
            if (go.CompareTag("car") && go.GetInstanceID() != gameObject.GetInstanceID())
                return;
        }
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private Collider2D[] Percept() {
        var angle = angle_towards(targetPosition);
        var forwardVector = GetForwardVector();
        var check_collision = transform.position + (Vector3)forwardVector*2.5f;
        var collision = Physics2D.OverlapBoxAll(check_collision, new Vector2(.5f, .5f), angle, carLayerMask);

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

    private void EvalRotation(float angle) {
        angle %= 360f;
        var spriteList = SpriteSelection[SpriteSelect].spriteList;
        if (angle < 22.5 || angle > 337.5)  {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.Timur];
        } else if (angle < 67.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.TimurLaut];
        } else if (angle < 112.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.Utara];
        } else if (angle < 157.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.BaratLaut];
        } else if (angle < 202.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.Barat];
        } else if (angle < 247.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.BaratDaya];
        } else if (angle < 292.5) {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.Selatan];
        } else {
            spriteRenderer.sprite = spriteList[(int)ArahMataAngin.Tenggara];
        }
    }
}
