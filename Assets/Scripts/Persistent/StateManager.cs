using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public static StateManager Instance;

    public int PlayerPublicOpinion = 0;
    public int PlayerHealth = 100;
    public int PlayerMental = 100;
    public int PlayerMoney = 100;
    public int PlayerCorrectCountTotal = 0;
    public int PlayerIncorrectCountTotal = 0;
    public int PlayerCorrectCountDay = 0;
    public int PlayerIncorrectCountDay = 0;
    public int DayCount = 0;

    public Dictionary<string, float> SpawnRate = new Dictionary<string, float>(){
        {"KTP",   0.1f},
        {"SIM",   0.1f},
        {"STNK", 0.1f},
        {"RL",   0.1f},
        {"DR",   0.1f},
        {"UA",   0.1f},
    };

    public float WorldTick;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }

    void Start() {
        
    }

    void Update()
    {
        
    }

    public void ResetPlayerStats() {
        PlayerPublicOpinion = 0;
        PlayerHealth = 100;
        PlayerMental = 100;
        PlayerMoney = 100;
        PlayerCorrectCountTotal = 0;
        PlayerIncorrectCountTotal = 0;
        PlayerCorrectCountDay = 0;
        PlayerIncorrectCountDay = 0;
        DayCount = 0;
    }

    public void ResetSpawnRate() {
        SpawnRate["KTP"] = 0.1f;
        SpawnRate["SIM"] = 0.1f;
        SpawnRate["STNK"] = 0.1f;
        SpawnRate["RL"] = 0.1f;
        SpawnRate["DR"] = 0.1f;
        SpawnRate["UA"] = 0.1f;
    }

    public void ResetAll() {
        ResetPlayerStats();
        ResetSpawnRate();
    }

    private float clamp01015(float spwn_rate){
        return math.clamp(spwn_rate, 0.1f, 0.15f);
    }

    public void ModifySpawnRate(string key, bool isCorrect) {
        if (isCorrect) SpawnRate[key] = clamp01015(SpawnRate[key] - 0.01f);
        else SpawnRate[key] = clamp01015(SpawnRate[key] + 0.01f);
    }

    private void ResetWorldTick() {
        WorldTick = 0;
    }
}
