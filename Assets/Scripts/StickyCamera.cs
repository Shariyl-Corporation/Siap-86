using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCamera : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    public float scale = 1;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = mainCamera.transform.position + new Vector3(0, 0, 5);
        gameObject.transform.localScale = new Vector3(mainCamera.orthographicSize*scale, mainCamera.orthographicSize*scale, 1);
    }
}
