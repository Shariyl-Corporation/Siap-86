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
        MainMenu.changeCursor(true);
    }

    void OnMouseExit()
    {
        material.SetTexture("_MainTex", defaultTex);
        MainMenu.changeCursor(false);
    }

    void OnMouseDown()
    {
        // audioSource.Play();
        StartCoroutine(play());
    }

    IEnumerator play()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Testing_AI");
    }
}
