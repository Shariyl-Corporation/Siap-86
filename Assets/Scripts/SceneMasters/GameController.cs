using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    int hour, minute;

    private TextMeshPro timeText;

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
        yield return Orchestrator.Instance.LoadScene("HUD");
        yield return Orchestrator.Instance.LoadScene("DialogueScene");
        DialogueScene.OnSceneUnloaded += AudioManager.Instance.PlayGameMusic;
        // WorldControl.Instance.EnableControl();
    }

    public void StartTimer(TextMeshPro time) {
        hour = 7;
        minute = 0;
        StartCoroutine(RunTimer(time));
    }

    public IEnumerator RunTimer(TextMeshPro time) {
        do {
            string minuteString = minute < 10 ? "0" + minute.ToString("D") : minute.ToString("D");
            string hourString = hour < 10 ? "0" + hour.ToString("D") : hour.ToString("D");
            time.text = hourString + ":" + minuteString;
            yield return new WaitForSeconds(5);
            incrementTime();
        } while (hour < 13 || minute < 10);
    }

    public void incrementTime(){
        minute += 10;
        if (minute == 60) {
            hour += 1;
            minute = 0;
        }
    }
    
    public void SetTimeText(TextMeshPro tm) {
        timeText = tm;
    }
}
