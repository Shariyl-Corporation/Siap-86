using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    void Start() {
        StartCoroutine(LoadWorldWithControl());
    }

    // Update is called once per frame
    void Update() {
        
    }

    private IEnumerator LoadWorldWithControl(){
        yield return Orchestrator.Instance.LoadWorld();
        WorldControl.Instance.EnableControl();
    }
}
