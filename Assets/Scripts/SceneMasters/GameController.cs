using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    void Start() {
        Orchestrator.Instance.RollIntro();
        IntroductionScene.OnSceneUnloaded += SetupAfterIntro;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetupAfterIntro(){
        StartCoroutine(LoadWorldAndDialogue());
        AudioManager.Instance.PlayDialogueMusic();
    }

    public IEnumerator LoadWorldAndDialogue(){
        yield return Orchestrator.Instance.LoadWorld();
        yield return Orchestrator.Instance.LoadScene("DialogueScene");
        DialogueScene.OnSceneUnloaded += AudioManager.Instance.PlayGameMusic;
        // WorldControl.Instance.EnableControl();

    }
}
