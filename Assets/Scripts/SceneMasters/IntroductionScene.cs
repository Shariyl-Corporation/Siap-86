using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Frame
{
    public Sprite sprite;
    public string text;
}


public class IntroductionScene : MonoBehaviour
{
    public static event System.Action OnSceneUnloaded;
    [SerializeField] private SpriteRenderer figureRenderer;
    [SerializeField] private TextMeshPro caption;

    public List<Frame> frames;
    private IEnumerator currentCoroutine;
    private int currentFrame = 0;
    private bool isAnimating = false;

    void Awake(){
    }

    void Start(){
        currentCoroutine = ChangeFrame(frames[currentFrame]);
        StartCoroutine(currentCoroutine);
    }

    void Update(){
        if (Input.GetMouseButtonDown(0) || !isAnimating){
            StopCoroutine(currentCoroutine);
            currentFrame++;
            if (currentFrame < frames.Count){
                currentCoroutine = ChangeFrame(frames[currentFrame]);
                StartCoroutine(currentCoroutine);
            } else {
                UnloadScene();
            }
        }
    }

    private void OnDestroy(){
        OnSceneUnloaded?.Invoke();
        OnSceneUnloaded = null;
    }

    IEnumerator ChangeFrame(Frame frame) {
        figureRenderer.sprite = frame.sprite;
        caption.text = frame.text;
        isAnimating = true;
        yield return StartCoroutine(RaiseOpacity());
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(LowerOpacity());
        yield return new WaitForSeconds(1);
        isAnimating = false;
    }

    IEnumerator LowerOpacity(){
        yield return StartCoroutine(LerpOpacity(1, 0, 2, .1f));
    }
    IEnumerator RaiseOpacity(){
        yield return StartCoroutine(LerpOpacity(0, 1, 2, .1f));
    }
    IEnumerator LerpOpacity(float col1, float col2, float duration, float step_duration)
    {
        float start = Time.time;
        float end = start + duration;
        Color figColor = figureRenderer.color;
        Color textColor = caption.color;
        while (Time.time < end) {
                figColor.a = Mathf.Lerp(col1, col2, (Time.time - start) / duration);
                textColor.a = Mathf.Lerp(col1, col2, (Time.time - start) / duration);
                figureRenderer.color = figColor;
                caption.color = textColor;
            yield return null;
        }
    }

    public void UnloadScene() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}