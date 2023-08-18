using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public enum State {
    phase1,
    phase2,
    phase3,
    phase4
}

public class TrafficLightController : MonoBehaviour {

    private float phaseTimer = 0f;

    public float phaseDuration = 5f;
    public State state;
    public Sprite[] phasesSprite;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        phaseTimer += Time.deltaTime;
        if (phaseTimer >= phaseDuration) {
            phaseTimer = 0f;
            changePhase();
        }
    }

    void changePhase() {
        int currentPhase = ((int)state + 1) % 4;
        spriteRenderer.sprite = phasesSprite[currentPhase];
        state = (State)currentPhase;
        // Debug.Log("Change phase to " + state);
    }
}