using UnityEngine;


public class BaseInterrogateButton : BaseButton {
    protected DialogueManager dialogueManager;
    
    public Sprite askedSprite;
    // protected bool isAsked = false;

    protected string hintText;
    public GameObject hintButton;

    protected override void Start() {
        base.Start();
        dialogueManager = FindObjectOfType<DialogueManager>();
        OnMouseExit();
    }

    protected override void OnMouseOver() {
        base.OnMouseOver();
        if (hintButton != null) hintButton.SetActive(true);
    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
        if (hintButton != null) hintButton.SetActive(false);
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