using UnityEngine;


public class BaseInterrogateButton : BaseButton {
    protected DialogueManager dialogueManager;
    
    public Sprite askedSprite;
    // protected bool isAsked = false;

    protected override void Start() {
        base.Start();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    protected override void OnMouseOver() {
        base.OnMouseOver();
    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
    }

    
    // public void SetAsked() {
    //     isAsked = false;
    //     spriteRenderer.sprite = askedSprite;
    // }

    // public void SetNotAsked() {
    //     isAsked = true;
    //     spriteRenderer.sprite = defaultSprite;
    // }
}