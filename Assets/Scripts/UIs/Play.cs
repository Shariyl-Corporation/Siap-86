using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public Texture2D defaultTex, hoverTex;

    public AudioSource audioSource;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        // audioSource.Stop();
    }

    void OnMouseOver()
    {
        material.SetTexture("_MainTex", hoverTex);
        MainMenu.Instance.changeCursor(true);
    }

    void OnMouseExit()
    {
        material.SetTexture("_MainTex", defaultTex);
        MainMenu.Instance.changeCursor(false);
    }

    void OnMouseDown()
    {
        // audioSource.Play();
        MainMenu.Instance.OnClickPlay();
    }
}
