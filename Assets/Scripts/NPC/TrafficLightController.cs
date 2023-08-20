using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class TrafficLightController : MonoBehaviour {

    private float phaseTimer = 0f;

    public float phaseDuration = 5f;
    public Sprite[] phasesSprite;
    public TileBase crossTile;
    public int phase;
    public static bool firstPrint = true;

    private SpriteRenderer spriteRenderer;
    public enum State {
        red,
        yellow,
        green,
        none
    };

    public enum DirectionFrom {
        right,
        left,
        up,
        down
    }

    public Dictionary<DirectionFrom, State> traffic_light;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        traffic_light =  new Dictionary<DirectionFrom, State>();
        build_traffic_light();
    }

    void Update() {
        phaseTimer += Time.deltaTime;
        if (phaseTimer >= phaseDuration) {
            phaseTimer = 0f;
            changePhase();
        }
    }

    void changePhase() {
        traffic_light[(DirectionFrom)phase] = State.red;
        for (int i = 0; i < 4; i++) {
            phase = (phase + 1) % 4;
            var dir = (DirectionFrom)phase;
            if (traffic_light.ContainsKey(dir)) {
                spriteRenderer.sprite = phasesSprite[phase];
                traffic_light[dir] = State.green;
                break;
            }
        }
    }


    void build_traffic_light() {
        var turn_tiles = CarManager.get_all_position_for_tile(crossTile);

        var right_tile = Vector3Int.RoundToInt(transform.position + new Vector3(6, -2, 0));
        var left_tile = Vector3Int.RoundToInt(transform.position + new Vector3(-6, 2, 0));
        var up_tile = Vector3Int.RoundToInt(transform.position + new Vector3(2, 6, 0));
        var down_tile = Vector3Int.RoundToInt(transform.position + new Vector3(-2, -6, 0));

        if (turn_tiles.Contains(right_tile)){
            traffic_light.Add(DirectionFrom.right, State.red);
            Debug.Log("right");
        }
        if (turn_tiles.Contains(left_tile)){
            traffic_light.Add(DirectionFrom.left, State.red);
            Debug.Log("left");
        }
        if (turn_tiles.Contains(up_tile)){
            traffic_light.Add(DirectionFrom.up, State.red);
            Debug.Log("up");
        }
        if (turn_tiles.Contains(down_tile)){
            traffic_light.Add(DirectionFrom.down, State.red);
            Debug.Log("down");
        }

        Debug.Log(right_tile + " " + left_tile + " " + up_tile + " " + down_tile);
        // foreach(var traffic in traffic_light) {
        //     Debug.Log(traffic.Key + " " + traffic.Value);
        // }
        if (firstPrint) {
            foreach(var tile in turn_tiles) {
                Debug.Log(tile);
            }
            firstPrint = false;
        }
        
        // pls fix
        traffic_light[DirectionFrom.right] = State.green;
        phase = (int)DirectionFrom.right;
    }

}