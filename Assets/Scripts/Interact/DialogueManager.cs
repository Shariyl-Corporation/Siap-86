using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GlobalControl globalControl;
    public UI uiManager;

    private Car activeCar;
    private Driver activeDriver;

    public TextMeshPro dialogue;

    public Animator driverAnimator;

    public AudioClip driverSound, playerSound;
    public AudioSource audioSource;

    void Start()
    {
        globalControl = FindObjectOfType<GlobalControl>();
        activeCar = globalControl.ActiveCar;
        activeDriver = activeCar.driver;
        dialogue.text = "";

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        StartCoroutine(StrikeConversation());
    }

    public IEnumerator StrikeConversation() {
        List<Convo> ConvoFlow = ConvoPair.ConvGreetings[Random.Range(0, ConvoPair.ConvGreetings.Count)];
        
        yield return StartCoroutine(PlayConvo(ConvoFlow));
    }

    public IEnumerator AskDocumentKTP(bool hasKTP) {
        List<Convo> ConvoFlow;
        if (hasKTP)
            ConvoFlow = ConvoPair.ConvKTP[Random.Range(0, ConvoPair.ConvKTP.Count)];
        else
            ConvoFlow = ConvoPair.ConvNoKTP[Random.Range(0, ConvoPair.ConvNoKTP.Count)];
        
        yield return StartCoroutine(PlayConvo(ConvoFlow));
    }

    public IEnumerator AskDocumentSIM(bool hasSIM) {
        List<Convo> ConvoFlow;
        if (hasSIM)
            ConvoFlow = ConvoPair.ConvSIM[Random.Range(0, ConvoPair.ConvSIM.Count)];
        else
            ConvoFlow = ConvoPair.ConvNoSIM[Random.Range(0, ConvoPair.ConvNoSIM.Count)];
        
        yield return StartCoroutine(PlayConvo(ConvoFlow));
    }

    public IEnumerator AskDocumentSTNK(bool hasSTNK) {
        List<Convo> ConvoFlow;
        if (hasSTNK)
            ConvoFlow = ConvoPair.ConvSTNK[Random.Range(0, ConvoPair.ConvSTNK.Count)];
        else
            ConvoFlow = ConvoPair.ConvNoSTNK[Random.Range(0, ConvoPair.ConvNoSTNK.Count)];
        
        yield return StartCoroutine(PlayConvo(ConvoFlow));
    }

    public IEnumerator GiveVerdict(bool isGuilty) {
        List<Convo> ConvoFlow;
        if (isGuilty)
            ConvoFlow = ConvoPair.ConvVerdictGuilty[Random.Range(0, ConvoPair.ConvVerdictGuilty.Count)];
        else
            ConvoFlow = ConvoPair.ConvVerdictInnocent[Random.Range(0, ConvoPair.ConvVerdictInnocent.Count)];
    
        yield return StartCoroutine(PlayConvo(ConvoFlow));
    }

    private float waitTime(char letter) {
        switch (letter)
        {
            case '.':
                return 0.75f;
            case ',':
                return 0.5f;
            case '!':
                return 1.0f;
            case '?':
                return 0.75f;
            case '\n':
                return 0.2f;
            default:
                return 0.05f;
        }
    }
    
    private IEnumerator PlayText(string s, string speaker) {

        dialogue.text = "";
        if (speaker == "You") {
            // driverAnimator?.SetBool("talking", true);
            audioSource.clip = driverSound;
        } else {
            audioSource.clip = playerSound;
        }

        audioSource.Play();

        foreach (char c in s.ToCharArray()) {
            if (c == ',' || c == '.' || c == '\n' || c == '!' || c == '?') {
                audioSource.Play();
                audioSource.Stop();
            } else {
                audioSource.Play();
            }
            dialogue.text += c;
            yield return new WaitForSecondsRealtime(waitTime(c));
        }

        // if (isSpeakerDriver) driverAnimator?.SetBool("talking", false);
        audioSource.Stop();

        yield return new WaitForSecondsRealtime(1.0f);
    }

    private IEnumerator PlayConvo(List<Convo> convoFlow) {
        foreach(var convo in convoFlow){
            yield return StartCoroutine(PlayText(convo.text, convo.speaker));
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }

}
