using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    void Start() {
        Orchestrator.Instance.RollIntro();
        IntroductionScene.OnSceneUnloaded += LoadWorldWithControlCaller;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void LoadWorldWithControlCaller(){
        StartCoroutine(LoadWorldWithControl());
    }

    public IEnumerator LoadWorldWithControl(){
        yield return Orchestrator.Instance.LoadWorld();
        WorldControl.Instance.EnableControl();
    }
}
