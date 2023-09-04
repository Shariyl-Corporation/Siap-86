using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueScene : MonoBehaviour {
    public static event System.Action OnSceneUnloaded;
    [SerializeField] private TextMeshPro t_speaker;
    [SerializeField] private TextMeshPro t_caption;

    public List<Convo> convoFlow;
    private IEnumerator currentCoroutine;
    private int currentConvo = 0;
    private bool isAnimating = false;
    private bool hold;

    void Awake(){
        convoFlow = new List<Convo>() {
            new Convo() {speaker = "Mentor", text = "Halo anak baru! Selamat datang di xxx"},
            new Convo() {speaker = "Mentor", text = "Tugasmu disini adalah menjaga ketertiban lalu lintas di dekat sini, jelas."},
            new Convo() {speaker = "Mentor", text = "Tentunya kamu sudah paham dengan peraturan berlalu lintas kan !?"},
            new Convo() {speaker = "Mentor", text = "Mengecek kelengkapan dokumen seperti STNK dan SIM, patuhi rambu lalu lintas, dan ya kau tau lah ya!?"},
            new Convo() {speaker = "Mentor", text = "Jika tidak yakin, kamu bisa membaca undang undang mengenai peraturan berlalu lintas di sini, silahkan dicek nanti."},
            new Convo() {speaker = "Joko", text = "Terimakasih Pak!"},
            new Convo() {speaker = "Mentor", text = "Oh, ya dan satu lagi sebelum kamu memulai tugasmu."},
            new Convo() {speaker = "Mentor", text = "Akhir akhir ini sering terjadi pelanggaran penerobosan lampu merah."},
            new Convo() {speaker = "Mentor", text = "Dan pelanggaran itu mengakibatkan GRIDLOCK. Ehe."},
            new Convo() {speaker = "Mentor", text = "Untuk mengatasi hal tersebut, atasan memberikan senjata ampuh yang bisa menghapus mobil yang membuat kemacetan."},
            new Convo() {speaker = "Mentor", text = "Ini merupakan informasi konfidental. Jangan bilang siapa siapa!"},
            new Convo() {speaker = "Mentor", text = "Dan jangan disalahgunakan! Sekian."},
            new Convo() {speaker = "Joko", text = "Siap, 86!"},
        };
        Debug.Log("Panjang convo: " + convoFlow.Count);
        currentConvo = 0;
        hold = false;
    } 

    void Start(){
        foreach(Convo c in convoFlow) {
            Debug.Log(c.speaker + " " + c.text);
        }
        currentCoroutine = PlayText(convoFlow[currentConvo].text, convoFlow[currentConvo].speaker);
        StartCoroutine(currentCoroutine);
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)) {
            // hold = true;
            if (!isAnimating){
                currentConvo++;
                if (currentConvo < convoFlow.Count) {
                    Debug.Log(currentConvo);
                    currentCoroutine = PlayText(convoFlow[currentConvo].text, convoFlow[currentConvo].speaker);
                    StartCoroutine(currentCoroutine);
                } else {
                    UnloadScene();
                }
                
            } else {
                StopCoroutine(currentCoroutine);
                isAnimating = false;
                t_caption.text = convoFlow[currentConvo].text;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){                          // skip all frames
            UnloadScene();
        }
    }

    private IEnumerator PlayText(string s, string speaker) {
        isAnimating = true;
        t_speaker.text = speaker;
        t_caption.text = "";

        foreach (char c in s.ToCharArray()) {
            t_caption.text += c;
            yield return new WaitForSecondsRealtime(waitTime(c));
        }

        isAnimating = false;
    }

    private IEnumerator PlayConvo(ConvoFlow convoFlow) {
        foreach(var convo in convoFlow.flow){
            yield return StartCoroutine(PlayText(convo.text, convo.speaker));
            yield return new WaitForSecondsRealtime(0.75f);
        }
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

    private void OnDestroy(){                                           // inform orchestrator that this scene is unloaded
        OnSceneUnloaded?.Invoke();
        OnSceneUnloaded = null;
    }

    public void UnloadScene() {
        AudioManager.Instance.StopMusic();
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
