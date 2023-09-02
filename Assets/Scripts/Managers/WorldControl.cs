using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour {
    public static WorldControl Instance;

    public ConvoPair convoPair;
    private Camera mainCamera;
    private CinemachineVirtualCamera vcam;
    private Animator animator;

    public Car ActiveCar;

    private Vector3 dragOrigin;
    private Vector3 dragEnd;
    private float dragTime;

    public bool isControlEnabled = false;
    private bool isDragging = false;
    private bool canInterrogate = true;

    [SerializeField] private Vector2 minPos; // = new Vector3(-19.5f, -67.5f, 0);
    [SerializeField] private Vector2 maxPos; // = new Vector3(42, -35, 0);
    [SerializeField] private float minCameraSize = 1;
    [SerializeField] private float maxCameraSize = 10;

    public float testing_deadzone;

    public SpriteRenderer fadeSpriteRenderer;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
        mainCamera = Camera.main;
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        convoPair = GetComponent<ConvoPair>();
        animator = GetComponent<Animator>();
    }

    IEnumerator Start() {
        Debug.Log(Time.timeScale);
        // yield return FastForward();
        yield return FadeIn();
    }

    void Update () {
        if (!isControlEnabled) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Car selectedCar = GetCar(ray);
        if (selectedCar != null) {
            Time.timeScale = 0.2f;
        } else {
            Time.timeScale = 1f;
        }
    }


    void LateUpdate() {

        if (isDragging) {
            dragEnd = getMousePosition();
            Vector3 diff = dragEnd - transform.position;
            Vector3 point_pos = dragOrigin - diff;
            transform.position = new Vector3(
                                        Mathf.Clamp(point_pos.x, minPos.x, maxPos.x),
                                        Mathf.Clamp(point_pos.y, minPos.y, maxPos.y),
                                        transform.position.z);
        }

        dragTime += Time.unscaledDeltaTime;
    }

    public void FastForwardRunner() {
        StartCoroutine(FastForward());
    }
    private IEnumerator FastForward(){
        Time.timeScale = 40;
        Debug.Log(Time.timeScale);
        yield return new WaitForSecondsRealtime(3);
        Debug.Log(Time.timeScale);
        Time.timeScale = 1;
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void OnDrag(InputAction.CallbackContext context) {
        if (!isControlEnabled) return;

        if (context.started) {
            dragOrigin = getMousePosition();
            if (!canInterrogate && 
                dragOrigin.x > transform.position.x + testing_deadzone*vcam.m_Lens.OrthographicSize) {
                return;
            }
            dragTime = 0;
        }
        isDragging = context.started || context.performed;

        if (!isDragging && dragTime < 0.1f && canInterrogate) {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Car selectedCar = GetCar(ray);
            if (selectedCar != null) {
                ActiveCar = selectedCar;
                Time.timeScale = 0.75f;
                DisableInterrogate();
                Debug.Log("Interacted with " + ActiveCar);

                SceneManager.LoadScene("Interact", LoadSceneMode.Additive);
            }
        }
    }

    public void OnScroll(InputAction.CallbackContext context) {
        if (!isControlEnabled) return;

        if (context.performed)
        {
            float z = context.ReadValue<float>();
            if (z > 0)
                vcam.m_Lens.OrthographicSize -= 1;
            else if (z < 0)
                vcam.m_Lens.OrthographicSize += 1;
        }
        vcam.m_Lens.OrthographicSize = Mathf.Clamp(vcam.m_Lens.OrthographicSize, minCameraSize, maxCameraSize);
    }
    
    public Vector3 getMousePosition() => mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    public void DisableInterrogate() {
        canInterrogate = false;
    }

    public void EnableInterrogate() {
        canInterrogate = true;
    }

    public void DisableControl() {
        isControlEnabled = false;
        animator.Play("Phase1");
    }
    
    public void EnableControl() {
        isControlEnabled = true;
        animator.enabled = false;
    }

    public Car GetCar(Ray ray) {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Car selectedCar = null;
        if (hit.collider != null) {
            GameObject gohit = hit.collider.gameObject;
            if (gohit.GetComponent<Car>() != null) {
                selectedCar = gohit.GetComponent<Car>();
            }
        }
        return selectedCar;
    }

    IEnumerator FadeIn(){
        Color color = Color.black;
        color.a = 1;
        fadeSpriteRenderer.color = color;
        while (color.a > 0) {
            fadeSpriteRenderer.color = color;
            color.a -= .5f * Time.deltaTime;
            yield return null;
        }
    }
}
