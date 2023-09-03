using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource clickAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> musicClips;

    public enum music {
        menu,
        intro1,
        intro2,
        game
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        clickAudioSource.Stop();
        sfxAudioSource.Stop();
        musicAudioSource.Stop();
    }

    public void Click() {
        clickAudioSource.Play();
    }

    public void PlaySFX(int clipIndex) {
        if (clipIndex >= 0 && clipIndex < sfxClips.Count) {
            sfxAudioSource.PlayOneShot(sfxClips[clipIndex]);
        }
        else {
            Debug.LogWarning("SFX clip index out of range.");
        }
    }

    public void PlayMusic(int clipIndex) {
        if (clipIndex >= 0 && clipIndex < musicClips.Count) {
            musicAudioSource.clip = musicClips[clipIndex];
            musicAudioSource.loop = true; 
            musicAudioSource.Play();
        }
        else {
            Debug.LogWarning("Music clip index out of range.");
        }
    }

    public void StopMusic() {
        musicAudioSource.Stop();
    }

    public void PlayMenuMusic() {
        PlayMusic((int)music.menu);
    }

    public void PlayIntro1Music() {
        PlayMusic((int)music.intro1);
    }

    public void PlayIntro2Music() {
        PlayMusic((int)music.intro2);
    }

    public void PlayGameMusic() {
        PlayMusic((int)music.game);
    }
}