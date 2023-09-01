using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    public static MainMenu Instance;

    [SerializeField] private SpriteRenderer fadeSpriteRenderer;
    [SerializeField] private GameObject playButton, settingsButton, creditsButton, quitButton;
    private Camera mainCamera;
    

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
        mainCamera = Camera.main;
    }

    void Start(){
        Color color = Color.black;
        color.a = 0;
        fadeSpriteRenderer.color = color;
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
        yield return FadeOut();
        yield return Orchestrator.Instance.GoToGame();
    }

    IEnumerator FadeOut(){
        Color color = fadeSpriteRenderer.color;
        while (color.a < 1) {
            fadeSpriteRenderer.color = color;
            color.a += .5f * Time.deltaTime;
            yield return null;
        }
    }
}
