using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    Ray ray;
    RaycastHit2D hit;
    public static event Action registeredEvents;
    [SerializeField] private Camera mainCamera;
    private bool isInVnMode = false;

    // Start is called before the first frame update

    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var car = onHover(ray, hit);

        if (Input.GetMouseButtonDown(0) && car != null)
        {
            Time.timeScale = 0;
            isInVnMode = true;
            startVnMode(car);
        }
    }
    public GameObject onHover(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        GameObject selectedCar = null;
        if (hit.collider == null)
        {
            Debug.Log("nothing hit");
        }
        else
        {
            print(hit.collider.name);
            print(hit.collider.gameObject);
            selectedCar = hit.collider.gameObject;
        }
        return selectedCar;
    }

    public void startVnMode(GameObject car)
    {

    }
}
