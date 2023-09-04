using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class HUDScene : MonoBehaviour {
    [SerializeField] private TextMeshPro timeText;
    public GameObject GunButton;
    void Awake(){
    } 

    void Start(){
        var gc = FindObjectOfType<GameController>();
        gc.SetTimeText(timeText);
    }

    void Update(){
    }

    public void UnloadScene() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
