using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseButton : MonoBehaviour
{
    protected AudioManager audioManager;
    public Sprite defaultSprite, hoverSprite;
    protected SpriteRenderer spriteRenderer;
    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    protected virtual void OnMouseOver() {
        spriteRenderer.sprite = hoverSprite;
        UI.changeCursor(true);
    }

    protected virtual void OnMouseExit() {
        spriteRenderer.sprite = defaultSprite;
        UI.changeCursor(false);
    }
}