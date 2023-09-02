using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    public static event System.Action OnSceneUnloaded;
    [SerializeField] private SpriteRenderer figureRenderer;

    IEnumerator Start(){
        yield return RaiseOpacity();
        yield return new WaitForSeconds(2);
        yield return LowerOpacity();
        UnloadScene();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){                          // skip all frames
            UnloadScene();
        }
    }

    private void OnDestroy(){                                           // inform orchestrator that this scene is unloaded
        OnSceneUnloaded?.Invoke();
        OnSceneUnloaded = null;
    }

    IEnumerator LowerOpacity(){
        yield return StartCoroutine(LerpOpacity(1, 0, 2, .1f));
    }
    IEnumerator RaiseOpacity(){
        yield return StartCoroutine(LerpOpacity(0, 1, 2, .1f));
    }
    IEnumerator LerpOpacity(float col1, float col2, float duration, float step_duration) {
        float start = Time.time;
        float end = start + duration;
        Color figColor = figureRenderer.color;
        while (Time.time < end) {
                figColor.a = Mathf.Lerp(col1, col2, (Time.time - start) / duration);
                figureRenderer.color = figColor;
            yield return null;
        }
    }

    public void UnloadScene() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
