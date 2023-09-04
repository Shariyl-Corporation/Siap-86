using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    public static MainMenu Instance;

    [SerializeField] private SpriteRenderer fadeSpriteRenderer;
    
    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }

    void Start(){
        StartCoroutine(FadeIn());
    }

    void Update(){}

    public void changeCursor(bool hover) {
        Orchestrator.Instance.ChangeCursor(hover);
    }

    public void OnClickPlay() {
        StartCoroutine(FadeToGame());
    }

    public void OnClickSettings() {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void OnClickCredits() {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void OnClickQuit() {
        Application.Quit();
    }

    public IEnumerator FadeToGame() {
        AudioManager.Instance.StopMusic();
        yield return FadeOut();
        yield return Orchestrator.Instance.GoToGame();
    }

    IEnumerator FadeOut(){
        Color color = Color.black;
        color.a = 0;
        fadeSpriteRenderer.color = color;
        while (color.a < 1) {
            fadeSpriteRenderer.color = color;
            color.a += .5f * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeIn(){
        Color color = Color.black;
        color.a = 1;
        fadeSpriteRenderer.color = color;
        while (color.a > 0) {
            fadeSpriteRenderer.color = color;
            color.a -= .5f * Time.deltaTime;
            yield return null;
        }
    }
}
