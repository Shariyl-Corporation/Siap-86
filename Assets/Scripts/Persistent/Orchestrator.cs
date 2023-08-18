using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orchestrator : MonoBehaviour
{
    public static Orchestrator Instance;
    public static Texture2D mouseCursor, hoverCursor;

    public static AsyncOperation asyncOperation;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
        mouseCursor = Resources.Load("Cursor") as Texture2D;
        hoverCursor = Resources.Load("CursorHover") as Texture2D;
        Cursor.SetCursor(mouseCursor, new Vector2(0, 0), CursorMode.Auto);
    }
    void Start()
    {
        GoToIntro();
    }

    void Update()
    {
        
    }

    public void GoToIntro(){
        SceneManager.LoadScene("Introduction", LoadSceneMode.Additive);
        IntroductionScene.OnSceneUnloaded += GoToMenu;
    }

    public void GoToMenu(){
        IntroductionScene.OnSceneUnloaded -= GoToMenu;
        // SceneManager.LoadScene("World", LoadSceneMode.Additive);
        
        // SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        StartCoroutine(LoadWorldThenMenu());
    }

    public IEnumerator LoadWorldThenMenu(){
        yield return LoadWorld();
        yield return LoadMenu();
    }

    public IEnumerator LoadWorld(){
        yield return LoadScene("World");
    }

    public IEnumerator LoadMenu(){
        yield return LoadScene("Menu");
    }

    public IEnumerator LoadGame(){
        yield return LoadScene("Game");
    }

    public IEnumerator LoadScene(string sceneName){
        asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncOperation.isDone) {
            yield return null;
        }
    }

    public IEnumerator GoToGame(){
        asyncOperation = SceneManager.UnloadSceneAsync("Menu");
        asyncOperation = SceneManager.UnloadSceneAsync("World");
        asyncOperation = SceneManager.LoadSceneAsync("GameMaster", LoadSceneMode.Additive);
        while (!asyncOperation.isDone) {
            yield return null;
        }
        // asyncOperation = SceneManager.UnloadSceneAsync("World");
        // while (!asyncOperation.isDone) {
        //     yield return null;
        // }
    }

    public void ChangeCursor(bool hover) {
        Cursor.SetCursor(hover ? hoverCursor : mouseCursor, new Vector2(0, 0), CursorMode.Auto);
    }

}
