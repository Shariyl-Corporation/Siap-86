using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraDrag : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 dragOrigin;
    private Vector3 dragEnd;
    private bool isDragging = false;
    

    [SerializeField] private Vector2 minPos;// = new Vector3(-19.5f, -67.5f, 0);
    [SerializeField] private Vector2 maxPos;// = new Vector3(42, -35, 0);
    [SerializeField] private float minCameraSize = 1;
    [SerializeField] private float maxCameraSize = 10;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (isDragging)
        {
            dragEnd = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 diff = dragEnd - transform.position;
            Vector3 point_pos = dragOrigin - diff;
            transform.position = new Vector3(
                                        Mathf.Clamp(point_pos.x, minPos.x, maxPos.x),
                                        Mathf.Clamp(point_pos.y, minPos.y, maxPos.y),
                                        transform.position.z);
        }
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        Debug.Log("Drag");
        if (context.started)
            dragOrigin = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        isDragging = context.started || context.performed;
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        Debug.Log("Scroll");

        if (context.performed)
        {
            float z = context.ReadValue<float>();
            if (z > 0)
                mainCamera.orthographicSize -= 1;
            else if (z < 0)
                mainCamera.orthographicSize += 1;
        }
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraSize, maxCameraSize);
    }

}
