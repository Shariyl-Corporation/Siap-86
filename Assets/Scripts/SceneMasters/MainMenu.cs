using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    public static MainMenu Instance;

    [SerializeField] private GameObject playButton, settingsButton, creditsButton, quitButton;
    private Camera mainCamera;
    

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }

    }

    void Start(){
        mainCamera = Camera.main;
    }

    void Update(){}

    public void changeCursor(bool hover) {
        Orchestrator.Instance.ChangeCursor(hover);
    }

    public void OnClickPlay() {
        StartCoroutine(Orchestrator.Instance.GoToGame());
    }

    public void OnClickSettings() {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void OnClickCredits() {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public static void OnClickQuit() {
        Application.Quit();
    }
}
