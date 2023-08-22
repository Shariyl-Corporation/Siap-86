using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficFront : MonoBehaviour
{
    public Sprite redSprite;   // Sprite for red light
    public Sprite greenSprite; // Sprite for green light
    public SpriteRenderer lightRenderer;

    public enum LightColor { Red, Green }
    public LightColor currentColor = LightColor.Red;
    private float timer = 0f;
    public float redDuration = 5f;
    public float greenDuration = 4f;

    private void Start()
    {
        lightRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        switch (currentColor)
        {
            case LightColor.Red:
                lightRenderer.sprite = redSprite;
                if (timer >= redDuration)
                {
                    currentColor = LightColor.Green;
                    timer = 0f;
                }
                break;

            case LightColor.Green:
                lightRenderer.sprite = greenSprite;
                if (timer >= greenDuration)
                {
                    currentColor = LightColor.Red;
                    timer = 0f;
                }
                break;
        }
    }
}
