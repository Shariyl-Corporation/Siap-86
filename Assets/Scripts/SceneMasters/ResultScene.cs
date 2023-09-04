using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class ResultScene : MonoBehaviour {
    public static event System.Action OnSceneUnloaded;
    [SerializeField] private TextMeshPro ScoreHuruf, FalseCount, TrueCount, TotalCount, Acc, Comment, Mission, hint;

    bool isAnimating;
    void Awake(){
        isAnimating = true;
        ScoreHuruf.text = "";
        FalseCount.text = "";
        TrueCount.text = "";
        TotalCount.text = "";
        Acc.text = "";
        Comment.text = "";
        Mission.text = "MISSION -----";
        hint.gameObject.SetActive(false);
    } 

    IEnumerator Start(){
        var correct = StateManager.Instance.PlayerCorrectCountDay;
        var incorrect = StateManager.Instance.PlayerIncorrectCountDay;
        var total = correct + incorrect;
        int acc = (int)Math.Truncate((float)correct / total * 100);

        string n;
        string c;

        yield return PlayText(FalseCount, $"False\n{incorrect}");
        yield return PlayText(TrueCount, $"True\n{correct}");
        yield return PlayText(TotalCount, $"Total\n{total}");
        yield return PlayText(Acc, $"Acc\n{acc}%");
        if (acc < 50) {
            n = "E";
            c = "POLICE BRUTALITY!";
        } else if (acc < 60) {
            n = "D";
            c = "Power abuse!";
        } else if (acc < 70) {
            n = "C";
            c = "Try harder!";
        } else if (acc < 80) {
            n = "B";
            c = "Good work!";
        } else if (acc < 90) {
            n = "A";
            c = "Incredible!";
        } else {
            n = "S";
            c = "Professional!";
        }
        string mission_status = acc < 70 ? "FAILED" : "SUCCESS";

        yield return PlayText(ScoreHuruf, n);
        StartCoroutine(PlayText(Comment, c));
        StartCoroutine(UpdateMissionStatus(mission_status));
        hint.gameObject.SetActive(true);
        isAnimating = false;
    }

    void Update(){
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape)) && !isAnimating) {
            // go to menu
        }
    }

    private IEnumerator PlayText(TextMeshPro tm, string s) {
        tm.text = "";

        foreach (char c in s.ToCharArray()) {
            tm.text += c;
            yield return new WaitForSecondsRealtime(waitTime(c));
        }
        yield return new WaitForSecondsRealtime(.5f);
    }

    private IEnumerator UpdateMissionStatus(string status) {
        while (Mission.text.EndsWith("-")){
            Mission.text = Mission.text[..^-1];
            yield return new WaitForSecondsRealtime(.1f);
        }

        foreach (char c in status.ToCharArray()) {
            Mission.text += c;
            yield return new WaitForSecondsRealtime(waitTime(c));
        }
    }


    private float waitTime(char letter) {
        switch (letter)
        {
            case '.':
                return 0.75f;
            case ',':
                return 0.5f;
            case '!':
                return 1.0f;
            case '?':
                return 0.75f;
            case '\n':
                return 0.2f;
            default:
                return 0.05f;
        }
    }

    private void OnDestroy(){                                           // inform orchestrator that this scene is unloaded
        OnSceneUnloaded?.Invoke();
        OnSceneUnloaded = null;
    }

    public void UnloadScene() {
        AudioManager.Instance.StopMusic();
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
