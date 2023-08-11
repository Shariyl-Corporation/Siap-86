using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interrogate : MonoBehaviour
{
    public Texture2D defaultTex, hoverTex, disabledTex;

    public AudioSource audioSource;

    private Material material;
    private bool isEnabled = false;


    void Start()
    {
        material = GetComponent<Renderer>().material;
        Debug.Log(material);
        
        // audioSource.Stop();
    }

    void OnMouseOver()
    {
        material.SetTexture("_MainTex", hoverTex);
        MainMenu.changeCursor(true);
    }

    void OnMouseExit()
    {
        material.SetTexture("_MainTex", defaultTex);
        MainMenu.changeCursor(false);
    }

    public void SetDisabled()
    {
        isEnabled = false;
        material.SetTexture("_MainTex", disabledTex);
    }

    public void SetEnabled()
    {
        isEnabled = true;
        material.SetTexture("_MainTex", defaultTex);
    }
}
