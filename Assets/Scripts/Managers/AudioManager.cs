using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickAudioSource;

    void Awake()
    {
        clickAudioSource.Stop();
        // audioSource = GetComponent<AudioSource>();
    }


    public void Click() {
        clickAudioSource.Play();
    }
}