using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GlobalControl globalControl;
    private ConvoPair convoPair;
    public UI uiManager;

    private Car activeCar;
    private Driver activeDriver;

    public TextMeshPro t_dialogue;
    public TextMeshPro t_speaker;

    public Animator driverAnimator;

    public AudioClip driverSound, playerSound;
    public AudioSource audioSource;

    void Start()
    {
        globalControl = FindObjectOfType<GlobalControl>();
        convoPair = globalControl.convoPair;
        activeCar = globalControl.ActiveCar;
        activeDriver = activeCar.driver;
        t_dialogue.text = "";

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        StartCoroutine(StrikeConversation());
    }

    public IEnumerator StrikeConversation() {
        ConvoFlow convoFlow = convoPair.ConvGreetings[Random.Range(0, convoPair.ConvGreetings.Count)];
        
        yield return StartCoroutine(PlayConvo(convoFlow));
    }

    public IEnumerator BasicChat() {
        ConvoFlow convoFlow = convoPair.ConvBasicChat[Random.Range(0, convoPair.ConvGreetings.Count)];
        
        yield return StartCoroutine(PlayConvo(convoFlow));
    }
    public IEnumerator AskDocumentKTP(bool hasKTP) {
        ConvoFlow convoFlow;
        if (hasKTP)
            convoFlow = convoPair.ConvKTP[Random.Range(0, convoPair.ConvKTP.Count)];
        else
            convoFlow = convoPair.ConvNoKTP[Random.Range(0, convoPair.ConvNoKTP.Count)];
        
        yield return StartCoroutine(PlayConvo(convoFlow));
    }

    public IEnumerator AskDocumentSIM(bool hasSIM) {
        ConvoFlow convoFlow;
        if (hasSIM)
            convoFlow = convoPair.ConvSIM[Random.Range(0, convoPair.ConvSIM.Count)];
        else
            convoFlow = convoPair.ConvNoSIM[Random.Range(0, convoPair.ConvNoSIM.Count)];
        
        yield return StartCoroutine(PlayConvo(convoFlow));
    }

    public IEnumerator AskDocumentSTNK(bool hasSTNK) {
        ConvoFlow convoFlow;
        if (hasSTNK)
            convoFlow = convoPair.ConvSTNK[Random.Range(0, convoPair.ConvSTNK.Count)];
        else
            convoFlow = convoPair.ConvNoSTNK[Random.Range(0, convoPair.ConvNoSTNK.Count)];
        
        yield return StartCoroutine(PlayConvo(convoFlow));
    }

    public IEnumerator GiveVerdict(bool isGuilty) {
        ConvoFlow convoFlow;
        if (isGuilty)
            convoFlow    = convoPair.ConvVerdictGuilty[Random.Range(0, convoPair.ConvVerdictGuilty.Count)];
        else
            convoFlow    = convoPair.ConvVerdictInnocent[Random.Range(0, convoPair.ConvVerdictInnocent.Count)];
    
        yield return StartCoroutine(PlayConvo(convoFlow ));
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

        t_dialogue.text = "";
        t_speaker.text = speaker;

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
            t_dialogue.text += c;
            yield return new WaitForSecondsRealtime(waitTime(c));
        }

        // if (isSpeakerDriver) driverAnimator?.SetBool("talking", false);
        audioSource.Stop();

        yield return new WaitForSecondsRealtime(1.0f);
    }

    private IEnumerator PlayConvo(ConvoFlow convoFlow) {
        foreach(var convo in convoFlow.flow){
            yield return StartCoroutine(PlayText(convo.text, convo.speaker));
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }

}
