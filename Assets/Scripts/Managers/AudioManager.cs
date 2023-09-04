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
        intro,
        dialogue,
        world   
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
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

    public IEnumerator PlayMusic(int clipIndex) {
        yield return FadeOutMusicCoroutine(1);
        if (clipIndex >= 0 && clipIndex < musicClips.Count) {
            musicAudioSource.clip = musicClips[clipIndex];
            musicAudioSource.loop = true; 
            musicAudioSource.Play();
        }
        else {
            Debug.LogWarning("Music clip index out of range.");
        }
        yield return FadeInMusicCoroutine(1);
    }

    public void PlayMenuMusic() {
        StartCoroutine(PlayMusic((int)music.menu));
    }

    public void PlayIntroMusic() {
        StartCoroutine(PlayMusic((int)music.intro));
    }

    public void PlayDialogueMusic() {
        StartCoroutine(PlayMusic((int)music.dialogue));
    }

    public void PlayGameMusic() {
        StartCoroutine(PlayMusic((int)music.world));
    }

    public void StopMusic() {
        StartCoroutine(FadeOutMusicCoroutine(1));
    }


    public void FadeInMusic(float duration) {
        StartCoroutine(FadeInMusicCoroutine(duration));
    }


    private IEnumerator FadeInMusicCoroutine(float duration) {
        float startVolume = musicAudioSource.volume;
        float targetVolume = .1f;
        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        musicAudioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutMusicCoroutine(float duration) {
        float startVolume = musicAudioSource.volume;
        float targetVolume = 0.0f;
        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        musicAudioSource.volume = targetVolume;
        musicAudioSource.Stop();
    }
}