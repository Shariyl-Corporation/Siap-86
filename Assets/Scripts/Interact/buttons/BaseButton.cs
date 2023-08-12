using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseButton : MonoBehaviour
{
    protected AudioManager audioManager;
    protected DialogueManager dialogueManager;
    public Sprite defaultSprite, hoverSprite, askedSprite;

    protected bool isAsked = false;
    protected SpriteRenderer spriteRenderer;
    protected void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        audioManager = FindObjectOfType<AudioManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    
    protected void OnMouseOver() {
        if (!isAsked) {
            spriteRenderer.sprite = hoverSprite;
            UI.changeCursor(true);
        }
    }

    protected void OnMouseExit() {
        if (!isAsked) {
            spriteRenderer.sprite = defaultSprite;
            UI.changeCursor(false);
        }
    }

    public void SetAsked() {
        isAsked = false;
        spriteRenderer.sprite = askedSprite;
    }

    public void SetNotAsked() {
        isAsked = true;
        spriteRenderer.sprite = defaultSprite;
    }
}