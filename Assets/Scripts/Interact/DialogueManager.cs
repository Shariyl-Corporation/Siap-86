using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public UI uiManager;

    public TextMeshPro dialogue;

    public Animator driverAnimator;

    public AudioClip driverSound, playerSound;
    public AudioSource audioSource;

    void Start()
    {

        dialogue.text = "";

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        StartCoroutine(introDialogue());
    }

    void Update()
    {
        // if (populatingOptions)
        // {
        //     if (!optionBoxFinished.Any(x => x == false))
        //     {
        //         populatingOptions = false;
        //         stateManager.setIdle(true);
        //     }
        // }
    }

    public void Converse(string guySentence, string girlSentence)
    {
        StartCoroutine(StartConversation(guySentence, girlSentence));
    }

    private float waitTime(char letter)
    {
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

    IEnumerator StartConversation(string playerSentence, string driverSentence)
    {
        yield return new WaitForSecondsRealtime(0.2f);

        playText(playerSentence, false);

        playText(driverSentence, true);
    }

    public IEnumerator GiveVerdict(bool isGuilty)
    {
        string playerSentence = "hmm...";
        yield return StartCoroutine(playText(playerSentence, false));

        if (isGuilty) yield return StartCoroutine(GuiltyVerdict());
        else  yield return StartCoroutine(InnocentVerdict());

        //end conv
    }

    public IEnumerator GuiltyVerdict() {
        yield return new WaitForSecondsRealtime(1.0f);

        string playerSentence = "hmm......";
        yield return StartCoroutine(playText(playerSentence, false));

        playerSentence = "Ini surat tilang anda";
        yield return StartCoroutine(playText(playerSentence, false));
    }
    public IEnumerator InnocentVerdict() {
        yield return new WaitForSecondsRealtime(1.0f);

        string playerSentence = "Silahkan melanjutkan perjalanan anda";
        yield return StartCoroutine(playText(playerSentence, false));

    }

    public void Win()
    {
        // uiManager.disableTalk();
        // uiManager.disableLeave();
    }
    private IEnumerator playText(string s, bool isSpeakerDriver) {

        dialogue.text = "";
        if (isSpeakerDriver) {
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
    private IEnumerator introDialogue()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        string playerSentence = " Selamat siang pengemudi";
        yield return StartCoroutine(playText(playerSentence, false));
        
        string driverSentence = "Selamat siang..";
        yield return StartCoroutine(playText(driverSentence, true));

        // uiManager.enableTalk();
        // uiManager.enableLeave();
    }
}
