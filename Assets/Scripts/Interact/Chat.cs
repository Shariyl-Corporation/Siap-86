using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chat : MonoBehaviour
{
    public AudioManager audioManager;
    public Texture2D defaultTex, hoverTex, disabledTex;

    private bool isEnabled = false;
    private Material material;

    void Start() {
        material = GetComponent<Renderer>().material;
        material.SetTexture("_MainTex", disabledTex);
    }

    void Update() {

    }

    void OnMouseOver() {
        if (isEnabled) {
            material.SetTexture("_MainTex", hoverTex);
            UI.changeCursor(true);
        }
    }

    void OnMouseExit() {
        if (isEnabled) {
            material.SetTexture("_MainTex", defaultTex);
            UI.changeCursor(false);
        }
    }

    void OnMouseDown() {
        Debug.Log("Chat clicked");
        audioManager.Click();
    }

    public void SetDisabled() {
        isEnabled = false;
        material.SetTexture("_MainTex", disabledTex);
    }

    public void SetEnabled() {
        isEnabled = true;
        material.SetTexture("_MainTex", defaultTex);
    }
}
