using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class TrafficLightController : MonoBehaviour {

    private float phaseTimer = 0f;

    public float phaseDuration = 20f;
    public Sprite[] phasesSprite;
    public TileBase crossTile;
    public int CurrentPhase;
    public static bool firstPrint = true;

    private SpriteRenderer spriteRenderer;
    public enum State {
        red,
        yellow,
        green,
        none
    };

    public enum DirectionFrom {
        left,
        down,
        right,
        up
    };

    private Dictionary<DirectionFrom, float> PhaseTime;
    
    [SerializeField] private float PhaseLeft = 15;
    [SerializeField] private float PhaseDown = 15;
    [SerializeField] private float PhaseRight = 15;
    [SerializeField] private float PhaseUp = 15;
    [SerializeField] private float PhaseYellow = 5;

    [SerializeField] private bool tlDirUp = true;
    [SerializeField] private bool tlDirRight = true;
    [SerializeField] private bool tlDirDown = true;
    [SerializeField] private bool tlDirLeft = true;

    [SerializeField] private DirectionFrom DefaultPhase = DirectionFrom.right;

    private bool isYellow = false;

    public static Dictionary<DirectionFrom, Vector3> DirectionToVector = new () {
        {DirectionFrom.left, new Vector3(1, 0, 0)},
        {DirectionFrom.down, new Vector3(0, 1, 0)},
        {DirectionFrom.right, new Vector3(-1, 0, 0)},
        {DirectionFrom.up, new Vector3(0, -1, 0)},
    };

    public static Dictionary<Vector3, DirectionFrom> VectorToDirection = new () {
        {new Vector3(1, 0, 0), DirectionFrom.left},
        {new Vector3(0, 1, 0), DirectionFrom.down},
        {new Vector3(-1, 0, 0), DirectionFrom.right},
        {new Vector3(0, -1, 0), DirectionFrom.up},
    };

    public Dictionary<DirectionFrom, State> traffic_light;

    [ContextMenu("Update Time")]
    void UpdatePhaseTime() {
        PhaseTime = new Dictionary<DirectionFrom, float>{
            {DirectionFrom.left, PhaseLeft},
            {DirectionFrom.down, PhaseDown},
            {DirectionFrom.right, PhaseRight},
            {DirectionFrom.up, PhaseUp},
        };
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        traffic_light =  new Dictionary<DirectionFrom, State>();
        build_traffic_light();
        UpdatePhaseTime();

    }

    void Update() {
        if (isYellow) return;
        phaseTimer += Time.deltaTime;
        if (phaseTimer >= PhaseTime[(DirectionFrom)CurrentPhase]) {
            phaseTimer = 0f;
            StartCoroutine(ChangePhase());
        }
    }

    IEnumerator ChangePhase() {
        traffic_light[(DirectionFrom)CurrentPhase] = State.red;
        DirectionFrom dir = new();
        for (int i = 0; i < 4; i++) {
            CurrentPhase = (CurrentPhase + 1) % 4;
            dir = (DirectionFrom)CurrentPhase;
            if (traffic_light.ContainsKey(dir)) {
                break;
            }
        }
        spriteRenderer.sprite = phasesSprite[4];
        isYellow = true;
        yield return new WaitForSeconds(PhaseYellow);
        isYellow = false;
        spriteRenderer.sprite = phasesSprite[CurrentPhase];
        traffic_light[dir] = State.green;
        // var color = spriteRenderer.color;
        // color.a = 0.3f;
        // spriteRenderer.color = color;
        yield return null;
    }


    void build_traffic_light() {
        var turn_tiles = CarManager.get_all_position_for_tile(crossTile);

        var right_tile = Vector3Int.RoundToInt(transform.position + new Vector3(6, -2, 0));
        var left_tile = Vector3Int.RoundToInt(transform.position + new Vector3(-6, 2, 0));
        var up_tile = Vector3Int.RoundToInt(transform.position + new Vector3(2, 6, 0));
        var down_tile = Vector3Int.RoundToInt(transform.position + new Vector3(-2, -6, 0));

        if (tlDirRight){// turn_tiles.Contains(right_tile)){
            traffic_light.Add(DirectionFrom.right, State.red);
            //Debug.Log("right");
        }
        if (tlDirLeft){// turn_tiles.Contains(left_tile)){
            traffic_light.Add(DirectionFrom.left, State.red);
            //Debug.Log("left");
        }
        if (tlDirUp){// turn_tiles.Contains(up_tile)){
            traffic_light.Add(DirectionFrom.up, State.red);
            //Debug.Log("up");
        }
        if (tlDirDown){// turn_tiles.Contains(down_tile)){
            traffic_light.Add(DirectionFrom.down, State.red);
            //Debug.Log("down");
        }

        // pls fix
        traffic_light[DefaultPhase] = State.green;
        CurrentPhase = (int)DefaultPhase;
    }

    public bool CheckAllowStraightThrough(Vector3Int dir_vector) {
        int index_left = ((int) VectorToDirection[dir_vector] - 1) % 4;
        return !traffic_light.ContainsKey((DirectionFrom)index_left);
    } 

    public bool CheckAllowTurn(Vector3Int dir_vector) {
        var state = traffic_light[VectorToDirection[dir_vector]];
        return state == State.green;
    }
}