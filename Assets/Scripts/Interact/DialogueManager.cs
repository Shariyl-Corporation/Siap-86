using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    private WorldControl control;
    private ConvoPair convoPair;
    public UI uiManager;

    private Car activeCar;
    private Driver activeDriver;

    public TextMeshPro t_dialogue;
    public TextMeshPro t_speaker;

    public Animator driverAnimator;

    public GameObject card;
    private SpriteRenderer cardRenderer;
    public Sprite redCard, blueCard;
    public bool isRedCard = true;

    public bool allowAction = true;

    public bool sanksiP288A1, sanksiP288A2, sanksiP281, sanksiP283, sanksiP287A2, sanksiP287A5;

    public AudioClip driverSound, playerSound;
    public AudioSource audioSource;

    [SerializeField] private GameObject ChoicesMain,
                                        ChoicesAsk,
                                        ChoicesSanksi,
                                        ChoicesVerdict,
                                        ButtonKembali;

    void Start() {
        control = FindObjectOfType<WorldControl>();
        convoPair = control.convoPair;
        activeCar = control.ActiveCar;
        activeDriver = activeCar.driver;
        t_dialogue.text = "";

        cardRenderer = card.GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        ShowChoicesMain();

        StartCoroutine(StrikeConversation());
    }

    // UI related
    // basic disable enable
    private void DisableAllChoices() {
        ChoicesMain.SetActive(false);
        ChoicesAsk.SetActive(false);
        ChoicesSanksi.SetActive(false);
        ChoicesVerdict.SetActive(false);
        ButtonKembali.SetActive(false);
    }

    private void ShowChoicesMain() {
        DisableAllChoices();
        ChoicesMain.SetActive(true);
    }

    private void ShowChoicesAsk() {
        DisableAllChoices();
        ChoicesAsk.SetActive(true);
        ButtonKembali.SetActive(true);
    }

    private void ShowChoicesSanksi() {
        DisableAllChoices();
        ChoicesSanksi.SetActive(true);
        ButtonKembali.SetActive(true);
    }

    private void ShowChoicesVerdict() {
        DisableAllChoices();
        ChoicesVerdict.SetActive(true);
        ButtonKembali.SetActive(true);
    }
    
    private void DisableAction() {
        allowAction = false;
    }

    private void EnableAction() {
        allowAction = true;
    }

    // animation related

    private IEnumerator SwapCard() {           // weird behaviour
        DisableAllChoices();
        yield return MoveObject(card, Vector3.down, 8f, 1);

        if (isRedCard) {
            cardRenderer.sprite = blueCard;
        } else {
            cardRenderer.sprite = redCard;
        }

        yield return MoveObject(card, Vector3.up, 8f, 1);

        isRedCard = !isRedCard;
        EnableAction();

        ShowChoicesMain();
        yield return null;
    }

    private float EaseInOutQuad(float t) {
        return t < 0.5f ? 2.0f * t * t : -1.0f + (4.0f - 2.0f * t) * t;
    }

    private IEnumerator MoveObject(GameObject obj, Vector3 direction, float distance, float duration) {
        Vector3 startPosition = obj.transform.position;
        Vector3 targetPosition = startPosition + direction * distance;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            float easedT = EaseInOutQuad(t);

            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, easedT);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ensure the object reaches the target position exactly
        obj.transform.position = targetPosition;
    }

    // button coro starter
    public void OnClickGanti() {
        DisableAction();
        StartCoroutine(SwapCard());
    }

    public void OnClickObrol() {
        DisableAction();
        StartCoroutine(BasicChat());
    }

    public void OnClickTanya() {
        DisableAction();
        ShowChoicesAsk();
    }
        public void OnClickKTP() {
            DisableAction();
            StartCoroutine(AskDocumentKTP(activeDriver.hasKTP));
        }

        public void OnClickSIM() {
            DisableAction();
            StartCoroutine(AskDocumentSIM(activeDriver.hasSIM));
        }

        public void OnClickSTNK() {
            DisableAction();
            StartCoroutine(AskDocumentSTNK(activeDriver.hasSTNK));
        }

    public void OnClickSanksi() {
        DisableAction();
        ShowChoicesSanksi();
    }
        // Tidak bawa STNK
        public void OnClickP288A1() {
            sanksiP281 = !sanksiP281;
        }

        // Tidak bawa SIM
        public void OnClickP288A2() {
            sanksiP288A2 = !sanksiP288A2;
        }

        // Tidak punya SIM
        public void OnClickP281() {
            sanksiP281 = !sanksiP281;
        }

        // Mengemudi tidak wajar  (terlalu lambat, belok belok (mungkin semacam break check))
        public void OnClickP283() {
            sanksiP283 = !sanksiP283;
        }

        // Menerobos lampu merah
        public void OnClickP287A2() {
            sanksiP287A2 = !sanksiP287A2;
        }

        // Melampaui batas kecepatan
        public void OnClickP287A5(){
            sanksiP287A5 = !sanksiP287A5;
        }

    public void OnClickAkhiri(){ 
        DisableAction();
        ShowChoicesVerdict();
    }
        public void OnClickLepas() {
            DisableAction();
            // call end conversation (no tilang)
            StartCoroutine(GiveVerdict(false));

            // call destroy self & enable control

        }

        public void OnClickTilang() {
            DisableAction();
            // call end conversation
            StartCoroutine(GiveVerdict(true));

            // call destroy self & enable control

        }
    
    public void OnClickKembali(){ 
        ShowChoicesMain();
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
