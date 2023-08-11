using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 dragOrigin;
    private Vector3 dragEnd;
    private bool isDragging = false;

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
            mainCamera.transform.position = dragOrigin - diff;
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
    }
}
