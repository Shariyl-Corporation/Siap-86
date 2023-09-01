using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Circular : MonoBehaviour {
    public Transform centerPoint;
    public float radius = 5f;
    public float rotationSpeed = 30f; // Degrees per second
    public float movementSpeed = 2f;
    private SpriteRenderer sr;

    public Sprite Timur, Tenggara, Selatan, BaratDaya, Barat, BaratLaut, Utara, TimurLaut;


    private void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(sr.gameObject);
        StartCoroutine(MoveInCircle());
    }

    void LateUpdate() {
        sr.gameObject.transform.localRotation *= Quaternion.Inverse(transform.rotation);;
    }

    private IEnumerator MoveInCircle()
    {
        float angle = 0f;

        while (true)
        {
            // Calculate the position using polar coordinates
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);

            Vector3 newPosition = new Vector3(x, y, transform.position.z);
            transform.position = newPosition;

            // Calculate the rotation based on the movement
            transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            angle += movementSpeed * Time.deltaTime;
            EvalRotation(angle * Mathf.Rad2Deg);
            yield return null;
        }
    }

    private void EvalRotation(float angle) {
        angle += 90;
        angle %= 360f;
        if (angle < 22.5 || angle > 337.5)  {
            sr.sprite = Timur;
        } else if (angle < 67.5) {
            sr.sprite = TimurLaut;
        } else if (angle < 112.5) {
            sr.sprite = Utara;
        } else if (angle < 157.5) {
            sr.sprite = BaratLaut;
        } else if (angle < 202.5) {
            sr.sprite = Barat;
        } else if (angle < 247.5) {
            sr.sprite = BaratDaya;
        } else if (angle < 292.5) {
            sr.sprite = Selatan;
        } else {
            sr.sprite = Tenggara;
        }
    }
}
