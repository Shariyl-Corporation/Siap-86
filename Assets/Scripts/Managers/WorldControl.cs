using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    public static WorldControl Instance;

    public ConvoPair convoPair;
    private Camera mainCamera;
    private CinemachineVirtualCamera vcam;  

    public Car ActiveCar;

    private Vector3 dragOrigin;
    private Vector3 dragEnd;
    private float dragTime;

    private bool isControlEnabled = false;
    private bool isDragging = false;
    private bool canInterrogate = true;

    [SerializeField] private Vector2 minPos;// = new Vector3(-19.5f, -67.5f, 0);
    [SerializeField] private Vector2 maxPos;// = new Vector3(42, -35, 0);
    [SerializeField] private float minCameraSize = 1;
    [SerializeField] private float maxCameraSize = 10;

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

        if (isDragging)
        {
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

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void OnDrag(InputAction.CallbackContext context) {
        if (!isControlEnabled) return;

        if (context.started) {
            dragOrigin = getMousePosition();
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
    }
    
    public void EnableControl() {
        isControlEnabled = true;
    }

    public Car GetCar(Ray ray) {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Car selectedCar = null;
        if (hit.collider == null) {
            // Debug.Log("nothing hit");
        }
        else {
            // print(hit.collider.name);
            // print(hit.collider.gameObject);
            GameObject gohit = hit.collider.gameObject;
            if (gohit.GetComponent<Car>() != null) {
                selectedCar = gohit.GetComponent<Car>();
            }
        }
        return selectedCar;
    }
}
